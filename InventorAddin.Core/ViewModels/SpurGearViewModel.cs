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

        private bool _isWholeDepthEnabled;

        public bool IsWholeDepthEnabled
        {
            get => _isWholeDepthEnabled;
            set => SetProperty(ref _isWholeDepthEnabled, value);
            
        }
        private bool _isPCDEnabled;

        public bool IsPCDEnabled
        {
            get => _isPCDEnabled;
            set => SetProperty(ref _isPCDEnabled, value);
        }
        
        private double _module;

        public double Module
        {
            get => _module;
            set
            {
                SetProperty(ref _module, value);

                if (!IsWholeDepthEnabled)
                {
                    CalcWholeDepth();
                }

                if (!IsPCDEnabled)
                {
                    CalcPCD();
                }
            }
            
        }

        private double _wholeDepth;

        public double WholeDepth
        {
            get => _wholeDepth;
            set => SetProperty(ref _wholeDepth, value);
        }

        private double _pcd;

        public double PCD
        {
            get => _pcd;
            set => SetProperty(ref _pcd, value);
            
            
        }

        private int _numOfTeeth;

        public int NumOfTeeth
        {
            get => _numOfTeeth;
            set
            {
                SetProperty(ref _numOfTeeth, value);

                if (!IsPCDEnabled)
                {
                    CalcPCD();
                }
            }
            
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

            IsPCDEnabled = true;
            IsWholeDepthEnabled = true;
        }

        private void CalcPCD()
        {
            PCD = Module * NumOfTeeth;
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
                var sketchedSymbolDefinition = dDoc.SketchedSymbolDefinitions["스퍼기어 요목표"];

                string[] PromptStrings = new string[4] {NumOfTeeth.ToString(), PCD.ToString(), WholeDepth.ToString(), Module.ToString() };
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