using Inventor;
using System;
using System.Runtime.InteropServices;
using InvAddIn.Converters;
using InvAddIn.Properties;
using InventorAddin.Views;
using System.Drawing;
using System.Windows;
using InvAddIn.Helpers.ViewFactorys;
using InvAddIn.Views;
using InventorAddin.Core.Helpers.ViewModelFactory;
using InventorAddin.Core.ViewModels;

namespace InventorAddin
{
    /// <summary>
    /// This is the primary AddIn Server class that implements the ApplicationAddInServer interface
    /// that all Inventor AddIns are required to implement. The communication between Inventor and
    /// the AddIn is via the methods on this interface.
    /// </summary>
    [GuidAttribute("b2e5a201-9d46-4e2d-8d8a-b16748936219")]
    public class StandardAddInServer : Inventor.ApplicationAddInServer
    {
        private GuidAttribute _guidAttribute;
        public string AddInClientID
        {
            get
            {
                return "{" + _guidAttribute.Value + "}";
            }
        }

        // Inventor application object.
        public static Inventor.Application m_inventorApplication;
        
        private RibbonTab globalsoftRibbonTab;

        private Ribbon drawingRibbon;

        private RibbonPanel titleColumnRibbonPanel;
        private RibbonPanel gearSummaryRibbonPanel;

        private ButtonDefinition titleColumnWindowButtonDefinition;
        private ButtonDefinition spurGearWindowButtonDefinition;
        private ButtonDefinition helicalWindowButtonDefinition;
        private ButtonDefinition timingWindowButtonDefinition;
        private ButtonDefinition bevelGearWindowButtonDefinition;
        private ButtonDefinition repinionWindowButtonDefinition;
        private ButtonDefinition wormGearWindowButtonDefinition;
        private ButtonDefinition sprocketWindowButtonDefinition;

        public StandardAddInServer()
        {
            _guidAttribute = ((GuidAttribute)GuidAttribute.GetCustomAttribute(typeof(StandardAddInServer), typeof(GuidAttribute)));
        }

        #region ApplicationAddInServer Members

