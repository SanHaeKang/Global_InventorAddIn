using InventorAddin.Core.ViewModels;
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
using InvAddIn.Views.Pages;
using InventorAddin.Core.Helpers.ViewModelFactory;

namespace InvAddIn.Views
{
    /// <summary>
    /// GearSummaryWindow.xaml에 대한 상호 작용 논리
    /// </summary>
    public partial class GearSummaryWindow : Window
    {
        private GearSummaryView currentView;
        public GearSummaryWindow()
        {
            InitializeComponent();
        }

        public GearSummaryWindow(GearSummaryView gearSummaryView, GearSummaryViewModel gearSummaryViewModel) : this()
        {
            currentView = gearSummaryView;
            currentView.DataContext = gearSummaryViewModel;
            MainFrame.Navigate(currentView);
        }

        private void CancelButton_OnClick(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void SubmitButton_OnClick(object sender, RoutedEventArgs e)
        {
            ((GearSummaryViewModel)currentView.DataContext).SetSummary();
            Close();
        }
    }
}
