using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Inventor;
using InventorAddin.Core;
using InventorAddin.Core.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using InventorAddin.Core.Converters;

namespace InventorAddin.Core.ViewModels
{
    public class TitleColumnWindowViewModel : ObservableObject
    {
        #region 상수
        private readonly double revisionPositionX = 13.999999999999988;
        private readonly double revisionPositionY = 2.5306133059096625;
        #endregion

        #region 멤버변수

        private Inventor.Application m_inventorApplication;
        private readonly PartDocument doc_Part;
        private readonly Document doc_Type;
        private readonly DrawingDocument oDoc;
        private readonly Sheet oSheet;
        private readonly Document oRefDoc;
        private readonly DrawingView oView;
        
        #endregion

        #region 프로퍼티 (ObservableCollection)

        public InventorAddin.Core.Models.Material SelectedMaterial
        {
            get => selectedMaterial;
            set => SetProperty(ref selectedMaterial, value);
        }

        private InventorAddin.Core.Models.Material selectedMaterial;

        public ObservableCollection<InventorAddin.Core.Models.Material> Materials
        {
            get => materials;
            set => SetProperty(ref materials, value);
        }

        private ObservableCollection<InventorAddin.Core.Models.Material> materials;

        private Unit selectedUnit;

        public Unit SelectedUnit
        {
            get => selectedUnit;
            set => SetProperty(ref selectedUnit, value);
        }

        private ObservableCollection<Unit> units;

        public ObservableCollection<Unit> Units
        {
            get => units;
            set => SetProperty(ref units, value);
        }

        private Size selectedSize;

        public Size SelectedSize
        {
            get => selectedSize;
            set => SetProperty(ref selectedSize, value);
        }

        private ObservableCollection<Size> sizes;

        public ObservableCollection<Size> Sizes
        {
            get => sizes;
            set => SetProperty(ref sizes, value);
        }

        private Revision selectedRevision;

        public Revision SelectedRevision
        {
            get => selectedRevision;
            set => SetProperty(ref selectedRevision, value);
        }

        private ObservableCollection<Revision> revisions;

        public ObservableCollection<Revision> Revisions
        {
            get => revisions;
            set => SetProperty(ref revisions, value);
            
        }

        private Scale selectedScale;

        public Scale SelectedScale
        {
            get => selectedScale;
            set => SetProperty(ref selectedScale, value);
        }

        private ObservableCollection<Scale> scales;

        public ObservableCollection<Scale> Scales
        {
            get => scales;
            set => SetProperty(ref scales, value);
        }

        #endregion

        #region 프로퍼티 (기타)

        private double pointPositionY;

        public double PointPositionY
        {
            get => pointPositionY;
            set => SetProperty(ref pointPositionY, value);
        }

        private double pointPositionX;

        public double PointPositionX
        {
            get => pointPositionX;
            set => SetProperty(ref pointPositionX, value);
        }

        private bool isPointUseChecked;
        public bool IsPointUseChecked
        {
            get => isPointUseChecked;
            set => SetProperty(ref isPointUseChecked, value);
        }

        private string status;

        public string Status
        {
            get => status;
            set => SetProperty(ref status, value);
        }

        private int rev;

        public int Rev
        {
            get => rev;
            set => SetProperty(ref rev, value);
        }

        private string note;

        public string Note
        {
            get => note;
            set => SetProperty(ref note, value);
        }

        private string quantity;

        public string Quantity
        {
            get => quantity;
            set => SetProperty(ref quantity, value);
        }

        private string orderQuantity;

        public string OrderQuantity
        {
            get => orderQuantity;
            set => SetProperty(ref orderQuantity, value);
        }

        private string surfacePreparation;

        public string SurfacePreparation
        {
            get => surfacePreparation;
            set => SetProperty(ref surfacePreparation, value);
        }

        private string dwgNo;

        public string DwgNo
        {
            get => dwgNo;
            set => SetProperty(ref dwgNo, value);
        }

        private string engrApprovedBy;

        public string EngrApprovedBy
        {
            get => engrApprovedBy;
            set => SetProperty(ref engrApprovedBy, value);
        }

        private string mass;

        public string Mass
        {
            get => mass;
            set => SetProperty(ref mass, value);
        }

        private string partnumber;

        public string PartNumber
        {
            get => partnumber;
            set => SetProperty(ref partnumber, value);
        }

        private string project;

