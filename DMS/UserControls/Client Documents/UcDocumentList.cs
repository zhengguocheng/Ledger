using System;
using System.Collections.Generic;
using System.Windows.Forms;
using DMS.CustomClasses;
using DAL;
using System.IO;
using System.Diagnostics;

namespace DMS.UserControls
{
    //this control is never used. delete it
    public partial class UcDocumentList : UserControlBase
    {
        Repository repository = new Repository(AppConstants.RecordType.Client);
        //VwDocumentList_Controller vwController = new VwDocumentList_Controller();
        long crrentFolderID = 0;
        public DAL.Client SelectedClient { get; set; }

        public UcDocumentList()
        {
            InitializeComponent();
            this.Caption = "Client Document";
            this.crudMessage = new CustomMessages("Document");
        }

        private void UcClientDocuments_Load(object sender, EventArgs e)
        {
            this.DisableCellHighlighting(grdDocuments);
            RefreshGrid();
        }

        public void RefreshGrid(bool loadRoolNode = false)
        {
            if (loadRoolNode)
                crrentFolderID = 0;
            List<VwDocumentList> lst;

            lst = repository.FetchAllView(SelectedClient.ID, crrentFolderID);
            
            foreach (VwDocumentList item in lst)
            {
                //DisplayManager.SetDocumentIcon(item);
            }
            if (SelectedClient != null)
            {
                grdDocuments.DataSource = lst;
            }
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            tblDocumentItem doc;
            if (grdDocuments.SelectedRows.Count > 0)
            {
                VwDocumentList vwDoc = (VwDocumentList)grdDocuments.SelectedRows[0].DataBoundItem;
                doc = repository.Find(vwDoc.ID);
            }
            else
            {
                doc = new tblDocumentItem();
                doc.RecordID = SelectedClient.ID;
                doc.ParentID = crrentFolderID;
            }

            UcAddDocument cnt = new UserControls.UcAddDocument();
            cnt.SelectedDoc = doc;
            
            if(DisplayManager.ShowDialouge(cnt) == DialogResult.OK)
                RefreshGrid();
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            
            //if (grdDocuments.SelectedRows.Count > 0)
            //{
            //    VwDocumentList vwDoc = (VwDocumentList)grdDocuments.SelectedRows[0].DataBoundItem;

            //    tblDocumentItem doc = repository.Find(vwDoc.ID);
            //    repository.Delete(doc);
            //    RefreshGrid();
            //    DisplayManager.DisplayCrudMessage(CrudMessageType.EntityDeleted, this.crudMessage);
            //}
        }

        private void btnDownload_Click(object sender, EventArgs e)
        {
            if (grdDocuments.SelectedRows.Count > 0)
            {
                VwDocumentList vwDoc = (VwDocumentList)grdDocuments.SelectedRows[0].DataBoundItem;
                tblDocumentItem doc = repository.Find(vwDoc.ID);
                
                saveFileDialog1.FileName = doc.Name;
                string filePath = null;

                if (saveFileDialog1.ShowDialog() == DialogResult.OK)
                {
                    filePath = saveFileDialog1.FileName;
                    FileHelper.WriteToTempFile(filePath, repository.GetFileContent(doc.ID));
                    if (!string.IsNullOrEmpty(filePath))
                    {
                        Process.Start(filePath);
                        DisplayManager.DisplayMessage("Record downloaded successfully.", MessageType.Success);
                    }
                }
            }
        }

        private void btnAddFolder_Click(object sender, EventArgs e)
        {
            tblDocumentItem doc;
            if (grdDocuments.SelectedRows.Count > 0)
            {
                VwDocumentList vwDoc = (VwDocumentList)grdDocuments.SelectedRows[0].DataBoundItem;
                doc = repository.Find(vwDoc.ID);
            }
            else
            {
                doc = new tblDocumentItem();
                doc.RecordID = SelectedClient.ID;
                doc.ParentID = crrentFolderID;
            }

            UcSaveFolder cnt = new UserControls.UcSaveFolder();
            doc.ParentID = crrentFolderID;
            cnt.SelectedDoc = doc;

            if (DisplayManager.ShowDialouge(cnt) == DialogResult.OK)
                RefreshGrid();
        }

        private void grdDocuments_DoubleClick(object sender, EventArgs e)
        {
            if (grdDocuments.SelectedRows.Count > 0)
            {
                VwDocumentList vwDoc = (VwDocumentList)grdDocuments.SelectedRows[0].DataBoundItem;
                if (vwDoc.IsFolder)
                {
                    crrentFolderID = vwDoc.ID;
                    RefreshGrid();
                }
            }
        }

        private void btnBack_Click(object sender, EventArgs e)
        {
            if (crrentFolderID != 0)
            {
                crrentFolderID = repository.Find(crrentFolderID).ParentID;
                RefreshGrid();
            }
        }
    }
}
