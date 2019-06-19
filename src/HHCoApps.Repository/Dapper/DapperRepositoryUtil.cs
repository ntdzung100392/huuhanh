using Dapper;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;

namespace HHCoApps.Repository.Dapper
{
    internal class DapperRepositoryUtil
    {
        public static int InsertIfNotExist(string tableName, string connectionString, object dynamicParameters, IEnumerable<string> keyNames)
        {
            var sql = BuildInsertIfNotExistSql(tableName, dynamicParameters, keyNames);

            using (IDbConnection cn = new SqlConnection(connectionString))
            {
                cn.Open();
                var rowsAffected = cn.Execute(sql, dynamicParameters);
                return rowsAffected;
            }
        }

        public static int UpdateOrInsert(string tableName, string connectionString, object dynamicParameters, IEnumerable<string> keyNames)
        {
            var sql = BuildUpdateOrInsertSql(tableName, dynamicParameters, keyNames);

            using (IDbConnection cn = new SqlConnection(connectionString))
            {
                cn.Open();
                var rowsAffected = cn.Execute(sql, dynamicParameters);
                return rowsAffected;
            }
        }

        public static int ExecuteStoredProcedure(string procedureName, string connectionString, object parameters)
        {
            using (IDbConnection cn = new SqlConnection(connectionString))
            {
                cn.Open();
                int rowsAffected = cn.Execute(procedureName, parameters, commandType: CommandType.StoredProcedure);
                return rowsAffected;
            }
        }

        public static int UpdateRecordInTable(string tableName, string connectionString, string uniqueColumnName, Guid uniqueId, object dynamicParameter, bool setUpdatedDateTime = false)
        {
            string sql = BuildUpdateSql(tableName, uniqueColumnName, uniqueId, dynamicParameter, setUpdatedDateTime);

            return ExecuteNonQuery(connectionString, dynamicParameter, sql);
        }

        public static int UpdateRecordInTable(string tableName, string connectionString, string idColumnName, int id, object dynamicParameter, bool setUpdatedDateTime = false)
        {
            string sql = BuildUpdateSql(tableName, idColumnName, id, dynamicParameter, setUpdatedDateTime);

            return ExecuteNonQuery(connectionString, dynamicParameter, sql);
        }

        public static int InsertIntoTable(string tableName, string connectionString, IEnumerable<object> dynamicParameters)
        {
            string insertSql = BuildInsertSql(tableName, dynamicParameters?.FirstOrDefault());

            if (string.IsNullOrWhiteSpace(insertSql))
                return 0;

            using (IDbConnection cn = new SqlConnection(connectionString))
            {
                cn.Open();
                var rowsAffected = cn.Execute(insertSql, dynamicParameters);

                return rowsAffected;
            }
        }

        public static int UpdateRecordInTable(string tableName, string connectionString, object dynamicParameters,
            IEnumerable<string> keyNames)
        {
            var sql = BuildUpdateSql(tableName, dynamicParameters, keyNames);

            return ExecuteNonQuery(connectionString, dynamicParameters, sql);
        }

        public static int InsertIntoTableWithIdReturned(string tableName, string connectionString, IEnumerable<object> dynamicParameters, string uniqueIdColumn, Guid uniqueId)
        {
            var insertSql = BuildInsertSql(tableName, dynamicParameters?.FirstOrDefault());

            if (string.IsNullOrWhiteSpace(insertSql))
                return 0;

            using (IDbConnection cn = new SqlConnection(connectionString))
            {
                cn.Open();
                cn.Execute(insertSql, dynamicParameters);
                return GetId(cn, tableName, uniqueIdColumn, uniqueId);
            }
        }

        public static int UpdateTableWithIdReturned(string tableName, string connectionString, object dynamicParameters, IEnumerable<string> keyNames, string uniqueIdColumn, Guid uniqueId)
        {
            var sql = BuildUpdateSql(tableName, dynamicParameters, keyNames);

            if (string.IsNullOrWhiteSpace(sql))
                return 0;

            using (IDbConnection cn = new SqlConnection(connectionString))
            {
                cn.Open();
                cn.Execute(sql, dynamicParameters);
                return GetId(cn, tableName, uniqueIdColumn, uniqueId);
            }
        }

        public static int InsertWithIdReturned(string tableName, string connectionString, object dynamicParameter)
        {
            var insertSql = BuildInsertSqlWithReturnValue(tableName, dynamicParameter);

            if (string.IsNullOrWhiteSpace(insertSql))
                return 0;

            using (IDbConnection cn = new SqlConnection(connectionString))
            {
                cn.Open();
                return cn.Query<int>(insertSql, dynamicParameter).Single();
            }
        }

