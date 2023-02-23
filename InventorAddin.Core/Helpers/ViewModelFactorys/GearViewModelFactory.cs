using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using InventorAddin.Core.ViewModels;

namespace InventorAddin.Core.Helpers.ViewModelFactory
{
    public abstract class GearSummaryViewModelFactory
    {
        public abstract GearSummaryViewModel CreateViewModel(Inventor.Application _m_inventorApplication);
    }

    public class SpurGearViewModelFactory : GearSummaryViewModelFactory
    {
        public override GearSummaryViewModel CreateViewModel(Inventor.Application _m_inventorApplication)
        {
            return new SpurGearViewModel(_m_inventorApplication);
        }
    }

    public class HelicalViewModelFactory : GearSummaryViewModelFactory
    {
        public override GearSummaryViewModel CreateViewModel(Inventor.Application _m_inventorApplication)
        {
            return new HelicalViewModel(_m_inventorApplication);
        }
    }

    public class TimingViewModelFactory : GearSummaryViewModelFactory
    {
        public override GearSummaryViewModel CreateViewModel(Inventor.Application _m_inventorApplication)
        {
            return new TimingViewModel(_m_inventorApplication);
        }
    }

    public class BevelGearViewModelFactory : GearSummaryViewModelFactory
    {
        public override GearSummaryViewModel CreateViewModel(Inventor.Application _m_inventorApplication)
        {
            return new BevelGearViewModel(_m_inventorApplication);
        }
    }

    public class RepinionViewModelFactory : GearSummaryViewModelFactory
    {
        public override GearSummaryViewModel CreateViewModel(Inventor.Application _m_inventorApplication)
        {
            return new RepinionViewModel(_m_inventorApplication);
        }
    }

    public class WormGearViewModelFactory : GearSummaryViewModelFactory
    {
        public override GearSummaryViewModel CreateViewModel(Inventor.Application _m_inventorApplication)
        {
            return new WormGearViewModel(_m_inventorApplication);
        }
    }

    public class SprocketViewModelFactory : GearSummaryViewModelFactory
    {
        public override GearSummaryViewModel CreateViewModel(Inventor.Application _m_inventorApplication)
        {
            return new SprocketViewModel(_m_inventorApplication);
        }
    }

    public enum GearType
    {
        SpurGear,
        Helical,
        Timing,
        BevelGear,
        Repinion,
        WormGear,
        Sprocket
    }
}
