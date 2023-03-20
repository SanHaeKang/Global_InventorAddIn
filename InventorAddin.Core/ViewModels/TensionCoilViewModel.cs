using Inventor;
using InventorAddin.Core.Helpers;
using System.Reflection;

namespace InventorAddin.Core.ViewModels
{
    public class TensionCoilViewModel : GearSummaryViewModel
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

        private double _freeLength;

        public double FreeLength
        {
            get => _freeLength;
            set => SetProperty(ref _freeLength, value);
        }

        private double _initialTension;

        public double InitialTension
        {
            get => _initialTension;
            set => SetProperty(ref _initialTension, value);
        }

        private double _definedLength;

        public double DefinedLength
        {
            get => _definedLength;
            set => SetProperty(ref _definedLength, value);
        }

        private double _definedLengthLoad;

        public double DefinedLengthLoad
        {
            get => _definedLengthLoad;
            set => SetProperty(ref _definedLengthLoad, value);
        }

        private double _springConstant;

        public double SpringConstant
        {
            get => _springConstant;
            set => SetProperty(ref _springConstant, value);
        }

        private double _maximumAllowedExtensionLength;

        public double MaximumAllowedExtensionLength
        {
            get => _maximumAllowedExtensionLength;
            set => SetProperty(ref _maximumAllowedExtensionLength, value);
        }

        private string _loopShape;

        public string LoopShape
        {
            get => _loopShape;
            set => SetProperty(ref _loopShape, value);
        }

        private string _surfaceTreatment;

        public string SurfaceTreatment
        {
            get => _surfaceTreatment;
            set => SetProperty(ref _surfaceTreatment, value);
        }

        private string _unit;
        public string Unit
        {
            get => _unit;
            set => SetProperty(ref _unit, value);
        }

        private Application m_inventorApplication;
        private Inventor.InteractionEvents interactionEvents;

        public TensionCoilViewModel(Application mInventorApplication)
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
                var sketchedSymbolDefinition = dDoc.SketchedSymbolDefinitions["인장코일 스프링"];

                string[] PromptStrings = new string[14]
                {
                    MaterialDiameter.ToString(),
                    Material,
                    CoilingDirection,
                    InitialTension.ToString(),
                    DefinedLength.ToString(),
                    CoilMeanDiameter.ToString(),
                    FreeLength.ToString(),
                    DefinedLengthLoad.ToString(),
                    SpringConstant.ToString(),
                    MaximumAllowedExtensionLength.ToString(),
                    LoopShape,
                    SurfaceTreatment,
                    CoilOuterDiameter.ToString(),
                    TotalCoilCount.ToString()
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