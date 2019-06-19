using System;
using System.Collections.Generic;
using System.Linq;
using HHCoApps.Core;
using HHCoApps.Libs;
using HHCoApps.Repository.Dapper;

namespace HHCoApps.Repository.Implementations
{
    internal class CategoryRepository : ICategoryRepository
    {
        private const string SQL_CONNECTION_STRING = "HHCoApps";
        private const string CATEGORY_TABLE_NAME = "dbo.Category";

        public IEnumerable<Category> GetCategories()
        {
            using (var context = new HuuHanhEntities())
            {
                return GetCategories(context.Categories).ToList();
            }
        }

        internal IQueryable<Category> GetCategories(IQueryable<Category> categories)
        {
            return categories.OrderBy(c => c.Code);
        }

        public Category GetCategoryById(int categoryId)
        {
            using (var context = new HuuHanhEntities())
            {
                return GetCategoryById(categoryId, context.Categories);
            }
        }

        internal Category GetCategoryById(int categoryId, IQueryable<Category> categories)
        {
            return categories.FirstOrDefault(c => c.Id == categoryId);
        }

        public void InsertCategory(Category entity)
        {
            if (entity == null || string.IsNullOrEmpty(entity.Name))
                throw new ArgumentNullException(nameof(entity));

            var keyName = new[]
            {
                "Name"
            };
            var parameters = new
            {
                entity.Name,
                entity.Code
            };

            var rowAffected = DapperRepositoryUtil.InsertIfNotExist(CATEGORY_TABLE_NAME, DbUtilities.GetConnString(SQL_CONNECTION_STRING), parameters, keyName);

            if (rowAffected < 1)
                throw new ArgumentException(GenericMessages.INSERT_UPDATE_ERROR_MESSAGE);
        }

        public void UpdateCategoryByUniqueId(Category entity)
        {
            if (entity == null || string.IsNullOrEmpty(entity.Name))
                throw new ArgumentNullException(nameof(entity));

            var parameter = new
            {
                entity.Code,
                entity.Name
            };

            var rowAffected = DapperRepositoryUtil.UpdateRecordInTable(CATEGORY_TABLE_NAME, DbUtilities.GetConnString(SQL_CONNECTION_STRING), "UniqueId", entity.UniqueId, parameter);

            if (rowAffected < 1)
                throw new ArgumentException(GenericMessages.INSERT_UPDATE_ERROR_MESSAGE);
        }
    }
}