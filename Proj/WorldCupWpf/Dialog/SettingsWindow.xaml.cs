using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.ComponentModel;
using SharedDataLib;
using WorldCupViewer.Localization;

namespace WorldCupWpf
{
    // Took this from SO but dunno were from exactly
    public class SettingsData : INotifyPropertyChanged
    {
        private int x = 1600;
        private int y = 900;

        public bool maximized = false;

        public int X
        {
            get { return x; }
            set { x = value; OnPropertyChanged(nameof(X)); }
        }
        public int Y
        {
            get { return y; }
            set { y = value; OnPropertyChanged(nameof(Y)); }
        }

        public override string? ToString()
        {
            return x.ToString() + " : " + y.ToString();
        }

        //below is the boilerplate code supporting PropertyChanged events:
        public event PropertyChangedEventHandler? PropertyChanged;
        protected void OnPropertyChanged(string name)
        {
            PropertyChangedEventHandler? handler = PropertyChanged;
            if (handler != null)
            {
                handler(this, new PropertyChangedEventArgs(name));
            }
        }
    }

    public class LanguageData
    {
        public SupportedLanguages.SupportedLanguageInfo languageInfo;

        public override string? ToString()
        {
            return languageInfo.name;
        }
    }

    /// <summary>
    /// Interaction logic for SettingsWindow.xaml
    /// </summary>
    public partial class SettingsWindow : Window
    {
        SettingsData resDat;
        bool initializing = true;

        public SettingsWindow(SettingsData resDat)
        {
            this.resDat = resDat;

            InitializeComponent();

            // Resolution data logic
            Binding xBinding = new(nameof(resDat.X));

            xBinding.Source = resDat;
            xBinding.Mode = BindingMode.TwoWay;
            xBinding.UpdateSourceTrigger = UpdateSourceTrigger.PropertyChanged;

            Binding yBinding = new(nameof(resDat.Y));

            yBinding.Source = resDat;
            yBinding.Mode = BindingMode.TwoWay;
            yBinding.UpdateSourceTrigger = UpdateSourceTrigger.PropertyChanged;

            BindingOperations.SetBinding(tbResolutionX, TextBox.TextProperty, xBinding);
            BindingOperations.SetBinding(tbResolutionY, TextBox.TextProperty, yBinding);

            SettingsData resDatOption1 = new() { X = 800, Y = 600 };
            SettingsData resDatOption2 = new() { X = 1280, Y = 800 };
            SettingsData resDatOption3 = new() { X = 1600, Y = 900 };

            cbResolution.Items.Add(resDatOption1);
            cbResolution.SelectedItem = resDatOption1;
            cbResolution.Items.Add(resDatOption2);
            cbResolution.Items.Add(resDatOption3);

            // Language data logic

            foreach (var languageInfo in SupportedLanguages.GetSupportedLanguageInfoList())
            {
                if (languageInfo == null)
                    continue;

                LanguageData languageData = new();
                languageData.languageInfo = languageInfo;

                cbLanguage.Items.Add(languageData);
                if (languageData.languageInfo.culture.ThreeLetterISOLanguageName == Thread.CurrentThread.CurrentCulture.ThreeLetterISOLanguageName)
                    cbLanguage.SelectedItem = languageData;
            }

            chbMaximized.IsChecked = resDat.maximized;

            DoLanguageBindings();

            initializing = false;
        }

        private void DoLanguageBindings()
        {
            LocDataBindable ldb = new();
            ldb.LocTarget = LocalizationOptions.Please_Set_Your_Preferences;
            ldb.SucceedingText = ":";

            Binding locBinding = new(nameof(ldb.AssembledString));

            locBinding.Source = ldb;
            locBinding.Mode = BindingMode.TwoWay;

            BindingOperations.SetBinding(lblPleaseSetPreferences, Label.ContentProperty, locBinding);


            ldb = new();
            ldb.LocTarget = LocalizationOptions.Language;
            ldb.SucceedingText = ":";

            locBinding = new(nameof(ldb.AssembledString));

            locBinding.Source = ldb;
            locBinding.Mode = BindingMode.TwoWay;

            BindingOperations.SetBinding(lblLanguage, Label.ContentProperty, locBinding);


            ldb = new();
            ldb.LocTarget = LocalizationOptions.Resolution;
            ldb.SucceedingText = ":";

            locBinding = new(nameof(ldb.AssembledString));

            locBinding.Source = ldb;
            locBinding.Mode = BindingMode.TwoWay;

            BindingOperations.SetBinding(lblResolution, Label.ContentProperty, locBinding);


            ldb = new();
            ldb.LocTarget = LocalizationOptions.Continue;

            locBinding = new(nameof(ldb.AssembledString));

            locBinding.Source = ldb;
            locBinding.Mode = BindingMode.TwoWay;

            BindingOperations.SetBinding(btnConfirm, Label.ContentProperty, locBinding);

            // STEP BACK, OLD MAN
            LocDataBindable.GenerateBindingsForTarget(chbMaximized, CheckBox.ContentProperty, LocalizationOptions.Maximized);
        }

        private void cbResolution_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (initializing || sender is not ComboBox cb)
                return;

            if (cb.SelectedItem is not SettingsData rd)
                return;

            resDat.X = rd.X;
            resDat.Y = rd.Y;
        }

        private void BtnConfirm_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void Window_PreviewKeyUp(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter || e.Key == Key.Escape)
            {
                this.Close();
            }
        }

        private void cbLanguage_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (sender is not ComboBox cb)
                return;

            if (cb.SelectedItem is not LanguageData ld)
                return;

            LocalizationHandler.SetCulture(ld.languageInfo.culture);
        }

        private void chbMaximized_Checked(object sender, RoutedEventArgs e)
        {
            if (initializing)
                return;

            resDat.maximized = chbMaximized.IsChecked ?? false;
        }

        // Am so mad rn
        private void chbMaximized_Unchecked(object sender, RoutedEventArgs e)
        {
            if (initializing)
                return;

            resDat.maximized = chbMaximized.IsChecked ?? false;
        }
    }
}
