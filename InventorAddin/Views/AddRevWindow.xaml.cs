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
using InventorAddin.Core;
using InventorAddin.Core.ViewModels;

namespace InvAddIn.Views
{
    /// <summary>
    /// AddRevWindow.xaml에 대한 상호 작용 논리
    /// </summary>
    public partial class AddRevWindow : Window
    {
        public AddRevWindow()
        {
            InitializeComponent();
            
            this.DataContext = ViewModelLocator.AddRevWindowVM;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (ValidateInputs())
            {
                DialogResult = true;
            }
        }

        private bool ValidateInputs()
        {
            if (
                string.IsNullOrEmpty(Tb_Explanation.Text) ||
                string.IsNullOrEmpty(Tb_Approval.Text)
            )
            {
                MessageBox.Show("값을 확인해주세요.");
                return false;
            }

            return true;
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
        }
    }
}
