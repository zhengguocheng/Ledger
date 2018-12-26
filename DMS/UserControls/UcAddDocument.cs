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
    public partial class UcAddDocument : UserControlBase
    {
        public DAL.tblDocumentItem SelectedDoc { get; set; }
        Repository docController = new Repository();

        public UcAddDocument()
        {
            InitializeComponent();
            this.crudMessage = new CustomMessages("File");
            this.Caption = "File";
        }
        
        private void btnSave_Click(object sender, EventArgs e)
        {
            if (chkIsMultiple.Checked)
            {
                DirectoryInfo dr = new DirectoryInfo(folderBrowserDialog1.SelectedPath);
                foreach (FileInfo f in dr.GetFiles())
                {
                    AddFile(f.FullName,f.Name);
                }
            }
            else
            {
                string fileName = txtFileName.Text.Trim() + txtFileExtention.Text.Trim();
                AddFile(openFileDialog1.FileName,fileName);
            }

            DisplayManager.CloseDialouge(DialogResult.OK);
        }

        bool AddFile(string path,string name)
        {

            if (!docController.DocumentExist(name, SelectedDoc.ClientID, SelectedDoc.ClientID))
            {
                tblDocumentItem doc = new tblDocumentItem();
                doc.ClientID = SelectedDoc.ClientID;
                doc.Name = name;
                doc.ParentID = SelectedDoc.ParentID;
                doc.IsFolder = false;
                doc.TempByteData = FileHelper.GetByteArray(path);
                doc.Notes = txtDescription.Text;
                docController.AddFile(doc);

                DisplayManager.DisplayCrudMessage(CrudMessageType.EntitySaved, this.crudMessage);
            }
            else
            {
                if (DisplayManager.DisplayMessage("Document with same name already exists. Do you want to update existing document?", MessageType.Confirmation) == DialogResult.Yes)
                {
                    tblDocumentItem doc = docController.FindByName(name, SelectedDoc.ClientID, SelectedDoc.ClientID);
                    doc.TempByteData = FileHelper.GetByteArray(path);
                    doc.ParentID = SelectedDoc.ParentID;
                    doc.Notes = txtDescription.Text;
                    docController.UpdateFile(doc);
                    DisplayManager.DisplayCrudMessage(CrudMessageType.EntityUpdated, this.crudMessage);
                }
            }
            return true;
        }

        private void btnSelectFile_Click(object sender, EventArgs e)
        {
            if (chkIsMultiple.Checked)
            {
                
                if (folderBrowserDialog1.ShowDialog() == DialogResult.OK)
                {
                    txtFilePath.Text = folderBrowserDialog1.SelectedPath;

                }
            }
            else if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                txtFilePath.Text = openFileDialog1.FileName;
                txtFileName.Text = Path.GetFileNameWithoutExtension(openFileDialog1.FileName);
                txtFileExtention.Text = Path.GetExtension(openFileDialog1.FileName);
            }
        }

       
    }
}
