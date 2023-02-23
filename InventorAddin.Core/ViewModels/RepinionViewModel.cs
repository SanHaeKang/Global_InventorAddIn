using Inventor;

namespace InventorAddin.Core.ViewModels
{
    public class RepinionViewModel : GearSummaryViewModel
    {
        private Inventor.Application m_inventorApplication;
        public RepinionViewModel(Application mInventorApplication)
        {
            m_inventorApplication = mInventorApplication;
        }

        public override void SetSummary()
        {
            throw new System.NotImplementedException();
        }
    }
}