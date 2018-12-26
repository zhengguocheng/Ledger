using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using DAL;
using DMS.UserControls;
using System.Diagnostics;
using DMS.CustomClasses;
using Telerik.WinControls.UI;
using WordControls;
using System.IO;

namespace DMS.UserControls
{
    public partial class UcTaggedDocumentList : UserControlBase
    {
        ClientController clntCntrl = new ClientController();
        Repository repCntr = new Repository(AppConstants.RecordType.Client);
        List<VwDocumentList> docList;
        DocBrowser docBrowser = null;
        Tags.TagType tagType;

        public UcTaggedDocumentList(Tags.TagType _tagType)
        {
            InitializeComponent();
            this.Caption = "Document List";
            tagType = _tagType;
        }

        private void UcTaggedDocumentList_Load(object sender, EventArgs e)
        {
            this.DisableCellHighlighting(grdDocuments);
            LoadGrid();
            ShowPreviewWindow(false);
        }

        void LoadGrid()
        {
            docList = repCntr.FindAllTagged(tagType);

            if (docList == null)
                return;

            foreach (VwDocumentList item in docList)
            {
                //item.
                DisplayManager.SetDocumentIcon(item);
            }

            grdDocuments.DataSource = docList;

            grdDocuments.Columns["colName"].FieldName = "Name";
            grdDocuments.Columns["colName"].ReadOnly = true;

            grdDocuments.Columns["colImg"].FieldName = "DocImg";
            grdDocuments.Columns["colImg"].IsVisible = true;

            if (docList != null)
                docList = docList.OrderBy(x => x.Client_Name).ThenByDescending(x => x.IsFolder).ThenBy(x => x.Name).ToList();

            grdDocuments.CurrentRow = null;
        }

        private void grdDocuments_CellClick(object sender, GridViewCellEventArgs e)
        {
            if (e.RowIndex != -1 && e.Column.Name == "colStar")
            {
                VwDocumentList itm = (VwDocumentList)grdDocuments.CurrentRow.DataBoundItem;
                tblDocumentItem docItm = repCntr.Find(itm.ID);
                if (docItm != null)
                {
                    docItm.ToggleStar();
                    int colInd = grdDocuments.Columns["colStar"].Index;

                    e.Row.Cells[colInd].Value = docItm.IsStarred() ? Properties.Resources.goldenStar : Properties.Resources.blackStar;
                }
            }
        }
       
        private void grdDocuments_DoubleClick(object sender, EventArgs e)
        {
            if (grdDocuments.SelectedRows.Count > 0)
            {
                VwDocumentList itm = (VwDocumentList)grdDocuments.CurrentRow.DataBoundItem;
                DisplayManager.LoadControl(new UcNavigation(false, itm.ParentID,itm.ID));
            }
        }

        bool IsPreviewEnabled()
        {
            return !grdSplitter.Panel2Collapsed;
        }

        private void PreviewItem()
        {
            if (!IsPreviewEnabled())
                return;

            //if (!IsRowSelected()) No need to check 
            //    return;

            try
            {
                grdSplitter.Panel2.Controls.Clear();
                VwDocumentList vwItm = (VwDocumentList)grdDocuments.CurrentRow.DataBoundItem;
                tblDocumentItem itm = repCntr.Find(vwItm.ID);

                if (itm != null)
                {
                    if (itm.IsFolder)
                    {
                        //DisplayManager.DisplayMessage("Folder preview is not supported.", MessageType.Error);
                    }
                    else
                    {
                        if (docBrowser != null)
                        {
                            docBrowser.DisposeWebBrowserControl();
                            docBrowser.Dispose();
                        }
                        docBrowser = new DocBrowser();
                        if (docBrowser.IsValidFile(itm.Name))
                        {
                            byte[] data = repCntr.GetFileContent(itm.ID);
                            string path = TempraryDocuments.SaveToTempFolder(itm.Name, data);
                            DirectoryInfo tmpDir = new DirectoryInfo(path);

                            grdSplitter.Panel2.Controls.Clear();
                            grdSplitter.Panel2.Controls.Add(docBrowser);
                            docBrowser.Dock = DockStyle.Fill;
                            docBrowser.Preview(tmpDir.FullName);
                            //DisplayManager.ShowDialouge(b);
                        }
                        else
                        {
                            //DisplayManager.DisplayMessage("Preview of this file type is not supported.", MessageType.Error);
                        }
                    }
                }
            }
            catch (Exception ecp)
            {
                GlobalLogger.logger.LogException(ecp);
            }
            finally
            {

            }
        }

        private void grdDocuments_CurrentRowChanged(object sender, CurrentRowChangedEventArgs e)
        {
            if (chkPreview.Checked)
            {
                PreviewItem();

                //ShowPreviewWindow(true);
             
                //grdDocuments.Refresh();
                //grdSplitter.Panel1.Refresh();
                //grdSplitter.Panel1Collapsed = true;
                //grdSplitter.Panel1Collapsed = false;

                //grdSplitter.Panel1.Controls.Clear();
                //if (grdDocuments != null)
                //{
                //    grdSplitter.Panel1.Controls.Add(grdDocuments);
                //    grdDocuments.Dock = DockStyle.Fill;
                //}
                //grdDocuments.Invalidate();
            }
        }

        private void chkPreview_ToggleStateChanged(object sender, StateChangedEventArgs args)
        {
            if (args.ToggleState == Telerik.WinControls.Enumerations.ToggleState.On)
                ShowPreviewWindow(true);
            else
                ShowPreviewWindow(false);
        }

        void ShowPreviewWindow(bool val)
        {
            grdSplitter.Panel2Collapsed = !val;
            grdSplitter.Panel2.Controls.Clear();
            if (docBrowser != null)
            {
                grdSplitter.Panel2.Controls.Add(docBrowser);
                docBrowser.Dock = DockStyle.Fill;
            }
            grdDocuments.Columns["colNotes"].IsVisible = !val;
            grdDocuments.Columns["colUpdatedAt"].IsVisible = !val;

            if (val)
            {
                PreviewItem();
            }
        }
    }
}
