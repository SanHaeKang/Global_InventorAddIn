using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using InvAddIn.Views;
using InvAddIn.Views.Pages;
using InventorAddin.Core.Helpers.ViewModelFactory;
using InventorAddin.Core.ViewModels;

namespace InvAddIn.Helpers.ViewFactorys
{
    public abstract class GearSummaryViewFactory
    {
        public abstract GearSummaryView CreateView();
    }

    public class CompressionCoilViewFactory : GearSummaryViewFactory
    {
        public override GearSummaryView CreateView()
        {
            return new CompressionCoilPage();
        }
    }

    public class TensionCoilViewFactory : GearSummaryViewFactory
    {
        public override GearSummaryView CreateView()
        {
            return new TensionCoilPage();
        }
    }

    public class SpurGearViewFactory : GearSummaryViewFactory
    {
        public override GearSummaryView CreateView()
        {
            return new SpurGearPage();
        }
    }

    public class HelicalViewFactory : GearSummaryViewFactory
    {
        public override GearSummaryView CreateView()
        {
            return new HelicalPage();
        }
    }

    public class TimingViewFactory : GearSummaryViewFactory
    {
        public override GearSummaryView CreateView()
        {
            return new TimingPage();
        }
    }

    public class BevelGearViewFactory : GearSummaryViewFactory
    {
        public override GearSummaryView CreateView()
        {
            return new BevelGearPage();
        }
    }

    public class RepinionViewFactory : GearSummaryViewFactory
    {
        public override GearSummaryView CreateView()
        {
            return new RepinionPage();
        }
    }

    public class WormGearViewFactory : GearSummaryViewFactory
    {
        public override GearSummaryView CreateView()
        {
            return new WormGearPage();
        }
    }

    public class SprocketViewFactory : GearSummaryViewFactory
    {
        public override GearSummaryView CreateView()
        {
            return new SprocketPage();
        }
    }
}
