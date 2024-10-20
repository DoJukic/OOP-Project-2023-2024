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
using System.Windows.Markup;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using WorldCupViewer.Localization;

namespace WorldCupWpf
{
    /// <summary>
    /// Interaction logic for ErrorWindow.xaml
    /// </summary>
    public partial class ErrorWindow : Window
    {
        private String errormsg;

        public ErrorWindow(LocalizationOptions ErrorMsg)
        {
            InitializeComponent();

            DoLanguageBindings(ErrorMsg);
        }

        private void DoLanguageBindings(LocalizationOptions ErrorMsg)
        {
            LocDataBindable ldb = new();
            ldb.LocTarget = LocalizationOptions.Application_Ran_Into_Error;
            ldb.SucceedingText = ".";

            Binding locBinding = new(nameof(ldb.AssembledString));

            locBinding.Source = ldb;
            locBinding.Mode = BindingMode.TwoWay;

            BindingOperations.SetBinding(lblErrorOccurred, Label.ContentProperty, locBinding);


            ldb = new();
            ldb.LocTarget = LocalizationOptions.Okay;

            locBinding = new(nameof(ldb.AssembledString));

            locBinding.Source = ldb;
            locBinding.Mode = BindingMode.TwoWay;

            BindingOperations.SetBinding(btnOkay, Button.ContentProperty, locBinding);


            ldb = new();
            ldb.LocTarget = ErrorMsg;
            ldb.SucceedingText = ".";

            locBinding = new(nameof(ldb.AssembledString));

            locBinding.Source = ldb;
            locBinding.Mode = BindingMode.TwoWay;

            BindingOperations.SetBinding(tbErrorData, TextBox.TextProperty, locBinding);

            // So that doesnt work actually
            // Well whatever
            tbErrorData.Text = ldb.AssembledString;
        }

        private void btnOkay_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}
