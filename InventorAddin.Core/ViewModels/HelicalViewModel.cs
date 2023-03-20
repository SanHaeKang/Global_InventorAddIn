using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Inventor;
using InventorAddin.Core.Converters;
using InventorAddin.Core.Helpers;

namespace InventorAddin.Core.ViewModels
{
    public class HelicalViewModel : GearSummaryViewModel
    {
        #region 프로퍼티
        
        private bool _isEnabledPCD;
        public bool IsEnabledPCD
        {
            get => _isEnabledPCD;
            set => SetProperty(ref _isEnabledPCD, value); 
        }

        private double _pcd;

        public double PCD
        {
            get => _pcd;
            set => SetProperty(ref _pcd, value);
        }
        private double _helixAngle;

        public double HelixAngle
        {
            get => _helixAngle;
            set
            {
                SetProperty(ref _helixAngle, value);

                if (!IsEnabledPCD)
                {
                    CalcPCD();
                }
            }
        }

        private double _numberOfTeeth;

        public double NumberOfTeeth
        {
            get => _numberOfTeeth;
            set
            {
                SetProperty(ref _numberOfTeeth, value);

                if (!IsEnabledPCD)
                {
                    CalcPCD();
                }
            }
        }

        private double _module;

        public double Module
        {
            get => _module;
            set
            {
                SetProperty(ref _module, value);

                if (!IsEnabledPCD)
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
        #endregion

        private Inventor.Application m_inventorApplication;
        private Inventor.InteractionEvents interactionEvents;

        public HelicalViewModel(Application mInventorApplication)
        {
            m_inventorApplication = mInventorApplication;

            Unit = UnitHelper.UnitToString(m_inventorApplication.ActiveDocument.UnitsOfMeasure.LengthUnits);

            IsEnabledPCD = true;
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
                var sketchedSymbolDefinition = dDoc.SketchedSymbolDefinitions["헬리컬기어 요목표"];

                string[] PromptStrings = new string[4] { 
                    NumberOfTeeth.ToString(), 
                    HelixAngle.ToString(), 
                    Module.ToString(), 
                    PCD.ToString()
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
        
        private void CalcPCD()
        {
            PCD = (Module * NumberOfTeeth) / Math.Cos(DegreeToRadianConverter.DegreeToRadianConvert(HelixAngle));
        }
    }
}
