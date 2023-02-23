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
using InvAddIn.Views;
using InventorAddin.Core;
using InventorAddin.Core.Models;
using InventorAddin.Core.ViewModels;

namespace InventorAddin.Views
{
    /// <summary>
    /// TitleColumnWindow.xaml에 대한 상호 작용 논리
    /// </summary>
    public partial class TitleColumnWindow : Window
    {
        public TitleColumnWindow(Inventor.Application m_inventorApplication)
        {
            InitializeComponent();

            try
            {
                this.DataContext = ViewModelLocator.TitleColumnWindowVM(m_inventorApplication);
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
                Close();
            }
        }

        private void Button_AddRev_Click(object sender, RoutedEventArgs e)
        {
            AddRevWindow arWindow = new AddRevWindow();
            if (arWindow.ShowDialog() == true)
            {
                string explanation = ((AddRevWindowViewModel)arWindow.DataContext).Explanation;
                string approval = ((AddRevWindowViewModel)arWindow.DataContext).Approval;
                DateTime date = ((AddRevWindowViewModel)arWindow.DataContext).Date;

                Revision rev = new Revision(){Explanation = explanation, Approval = approval, Date = date};
                ((TitleColumnWindowViewModel)this.DataContext).AddRevCommand.Execute(rev);
            }
        }

        private void Button_Cancel_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void Button_Apply_Click(object sender, RoutedEventArgs e)
        {
            ((TitleColumnWindowViewModel)this.DataContext).SubmitCommand.Execute(null);
        }

        private void Button_Submit_Click(object sender, RoutedEventArgs e)
        {
            ((TitleColumnWindowViewModel)this.DataContext).SubmitCommand.Execute(null);
            Close();
        }
    }
}