        public static int DeleteFromTable(string tableName, string connectionString, IEnumerable<object> dynamicParameters)
        {
            string deleteSql = BuildDeleteSql(tableName, dynamicParameters?.FirstOrDefault());

            if (string.IsNullOrWhiteSpace(deleteSql))
                return 0;

            using (IDbConnection cn = new SqlConnection(connectionString))
            {
                cn.Open();
                var rowsAffected = cn.Execute(deleteSql, dynamicParameters);

                return rowsAffected;
            }
        }

        internal static string BuildInsertIfNotExistSql(string tableName, object dynamicParameter, IEnumerable<string> keyNames)
        {
            if (dynamicParameter == null || keyNames == null)
                return string.Empty;

            var propertyNames = dynamicParameter.GetType().GetProperties().Select(x => x.Name);

            var parameterNames = string.Join(", ", propertyNames);
            var parameterVariables = string.Join(", ", propertyNames.Select(x => "@" + x));

            if (string.IsNullOrWhiteSpace(parameterNames) || !keyNames.Any())
                return string.Empty;

            if (keyNames.Except(propertyNames).Any())
                return string.Empty;

            string insertSql = $"insert into {tableName} ({parameterNames}) values ({parameterVariables})";
            string whereClauseSql = string.Join(" and ", keyNames.Select(keyName => $"{keyName} = @{keyName}"));

            string insertIfNotExistSql =
                $@"begin transaction
                 if not exists (select * from {tableName} with (holdlock) where {whereClauseSql})
                 begin
                    {insertSql}
                 end
                commit transaction";

            return insertIfNotExistSql;
        }

        internal static string BuildUpdateOrInsertSql(string tableName, object dynamicParameter, IEnumerable<string> keyNames)
        {
            if (dynamicParameter == null || keyNames == null)
                return string.Empty;

            var propertyNames = dynamicParameter.GetType().GetProperties().Select(x => $"{x.Name}");

            var parameterNames = string.Join(", ", propertyNames.Select(x => $"[{x}]"));
            var parameterVariables = string.Join(", ", propertyNames.Select(x => "@" + x));

            if (string.IsNullOrWhiteSpace(parameterNames) || !keyNames.Any())
                return string.Empty;

            if (keyNames.Except(propertyNames).Any())
                return string.Empty;

            var whereClauseSql = string.Join(" and ", keyNames.Select(keyName => $"[{keyName}] = @{keyName}"));
            var setters = string.Join(", ", propertyNames.Select(propertyName => $"[{propertyName}] = @{propertyName}"));
            var insertSql = $"insert into {tableName} ({parameterNames}) values ({parameterVariables})";

            return $@"begin transaction
                   update {tableName} with (holdlock) set {setters} where {whereClauseSql}
                   if @@rowcount = 0
                   begin
                       {insertSql}
                   end
                   commit transaction";
        }

        internal static string BuildUpdateSql(string tableName, object dynamicParameter, IEnumerable<string> keyNames)
        {
            if (dynamicParameter == null || keyNames == null)
                return string.Empty;

            var propertyNames = dynamicParameter.GetType().GetProperties().Select(x => x.Name);
            var parameterNames = string.Join(", ", propertyNames);

            if (string.IsNullOrWhiteSpace(parameterNames) || !keyNames.Any())
                return string.Empty;

            if (keyNames.Except(propertyNames).Any())
                return string.Empty;

            var whereClauseSql = string.Join(" and ", keyNames.Select(keyName => $"[{keyName}] = @{keyName}"));
            var setters = string.Join(", ", propertyNames.Select(propertyName => $"[{propertyName}] = @{propertyName}"));

            return $"update {tableName} set {setters} where {whereClauseSql}";
        }

        private static int GetId(IDbConnection cn, string tableName, string uniqueIdColumn, Guid uniqueId)
        {
            return cn.Query<int>($"select Id from {tableName} where {uniqueIdColumn}='{uniqueId}'").Single();
        }

        internal static string BuildUpdateSql(string tableName, string uniqueIdColumnName, Guid uniqueId, object parameters, bool setUpdatedDateTime)
        {
            string uniqueIdString = $"'{uniqueId}'";
            return BuildUpdateSql(tableName, uniqueIdColumnName, uniqueIdString, parameters, setUpdatedDateTime);
        }