        public string Project
        {
            get => project;
            set => SetProperty(ref project, value);
        }

        private string design;

        public string Design
        {
            get => design;
            set => SetProperty(ref design, value);
        }

        private string fileName;

        public string FileName
        {
            get => fileName;
            set => SetProperty(ref fileName, value);
        }

        private DateTime creationTime;

        public DateTime CreationTime
        {
            get => creationTime;
            set => SetProperty(ref creationTime, value);
        }
        #endregion

        #region RelayCommand
        public IRelayCommand SetPointPositionLeftCommand => new RelayCommand(SetPointPositionLeftExecute);

        public IRelayCommand SetPointPositionRightCommand => new RelayCommand(SetPointPositionRightExecute);

        public IRelayCommand SubmitCommand => new RelayCommand(SubmitExecute);

        public IRelayCommand<Revision> AddRevCommand =>
            new RelayCommand<Revision>(AddRevExecute);
        public IRelayCommand DeleteRevCommand => new RelayCommand(DeleteRevExecute);

        [RelayCommand]
        private void SubmitExecute()
        {
            var checkStr = GetCheck2();
            if (string.IsNullOrEmpty(checkStr))
            {
                Status = "에러입니다. 아래 옵션을 확인해주세요.\n[" + checkStr + "]";
                return;
            }

            SetSave();
        }

        [RelayCommand]
        private void SetPointPositionLeftExecute()
        {
            PointPositionX = 3.5;
            PointPositionY = 26;
        }

        [RelayCommand]
        private void SetPointPositionRightExecute()
        {
            PointPositionX = oSheet.Width - 3.5;
            PointPositionY = 26;
        }

        [RelayCommand]
        private void AddRevExecute(Revision _rev)
        {
            _rev.Rev = Revisions.Count + 1;

            Revisions.Add(_rev);
        }

        [RelayCommand]
        private void DeleteRevExecute()
        {
            Revisions.Remove(SelectedRevision);

            for (int i = 0; i < Revisions.Count; i++)
            {
                Revisions[i].Rev = i + 1;
            }
        }

        #endregion

        public TitleColumnWindowViewModel()
        {

        }

        public TitleColumnWindowViewModel(Inventor.Application _m_inventorApplication)
        {
            m_inventorApplication = _m_inventorApplication;

            try
            {
                doc_Type = m_inventorApplication.ActiveDocument;

                if(!doc_Type.DocumentType.Equals(DocumentTypeEnum.kDrawingDocumentObject))
                    throw new Exception("문서 타입이 DrawingDocument 타입인지 확인해주세요.");
            }
            catch (Exception)
            {
                throw new Exception("현재 문서가 제대로 활성화되지 못했습니다.");
            }

            oDoc = doc_Type as DrawingDocument;
            oSheet = oDoc.ActiveSheet;
            if (oSheet.DrawingViews.Count == 0)
                throw new Exception("DrawingView 가 없습니다.");
            
            oView = oSheet.DrawingViews[1];
            
            try
            {
                oRefDoc = oView.ReferencedDocumentDescriptor.ReferencedDocument as Document;
                doc_Part = oRefDoc as PartDocument;
            }
            catch (Exception)
            {
                throw new Exception("부품 내역이 없습니다.");
            }
            FileName = System.IO.Path.GetFileName(oDoc.FullFileName);

            PropertySet oPropSet;

            Revisions = new ObservableCollection<Revision>();
            Revisions.CollectionChanged += (sender, e) =>
            {
                Rev = Revisions.Count;
            };

            materials = new ObservableCollection<InventorAddin.Core.Models.Material>();
            Scales = new ObservableCollection<Scale>();
            Sizes = new ObservableCollection<Size>();
            Units = new ObservableCollection<Unit>();

            GetRevision();
            GetMaterial();
            GetSize();
            GetScale();
            GetUnit();
            GetSymbol();

            SelectedMaterial =
                (from mat
                        in Materials
                 where mat.Name.Equals(doc_Part.ActiveMaterial.DisplayName)
                 select mat).First();

            SelectedScale =
                (from scale in Scales
                 where scale.Code.Equals(oView.Scale.ToString())
                 select scale).First();

            SelectedSize =
                (from size in Sizes
                 where size.Code.Equals(oSheet.Size)
                 select size).First();

            SelectedUnit =
                (from unit in Units
                 where unit.Code.Equals(oDoc.UnitsOfMeasure.LengthUnits.ToString())
                 select unit).First();
            
            if (doc_Part != null)
            {
                oPropSet = doc_Part.PropertySets["Design Tracking Properties"];
                Design = oPropSet["Designer"].Value.ToString();
                Project = oPropSet["Project"].Value.ToString();
                CreationTime = DateTime.Parse(oPropSet["Creation Time"].Value.ToString());
                PartNumber = oPropSet["Part Number"].Value.ToString();
                Mass = string.IsNullOrEmpty(oPropSet["Mass"].Value.ToString())
                    ? oPropSet["Mass"].Value.ToString()
                    : (double.Parse(oPropSet["Mass"].Value.ToString()) * 0.001).ToString();
                EngrApprovedBy = oPropSet["Engr Approved By"].Value.ToString();
                DwgNo = oPropSet["Stock Number"].Value.ToString();

                oPropSet = doc_Part.PropertySets["Inventor User Defined Properties"];
                SurfacePreparation = oPropSet["후처리"].Value.ToString();
                OrderQuantity = oPropSet["발주수량"].Value.ToString();
                Quantity = oPropSet["수량"].Value.ToString();
            }

            oPropSet = doc_Type.PropertySets["Inventor Summary Information"];
            Note = oPropSet["Keywords"].Value.ToString();
        }


