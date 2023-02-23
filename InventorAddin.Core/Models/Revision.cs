using CommunityToolkit.Mvvm.ComponentModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventorAddin.Core.Models
{
    public class Revision : ObservableObject
    {
        private int rev;
        public int Rev
        {
            get => rev;
            set => SetProperty(ref rev, value);
        }
        public string Explanation { get; set; }
        public DateTime Date { get; set; }
        public string Approval { get; set; }
    }
}