        private static string BuildUpdateSql(string tableName, string idColumnName, int id, object parameters, bool setUpdatedDateTime)
        {
            string idString = id.ToString();
            return BuildUpdateSql(tableName, idColumnName, idString, parameters, setUpdatedDateTime);
        }

        public static int DeleteRecordsInTable(string tableName, string connectionString, string idColumnName, int id)
        {
            var sql = BuildDeleteSql(tableName, idColumnName, id);

            using (IDbConnection cn = new SqlConnection(connectionString))
            {
                cn.Open();
                var rowsAffected = cn.Execute(sql);
                return rowsAffected;
            }
        }

        public static int DeleteRecordsInTable(string tableName, string connectionString, string idColumnName, Guid uniqueId)
        {
            var sql = BuildDeleteSql(tableName, idColumnName, uniqueId);

            using (IDbConnection cn = new SqlConnection(connectionString))
            {
                cn.Open();
                var rowsAffected = cn.Execute(sql);
                return rowsAffected;
            }
        }

        internal static string BuildInsertSql(string tableName, object dynamicParameter)
        {
            if (dynamicParameter == null)
                return string.Empty;

            var propertyNames = dynamicParameter.GetType().GetProperties().Select(x => x.Name);

            var names = string.Join(",", propertyNames.Select(x => $"[{x}]"));
            var values = string.Join(",", propertyNames.Select(x => $"@{x}"));

            if (string.IsNullOrWhiteSpace(names))
                return string.Empty;

            string insertSql = $"INSERT INTO {tableName} ({names}) VALUES ({values})";
            return insertSql;
        }

        internal static string BuildInsertSqlWithReturnValue(string tableName, object dynamicParameter)
        {
            if (dynamicParameter == null)
                return string.Empty;

            var propertyNames = dynamicParameter.GetType().GetProperties().Select(x => x.Name);

            var names = string.Join(",", propertyNames.Select(x => $"[{x}]"));
            var values = string.Join(",", propertyNames.Select(x => $"@{x}"));

            if (string.IsNullOrWhiteSpace(names))
                return string.Empty;

            string insertSql = $"INSERT INTO {tableName} ({names}) VALUES ({values}); SELECT CAST(SCOPE_IDENTITY() as int)";
            return insertSql;
        }

        private static int ExecuteNonQuery(string connectionString, object dynamicParameter, string sql)
        {
            if (string.IsNullOrWhiteSpace(sql))
                return 0;

            using (IDbConnection cn = new SqlConnection(connectionString))
            {
                cn.Open();
                var rowsAffected = cn.Execute(sql, dynamicParameter);

                return rowsAffected;
            }
        }

        private static string BuildUpdateSql(string tableName, string idColumnName, string id, object parameters, bool setUpdatedDateTime = false)
        {
            var properties = parameters.GetType().GetProperties().Select(x => x.Name);
            var setters = string.Join(",", properties.Select(propertyName => $"{propertyName}=@{propertyName}"));

            if (setUpdatedDateTime)
            {
                setters += ", Updated = getdate()";
            }

            if (string.IsNullOrWhiteSpace(setters))
                return string.Empty;

            string insertSql = $"UPDATE {tableName} SET {setters} WHERE {idColumnName}={id}";
            return insertSql;
        }

        internal static string BuildDeleteSql(string tableName, string idColumnName, int id)
        {
            if (string.IsNullOrEmpty(tableName) || string.IsNullOrEmpty(idColumnName))
                return string.Empty;

            var deleteSql = $"DELETE FROM {tableName} WHERE {idColumnName}={id}";
            return deleteSql;
        }

        internal static string BuildDeleteSql(string tableName, string idColumnName, Guid uniqueId)
        {
            if (string.IsNullOrEmpty(tableName) || string.IsNullOrEmpty(idColumnName))
                return string.Empty;

            var deleteSql = $"DELETE FROM {tableName} WHERE {idColumnName}='{uniqueId}'";
            return deleteSql;
        }

        internal static string BuildDeleteSql(string tableName, object parameters)
        {
            var properties = parameters.GetType().GetProperties().Select(x => x.Name);
            var filters = string.Join(" AND ", properties.Select(propertyName => $"[{propertyName}]=@{propertyName}"));

            if (string.IsNullOrWhiteSpace(filters))
                return string.Empty;

            var deleteSql = $"DELETE FROM {tableName} WHERE {filters}";
            return deleteSql;
        }
    }
}
