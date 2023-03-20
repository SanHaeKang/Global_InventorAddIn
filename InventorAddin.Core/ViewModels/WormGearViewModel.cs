using System;
using Inventor;
using InventorAddin.Core.Converters;
using InventorAddin.Core.Helpers;

namespace InventorAddin.Core.ViewModels
{
    public class WormGearViewModel : GearSummaryViewModel
    {
        private bool _isEnabledLeadAngle;

        public bool IsEnabledLeadAngle
        {
            get => _isEnabledLeadAngle;
            set => SetProperty(ref _isEnabledLeadAngle, value);
        }

        private double _pcd;

        public double PCD
        {
            get => _pcd;
            set
            {
                SetProperty(ref _pcd, value);
                if (!IsEnabledLeadAngle)
                {
                    CalcLeadAngle();
                }
            }
        }
        private double _diametralPitch;

        public double DiametralPitch
        {
            get => _diametralPitch;
            set => SetProperty(ref _diametralPitch, value);
        }
        private double _leadAngle;

        public double LeadAngle
        {
            get => _leadAngle;
            set => SetProperty(ref _leadAngle, value);
        }

        private string _twistDirection;

        public string TwistDirection
        {
            get => _twistDirection;
            set => SetProperty(ref _twistDirection, value);
        }

        private int _numberOfLine;

        public int NumberOfLine
        {
            get => _numberOfLine;
            set
            {
                SetProperty(ref _numberOfLine, value);
                if (!IsEnabledLeadAngle)
                {
                    CalcLeadAngle();
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

                if (!IsEnabledLeadAngle)
                {
                    CalcLeadAngle();
                }
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

        private Inventor.Application m_inventorApplication;
        private Inventor.InteractionEvents interactionEvents;

        public WormGearViewModel(Application mInventorApplication)
        {
            m_inventorApplication = mInventorApplication;

            Unit = UnitHelper.UnitToString(m_inventorApplication.ActiveDocument.UnitsOfMeasure.LengthUnits);

            IsEnabledLeadAngle = true;
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
                var sketchedSymbolDefinition = dDoc.SketchedSymbolDefinitions["웜기어"];

                string[] PromptStrings = new string[7]
                {
                    GearProfile, 
                    Module.ToString(),
                    NumberOfLine.ToString(),
                    TwistDirection,
                    PCD.ToString(),
                    DiametralPitch.ToString(),
                    LeadAngle.ToString()
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

        private void CalcLeadAngle()
        {
            LeadAngle = Math.Atan(DegreeToRadianConverter.DegreeToRadianConvert(Module * NumberOfLine / PCD));
        }
    }
}