        public void Activate(Inventor.ApplicationAddInSite addInSiteObject, bool firstTime)
        {
            // This method is called by Inventor when it loads the addin.
            // The AddInSiteObject provides access to the Inventor Application object.
            // The FirstTime flag indicates if the addin is loaded for the first time.

            // STEP 0 :: Initialize AddIn members.
            m_inventorApplication = addInSiteObject.Application;

            // STEP 1 :: "Globalsoft" 이름으로 리본 탭 생성
            drawingRibbon = m_inventorApplication.UserInterfaceManager.Ribbons["Drawing"];
            globalsoftRibbonTab = drawingRibbon.RibbonTabs.Add("Globalsoft", "Globalsoft", AddInClientID);
            
            // STEP 2 :: RibbonPanel 생성
            titleColumnRibbonPanel = globalsoftRibbonTab.RibbonPanels.Add("표제란", "표제란", AddInClientID);
            gearSummaryRibbonPanel = globalsoftRibbonTab.RibbonPanels.Add("기어 요목표", "기어 요목표", AddInClientID);

            // STEP 3 :: 이름과 아이콘을 가진 버튼 생성
            titleColumnWindowButtonDefinition =
                m_inventorApplication.CommandManager.ControlDefinitions.AddButtonDefinition("표제란", "표제란",
                    CommandTypesEnum.kNonShapeEditCmdType, AddInClientID, "DescriptionText_표제란", "ToolTipText_표제란",
                    PictureDispConverter.Convert(Resources.icon_chart1.ToBitmap()), 
                    PictureDispConverter.Convert(Resources.icon_chart1.ToBitmap()));
            spurGearWindowButtonDefinition =
                m_inventorApplication.CommandManager.ControlDefinitions.AddButtonDefinition("스퍼기어", "스퍼기어",
                    CommandTypesEnum.kNonShapeEditCmdType, AddInClientID, "DescriptionText_스퍼 기어", "ToolTipText_스퍼 기어",
                    PictureDispConverter.Convert(Resources.icon_new_01.ToBitmap()),
                    PictureDispConverter.Convert(Resources.icon_new_01.ToBitmap()));
            helicalWindowButtonDefinition =
                m_inventorApplication.CommandManager.ControlDefinitions.AddButtonDefinition("헬리컬", "헬리컬",
                    CommandTypesEnum.kNonShapeEditCmdType, AddInClientID, "DescriptionText_헬리컬", "ToolTipText_헬리컬",
                    PictureDispConverter.Convert(Resources.icon_new_06.ToBitmap()),
                    PictureDispConverter.Convert(Resources.icon_new_06.ToBitmap()));
            timingWindowButtonDefinition =
                m_inventorApplication.CommandManager.ControlDefinitions.AddButtonDefinition("타이밍", "타이밍",
                    CommandTypesEnum.kNonShapeEditCmdType, AddInClientID, "DescriptionText_타이밍", "ToolTipText_타이밍",
                    PictureDispConverter.Convert(Resources.icon_new_05.ToBitmap()),
                    PictureDispConverter.Convert(Resources.icon_new_05.ToBitmap()));
            bevelGearWindowButtonDefinition =
                m_inventorApplication.CommandManager.ControlDefinitions.AddButtonDefinition("베벨기어", "베벨기어",
                    CommandTypesEnum.kNonShapeEditCmdType, AddInClientID, "DescriptionText_베벨기어", "ToolTipText_베벨기어",
                    PictureDispConverter.Convert(Resources.icon_new_02.ToBitmap()),
                    PictureDispConverter.Convert(Resources.icon_new_02.ToBitmap()));
            repinionWindowButtonDefinition =
                m_inventorApplication.CommandManager.ControlDefinitions.AddButtonDefinition("렉피니언", "렉피니언",
                    CommandTypesEnum.kNonShapeEditCmdType, AddInClientID, "DescriptionText_렉피니언", "ToolTipText_렉피니언",
                    PictureDispConverter.Convert(Resources.icon_new_03.ToBitmap()),
                    PictureDispConverter.Convert(Resources.icon_new_03.ToBitmap()));
            wormGearWindowButtonDefinition =
                m_inventorApplication.CommandManager.ControlDefinitions.AddButtonDefinition("웜기어", "웜기어",
                    CommandTypesEnum.kNonShapeEditCmdType, AddInClientID, "DescriptionText_웜기어", "ToolTipText_웜기어",
                    PictureDispConverter.Convert(Resources.icon_new_07.ToBitmap()),
                    PictureDispConverter.Convert(Resources.icon_new_07.ToBitmap()));
            sprocketWindowButtonDefinition =
                m_inventorApplication.CommandManager.ControlDefinitions.AddButtonDefinition("스프로킷", "스프로킷",
                    CommandTypesEnum.kNonShapeEditCmdType, AddInClientID, "DescriptionText_스프로킷", "ToolTipText_스프로킷",
                    PictureDispConverter.Convert(Resources.icon_new_04.ToBitmap()),
                    PictureDispConverter.Convert(Resources.icon_new_04.ToBitmap()));

            // STEP 4 :: 버튼 생성 및 이벤트 핸들러 등록
            // 표제란
            titleColumnWindowButtonDefinition.OnExecute += delegate(NameValueMap context)
            {
                TitleColumnWindow tw = new TitleColumnWindow(m_inventorApplication);
                tw.Show();
            };
            // 기어 요목표
            spurGearWindowButtonDefinition.OnExecute += (context) => GearButtonClick(spurGearWindowButtonDefinition.InternalName);
            helicalWindowButtonDefinition.OnExecute += (context) => GearButtonClick(helicalWindowButtonDefinition.InternalName);
            timingWindowButtonDefinition.OnExecute += (context) => GearButtonClick(timingWindowButtonDefinition.InternalName);
            bevelGearWindowButtonDefinition.OnExecute += (context) => GearButtonClick(bevelGearWindowButtonDefinition.InternalName);
            repinionWindowButtonDefinition.OnExecute += (context) => GearButtonClick(repinionWindowButtonDefinition.InternalName);
            wormGearWindowButtonDefinition.OnExecute += (context) => GearButtonClick(wormGearWindowButtonDefinition.InternalName);
            sprocketWindowButtonDefinition.OnExecute += (context) => GearButtonClick(sprocketWindowButtonDefinition.InternalName);

            // STEP 5 :: 버튼 추가
            titleColumnRibbonPanel.CommandControls.AddButton(titleColumnWindowButtonDefinition, true);
            gearSummaryRibbonPanel.CommandControls.AddButton(spurGearWindowButtonDefinition, true);
            gearSummaryRibbonPanel.CommandControls.AddButton(helicalWindowButtonDefinition, true);
            gearSummaryRibbonPanel.CommandControls.AddButton(timingWindowButtonDefinition, true);
            gearSummaryRibbonPanel.CommandControls.AddButton(bevelGearWindowButtonDefinition, true);
            gearSummaryRibbonPanel.CommandControls.AddButton(repinionWindowButtonDefinition, true);
            gearSummaryRibbonPanel.CommandControls.AddButton(wormGearWindowButtonDefinition, true);
            gearSummaryRibbonPanel.CommandControls.AddButton(sprocketWindowButtonDefinition, true);
        }


