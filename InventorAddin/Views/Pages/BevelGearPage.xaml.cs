﻿using InventorAddin.Core.Helpers;
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
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace InvAddIn.Views.Pages
{
    /// <summary>
    /// HelicalPage.xaml에 대한 상호 작용 논리
    /// </summary>
    public partial class BevelGearPage : GearSummaryView
    {
        public BevelGearPage()
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
