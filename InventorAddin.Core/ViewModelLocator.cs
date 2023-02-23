using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CommunityToolkit.Mvvm.DependencyInjection;
using InventorAddin.Core.ViewModels;
using Microsoft.Extensions.DependencyInjection;

namespace InventorAddin.Core
{
    public class ViewModelLocator
    {
        private static TitleColumnWindowViewModel titleColumnWindowViewModel;

        public static TitleColumnWindowViewModel TitleColumnWindowVM(Inventor.Application _m_inventorApplication)
        {
            if (titleColumnWindowViewModel == null)
            {
                titleColumnWindowViewModel = new TitleColumnWindowViewModel(_m_inventorApplication);
            }
            return titleColumnWindowViewModel;
        }

        private static AddRevWindowViewModel addRevWindowViewModel;

        public static AddRevWindowViewModel AddRevWindowVM
        {
            get
            {
                if (addRevWindowViewModel == null)
                {
                    addRevWindowViewModel = new AddRevWindowViewModel();
                }
                return addRevWindowViewModel;
            }
        }
        
    }
}
