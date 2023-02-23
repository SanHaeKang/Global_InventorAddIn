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
    /// BorderTextBlock.xaml에 대한 상호 작용 논리
    /// </summary>
    public partial class BorderTextBlock : UserControl
    {
        public static readonly DependencyProperty StyleProperty =
            DependencyProperty.Register("Style", typeof(Style), typeof(BorderTextBlock), new PropertyMetadata(null));

        public Style Style
        {
            get => (Style)GetValue(StyleProperty);
            set => SetValue(StyleProperty, value);
        }

        public static readonly DependencyProperty TextProperty =
            DependencyProperty.Register("Text", typeof(string), typeof(BorderTextBlock), new PropertyMetadata(""));

        public string Text
        {
            get => (string)GetValue(TextProperty);
            set => SetValue(TextProperty, value);
        }

        public BorderTextBlock()
        {
            InitializeComponent();
            DataContext = this;
        }
    }
}
