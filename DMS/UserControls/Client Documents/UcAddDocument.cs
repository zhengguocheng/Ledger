using System;
using System.Windows.Forms;
using System.IO;
using DMS.CustomClasses;
using DAL;

namespace DMS.UserControls
{
    /*
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
    */

    public partial class UcAddDocument : UserControlBase
    {
        public DAL.tblDocumentItem SelectedDoc { get; set; }
        Repository docController = new Repository(AppConstants.RecordType.Client);

        public UcAddDocument()
        {
            InitializeComponent();
            this.crudMessage = new CustomMessages("File");
            this.Caption = "File";
        }

        bool AddFile(string path, string name)
        {
            bool isFileAdded = false;

            if (!docController.DocumentExist(name, SelectedDoc.ParentID, SelectedDoc.RecordID))
            {
                //Add new document
                tblDocumentItem doc = new tblDocumentItem();
                doc.RecordID = SelectedDoc.RecordID;
                doc.Name = name;
                doc.ParentID = SelectedDoc.ParentID;
                doc.IsFolder = false;
                doc.TempByteData = FileHelper.GetByteArray(path);
                doc.Notes = txtDescription.Text;
                isFileAdded = docController.AddFile(doc);

                DisplayManager.DisplayCrudMessage(CrudMessageType.EntitySaved, this.crudMessage);
            }
            else
            {
                if (DisplayManager.DisplayMessage(name + " already exists in DMS. Do you want to replace existing document?", MessageType.Confirmation) == DialogResult.Yes)
                {
                    //update existing document
                    tblDocumentItem doc = docController.FindByName(name, SelectedDoc.ParentID, SelectedDoc.RecordID);
                    doc.TempByteData = FileHelper.GetByteArray(path);
                    doc.ParentID = SelectedDoc.ParentID;
                    doc.Notes = txtDescription.Text;
                    isFileAdded = docController.UpdateFile(doc);
                    DisplayManager.DisplayCrudMessage(CrudMessageType.EntityUpdated, this.crudMessage);
                }
            }
            return isFileAdded;
        }

        void AddRecursive(DirectoryInfo dr, long prntID)
        {
            //if folder doesnt exist in DMS
            if (!docController.DocumentExist(dr.Name, SelectedDoc.ParentID, SelectedDoc.RecordID))
            {
                //Add Folder in repository
                tblDocumentItem doc = new tblDocumentItem();
                doc.RecordID = SelectedDoc.RecordID;
                doc.ParentID = prntID;
                doc.Name = dr.Name;
                doc.IsFolder = true;
                doc.Notes = txtDescription.Text;
                docController.AddFolder(doc);
                
                prntID = doc.ID;
            }

            //Add all files
            foreach (FileInfo f in dr.GetFiles())
            {
                //Add Files
                tblDocumentItem doc = new tblDocumentItem();
                doc.RecordID = SelectedDoc.RecordID;
                doc.Name = f.Name;
                doc.ParentID = prntID;
                doc.IsFolder = false;
                doc.TempByteData = FileHelper.GetByteArray(f.FullName);
                doc.Notes = txtDescription.Text;
                docController.AddFile(doc);
            }

            foreach (DirectoryInfo dir in dr.GetDirectories())
            {
                AddRecursive(dir, prntID);
            }
        }

        #region events

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtFilePath.Text))
            {
                DisplayManager.DisplayMessage("Please select item(s) to import.", MessageType.Error);
                return;
            }

            if (chkImportFolder.Checked)
            {
                if (Directory.Exists(txtFilePath.Text.Trim()))
                {
                    DirectoryInfo dr = new DirectoryInfo(txtFilePath.Text.Trim());
                    AddRecursive(dr, SelectedDoc.ParentID);
                }
                else
                {
                    DisplayManager.DisplayMessage("Folder not found. Please select folder again.", MessageType.Error);
                    return;
                }
            }
            else
            {
                bool delFile = false;
                if (DisplayManager.DisplayMessage("Do you want to delete imported file(s) from your PC?", MessageType.Confirmation) == DialogResult.Yes)
                {
                    delFile = true;
                }

                foreach (var filePath in openFileDialog1.FileNames)
                {
                    if (File.Exists(filePath.Trim()))
                    {
                        string fileName = Path.GetFileName(filePath);
                        if (AddFile(filePath, fileName))
                        {
                            if(delFile)
                                File.Delete(txtFilePath.Text.Trim());
                        }
                    }
                    else
                    {
                        DisplayManager.DisplayMessage("File not found. Please enter valid path.", MessageType.Error);
                        return;
                    }    
                }
            }

            DisplayManager.CloseDialouge(DialogResult.OK);
        }

        private void btnSelectFile_Click(object sender, EventArgs e)
        {
            txtFilePath.Text = string.Empty;
            if (chkImportFolder.Checked)
            {
                if (folderBrowserDialog1.ShowDialog() == DialogResult.OK)
                {
                    txtFilePath.Text = folderBrowserDialog1.SelectedPath;
                }
            }
            else if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                foreach (var filePath in openFileDialog1.FileNames)
                {
                    txtFilePath.Text += string.Format("\"{0}\" , ",Path.GetFileName(filePath));
                }
                txtFilePath.Text = txtFilePath.Text.TrimEnd(',');
            }
        }
        
        #endregion

        private void chkImportFolder_ToggleStateChanged(object sender, Telerik.WinControls.UI.StateChangedEventArgs args)
        {
            txtFilePath.Text = string.Empty;//to avoid error when user change chkImportFolder value after selecting item to import.
        }
    }
}
