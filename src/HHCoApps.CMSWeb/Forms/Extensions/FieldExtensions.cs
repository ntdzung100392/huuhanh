using System;
using System.Collections.Generic;
using System.Linq;
using Umbraco.Forms.Core.Models;

namespace HHCoApps.CMSWeb.Forms.Extensions
{
    public static class FieldExtensions
    {
        public static Field GetField(this IEnumerable<Field> fields, string fieldAlias)
        {
            return fields?.FirstOrDefault(x => x.Alias.Equals(fieldAlias, StringComparison.OrdinalIgnoreCase));
        }

        public static string GetFieldValue(this IEnumerable<Field> fields, string fieldAlias)
        {
            return fields?.GetField(fieldAlias)?.Values?.FirstOrDefault()?.ToString() ?? string.Empty;
        }
    }
}