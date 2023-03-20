using System;
using Inventor;
using InventorAddin.Core.Helpers;
using System.Reflection;

namespace InventorAddin.Core.ViewModels
{
    public class RepinionViewModel : GearSummaryViewModel
    {
        private bool _isEnabledRackLength;
        public bool IsEnabledRackLength
        {
            get => _isEnabledRackLength;
            set => SetProperty(ref _isEnabledRackLength, value);
        }
        private bool _isEnabledWholeDepth;
        public bool IsEnabledWholeDepth
        {
            get => _isEnabledWholeDepth;
            set => SetProperty(ref _isEnabledWholeDepth, value);
        }

        private double _module;

        public double Module
        {
            get => _module;
            set
            {
                SetProperty(ref _module, value);
                if (!IsEnabledRackLength)
                {
                    CalcRackLength();
                }

                if (!IsEnabledWholeDepth)
                {
                    CalcWholeDepth();
                }
            }
        }

        private double _rackLength;
        public double RackLength
        {
            get => _rackLength;
            set => SetProperty(ref _rackLength, value);
        }

        private int _numberOfTeeth;

        public int NumberOfTeeth
        {
            get => _numberOfTeeth;
            set
            {
                SetProperty(ref _numberOfTeeth, value);
                if (!IsEnabledRackLength)
                {
                    CalcRackLength();
                }
            }
        }

        private double _wholeDepth;

        public double WholeDepth
        {
            get => _wholeDepth;
            set => SetProperty(ref _wholeDepth, value);
        }
        private string _unit;
        public string Unit
        {
            get => _unit;
            set => SetProperty(ref _unit, value);
        }

        private Inventor.Application m_inventorApplication;
        private Inventor.InteractionEvents interactionEvents;

        public RepinionViewModel(Application mInventorApplication)
        {
            m_inventorApplication = mInventorApplication;

            Unit = UnitHelper.UnitToString(m_inventorApplication.ActiveDocument.UnitsOfMeasure.LengthUnits);

            IsEnabledWholeDepth = true;
            IsEnabledRackLength = true;
        }

        private void CalcRackLength()
        {
            RackLength = Module * NumberOfTeeth * Math.PI;
        }

        private void CalcWholeDepth()
        {
            WholeDepth = Module * 2.25;
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
                var sketchedSymbolDefinition = dDoc.SketchedSymbolDefinitions["랙&피니언"];

                string[] PromptStrings = new string[4]
                {
                    Module.ToString(),
                    RackLength.ToString(),
                    NumberOfTeeth.ToString(),
                    WholeDepth.ToString()
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