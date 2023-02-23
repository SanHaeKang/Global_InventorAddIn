using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventorAddin.Core.ViewModels
{
    public class AddRevWindowViewModel : ObservableObject
    {
        private string explanation;

        public string Explanation
        {
            get => explanation;
            set => SetProperty(ref explanation, value);
        }

        private DateTime date;

        public DateTime Date
        {
            get => date;
            set => SetProperty(ref date, value);
        }

        private string approval;

        public string Approval
        {
            get => approval;
            set => SetProperty(ref approval, value);
        }

        public AddRevWindowViewModel()
        {
            Date = DateTime.Now;
        }
    }
}
