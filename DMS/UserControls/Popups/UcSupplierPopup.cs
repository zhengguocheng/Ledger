using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using DAL;

namespace DMS.UserControls.Popups
{
    public partial class UcSupplierPopup : UserControlBase
    {
        public string SelectedText;
        public string SelectedNominalCode;
        public long YearEndID;
        string enteredText;
        bool unselecText = true;
        public UcSupplierPopup(long yearEndID, string _enteredText)
        {
            InitializeComponent();
            YearEndID = yearEndID;
            enteredText = _enteredText;
        }

        private void UcNominalCodePopup_Load(object sender, EventArgs e)
        {
            DropDownHelper.BindSupplier(drpDescription);
            drpDescription.DropDownListElement.AutoCompleteAppend.LimitToList = false;
            
            drpDescription.Text = enteredText;
            
            drpNomCode.Visible = lblNomCode.Visible = false;
            drpNomCode.KeyDown += new System.Windows.Forms.KeyEventHandler(this.drpNominalCode_KeyDown);
        }

        private void btnOk_Click(object sender, EventArgs e)
        {
            if (!DropDownHelper.IsTextExist(drpDescription) && drpNomCode.Visible == false)//if supplier dont exist in db
            {
                drpNomCode.Visible = lblNomCode.Visible = true;
                DropDownHelper.BindNominalCode(drpNomCode, 0);
                drpNomCode.SelectedIndex = 1;
                System.Windows.Forms.SendKeys.Send("{TAB}");
                return;
            }

            if (!DropDownHelper.IsTextExist(drpNomCode) && drpNomCode.Visible == true)
            {
                DisplayManager.DisplayMessage("Please select valid Nominal Code", MessageType.Error);
                return;
            }

            AddSupplier();
            SelectedText = drpDescription.Text;
            if (!string.IsNullOrWhiteSpace(SelectedText) && SelectedText.Contains(UcExcelSheet.SeperatorChar))
            {
                SelectedText = SelectedText.Split(UcExcelSheet.SeperatorChar.ToCharArray(), StringSplitOptions.RemoveEmptyEntries)[0].TrimEnd();
            }
            DisplayManager.CloseDialouge(DialogResult.OK);
        }

        tblSupplierController supCnt = new tblSupplierController();

        void AddSupplier()
        {
            var val = drpDescription.Text.Trim();
            var ind = drpDescription.FindString(val);


            if (!DropDownHelper.IsTextExist(drpDescription))//if supplier dont exist in db
            {
                //add supplier in db
                var s = new tblSupplier();
                s.Name = val;
                
                s.NominalCodeID = AppConverter.ToInt(DropDownHelper.GetSelectedValue(drpNomCode),-1);
                supCnt.Save(s);
            }
            else
            {
                var supID = DropDownHelper.GetSelectedValue(drpDescription);
                var sup = supCnt.Find(Convert.ToInt32(supID));
                if(sup.NominalCodeID.HasValue)
                {
                    //Supplier is bound to generic Nominal Codes(YrEndFolID = 0). So we need to find similar Nominal code against specific yearend
                    var nomCnt = new tblChartAccountController();
                    var objNom = nomCnt.FetchSimilarRecord(sup.NominalCodeID.Value,YearEndID);
                    if(objNom != null)
                    {
                        SelectedNominalCode = objNom.Code;
                    }
                }
            }
        }

        private void drpVATCode_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                btnOk_Click(null, null);
            else
            {
                if (unselecText)
                {
                    drpDescription.SelectText(1, drpDescription.Text.Length - 1);
                    unselecText = false;
                }
            }
        }


        private void drpNominalCode_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                //DropDownHelper.CorrectInvalidSearchTerm(drpNomCode);//Focus changed will not call in this case we need to call CorrectInvalidSearchTerm manually 
                btnOk_Click(null, null);
            }
        }

    }
}