        public void Deactivate()
        {
            // This method is called by Inventor when the AddIn is unloaded CommunityToolkit.Mvvm, Version=8.1.0.0, Culture=neutral, PublicKeyToken=4aff67a105548ee2' 또는 여기에 종속되어 있는 파일이나 어셈블리 중 하나를 로드할 수 없습니다. 지정된 파일을 찾을 수 없습니다.'

            // The AddIn will be unloaded either manually by the user or
            // when the Inventor session is terminated

            // TODO: Add ApplicationAddInServer.Deactivate implementation

            // Release objects.
            titleColumnRibbonPanel.Delete();
            gearSummaryRibbonPanel.Delete();
            globalsoftRibbonTab.Delete();

            titleColumnRibbonPanel = null;
            gearSummaryRibbonPanel = null;
            globalsoftRibbonTab = null;

            m_inventorApplication = null;

            GC.Collect();
            GC.WaitForPendingFinalizers();
        }

        public void ExecuteCommand(int CommandID)
        {
            throw new NotImplementedException();
        }

        public object Automation => throw new NotImplementedException();

        #endregion

        #region private method
        private void GearButtonClick(string InternalName)
        {
            GearType gearType = GetGearTypeFromContext(InternalName);
            GearSummaryViewModelFactory factoryViewModel;
            GearSummaryViewFactory factoryView;

            switch (gearType)
            {
                case GearType.SpurGear:
                    factoryView = new SpurGearViewFactory();
                    factoryViewModel = new SpurGearViewModelFactory();
                    break;
                case GearType.Helical:
                    factoryView = new HelicalViewFactory();
                    factoryViewModel = new HelicalViewModelFactory();
                    break;
                case GearType.Timing:
                    factoryView = new TimingViewFactory();
                    factoryViewModel = new TimingViewModelFactory();
                    break;
                case GearType.BevelGear:
                    factoryView = new BevelGearViewFactory();
                    factoryViewModel = new BevelGearViewModelFactory();
                    break;
                case GearType.Repinion:
                    factoryView = new RepinionViewFactory();
                    factoryViewModel = new RepinionViewModelFactory();
                    break;
                case GearType.WormGear:
                    factoryView = new WormGearViewFactory();
                    factoryViewModel = new WormGearViewModelFactory();
                    break;
                case GearType.Sprocket:
                    factoryView = new SprocketViewFactory();
                    factoryViewModel = new SprocketViewModelFactory();
                    break;
                default:
                    throw new ArgumentException($"Unknown gear type : {gearType}");
            }

            GearSummaryView gearSummaryView = factoryView.CreateView();
            GearSummaryViewModel gearSummaryViewModel = factoryViewModel.CreateViewModel(m_inventorApplication);

            GearSummaryWindow gearSummaryWindow = new GearSummaryWindow(gearSummaryView, gearSummaryViewModel);
            gearSummaryWindow.Show();
        }

        private GearType GetGearTypeFromContext(string InternalName)
        {
            switch (InternalName)
            {
                case "스퍼기어":
                    return GearType.SpurGear;
                case "헬리컬":
                    return GearType.Helical;
                case "타이밍":
                    return GearType.Timing;
                case "베벨기어":
                    return GearType.BevelGear;
                case "렉피니언":
                    return GearType.Repinion;
                case "웜기어":
                    return GearType.WormGear;
                case "스프로킷":
                    return GearType.Sprocket;
                default:
                    throw new ArgumentException($"Unknown gear type : {InternalName}");
            }
        }
        #endregion
    }
}
