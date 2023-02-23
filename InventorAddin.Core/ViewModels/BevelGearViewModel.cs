using Inventor;

namespace InventorAddin.Core.ViewModels
{
    public class BevelGearViewModel : GearSummaryViewModel
    {
        private Inventor.Application m_inventorApplication;
        public BevelGearViewModel(Application mInventorApplication)
        {
            m_inventorApplication = mInventorApplication;
        }

        public override void SetSummary()
        {
            throw new System.NotImplementedException();
        }
    }
}