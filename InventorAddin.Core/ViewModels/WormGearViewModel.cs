using Inventor;

namespace InventorAddin.Core.ViewModels
{
    public class WormGearViewModel : GearSummaryViewModel
    {
        private Inventor.Application m_inventorApplication;
        public WormGearViewModel(Application mInventorApplication)
        {
            m_inventorApplication = mInventorApplication;
        }

        public override void SetSummary()
        {
            throw new System.NotImplementedException();
        }
    }
}