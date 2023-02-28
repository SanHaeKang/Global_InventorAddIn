using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Forms.VisualStyles;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using InventorAddin.Core.ViewModels;

namespace InvAddIn.Views.Pages
{
    /// <summary>
    /// SpurGearPage.xaml에 대한 상호 작용 논리
    /// </summary>
    public partial class SpurGearPage : GearSummaryView
    {
        public SpurGearPage()
        {
            InitializeComponent();
        }
        
        private void NumberTextBox_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            e.Handled = !IsDecimalNumber(sender, e.Text);
        }

        private bool IsDecimalNumber(object sender, string text)
        {
            TextBox textBox = (TextBox)sender;

            bool isNumeric = double.TryParse(text, out double result);
            bool isDecimalSeparator = text == CultureInfo.CurrentCulture.NumberFormat.NumberDecimalSeparator;
            bool isValidDecimal = !(isDecimalSeparator && textBox.Text.Contains(CultureInfo.CurrentCulture.NumberFormat.NumberDecimalSeparator));

            return isNumeric || (isDecimalSeparator && isValidDecimal);
        }
    }
}
