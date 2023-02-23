using Inventor;

namespace InventorAddin.Core.ViewModels
{
    public class TimingViewModel : GearSummaryViewModel
    {
        private Inventor.Application m_inventorApplication;
        public TimingViewModel(Application mInventorApplication)
        {
            m_inventorApplication = mInventorApplication;
        }

        public override void SetSummary()
        {
            throw new System.NotImplementedException();
        }
    }
}