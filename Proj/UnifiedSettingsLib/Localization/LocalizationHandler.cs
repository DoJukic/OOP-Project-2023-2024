using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using SharedDataLib;
using SharedDataLib.Resources;

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
        Download,
        Download_State,
        Ready,
        Busy,
        Data_is_Loading,
        Download_Failed,
        Cancel,
        Team_Slash_Player_Select,
        Failed_to_Load_Data,
        Failed_To_Save_Settings,
        Yes,
        No,
        Yes_And_Do_Not_Ask_Again,
        Are_You_Sure,
        Are_You_Sure_You_Want_To_Change_The_Language,
        Name,
        Year,
        Selected_Team,
        Failed_To_Load_Settings_Data_Will_Not_Be_Saved,
        Favourites,
        Guid_Error_Data_Might_Not_Be_Saved,
        Player_List,
        Players,
        Shirt_Number,
        Games_Captained,
        Usual_Position,
        Player_Data_Is_Loading,
        Confirm_Selection,
        Team_Statistics,
        Players_By_Goals_Scored,
        Players_By_Yellow_Cards,
        Matches_By_Attendance,
        Are_You_Sure_You_Want_To_Close_The_Application,
        Choose_An_Image,
        Errors_Detected,
        The_Following_Errors_Have_Been_Detected_While_Loading_Data,
        Yellow_Cards,
        Goals_Scored,
        Errors_Detected_In_Data,
        Home_Team,
        Attendance,
        Away_Team,
        Print,
        Please_Set_Your_Preferences,
        Language,
        Resolution,
        Continue,
        Application_Ran_Into_Error,
        Could_Not_Load_Data_Fatal,
        Could_Not_Save_Data_Warning,
        Could_Not_Load_Cup_Fatal,
        Maximized,
        Starting_Eleven,
        Bench,
        Draws,
        Games,
        Goals,
        Losses,
        Played,
        Scored,
        Taken,
        Wins,
        Differential,
        Position,
        Captain,
        Match_Data,
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
                LocalizationOptions.Match_Data => Resource.LOC_Match_Data,
                LocalizationOptions.Position => Resource.LOC_Position,
                LocalizationOptions.Captain => Resource.LOC_Captain,
                LocalizationOptions.Draws => Resource.LOC_Draws,
                LocalizationOptions.Games => Resource.LOC_Games,
                LocalizationOptions.Goals => Resource.LOC_Goals,
                LocalizationOptions.Losses => Resource.LOC_Losses,
                LocalizationOptions.Played => Resource.LOC_Played,
                LocalizationOptions.Scored => Resource.LOC_Scored,
                LocalizationOptions.Taken => Resource.LOC_Taken,
                LocalizationOptions.Wins => Resource.LOC_Wins,
                LocalizationOptions.Differential => Resource.LOC_Differential,
                LocalizationOptions.Maximized => Resource.LOC_Maximized,
                LocalizationOptions.Could_Not_Load_Data_Fatal => Resource.LOC_WPF_Could_Not_Load_Data_Fatal,
                LocalizationOptions.Starting_Eleven => Resource.LOC_Starting_Eleven,
                LocalizationOptions.Bench => Resource.LOC_Bench,
                LocalizationOptions.Could_Not_Save_Data_Warning => Resource.LOC_WPF_Could_Not_Save_Data_Warning,
                LocalizationOptions.Could_Not_Load_Cup_Fatal => Resource.LOC_WPF_Could_Not_Load_Cup_Fatal,
                LocalizationOptions.Application_Ran_Into_Error => Resource.LOC_Application_Ran_Into_Error,
                LocalizationOptions.Continue => Resource.LOC_Continue,
                LocalizationOptions.Resolution => Resource.LOC_Resolution,
                LocalizationOptions.Language => Resource.LOC_Language,
                LocalizationOptions.Please_Set_Your_Preferences => Resource.LOC_Please_Set_Your_Preferences,
                LocalizationOptions.Print => Resource.LOC_Print,
                LocalizationOptions.Home_Team => Resource.LOC_Home_Team,
                LocalizationOptions.Attendance => Resource.LOC_Attendance,
                LocalizationOptions.Away_Team => Resource.LOC_Away_Team,
                LocalizationOptions.Errors_Detected_In_Data => Resource.LOC_Errors_Detected_in_Data,
                LocalizationOptions.Yellow_Cards => Resource.LOC_Yellow_Cards,
                LocalizationOptions.Goals_Scored => Resource.LOC_Goals_Scored,
                LocalizationOptions.Errors_Detected => Resource.LOC_Errors_Detected,
                LocalizationOptions.The_Following_Errors_Have_Been_Detected_While_Loading_Data => Resource.LOC_The_Following_Errors_Have_Been_Detected_While_Loading_Data,
                LocalizationOptions.Choose_An_Image => Resource.LOC_Choose_An_Image,
                LocalizationOptions.Are_You_Sure_You_Want_To_Close_The_Application => Resource.LOC_Are_You_Sure_You_Want_To_Close_The_Application,
                LocalizationOptions.Players_By_Goals_Scored => Resource.LOC_Players_By_Goals_Scored,
                LocalizationOptions.Players_By_Yellow_Cards => Resource.LOC_Players_By_Yellow_Cards,
                LocalizationOptions.Matches_By_Attendance => Resource.LOC_Matches_By_Attendance,
                LocalizationOptions.Confirm_Selection => Resource.LOC_Confirm_Selection,
                LocalizationOptions.Team_Statistics => Resource.LOC_Team_Statistics,
                LocalizationOptions.Players => Resource.LOC_Players,
                LocalizationOptions.Shirt_Number => Resource.LOC_Shirt_Number,
                LocalizationOptions.Games_Captained => Resource.LOC_Games_Captained,
                LocalizationOptions.Usual_Position => Resource.LOC_Usual_Position,
                LocalizationOptions.Player_Data_Is_Loading => Resource.LOC_Player_Data_is_Loading,
                LocalizationOptions.Data_Select_Slash_Config => Resource.LOC_Data_Select___Config,
                LocalizationOptions.Player_List => Resource.LOC_Player_List,
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
                LocalizationOptions.Download_State => Resource.LOC_Download_State,
                LocalizationOptions.Ready => Resource.LOC_Ready,
                LocalizationOptions.Busy => Resource.LOC_Busy,
                LocalizationOptions.Data_is_Loading => Resource.LOC_Data_is_Loading,
                LocalizationOptions.Download_Failed => Resource.LOC_Download_Failed,
                LocalizationOptions.Cancel => Resource.LOC_Cancel,
                LocalizationOptions.Team_Slash_Player_Select => Resource.LOC_Team_Slash_Player_Select,
                LocalizationOptions.Failed_to_Load_Data => Resource.LOC_Failed_to_Load_Data,
                LocalizationOptions.Failed_To_Save_Settings => Resource.LOC_Failed_To_Save_Settings,
                LocalizationOptions.Yes => Resource.LOC_Yes,
                LocalizationOptions.No => Resource.LOC_No,
                LocalizationOptions.Yes_And_Do_Not_Ask_Again => Resource.LOC_Yes_And_Do_Not_Ask_Again,
                LocalizationOptions.Are_You_Sure => Resource.LOC_Are_You_Sure,
                LocalizationOptions.Are_You_Sure_You_Want_To_Change_The_Language => Resource.LOC_Are_You_Sure_You_Want_To_Change_The_Language,
                LocalizationOptions.Name => Resource.LOC_Name,
                LocalizationOptions.Year => Resource.LOC_Year,
                LocalizationOptions.Selected_Team => Resource.LOC_Selected_Team,
                LocalizationOptions.Failed_To_Load_Settings_Data_Will_Not_Be_Saved => Resource.LOC_Failed_To_Load_Settings_Data_Will_Not_Be_Saved,
                LocalizationOptions.Favourites => Resource.LOC_Favourites,
                LocalizationOptions.Guid_Error_Data_Might_Not_Be_Saved => Resource.LOC_Guid_Error_Data_Might_Not_Be_Saved,
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

        public static void LocalizeOne(ILocalizeable target)
        {
            ApplyCulture();

            target.SetLocalizedText(GetCurrentLocOptionsString(target.GetLocalizationTarget()));
        }
    }
}
