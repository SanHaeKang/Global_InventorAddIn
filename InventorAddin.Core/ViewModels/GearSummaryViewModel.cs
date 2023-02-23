using CommunityToolkit.Mvvm.ComponentModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using CommunityToolkit.Mvvm.Input;
using Inventor;

namespace InventorAddin.Core.ViewModels
{
    public abstract class GearSummaryViewModel : ObservableObject
    {
        public abstract void SetSummary();
    }
}
