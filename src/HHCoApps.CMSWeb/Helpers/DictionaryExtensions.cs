using System;
using System.Collections.Generic;

namespace HHCoApps.CMSWeb.Helpers
{
    public static class DictionaryExtensions
    {
        public static TValue GetValue<TValue>(this IDictionary<string, object> macroParams, string macroParamName, TValue defaultValue)
        {
            if (macroParams.TryGetValue(macroParamName, out var macroValue))
            {
                // Fixing the issue of macro parameters return "null" string for empty value
                if ("null".Equals(macroValue))
                {
                    return defaultValue;
                }

                return (TValue)Convert.ChangeType(macroValue, typeof(TValue));
            }

            return defaultValue;
        }

        public static string[] GetContentIds(this IDictionary<string, object> macroParams, string macroParamName)
        {
            var paramValue = GetValue(macroParams, macroParamName, string.Empty);
            return paramValue.Split(new []{ ',' }, StringSplitOptions.RemoveEmptyEntries);
        }
    }
}