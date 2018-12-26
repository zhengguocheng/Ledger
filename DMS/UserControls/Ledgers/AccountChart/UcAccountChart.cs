using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using DAL;
using DMS;

namespace DMS
{
    public partial class UcAccountChart : UserControlBase
    {
        tblChartAccountController cntrl = new tblChartAccountController();
        public tblChartAccount SelectedItem { get; set; }
        public long? yrEndFolID;

        public UcAccountChart()
        {
            InitializeComponent();
            SelectedItem = new tblChartAccount();
            this.Caption = "Chart Account";
        }

        private void UcAccountChart_Load(object sender, EventArgs e)
        {
            LoadItem();
            AddKeyDownControls(txtCode, drpType, drpGroupID, txtDescription);

        }

        void LoadItem()
        {
            DropDownHelper.BindNominalCode(drpNominalCode, 0, true);

            drpType.DataSource = new List<DropDownItem>()
	        {
	            new DropDownItem(){ DisplayText = "P", Value = "P"},
	            new DropDownItem(){ DisplayText = "B", Value = "B"}
	        };
            drpType.DisplayMember = "DisplayText";
            drpType.ValueMember = "Value";

            drpGroupID.DataSource = new tblAccountGroupController().FetchByYearEndID(yrEndFolID.Value);
            drpGroupID.DisplayMember = "Name";
            drpGroupID.ValueMember = "ID";

            if (SelectedItem != null && SelectedItem.ID > 0)
            {
                txtCode.Text = SelectedItem.Code;
                txtDescription.Text = SelectedItem.Description.ToString();
                drpType.SelectedValue = SelectedItem.Type.Trim();
                drpGroupID.SelectedValue = SelectedItem.AccountGroupID;
                if (SelectedItem.YearEndCodeID.HasValue)
                {
                    DropDownHelper.SelectByValue(drpNominalCode, SelectedItem.YearEndCodeID.ToString());
                }
            }
        }

        bool InputValidate()
        {
            if (string.IsNullOrEmpty(txtCode.Text.Trim()))
            {
                ShowValidationError(txtCode, CustomMessages.GetValidationMessage("Code"));
                return false;
            }

            if (drpType.SelectedIndex < 0)
            {
                ShowValidationError(drpType, CustomMessages.GetValidationMessage("Type"));
                return false;
            }

            if (drpGroupID.SelectedIndex < 0)
            {
                ShowValidationError(drpGroupID, CustomMessages.GetValidationMessage("Account Group"));
                return false;
            }

            if (string.IsNullOrEmpty(txtDescription.Text.Trim()))
            {
                ShowValidationError(txtDescription, CustomMessages.GetValidationMessage("Description"));
                return false;
            }
            return true;
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (!InputValidate())
                return;

            SelectedItem.Code = txtCode.Text.Trim();
            SelectedItem.Type = drpType.SelectedValue.ToString();
            SelectedItem.AccountGroupID = (int)drpGroupID.SelectedValue;
            SelectedItem.Description = txtDescription.Text;

            if (DropDownHelper.IsEmpty(drpNominalCode))
                SelectedItem.YearEndCodeID = null;
            else
                SelectedItem.YearEndCodeID = Convert.ToInt32(DropDownHelper.GetSelectedValue(drpNominalCode));

            if (yrEndFolID.HasValue)
                SelectedItem.YearEndFolderID = yrEndFolID;

            try
            {
                var yrendFol = SelectedItem.YearEndFolderID.HasValue ? SelectedItem.YearEndFolderID : 0;
                var existing = cntrl.FetchByCode(SelectedItem.Code, yrendFol.Value);
                if (existing != null && existing.ID != SelectedItem.ID)
                {
                    ShowValidationError(txtCode, "This code already exists. Please enter new code.");
                    return;
                }
                if (cntrl.Save(SelectedItem))
                {
                    ShowSaveConfirmation();
                    GoBack();
                }
            }
            catch (Exception ecp)
            {
                HandleException(ecp);
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            GoBack();
        }

        void GoBack()
        {
            //ImportData();
            DisplayManager.LoadControl(new UcAccountChartList(), true);
        }

        void ImportData()
        {
            tblGenericListController genLstCnt = new tblGenericListController();
            Utilities.Excel.ExcelReader er = new Utilities.Excel.ExcelReader();
            DataTable dt = er.WorksheetToDataTable(@"D:\Projects\oDesk\Ivan Lee Projects\Ledger\Documents\1.xlsx", 1, 2);

            foreach (DataRow dr in dt.Rows)
            {
                tblChartAccount ac = new tblChartAccount();
                ac.Code = dr[0].ToString().Trim();
                ac.Description = dr[1].ToString().Trim();
                ac.Type = dr[2].ToString().Trim();

                //genLstCnt.FetchByType(
                long acntGrpID = 0;
                tblGenericList acntGrp = new tblGenericList();
                acntGrp.Name = dr[3].ToString().Trim();
                acntGrp.TypeID = 1;//for account news

                tblGenericList existingItm = genLstCnt.SearchItem(GenericListType.TypeEnum.AccountGroup, acntGrp.Name);

                if (existingItm == null)
                {
                    genLstCnt.Save(acntGrp);
                    acntGrpID = acntGrp.ID;
                }
                else
                    acntGrpID = existingItm.ID;

                ac.AccountGroupID = (int)acntGrpID;
                cntrl.Save(ac);
            }
        }

        public void ImportYearEndCodeData()
        {
            tblGenericListController genLstCnt = new tblGenericListController();
            Utilities.Excel.ExcelReader er = new Utilities.Excel.ExcelReader();
            DataTable dt = er.WorksheetToDataTable(@"1.xlsx", 1, 1);

            foreach (DataRow dr in dt.Rows)
            {
                string code = dr[0].ToString().Trim();
                tblChartAccount ac = cntrl.FetchByCode(code, 0);
                if (ac != null)
                {
                    string yrEndCode = dr[4].ToString().Trim();
                    if (!string.IsNullOrWhiteSpace(yrEndCode))
                    {
                        var o = cntrl.FetchByCode(yrEndCode, 0);
                        if (o != null && o.YearEndCodeID == null)//only save value if value dont exist
                        {
                            ac.YearEndCodeID = o.ID;
                            cntrl.Save(ac);
                        }
                    }
                }
            }

            CopyYearEndCodeToSimilar();
        }

        public void CopyYearEndCodeToSimilar()
        {
            var lstParent = cntrl.FetchByYearEndID(0);
            foreach (var objParent in lstParent)
            {
                if (objParent.YearEndCodeID.HasValue)
                {
                    var lstSimilar = cntrl.FetchByCode(objParent.Code);
                    foreach (var objSimilar in lstSimilar)
                    {
                        objSimilar.YearEndCodeID = objParent.YearEndCodeID;
                        cntrl.Save(objSimilar);
                    }
                }
            }
        }

        private void label3_Click(object sender, EventArgs e)
        {

        }
    }

}
