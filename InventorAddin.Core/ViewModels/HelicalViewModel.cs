using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Inventor;

namespace InventorAddin.Core.ViewModels
{
    public class HelicalViewModel : GearSummaryViewModel
    {
        private Inventor.Application m_inventorApplication;
        public HelicalViewModel(Application mInventorApplication)
        {
            m_inventorApplication = mInventorApplication;
        }

        public override void SetSummary()
        {
            throw new NotImplementedException();
        }
    }
}
