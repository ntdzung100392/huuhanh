using Umbraco.Core.Dictionary;

namespace HHCoApps.CMSWeb.Helpers
{
    public static class CultureDictionaryExtension
    {
        public static string GetTranslatedOrDefault(this ICultureDictionary dictionary, string key, string defaultWord)
        {
            var translatedWord = dictionary[key];
            return string.IsNullOrEmpty(translatedWord) ? defaultWord : translatedWord;
        }
    }
}