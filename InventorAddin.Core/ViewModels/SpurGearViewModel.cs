using System;
using System.Windows;
using Inventor;
using InventorAddin.Core.Helpers;

namespace InventorAddin.Core.ViewModels
{
    public class SpurGearViewModel : GearSummaryViewModel
    {
        private Inventor.Application m_inventorApplication;
        private Inventor.InteractionEvents interactionEvents;

        private string _module;

        public string Module
        {
            get => _module;
            set => SetProperty(ref _module, value);
        }

        private string _wholeDepth;

        public string WholeDepth
        {
            get => _wholeDepth;
            set => SetProperty(ref _wholeDepth, value);
        }

        private string _pcd;

        public string PCD
        {
            get => _pcd;
            set => SetProperty(ref _pcd, value);
        }

        private string _numOfTeeth;

        public string NumOfTeeth
        {
            get => _numOfTeeth;
            set => SetProperty(ref _numOfTeeth, value);
        }

        private string _unit;
        public string Unit
        {
            get => _unit;
            set => SetProperty(ref _unit, value);
        }

        public SpurGearViewModel(Inventor.Application _m_inventorApplication)
        {
            m_inventorApplication = _m_inventorApplication;

            Unit = UnitHelper.UnitToString(m_inventorApplication.ActiveDocument.UnitsOfMeasure.LengthUnits);

            NumOfTeeth = string.Empty;
            PCD = string.Empty;
            WholeDepth = string.Empty;
            Module = string.Empty;
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
                var sketchedSymbolDefinition = dDoc.SketchedSymbolDefinitions["스퍼기어 요목표"];

                string[] PromptStrings = new string[4] {NumOfTeeth, PCD, WholeDepth, Module};
                var sketchedSymbol = sheet.SketchedSymbols.Add(
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