        #region private Method

        private void GetSymbol()
        {
            foreach (SketchedSymbol symbol in oSheet.SketchedSymbols)
            {
                if (symbol.Name.Equals("출도도장"))
                {
                    IsPointUseChecked = true;
                    PointPositionX = symbol.Position.X;
                    PointPositionY = symbol.Position.Y;
                    break;
                }
            }
        }

        private void GetUnit()
        {
            Units.Add(new Unit { Code = UnitsTypeEnum.kCentimeterLengthUnits.ToString(), Name = "cm" });
            Units.Add(new Unit { Code = UnitsTypeEnum.kMillimeterLengthUnits.ToString(), Name = "mm" });
            Units.Add(new Unit { Code = UnitsTypeEnum.kInchLengthUnits.ToString(), Name = "inch" });
        }

        private void GetSize()
        {
            Sizes.Add(new Size { Code = DrawingSheetSizeEnum.kCustomDrawingSheetSize, Name = "Custom" });
            Sizes.Add(new Size { Code = DrawingSheetSizeEnum.kADrawingSheetSize, Name = "A" });
            Sizes.Add(new Size { Code = DrawingSheetSizeEnum.kBDrawingSheetSize, Name = "B" });
            Sizes.Add(new Size { Code = DrawingSheetSizeEnum.kCDrawingSheetSize, Name = "C" });
            Sizes.Add(new Size { Code = DrawingSheetSizeEnum.kDDrawingSheetSize, Name = "D" });
            Sizes.Add(new Size { Code = DrawingSheetSizeEnum.kEDrawingSheetSize, Name = "E" });
            Sizes.Add(new Size { Code = DrawingSheetSizeEnum.kFDrawingSheetSize, Name = "F" });
            Sizes.Add(new Size { Code = DrawingSheetSizeEnum.kA0DrawingSheetSize, Name = "A0" });
            Sizes.Add(new Size { Code = DrawingSheetSizeEnum.kA1DrawingSheetSize, Name = "A1" });
            Sizes.Add(new Size { Code = DrawingSheetSizeEnum.kA2DrawingSheetSize, Name = "A2" });
            Sizes.Add(new Size { Code = DrawingSheetSizeEnum.kA3DrawingSheetSize, Name = "A3" });
            Sizes.Add(new Size { Code = DrawingSheetSizeEnum.kA4DrawingSheetSize, Name = "A4" });
            Sizes.Add(new Size { Code = DrawingSheetSizeEnum.k9x12InDrawingSheetSize, Name = "9 x 12" });
            Sizes.Add(new Size { Code = DrawingSheetSizeEnum.k12x18InDrawingSheetSize, Name = "12 x 18" });
            Sizes.Add(new Size { Code = DrawingSheetSizeEnum.k18x24InDrawingSheetSize, Name = "18 x 24" });
            Sizes.Add(new Size { Code = DrawingSheetSizeEnum.k24x36InDrawingSheetSize, Name = "24 x 36" });
            Sizes.Add(new Size { Code = DrawingSheetSizeEnum.k36x48InDrawingSheetSize, Name = "36 x 48" });
            Sizes.Add(new Size { Code = DrawingSheetSizeEnum.k30x42InDrawingSheetSize, Name = "30 x 42" });
        }

