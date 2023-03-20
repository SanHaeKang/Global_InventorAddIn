using System;
using Inventor;
using InventorAddin.Core.Helpers;
using System.Reflection;
using InventorAddin.Core.Converters;

namespace InventorAddin.Core.ViewModels
{
    public class SprocketViewModel : GearSummaryViewModel
    {
        private bool _isEnabledSprocketPeachTelescope;

        public bool IsEnabledSprocketPeachTelescope
        {
            get => _isEnabledSprocketPeachTelescope;
            set => SetProperty(ref _isEnabledSprocketPeachTelescope, value);
        }

        private double _sprocketPeachTelescope;

        public double SprocketPeachTelescope
        {
            get => _sprocketPeachTelescope; 
            set => SetProperty(ref _sprocketPeachTelescope, value);
        }

        private string _sprocketToothProfile;

        public string SprocketToothProfile
        {
            get => _sprocketToothProfile; 
            set => SetProperty(ref _sprocketToothProfile, value);
        }

        private int _sprocketNumOfTeeth;

        public int SprocketNumOfTeeth
        {
            get => _sprocketNumOfTeeth;
            set
            {
                SetProperty(ref _sprocketNumOfTeeth, value);
                if (!IsEnabledSprocketPeachTelescope)
                {
                    CalcSprocketPeachTelescope();
                }
            }
        }

        private double _chainRollerOuterDiameter;

        public double ChainRollerOuterDiameter
        {
            get => _chainRollerOuterDiameter; 
            set => SetProperty(ref _chainRollerOuterDiameter, value);
        }

        private double _chainCircumferencePitch;

        public double ChainCircumferencePitch
        {
            get => _chainCircumferencePitch;
            set
            {
                SetProperty(ref _chainCircumferencePitch, value);
                if (!IsEnabledSprocketPeachTelescope)
                {
                    CalcSprocketPeachTelescope();
                }
            }
        }

        private string _chainTitle;

        public string ChainTitle
        {
            get => _chainTitle;
            set => SetProperty(ref _chainTitle, value);
        }

        private string _unit;
        public string Unit
        {
            get => _unit;
            set => SetProperty(ref _unit, value);
        }

        private Inventor.Application m_inventorApplication;
        private Inventor.InteractionEvents interactionEvents;
        public SprocketViewModel(Application mInventorApplication)
        {
            m_inventorApplication = mInventorApplication;

            Unit = UnitHelper.UnitToString(m_inventorApplication.ActiveDocument.UnitsOfMeasure.LengthUnits);

            IsEnabledSprocketPeachTelescope = false;
        }

        private void CalcSprocketPeachTelescope()
        {
            SprocketPeachTelescope = ChainCircumferencePitch *
                                     (1 / Math.Sin(
                                         DegreeToRadianConverter.DegreeToRadianConvert(180 / SprocketNumOfTeeth)));
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
                var sketchedSymbolDefinition = dDoc.SketchedSymbolDefinitions["체인 스프로킷"];

                string[] PromptStrings = new string[6]
                {
                    ChainTitle, 
                    ChainCircumferencePitch.ToString(), 
                    ChainRollerOuterDiameter.ToString(), 
                    SprocketNumOfTeeth.ToString(),
                    SprocketToothProfile,
                    SprocketPeachTelescope.ToString()
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