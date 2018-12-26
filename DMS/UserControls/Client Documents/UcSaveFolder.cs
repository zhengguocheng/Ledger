using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
using DMS.CustomClasses;
using DAL;
using Telerik.WinControls.UI;

namespace DMS.UserControls
{
    public partial class UcSaveFolder : UserControlBase
    {
        public DAL.tblDocumentItem SelectedDoc { get; set; }

        public UcSaveFolder()
        {
            InitializeComponent();
            this.crudMessage = new CustomMessages("Folder");
            this.Caption = "Folder";
        }

        private void UcSaveFolder_Load(object sender, EventArgs e)
        {

        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            //Repository docController = new Repository(AppConstants.RecordType.Client);

            AppConstants.RecordType rtype = AppConstants.IsLedger ? AppConstants.RecordType.Ledger : AppConstants.RecordType.Client;
            Repository docController = new Repository(rtype);


            if (!docController.DocumentExist(txtFolderName.Text.Trim(), SelectedDoc.ParentID, SelectedDoc.RecordID))
            {
                tblDocumentItem doc = new tblDocumentItem();
                doc.RecordID = SelectedDoc.RecordID;
                doc.ParentID = SelectedDoc.ParentID;
                doc.Name = txtFolderName.Text.Trim();
                doc.IsFolder = true;
                doc.Notes = txtDescription.Text;
                docController.AddFolder(doc);
            }
            else
            {
                DisplayManager.DisplayMessage("Folder with same name already exist. Please specify another name.",MessageType.Error);

            }

            DisplayManager.DisplayCrudMessage(CrudMessageType.EntityUpdated, this.crudMessage);
            DisplayManager.CloseDialouge(DialogResult.OK);
        }

    }
}
