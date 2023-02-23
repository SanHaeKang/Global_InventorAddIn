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

            // STEP 1 :: "Globalsoft" �̸����� ���� �� ����
            drawingRibbon = m_inventorApplication.UserInterfaceManager.Ribbons["Drawing"];
            globalsoftRibbonTab = drawingRibbon.RibbonTabs.Add("Globalsoft", "Globalsoft", AddInClientID);
            
            // STEP 2 :: RibbonPanel ����
            titleColumnRibbonPanel = globalsoftRibbonTab.RibbonPanels.Add("ǥ����", "ǥ����", AddInClientID);
            gearSummaryRibbonPanel = globalsoftRibbonTab.RibbonPanels.Add("��� ���ǥ", "��� ���ǥ", AddInClientID);

            // STEP 3 :: �̸��� �������� ���� ��ư ����
            titleColumnWindowButtonDefinition =
                m_inventorApplication.CommandManager.ControlDefinitions.AddButtonDefinition("ǥ����", "ǥ����",
                    CommandTypesEnum.kNonShapeEditCmdType, AddInClientID, "DescriptionText_ǥ����", "ToolTipText_ǥ����",
                    PictureDispConverter.Convert(Resources.icon_chart1.ToBitmap()), 
                    PictureDispConverter.Convert(Resources.icon_chart1.ToBitmap()));
            spurGearWindowButtonDefinition =
                m_inventorApplication.CommandManager.ControlDefinitions.AddButtonDefinition("���۱��", "���۱��",
                    CommandTypesEnum.kNonShapeEditCmdType, AddInClientID, "DescriptionText_���� ���", "ToolTipText_���� ���",
                    PictureDispConverter.Convert(Resources.icon_new_01.ToBitmap()),
                    PictureDispConverter.Convert(Resources.icon_new_01.ToBitmap()));
            helicalWindowButtonDefinition =
                m_inventorApplication.CommandManager.ControlDefinitions.AddButtonDefinition("�︮��", "�︮��",
                    CommandTypesEnum.kNonShapeEditCmdType, AddInClientID, "DescriptionText_�︮��", "ToolTipText_�︮��",
                    PictureDispConverter.Convert(Resources.icon_new_06.ToBitmap()),
                    PictureDispConverter.Convert(Resources.icon_new_06.ToBitmap()));
            timingWindowButtonDefinition =
                m_inventorApplication.CommandManager.ControlDefinitions.AddButtonDefinition("Ÿ�̹�", "Ÿ�̹�",
                    CommandTypesEnum.kNonShapeEditCmdType, AddInClientID, "DescriptionText_Ÿ�̹�", "ToolTipText_Ÿ�̹�",
                    PictureDispConverter.Convert(Resources.icon_new_05.ToBitmap()),
                    PictureDispConverter.Convert(Resources.icon_new_05.ToBitmap()));
            bevelGearWindowButtonDefinition =
                m_inventorApplication.CommandManager.ControlDefinitions.AddButtonDefinition("�������", "�������",
                    CommandTypesEnum.kNonShapeEditCmdType, AddInClientID, "DescriptionText_�������", "ToolTipText_�������",
                    PictureDispConverter.Convert(Resources.icon_new_02.ToBitmap()),
                    PictureDispConverter.Convert(Resources.icon_new_02.ToBitmap()));
            repinionWindowButtonDefinition =
                m_inventorApplication.CommandManager.ControlDefinitions.AddButtonDefinition("���ǴϾ�", "���ǴϾ�",
                    CommandTypesEnum.kNonShapeEditCmdType, AddInClientID, "DescriptionText_���ǴϾ�", "ToolTipText_���ǴϾ�",
                    PictureDispConverter.Convert(Resources.icon_new_03.ToBitmap()),
                    PictureDispConverter.Convert(Resources.icon_new_03.ToBitmap()));
            wormGearWindowButtonDefinition =
                m_inventorApplication.CommandManager.ControlDefinitions.AddButtonDefinition("�����", "�����",
                    CommandTypesEnum.kNonShapeEditCmdType, AddInClientID, "DescriptionText_�����", "ToolTipText_�����",
                    PictureDispConverter.Convert(Resources.icon_new_07.ToBitmap()),
                    PictureDispConverter.Convert(Resources.icon_new_07.ToBitmap()));
            sprocketWindowButtonDefinition =
                m_inventorApplication.CommandManager.ControlDefinitions.AddButtonDefinition("������Ŷ", "������Ŷ",
                    CommandTypesEnum.kNonShapeEditCmdType, AddInClientID, "DescriptionText_������Ŷ", "ToolTipText_������Ŷ",
                    PictureDispConverter.Convert(Resources.icon_new_04.ToBitmap()),
                    PictureDispConverter.Convert(Resources.icon_new_04.ToBitmap()));

            // STEP 4 :: ��ư ���� �� �̺�Ʈ �ڵ鷯 ���
            // ǥ����
            titleColumnWindowButtonDefinition.OnExecute += delegate(NameValueMap context)
            {
                TitleColumnWindow tw = new TitleColumnWindow(m_inventorApplication);
                tw.Show();
            };
            // ��� ���ǥ
            spurGearWindowButtonDefinition.OnExecute += (context) => GearButtonClick(spurGearWindowButtonDefinition.InternalName);
            helicalWindowButtonDefinition.OnExecute += (context) => GearButtonClick(helicalWindowButtonDefinition.InternalName);
            timingWindowButtonDefinition.OnExecute += (context) => GearButtonClick(timingWindowButtonDefinition.InternalName);
            bevelGearWindowButtonDefinition.OnExecute += (context) => GearButtonClick(bevelGearWindowButtonDefinition.InternalName);
            repinionWindowButtonDefinition.OnExecute += (context) => GearButtonClick(repinionWindowButtonDefinition.InternalName);
            wormGearWindowButtonDefinition.OnExecute += (context) => GearButtonClick(wormGearWindowButtonDefinition.InternalName);
            sprocketWindowButtonDefinition.OnExecute += (context) => GearButtonClick(sprocketWindowButtonDefinition.InternalName);

            // STEP 5 :: ��ư �߰�
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
            // This method is called by Inventor when the AddIn is unloaded CommunityToolkit.Mvvm, Version=8.1.0.0, Culture=neutral, PublicKeyToken=4aff67a105548ee2' �Ǵ� ���⿡ ���ӵǾ� �ִ� �����̳� ����� �� �ϳ��� �ε��� �� �����ϴ�. ������ ������ ã�� �� �����ϴ�.'

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
                case "���۱��":
                    return GearType.SpurGear;
                case "�︮��":
                    return GearType.Helical;
                case "Ÿ�̹�":
                    return GearType.Timing;
                case "�������":
                    return GearType.BevelGear;
                case "���ǴϾ�":
                    return GearType.Repinion;
                case "�����":
                    return GearType.WormGear;
                case "������Ŷ":
                    return GearType.Sprocket;
                default:
                    throw new ArgumentException($"Unknown gear type : {InternalName}");
            }
        }
        #endregion
    }
}
