using System;
using Inventor;
using InventorAddin.Core.Helpers;
using System.Reflection;
using InventorAddin.Core.Converters;

namespace InventorAddin.Core.ViewModels
{
    public class TimingViewModel : GearSummaryViewModel
    {
        private bool _isEnabledBeltWidth;

        public bool IsEnabledBeltWidth
        {
            get => _isEnabledBeltWidth;
            set => SetProperty(ref _isEnabledBeltWidth, value);
        }
        private bool _isEnabledPCD;

        public bool IsEnabledPCD
        {
            get => _isEnabledPCD;
            set => SetProperty(ref _isEnabledPCD, value);
        }

        private double _faceSummary;
        public double FaceSummary
        {
            get => _faceSummary;
            set
            {
                SetProperty(ref _faceSummary, value);
                if (!IsEnabledPCD)
                {
                    CalcPCD();
                }
                if (!IsEnabledBeltWidth)
                {
                    CalcBeltWidth();
                }
            }
        }
        private int _numberOfTeeth;
        public int NumberOfTeeth
        {
            get => _numberOfTeeth;
            set
            {
                SetProperty(ref _numberOfTeeth, value);
                if (!IsEnabledPCD)
                {
                    CalcPCD();
                }
                if (!IsEnabledBeltWidth)
                {
                    CalcBeltWidth();
                }
            }
        }
        private double _pcd;
        public double PCD
        {
            get => _pcd;
            set => SetProperty(ref _pcd, value);
        }
        private double _od;
        public double OD
        {
            get => _od;
            set => SetProperty(ref _od, value);
        }
        private double _beltWidth;
        public double BeltWidth
        {
            get => _beltWidth;
            set => SetProperty(ref _beltWidth, value);
        }
        private string _unit;
        public string Unit
        {
            get => _unit;
            set => SetProperty(ref _unit, value);
        }

        private Inventor.Application m_inventorApplication;
        private Inventor.InteractionEvents interactionEvents;
        public TimingViewModel(Application mInventorApplication)
        {
            m_inventorApplication = mInventorApplication;

            Unit = UnitHelper.UnitToString(m_inventorApplication.ActiveDocument.UnitsOfMeasure.LengthUnits);

            IsEnabledPCD = true;
            IsEnabledBeltWidth = true;
        }

        private void CalcPCD()
        {
            PCD = (NumberOfTeeth * FaceSummary) / Math.PI;
        }

        private void CalcBeltWidth()
        {
            BeltWidth = NumberOfTeeth * FaceSummary * Math.Sin(DegreeToRadianConverter.DegreeToRadianConvert(180 / NumberOfTeeth)) / 2;
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
                var sketchedSymbolDefinition = dDoc.SketchedSymbolDefinitions["타이밍 풀리"];

                string[] PromptStrings = new string[5]
                {
                    FaceSummary.ToString(),
                    NumberOfTeeth.ToString(),
                    PCD.ToString(),
                    OD.ToString(),
                    BeltWidth.ToString()
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