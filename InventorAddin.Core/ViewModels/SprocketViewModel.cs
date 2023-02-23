using Inventor;

namespace InventorAddin.Core.ViewModels
{
    public class SprocketViewModel : GearSummaryViewModel
    {
        private Inventor.Application m_inventorApplication;
        public SprocketViewModel(Application mInventorApplication)
        {
            m_inventorApplication = mInventorApplication;
        }

        public override void SetSummary()
        {
            throw new System.NotImplementedException();
        }
    }
}