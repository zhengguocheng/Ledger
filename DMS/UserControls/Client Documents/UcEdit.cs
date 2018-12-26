using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using DAL;
using System.IO;

namespace DMS.UserControls
{
    public partial class UcEdit : UserControlBase
    {
        public string NewName { get; set; }

        public tblDocumentTemplate SelTemplate { get; set; }
        

        bool displayTemplate = false;

        public UcEdit(bool pDisplayTemplate)
        {
            InitializeComponent();
            displayTemplate = pDisplayTemplate;
        }
        
        private void btnSave_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtFolderName.Text))
            {
                ShowValidationError(txtFolderName, "Please enter valid name");
                return;
            }

            string enteredName = txtFolderName.Text.Trim();
            if (!enteredName.EndsWith(Path.GetExtension(NewName)))
            {
                enteredName += Path.GetExtension(NewName);
            }
            NewName = enteredName;

            //if (!NewName.EndsWith(".docx"))
            //{
            //    NewName += ".docx";
            //}
            
            if (displayTemplate)
            {
                SelTemplate = (tblDocumentTemplate)drpTemplateType.SelectedItem.DataBoundItem;
                if (SelTemplate.ID == 0)
                {
                    ShowValidationError(drpTemplateType, "Please enter valid Template");
                    return;
                }
            }
            this.ParentForm.DialogResult = DialogResult.OK;
        }
        
        private void UcEdit_Load(object sender, EventArgs e)
        {
            //string ext = Path.GetExtension(NewName);
            
            txtFolderName.Text = Path.GetFileNameWithoutExtension( NewName);

            if (displayTemplate)
            {
                List<tblDocumentTemplate> lst = new DocumentTemplateController().FetchAll();
                lst.Insert(0,new tblDocumentTemplate(){ID = 0, Name = " "});
                drpTemplateType.DataSource = lst;
                drpTemplateType.DisplayMember = "Name";
                drpTemplateType.ValueMember = "ID";
            }
            else
                ShowTemplateSelection(false);
        }

        void ShowTemplateSelection(bool show)
        {
            drpTemplateType.Visible = lblTemplate.Visible = show;
        }
    }
}
