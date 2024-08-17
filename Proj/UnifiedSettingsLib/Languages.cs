using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharedDataLib
{
    public static class SupportedLanguages
    {
        public enum SupportedLanguage
        {
            EN,
            HR
        }
        public static IEnumerable<SupportedLanguage> GetSupportedLanguageList()
        {
            return (SupportedLanguage[])Enum.GetValues(typeof(SupportedLanguage));
        }

        public static SupportedLanguageInfo GetSupportedLanguageInfo(SupportedLanguage target)
        {
            return target switch
            {
                SupportedLanguage.HR => new(new("hr"), "Hrvatski", SupportedLanguage.HR),
                SupportedLanguage.EN => new(new("en"), "English", SupportedLanguage.EN),
                _ => throw new NotImplementedException()
            };
        }
        public static IEnumerable<SupportedLanguageInfo> GetSupportedLanguageInfoList()
        {
            List<SupportedLanguageInfo> theReturnables = new();

            foreach (var language in GetSupportedLanguageList())
                theReturnables.Add(GetSupportedLanguageInfo(language));

            return theReturnables;
        }

        public static SupportedLanguageInfo GetDefaultSupportedLanguageInfo()
        {
            return GetSupportedLanguageInfo(0);
        }

        public class SupportedLanguageInfo
        {
            public readonly CultureInfo culture;
            public readonly String name;
            public readonly SupportedLanguage langID;

            public String Name { get { return name; } }

            internal SupportedLanguageInfo(String _culture, String _name, SupportedLanguage langID)
            {
                culture = new(_culture);
                name = _name;
                this.langID = langID;
            }
        }
    }
}