        private void GetMaterial()
        {
            foreach (Inventor.Material oMaterial in doc_Part.Materials) 
                Materials.Add(new InventorAddin.Core.Models.Material { Name = oMaterial.Name });
        }

        private void GetRevision()
        {
            if (oSheet.RevisionTables.Count == 0) 
                return;

            foreach (RevisionTableRow oRevRow in oSheet.RevisionTables[1].RevisionTableRows)
            {
                Revisions.Add(new Revision
                {
                    Rev = int.Parse(oRevRow[1].Text), 
                    Approval = oRevRow[2].Text, 
                    Date = StringToObjectConverter.ConvertToDateTime(oRevRow[3].Text), 
                    Explanation = oRevRow[4].Text
                });
            }

            Rev = Revisions.Count;
        }

        private void GetScale()
        {
            Scales.Clear();

            Scales.Add(new Scale { Code = "0", Name = "N/S" });
            Scales.Add(new Scale { Code = "100", Name = "100 : 1" });
            Scales.Add(new Scale { Code = "50", Name = "50 : 1" });
            Scales.Add(new Scale { Code = "20", Name = "20 : 1" });
            Scales.Add(new Scale { Code = "10", Name = "10 : 1" });
            Scales.Add(new Scale { Code = "5", Name = "5 : 1" });
            Scales.Add(new Scale { Code = "2", Name = "2 : 1" });
            Scales.Add(new Scale { Code = "1", Name = "1 : 1" });
            Scales.Add(new Scale { Code = "0.5", Name = "1 : 2" });
            Scales.Add(new Scale { Code = "0.2", Name = "1 : 5" });
            Scales.Add(new Scale { Code = "0.1", Name = "1 : 10" });
            Scales.Add(new Scale { Code = "0.05", Name = "1 : 20" });
            Scales.Add(new Scale { Code = "0.02", Name = "1 : 50" });
            Scales.Add(new Scale { Code = "0.01", Name = "1 : 100" });
            Scales.Add(new Scale { Code = "0.005", Name = "1 : 200" });
            Scales.Add(new Scale { Code = "0.002", Name = "1 : 500" });
            Scales.Add(new Scale { Code = "0.001", Name = "1 : 1000" });
            Scales.Add(new Scale { Code = "0.0005", Name = "1 : 2000" });
            Scales.Add(new Scale { Code = "0.0002", Name = "1 : 5000" });
            Scales.Add(new Scale { Code = "0.0001", Name = "1 : 10000" });
        }

