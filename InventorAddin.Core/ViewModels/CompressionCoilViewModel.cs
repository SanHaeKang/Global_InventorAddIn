using Inventor;
using InventorAddin.Core.Helpers;
using InventorAddin.Core.Models;
using System.Reflection;

namespace InventorAddin.Core.ViewModels
{
    public class CompressionCoilViewModel : GearSummaryViewModel
    {
        private string _material;

        public string Material
        {
            get => _material;
            set => SetProperty(ref _material, value);
        }

        private double _materialDiameter;

        public double MaterialDiameter
        {
            get => _materialDiameter;
            set => SetProperty(ref _materialDiameter, value);
        }

        private double _coilMeanDiameter;

        public double CoilMeanDiameter
        {
            get => _coilMeanDiameter;
            set => SetProperty(ref _coilMeanDiameter, value);
        }

        private double _coilOuterDiameter;

        public double CoilOuterDiameter
        {
            get => _coilOuterDiameter;
            set => SetProperty(ref _coilOuterDiameter, value);
        }

        private int _activeCoilCount;

        public int ActiveCoilCount
        {
            get => _activeCoilCount;
            set => SetProperty(ref _activeCoilCount, value);
        }

        private int _totalCoilCount;

        public int TotalCoilCount
        {
            get => _totalCoilCount;
            set => SetProperty(ref _totalCoilCount, value);
        }

        private string _coilingDirection;

        public string CoilingDirection
        {
            get => _coilingDirection;
            set => SetProperty(ref _coilingDirection, value);
        }

        private double _freeHeight;

        public double FreeHeight
        {
            get => _freeHeight;
            set => SetProperty(ref _freeHeight, value);
        }

        private double _installedLoad;

        public double InstalledLoad
        {
            get => _installedLoad;
            set => SetProperty(ref _installedLoad, value);
        }

        private double _installedHeight;

        public double InstalledHeight
        {
            get => _installedHeight;
            set => SetProperty(ref _installedHeight, value);
        }

        private double _maxLoadAtInstalledHeight;

        public double MaxLoadAtInstalledHeight
        {
            get => _maxLoadAtInstalledHeight;
            set => SetProperty(ref _maxLoadAtInstalledHeight, value);
        }

        private double _maxLoadHeight;

        public double MaxLoadHeight
        {
            get => _maxLoadHeight;
            set => SetProperty(ref _maxLoadHeight, value);
        }

        private double _springIndex;

        public double SpringIndex
        {
            get => _springIndex;
            set => SetProperty(ref _springIndex, value);
        }

        private string _surfaceTreatmentAfterForming;

        public string SurfaceTreatmentAfterForming
        {
            get => _surfaceTreatmentAfterForming;
            set => SetProperty(ref _surfaceTreatmentAfterForming, value);
        }

        private string _surfaceTreatmentAfterShotPeening;

        public string SurfaceTreatmentAfterShotPeening
        {
            get => _surfaceTreatmentAfterShotPeening;
            set => SetProperty(ref _surfaceTreatmentAfterShotPeening, value);
        }

        private string _unit;
        public string Unit
        {
            get => _unit;
            set => SetProperty(ref _unit, value);
        }

        private Inventor.Application m_inventorApplication;
        private Inventor.InteractionEvents interactionEvents;

        public CompressionCoilViewModel(Application mInventorApplication)
        {
            m_inventorApplication = mInventorApplication;

            Unit = UnitHelper.UnitToString(m_inventorApplication.ActiveDocument.UnitsOfMeasure.LengthUnits);
        }
        public override void SetSummary()
        {
            interactionEvents = m_inventorApplication.CommandManager.CreateInteractionEvents();
            interactionEvents.OnTerminate += InteractionEventsOnOnTerminate;
            interactionEvents.MouseEvents.OnMouseClick += MouseEventsOnOnMouseClick;
            interactionEvents.Start();
        }

        private void MouseEventsOnOnMouseClick(MouseButtonEnum button, ShiftStateEnum shiftkeys, Point modelposition, Point2d viewposition, View view)
        {
            if (button == MouseButtonEnum.kLeftMouseButton)
            {
                var dDoc = m_inventorApplication.ActiveDocument as DrawingDocument;
                var sheet = dDoc.ActiveSheet;
                var sketchedSymbolDefinition = dDoc.SketchedSymbolDefinitions["압축코일 스프링"];

                string[] PromptStrings = new string[15]
                {
                    MaterialDiameter.ToString(),
                    Material,
                    CoilingDirection,
                    InstalledLoad.ToString(),
                    CoilMeanDiameter.ToString(),
                    FreeHeight.ToString(),
                    InstalledHeight.ToString(),
                    MaxLoadAtInstalledHeight.ToString(),
                    MaxLoadHeight.ToString(),
                    SpringIndex.ToString(),
                    SurfaceTreatmentAfterForming,
                    CoilOuterDiameter.ToString(),
                    TotalCoilCount.ToString(),
                    ActiveCoilCount.ToString(),
                    SurfaceTreatmentAfterShotPeening
                };
                sheet.SketchedSymbols.Add(
                    sketchedSymbolDefinition,
                    m_inventorApplication.TransientGeometry.CreatePoint2d(modelposition.X, modelposition.Y),
                    0, 1, PromptStrings
                );
                interactionEvents.Stop();
            }
        }

        private void InteractionEventsOnOnTerminate()
        {
            interactionEvents.OnTerminate -= InteractionEventsOnOnTerminate;
            interactionEvents.MouseEvents.OnMouseClick -= MouseEventsOnOnMouseClick;
            interactionEvents = null;
        }
    }
}