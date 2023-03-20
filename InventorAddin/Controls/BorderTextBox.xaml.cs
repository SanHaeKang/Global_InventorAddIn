using InventorAddin.Core.Helpers;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace InvAddIn.Controls
{
    /// <summary>
    /// BorderTextBox.xaml에 대한 상호 작용 논리
    /// </summary>
    public partial class BorderTextBox : UserControl
    {
        public static readonly DependencyProperty IsEnabledProperty = 
            DependencyProperty.Register("IsEnabled", typeof(bool), typeof(BorderTextBox), new FrameworkPropertyMetadata(true, FrameworkPropertyMetadataOptions.BindsTwoWayByDefault));

        public bool IsEnabled
        {
            get => (bool) GetValue(IsEnabledProperty);
            set => SetValue(IsEnabledProperty, value);
        }

        public static readonly DependencyProperty StyleProperty =
            DependencyProperty.Register("Style", typeof(Style), typeof(BorderTextBox), new PropertyMetadata(null));

        public Style Style
        {
            get => (Style)GetValue(StyleProperty);
            set => SetValue(StyleProperty, value);
        }

        public static readonly DependencyProperty TextProperty =
            DependencyProperty.Register("Text", typeof(string), typeof(BorderTextBox), new FrameworkPropertyMetadata(null, FrameworkPropertyMetadataOptions.BindsTwoWayByDefault));

        public string Text
        {
            get => (string)GetValue(TextProperty);
            set => SetValue(TextProperty, value);
        }
        public BorderTextBox()
        {
            InitializeComponent();
        }

        private void NumberTextBox_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            TextBox textBox = (TextBox)sender;
            e.Handled = !ValidateHelper.IsDecimalNumber(textBox.Text, e.Text);
        }
    }
}
