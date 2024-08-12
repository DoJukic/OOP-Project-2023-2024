using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using SharedDataLib;
using WorldCupViewer.Properties;
using WorldCupViewer.Resources;

namespace WorldCupViewer.Localization
{
    public enum LocalizationOptions
    {
        TestString,
        TestString2,
        Data_Select_Slash_Config,
        DataSources,
        Local,
        Remote,
        Load,
        Delete,
        Info,
        Structure,
        Data,
        Wait,
        Okay,
        Error,
        Download
    }

    /// <summary>
    /// Plz only use from ui thread
    /// </summary>
    public static class LocalizationHandler
    {
        private static event Action? OnLocalizationChanged;
        private static CultureInfo currentCultureInfo;

        public static string GetCurrentLocOptionsString(LocalizationOptions link)
        {
            return link switch
            {
                LocalizationOptions.TestString => Resource.TestString,
                LocalizationOptions.TestString2 => Resource.TestString2,
                LocalizationOptions.Data_Select_Slash_Config => Resource.LOC_Data_Select___Config,
                LocalizationOptions.DataSources => Resource.LOC_Data_Sources,
                LocalizationOptions.Local => Resource.LOC_Local,
                LocalizationOptions.Remote => Resource.LOC_Remote,
                LocalizationOptions.Load => Resource.LOC_Load,
                LocalizationOptions.Delete => Resource.LOC_Delete,
                LocalizationOptions.Info => Resource.LOC_Info,
                LocalizationOptions.Structure => Resource.LOC_Structure,
                LocalizationOptions.Data => Resource.LOC_Data,
                LocalizationOptions.Wait => Resource.LOC_Wait,
                LocalizationOptions.Okay => Resource.LOC_Okay,
                LocalizationOptions.Error => Resource.LOC_Error,
                LocalizationOptions.Download => Resource.LOC_Download,
                _ => throw new NotImplementedException(),
            };
        }

        static LocalizationHandler()
        {
            currentCultureInfo = SupportedLanguages.GetDefaultSupportedLanguageInfo().culture;
        }

        /// <summary>
        /// Will run your action once the localization changes, simple as
        /// </summary>
        /// <param name="action"></param>
        public static void SubscribeToLocalizationChanged(Action action)
        {
            OnLocalizationChanged += action;
        }

        /// <summary>
        /// Necessary if you wanna not crash the handler when your thingamajig is GCd
        /// </summary>
        /// <param name="action"></param>
        public static void UnsubscribeFromLocalizationChanged(Action action)
        {
            OnLocalizationChanged -= action;
        }

        private static void ApplyCulture()
        {
            if (Thread.CurrentThread.CurrentCulture != currentCultureInfo)
                Thread.CurrentThread.CurrentCulture = currentCultureInfo;
            if (Thread.CurrentThread.CurrentUICulture != currentCultureInfo)
                Thread.CurrentThread.CurrentUICulture = currentCultureInfo;
        }

        public static void SetCulture(CultureInfo culture)
        {
            currentCultureInfo = culture;

            ApplyCulture();

            OnLocalizationChanged?.Invoke();
        }

        public static void LocalizeAllChildren(Control target)
        {
            ApplyCulture();

            foreach (ILocalizeable localizeable in LocalUtils.GetAllControls(target).OfType<ILocalizeable>())
            {
                localizeable.SetLocalizedText(GetCurrentLocOptionsString(localizeable.GetLocalizationTarget()));
            }
        }

        public static void LocalizeOne(ILocalizeable target)
        {
            ApplyCulture();

            target.SetLocalizedText(GetCurrentLocOptionsString(target.GetLocalizationTarget()));
        }
    }
}
