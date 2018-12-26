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
    public partial class UcAnalysisCode : UserControlBase
    {
        tblAnalysisCodeController cntrl = new tblAnalysisCodeController();
        public tblAnalysisCode SelectedItem { get; set; }

        public long? yrEndFolID;

        public UcAnalysisCode()
        {
            InitializeComponent();
            SelectedItem = new tblAnalysisCode();
            this.Caption = "Analysis Code";
        }

        private void UcAccountChart_Load(object sender, EventArgs e)
        {
            LoadItem();
            AddKeyDownControls(txtCode, txtNotes);

        }

        void LoadItem()
        {
            if (SelectedItem != null && SelectedItem.ID > 0)
            {
                txtNotes.Text = SelectedItem.Notes.ToString();
                txtCode.Text = SelectedItem.Code;
            }
        }

        bool InputValidate()
        {
            if (string.IsNullOrEmpty(txtCode.Text.Trim()))
            {
                ShowValidationError(txtCode, CustomMessages.GetValidationMessage("Code"));
                return false;
            }
            
           
            return true;
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (!InputValidate())
                return;
            
            SelectedItem.Notes = txtNotes.Text;
            SelectedItem.Code = txtCode.Text;

            if (yrEndFolID.HasValue)
                SelectedItem.YearEndFolderID = yrEndFolID;

            try
            {
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
            DisplayManager.LoadControl(new UcAnalysisCodeList(),true);
        }
    }

}
