using System;
using Inventor;
using InventorAddin.Core.Converters;
using InventorAddin.Core.Helpers;

namespace InventorAddin.Core.ViewModels
{
    public class BevelGearViewModel : GearSummaryViewModel
    {
        #region 프로퍼티

        private bool _isCheckedConDistance;

        public bool IsCheckedConDistance
        {
            get => _isCheckedConDistance;
            set
            {
                
                SetProperty(ref _isCheckedConDistance, value);
                if (IsCheckedConDistance)
                {
                    EnableControls();
                    IsEnabledConDistance = false;
                }
            }
        }

        private bool _isCheckedWholeDepth;

        public bool IsCheckedWholeDepth
        {
            get => _isCheckedWholeDepth;
            set
            {
                SetProperty(ref _isCheckedWholeDepth, value);
                if (IsCheckedWholeDepth)
                {
                    EnableControls();
                    IsEnabledWholeDepth = false;
                }
            }
        }

        private bool _isCheckedPCD;

        public bool IsCheckedPCD
        {
            get => _isCheckedPCD;
            set
            {
                SetProperty(ref _isCheckedPCD, value);
                if (IsCheckedPCD)
                {
                    EnableControls();
                    IsEnabledPCDMaster = false;
                    IsEnabledPCDPinion = false;
                }
            }
        }

        private bool _isCheckedCustom;

        public bool IsCheckedCustom 
        {
            get => _isCheckedCustom;
            set
            {
                SetProperty(ref _isCheckedCustom, value);

                if(IsCheckedCustom)
                    EnableControls();
            }
        }

        private bool _isEnabledConDistance;

        public bool IsEnabledConDistance
        {
            get => _isEnabledConDistance;
            set => SetProperty(ref _isEnabledConDistance, value);
        }

        private bool _isEnabledWholeDepth;

        public bool IsEnabledWholeDepth
        {
            get => _isEnabledWholeDepth;
            set => SetProperty(ref _isEnabledWholeDepth, value);
        }

        private bool _isEnabledPCDMaster;

        public bool IsEnabledPCDMaster
        {
            get => _isEnabledPCDMaster;
            set => SetProperty(ref _isEnabledPCDMaster, value);
        }

        private bool _isEnabledPCDPinion;

        public bool IsEnabledPCDPinion
        {
            get => _isEnabledPCDPinion;
            set => SetProperty(ref _isEnabledPCDPinion, value);
        }

        private bool _isEnabledDedendum;

        public bool IsEnabledDedendum
        {
            get => _isEnabledDedendum;
            set => SetProperty(ref _isEnabledDedendum, value);
        }

        private bool _isEnabledAddendum;

        public bool IsEnabledAddendum
        {
            get => _isEnabledAddendum;
            set => SetProperty(ref _isEnabledAddendum, value);
        }

        private double _conDistance;

        public double ConDistance
        {
            get => _conDistance;
            set => SetProperty(ref _conDistance, value);
        }

        private double _dedendum;

        public double Dedendum
        {
            get => _dedendum;
            set
            {
                SetProperty(ref _dedendum, value);
                if (IsCheckedWholeDepth)
                    CalcWholeDepth();
            }
        }
        
        private double _addendum;

        public double Addendum
        {
            get => _addendum;
            set
            {
                SetProperty(ref _addendum, value);
                if (IsCheckedWholeDepth)
                    CalcWholeDepth();
            }
        }

        private double _wholeDepth;
        public double WholeDepth
        {
            get => _wholeDepth;
            set => SetProperty(ref _wholeDepth, value);
        }

        private double _pcdMaster;
        public double PCDMaster
        {
            get => _pcdMaster;
            set
            {
                SetProperty(ref _pcdMaster, value);
                if (IsCheckedConDistance)
                    CalcConDistance();
            }
        }

        private double _pcdPinion;
        public double PCDPinion
        {
            get => _pcdPinion;
            set => SetProperty(ref _pcdPinion, value);
        }

        private double _shaftAngle;
        public double ShaftAngle
        {
            get => _shaftAngle;
            set => SetProperty(ref _shaftAngle, value);
        }

        private int _numOfTeethPinion;
        public int NumOfTeethPinion
        {
            get => _numOfTeethPinion;
            set
            {
                SetProperty(ref _numOfTeethPinion, value);
                if (IsCheckedPCD)
                    CalcPCD();
            }
        }

        private int _numOfTeethMaster;
        public int NumOfTeethMaster
        {
            get => _numOfTeethMaster;
            set
            {
                SetProperty(ref _numOfTeethMaster, value);
                if (IsCheckedPCD)
                    CalcPCD();
            }
        }

        private double _angleOfPressure;
        public double AngleOfPressure
        {
            get => _angleOfPressure; 
            set => SetProperty(ref _angleOfPressure, value);
        }

        private double _module;
        public double Module
        {
            get => _module;
            set
            {
                SetProperty(ref _module, value);

                if (IsCheckedPCD)
                    CalcPCD();
                if (!IsEnabledAddendum)
                    CalcAddendum();
                if (!IsEnabledDedendum)
                    CalcDedendum();
            }
        }
        
        private string _gearProfile;
        public string GearProfile
        {
            get => _gearProfile;
            set => SetProperty(ref _gearProfile, value);
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

        private void EnableControls()
        {
            IsEnabledPCDMaster = true;
            IsEnabledPCDPinion = true;
            IsEnabledWholeDepth = true;
            IsEnabledConDistance = true;
        }

        
        public BevelGearViewModel(Application mInventorApplication)
        {
            m_inventorApplication = mInventorApplication;

            Unit = UnitHelper.UnitToString(m_inventorApplication.ActiveDocument.UnitsOfMeasure.LengthUnits);

            IsCheckedCustom = true;
            IsEnabledAddendum = true;
            IsEnabledDedendum = true;
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
                var sketchedSymbolDefinition = dDoc.SketchedSymbolDefinitions["바벨기어"];

                string[] PromptStrings = new string[12]
                {
                    GearProfile,
                    AngleOfPressure.ToString(),
                    NumOfTeethMaster.ToString(),
                    PCDMaster.ToString(),
                    WholeDepth.ToString(),
                    Module.ToString(),
                    NumOfTeethPinion.ToString(),
                    ShaftAngle.ToString(),
                    PCDPinion.ToString(),
                    Addendum.ToString(),
                    Dedendum.ToString(),
                    ConDistance.ToString()
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
            PCDMaster = Module * NumOfTeethMaster;
            PCDPinion = Module * NumOfTeethPinion;
        }
        private void CalcWholeDepth()
        {
            WholeDepth = Addendum + Dedendum;
        }
        private void CalcConDistance()
        {
            ConDistance = PCDMaster / (2 * Math.Sin(90 - DegreeToRadianConverter.DegreeToRadianConvert(PCDMaster)));
        }
        private void CalcDedendum()
        {
            Dedendum = Module * 1.25;
        }

        private void CalcAddendum()
        {
            Addendum = Module * 1.0;
        }
    }
}