        public void SetSave()
        {
            // 재질
            var materialDef = doc_Part.Materials[SelectedMaterial.Name];
            doc_Part.ComponentDefinition.Material = materialDef;

            // Unit
            switch (SelectedUnit.Name)
            {
                case "mm":
                    doc_Type.UnitsOfMeasure.LengthUnits = UnitsTypeEnum.kMillimeterLengthUnits;
                    doc_Part.UnitsOfMeasure.LengthUnits = UnitsTypeEnum.kMillimeterLengthUnits;
                    break;
                case "cm":
                    doc_Type.UnitsOfMeasure.LengthUnits = UnitsTypeEnum.kCentimeterLengthUnits;
                    doc_Part.UnitsOfMeasure.LengthUnits = UnitsTypeEnum.kCentimeterLengthUnits;
                    break;
                case "inch":
                    doc_Type.UnitsOfMeasure.LengthUnits = UnitsTypeEnum.kInchLengthUnits;
                    doc_Part.UnitsOfMeasure.LengthUnits = UnitsTypeEnum.kInchLengthUnits;
                    break;
            }

            // 리비전 (DT)
            // 모든 REV 로우 삭제
            if (Revisions.Count == 0)
            {
                oSheet.RevisionTables[1].Delete();
            }
            else
            {
                RevisionTable table;
                try
                {
                    table = oSheet.RevisionTables[1];
                }
                catch (Exception)
                {
                    Inventor.Point2d position = m_inventorApplication.TransientGeometry.CreatePoint2d(revisionPositionX, revisionPositionY);

                    oSheet.RevisionTables.Add(position);
                    table = oSheet.RevisionTables[1];
                    table.Style = oDoc.StylesManager.RevisionTableStyles["리비젼 테이블 Globalsoft"];
                }
                foreach (RevisionTableRow row in table.RevisionTableRows)
                {
                    if (!row.IsActiveRow) row.Delete();
                }

                for (int i = 0; i < Revisions.Count; i++)
                {
                    RevisionTableRow row;
                    try
                    {
                        row = table.RevisionTableRows[i + 1];
                    }
                    catch (Exception)
                    {
                        table.RevisionTableRows.Add();
                        row = table.RevisionTableRows[i + 1];
                    }

                    row[1].Text = Revisions[i].Rev.ToString();
                    row[2].Text = Revisions[i].Explanation;
                    row[3].Text = ObjectToStringConverters.ConvertToString(Revisions[i].Date);
                    row[4].Text = Revisions[i].Approval;
                }
            }

            // SIZE
            oSheet.Size = SelectedSize.Code;

            // 도장 (쉽지않음)
            foreach (SketchedSymbol symbol in oSheet.SketchedSymbols)
            {
                if (symbol.Name.Equals("출도도장"))
                {
                    symbol.Delete();
                    break;
                }
            }

            if (IsPointUseChecked)
            {
                oSheet.SketchedSymbols.Add(
                    oDoc.SketchedSymbolDefinitions["출도도장"],
                    m_inventorApplication.TransientGeometry.CreatePoint2d(PointPositionX, PointPositionY)
                    );
            }

            // Scale
            oView.Scale = double.Parse(SelectedScale.Code);

            // 수량
            doc_Part.PropertySets["Inventor User Defined Properties"]["수량"].Value = Quantity;

            // 발주수량
            doc_Part.PropertySets["Inventor User Defined Properties"]["발주수량"].Value = OrderQuantity;

            // 비고
            doc_Part.PropertySets["Inventor Summary Information"]["Keywords"].Value = Note;

            // 표면처리
            doc_Part.PropertySets["Inventor User Defined Properties"]["후처리"].Value = SurfacePreparation;

            // Project
            doc_Part.PropertySets["Design Tracking Properties"]["Project"].Value = Project;

            // PartName
            doc_Part.PropertySets["Design Tracking Properties"]["Part Number"].Value = PartNumber;

            // 설계
            doc_Part.PropertySets["Design Tracking Properties"]["Designer"].Value = Design;

            // DWGNO
            doc_Part.PropertySets["Design Tracking Properties"]["Stock Number"].Value = DwgNo;

            // REV (Revision 테이블 개수)
            //doc_Part.PropertySets["Inventor Summary Information"]["Revision Number"].Value = Rev;

            // 검도/승인
            doc_Part.PropertySets["Design Tracking Properties"]["Engr Approved By"].Value = EngrApprovedBy;
            
            // Weight
            doc_Part.PropertySets["Design Tracking Properties"]["Mass"].Value = Mass;

            // 작성일자
            doc_Part.PropertySets["Design Tracking Properties"]["Creation Time"].Value = CreationTime;
            
            // FileName
            //string newFileName = oDoc.FullFileName.Substring(0, oDoc.FullFileName.LastIndexOf("\\") + 1) + FileName;
            //oDoc.SaveAs(newFileName, false);

            oDoc.Update();
            doc_Part.Update();
        }

        private string GetCheck2()
        {
            var result = string.Empty;
            var lstResult = new List<string>();

            var nTemp = -1;
            double dTemp = -1;

            #region 검사

            if (SelectedMaterial is null) lstResult.Add("재질");
            if (SelectedUnit is null) lstResult.Add("Unit");
            if (SelectedRevision is null) lstResult.Add("Revision");
            if (SelectedSize is null) lstResult.Add("SIZE");
            if (SelectedScale is null) lstResult.Add("Scale");
            if (string.IsNullOrEmpty(Quantity) || int.TryParse(Quantity, out nTemp)) lstResult.Add("수량");
            if (string.IsNullOrEmpty(OrderQuantity) || int.TryParse(OrderQuantity, out nTemp)) lstResult.Add("발주수량");
            if (string.IsNullOrEmpty(Note)) lstResult.Add("비고");
            if (string.IsNullOrEmpty(SurfacePreparation)) lstResult.Add("표면처리");
            if (string.IsNullOrEmpty(Project)) lstResult.Add("Project");
            if (string.IsNullOrEmpty(PartNumber)) lstResult.Add("PartName");
            if (string.IsNullOrEmpty(Design)) lstResult.Add("설계");
            if (string.IsNullOrEmpty(DwgNo) || int.TryParse(DwgNo, out nTemp)) lstResult.Add("DwgNo");
            if (string.IsNullOrEmpty(EngrApprovedBy)) lstResult.Add("검도/승인");
            if (string.IsNullOrEmpty(FileName)) lstResult.Add("FileName");
            if (string.IsNullOrEmpty(Mass) || double.TryParse(Mass, out dTemp)) lstResult.Add("Weight");

            #endregion

            if (lstResult.Count > 0)
                for (var i = 0; i < lstResult.Count; i++)
                {
                    result += lstResult[i];

                    if (i != lstResult.Count - 1) result += ", ";
                }

            return result;
        }

        private void GetCheck()
        {
            var arr_Check = new string[15, 2];
            var str_CheckString = string.Empty;
            var str_Check = string.Empty;

            if (string.IsNullOrEmpty(Design))
            {
                arr_Check[0, 0] = "설계자";
                arr_Check[0, 1] = "NO";
                str_CheckString += "설계자, ";
            }

            if (string.IsNullOrEmpty(Project))
            {
                arr_Check[1, 0] = "Project";
                arr_Check[1, 1] = "NO";
                str_CheckString += "Project, ";
            }

            if (CreationTime == null)
            {
                arr_Check[2, 0] = "작성일자";
                arr_Check[2, 1] = "NO";
                str_CheckString += "작성일자, ";
            }

            /*
            if (string.IsNullOrEmpty(Material))
            {
                arr_Check[3, 0] = "재질";
                arr_Check[3, 1] = "NO";
                str_CheckString += "재질, ";
            }
            */
            if (string.IsNullOrEmpty(PartNumber))
            {
                arr_Check[4, 0] = "PartNumber";
                arr_Check[4, 1] = "NO";
                str_CheckString += "PartName, ";
            }

            if (string.IsNullOrEmpty(Mass))
            {
                arr_Check[5, 0] = "WEIGHT";
                arr_Check[5, 1] = "NO";
                str_CheckString += "WEIGHT, ";
            }

            if (string.IsNullOrEmpty(SurfacePreparation))
            {
                arr_Check[6, 0] = "표면처리";
                arr_Check[6, 1] = "NO";
                str_CheckString += "표면처리, ";
            }

            if (string.IsNullOrEmpty(OrderQuantity))
            {
                arr_Check[7, 0] = "발주수량";
                arr_Check[7, 1] = "NO";
                str_CheckString += "발주수량, ";
            }

            if (string.IsNullOrEmpty(Quantity))
            {
                arr_Check[8, 0] = "수량";
                arr_Check[8, 1] = "NO";
                str_CheckString += "수량, ";
            }

            if (string.IsNullOrEmpty(Note))
            {
                arr_Check[9, 0] = "비고";
                arr_Check[9, 1] = "NO";
                str_CheckString += "비고, ";
            }

            if (SelectedScale == null)
            {
                arr_Check[10, 0] = "Scale";
                arr_Check[10, 1] = "NO";
                str_CheckString += "Scale, ";
            }

            if (Revisions.Count == 0)
            {
                arr_Check[12, 0] = "수정이력";
                arr_Check[12, 1] = "NO";
                str_CheckString += "수정이력, ";
            }
            
            if (str_CheckString.Length > 0)
            {
                if (string.IsNullOrEmpty(str_Check))
                    str_CheckString = str_CheckString.Substring(0, str_CheckString.Length - 2);
                Status = str_CheckString + str_Check + "의 옵션이 미설정입니다. 다시 확인 해주시기 바랍니다.";
            }
        }

        private bool GetCompareRows(ObservableCollection<Revision> lstReturn01,

            ObservableCollection<Revision> lstReturn02)
        {
            if (lstReturn01.Count != lstReturn02.Count)
            {
                return false;
            }

            for (int i = 0; i < lstReturn01.Count; i++)
            {
                if (
                    (lstReturn01[i].Rev != lstReturn02[i].Rev) ||
                    (lstReturn01[i].Approval != lstReturn02[i].Approval) ||
                    (lstReturn01[i].Date != lstReturn02[i].Date) ||
                    (lstReturn01[i].Explanation != lstReturn02[i].Explanation)
                )
                {
                    return false;
                }
            }

            return true;
        }

        #endregion
    }
}
