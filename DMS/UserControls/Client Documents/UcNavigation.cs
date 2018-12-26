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
using DMS.UIForms;
using DMS.Reports;
using DAL.Controllers;

namespace DMS
{
    public partial class UcNavigation : UserControlBase
    {
        ClientController clntCntrl = new ClientController();
        LedgerRepository repCntr = null;//new Repository(AppConstants.RecordType.Client);
        DocBrowser docBrowser = new DocBrowser();

        long cfID;
        long CurrentFolderID
        {
            get
            {
                return cfID;
            }
            set
            {
                //handle duplicate set commands
                if (value != cfID)//it means new value is coming
                    preFolderID = cfID;
                cfID = value;
            }
        }

        long preFolderID = 0;//used for 
        long currentDocID = 0;//only used first time to select specific document in grid
        bool isPopupMode = false;
        bool fromLoadEvent = false;
        long initialFolderID = 0;//used to change color of selected folder in treeview

        tblDocumentItem PopupSelection { get; set; }

        List<tblDocumentItem> docList;
        List<tblDocumentItem> clipBoardList = new List<tblDocumentItem>();
        MenuAction lastAction;
        LstSource lstSrcTyp;
        long? copySrcFolderID = null;

        List<DropDownItem> lstRecentDirs = new List<DropDownItem>();

        public UcNavigation(bool _isPopupMode, long prntFolID = 0, long _currentDocID = 0)
        {
            InitializeComponent();

            //this.DoubleBuffered = true;
            SetStyle(ControlStyles.OptimizedDoubleBuffer |
                        ControlStyles.UserPaint |
                        ControlStyles.AllPaintingInWmPaint, true);

            //AppConstants.RecordType rtype = AppConstants.IsLedger ? AppConstants.RecordType.Ledger : AppConstants.RecordType.Client;
            repCntr = new LedgerRepository();

            grdSplitter.Panel2Collapsed = true;
            this.Caption = "Client List";
            this.crudMessage = new CustomMessages("Item");
            isPopupMode = _isPopupMode;
            CurrentFolderID = prntFolID;
            currentDocID = _currentDocID;
            initialFolderID = prntFolID;

            AddLedgerControl(btnNewSheet);
        }

        private void UcNavigation_Load(object sender, EventArgs e)
        {
            this.DisableCellHighlighting(grdDocuments);
            chkShowTreeView.Checked = true;
            pnlDocManagment.Visible = !isPopupMode;
            ShowEditButton(false);  //by default it will be hidden. When user check item it get visible
            pnlPopupButtons.Visible = isPopupMode;

            LoadGrid();
            treeView1.Nodes.Clear();

            if (CurrentFolderID == 0)
            {
                //List<tblDocumentItem> lst = repCntr.FetchAll().OrderBy(x => x.ParentID).OrderBy(x => x.ID).ToList();
                //PopulateNodes(lst, treeView1.Nodes);
                PopulateNodes(docList, treeView1.Nodes);
            }
            else
            {
                PopulateNodes(repCntr.FetchAllRooteNotes(), treeView1.Nodes);
            }

            grdDocuments.ShowFilteringRow = false;

            fromLoadEvent = true;
            TreeNode selectedNode = (treeView1.Nodes.Find(CurrentFolderID.ToString(), true))[0];
            selectedNode.ForeColor = Color.Blue;
            treeView1.SelectedNode = selectedNode;
            fromLoadEvent = false;
        }

        #region Load and Bind Grid
        tblDocumentItem openedFolder = null;

        void CreateFiles()
        {
            var opndFol = repCntr.Find(CurrentFolderID);

            if (opndFol != null && string.Equals(opndFol.Name, AppConstants.LedgerFolder.Reports))
            {
                SpecialFiles.AddReports(CurrentFolderID);
            }
        }

        void LoadGrid()
        {
            CreateFiles();

            SetDocList_FromDB();
            SetDocOrderInList();
            BindGrid();
            //LoadClientInfo();
            grdDocuments.CurrentRow = null;
            ShowEditButton(false);
            string parentname = null;

            #region Load Grid

            if (docList != null)
            {
                openedFolder = repCntr.Find(CurrentFolderID);
                if (openedFolder == null)
                    return;


                parentname = repCntr.FindFullPath(openedFolder);

                if (lstRecentDirs.FirstOrDefault(x => x.Value == openedFolder.ID.ToString()) == null)
                {
                    lstRecentDirs.Insert(0, new DropDownItem { DisplayText = parentname, Value = openedFolder.ID.ToString() });

                    drpDocPath.DataSource = lstRecentDirs;
                    drpDocPath.DisplayMember = "DisplayText";
                    drpDocPath.ValueMember = "Value";
                    drpDocPath.Rebind();

                    if (lstRecentDirs.Count > 0)
                        drpDocPath.SelectedIndex = 0;
                }

                txtClientInfo.Text = string.Empty;
                var nameArr = parentname.Split(">".ToCharArray(), StringSplitOptions.RemoveEmptyEntries);
                for (int i = 0; i < 2 && i < nameArr.Length; i++)
                {
                    txtClientInfo.Text += nameArr[i] + " - ";
                }
                txtClientInfo.Text = txtClientInfo.Text.TrimEnd().TrimEnd("-".ToCharArray());
            }

            if (currentDocID > 0)
            {
                SetCurrentRow(currentDocID);
                currentDocID = 0;//set value to 0 after selecting document first time.
            }
            #endregion

            #region Button Visibility

            ButtonVisibility();

            btnNewSheet.Text = "New Sheet";

            if (!string.IsNullOrWhiteSpace(parentname))
            {
                string ledgerFol = null;
                var nameArr = parentname.Split(">".ToCharArray(), StringSplitOptions.RemoveEmptyEntries);
                if (nameArr.Length >= 3)
                    ledgerFol = nameArr[2].Trim();

                if (string.Equals(ledgerFol, AppConstants.LedgerFolder.Payment))//if Payment Folder is opened
                {
                    btnNewSheet.Tag = AppConstants.LedgerFolder.Payment;
                    btnNewSheet.Text = "New Payment";
                }
                if (string.Equals(ledgerFol, AppConstants.LedgerFolder.Receipts))//if Receipts Folder is opened
                {
                    btnNewSheet.Tag = AppConstants.LedgerFolder.Receipts;
                    btnNewSheet.Text = "New Receipt";
                }
                if (string.Equals(ledgerFol, AppConstants.LedgerFolder.SDB))//if Receipts Folder is opened
                {
                    btnNewSheet.Tag = AppConstants.LedgerFolder.SDB;
                    btnNewSheet.Text = "New Sheet";
                }
            }

            #endregion

            //In case of ledger if rootnode dont have any elements then display UcCreateYearEndFolder
            if (docList.Count == 0 && repCntr.recordType == AppConstants.RecordType.Ledger && openedFolder != null && openedFolder.ParentID == 0)
            {
                btnAddYearEndFolder_Click(null, null);
            }



            TreeNode[] arr = treeView1.Nodes.Find(CurrentFolderID.ToString(), true);
            if (arr.Length > 0)
            {
                PopulateSubLevel(CurrentFolderID, arr[0]);
                arr[0].EnsureVisible();
                arr[0].ForeColor = Color.Red;
            }

            arr = treeView1.Nodes.Find(preFolderID.ToString(), true);
            if (arr.Length > 0)
            {
                arr[0].ForeColor = Color.Blue;
            }
        }

        void BindGrid()
        {
            if (docList == null)
                return;

            foreach (tblDocumentItem item in docList)
            {
                DisplayManager.SetDocumentIcon(item);
            }

            grdDocuments.DataSource = docList;

            grdDocuments.Columns["colName"].FieldName = "Name";
            grdDocuments.Columns["colName"].ReadOnly = true;

            grdDocuments.Columns["colImg"].FieldName = "DocImg";
            grdDocuments.Columns["colImg"].IsVisible = true;

            //grdDocuments.Columns["colSelect"].FieldName = "IsSelected";
            //grdDocuments.Columns["colSelect"].ReadOnly = false;

        }

        void SetDocList_FromDB()
        {
            btnBack.Enabled = !(CurrentFolderID == 0);
            docList = repCntr.FetchChildren(CurrentFolderID);
        }

        void SetDocOrderInList()
        {
            if (docList != null)
            {
                //Ivan: 27/6/16: Folders of each client should be sorted in alphabetical order automatically all the time.
                //If all items are folder then sort by name.
                if (docList.All(x => x.IsFolder))
                {
                    docList = docList.OrderBy(x => x.Name).ToList();
                    return;
                }
            }

            if (docList != null)
                docList = docList.OrderByDescending(x => x.OrderNO).ThenByDescending(x => x.CreatedAT).ThenByDescending(x => x.IsFolder).ThenBy(x => x.Name).ToList();

            #region set order where it is null
            //Trial Balance order is static
            if (docList != null && docList.Any(x => x.IsTagged(x.ItemTag, Tags.TagType.ThisYearClosingTrialBal)))
            {
                tblDocumentItem item = null;

                item = docList.FirstOrDefault(x => x.IsTagged(x.ItemTag, Tags.TagType.ThisYearClosingTrialBal));
                if (item != null)
                    item.OrderNO = 4;

                item = docList.FirstOrDefault(x => x.IsTagged(x.ItemTag, Tags.TagType.OpeningTrialBal));
                if (item != null)
                    item.OrderNO = 3;

                item = docList.FirstOrDefault(x => x.IsTagged(x.ItemTag, Tags.TagType.LastYearClosingTrialBal));
                if (item != null)
                    item.OrderNO = 2;

                item = docList.FirstOrDefault(x => x.IsTagged(x.ItemTag, Tags.TagType.NextYearOpeningTrialBal));
                if (item != null)
                    item.OrderNO = 1;
            }

            else
            {
                int? maxOrder = docList.Max(x => x.OrderNO);

                if (maxOrder == null)
                {
                    maxOrder = 1;
                }

                if (CurrentFolderID > 0)
                {
                    for (int i = docList.Count - 1; i >= 0; i--)
                    {
                        if (!docList[i].OrderNO.HasValue)
                        {
                            docList[i].OrderNO = ++maxOrder;
                        }
                    }

                    repCntr.BatchUpdate(docList, false);
                }
            }
            #endregion

            if (docList != null)
                docList = docList.OrderByDescending(x => x.OrderNO).ThenByDescending(x => x.IsFolder).ThenBy(x => x.Name).ToList();

        }

        string clntName = null;
        void LoadClientInfo()
        {
            txtClientInfo.Text = string.Empty;
            var docItem = repCntr.Find(CurrentFolderID);
            if (docItem != null)
            {
                var clnt = clntCntrl.Find(docItem.RecordID);
                if (clnt != null)
                {
                    clntName = clnt.Client_Name;
                    txtClientInfo.Text = string.Format("Reference: {0}    UTR: {1}    Company No: {2}", clnt.Reference, clnt.UTR, clnt.Company_Number);
                }
            }

        }
        #endregion

        #region Custom Methods

        void LoadFolder(long folderID)
        {
            CurrentFolderID = folderID;
            LoadGrid();
        }
        void SwapOrder(int currentIndex, int newIndex)
        {
            if (grdDocuments.SelectedRows.Count != 1)
            {

            }
            if (currentIndex >= 0 && currentIndex < grdDocuments.Rows.Count
                && newIndex >= 0 && newIndex < grdDocuments.Rows.Count)
            {
                tblDocumentItem currentRow = (tblDocumentItem)grdDocuments.Rows[currentIndex].DataBoundItem;
                tblDocumentItem row2 = (tblDocumentItem)grdDocuments.Rows[newIndex].DataBoundItem;

                int? n = currentRow.OrderNO;
                currentRow.OrderNO = row2.OrderNO;
                row2.OrderNO = n;

                SetDocOrderInList();
                BindGrid();
                SetCurrentRow(currentRow.ID);

                repCntr.BatchUpdate(new List<tblDocumentItem> { currentRow, row2 });
            }
        }


        List<tblDocumentItem> GetSelected_GridItem()
        {
            List<tblDocumentItem> selList = new List<tblDocumentItem>();
            if (grdDocuments.SelectedRows.Count > 0)
            {
                foreach (GridViewRowInfo row in grdDocuments.SelectedRows)
                {
                    tblDocumentItem itm = (tblDocumentItem)row.DataBoundItem;
                    selList.Add(itm);
                }
                return selList;
            }
            else
                return null;

            #region Commented value on bases of check box
            //foreach (GridViewDataRowInfo row in grdDocuments.Rows)
            //{
            //    string value = row.Cells[0].Value.ToString();
            //    if (String.Compare(value, "true", true) == 0)
            //        return (tblDocumentItem)row.DataBoundItem;
            //}

            //return null;
            #endregion
        }

        bool IsRowSelected()
        {
            return GetSelected_GridItem() != null;
        }

        bool IsSingleRowSelected()
        {
            var lst = GetSelected_GridItem();
            if (lst == null)
                return false;
            if (lst.Count > 1)
                return false;
            else
                return true;
        }

        bool IsSingleFileSelected()
        {
            if (IsSingleRowSelected())
            {
                var doc = GetSelected_GridItem()[0];
                if (doc.IsFolder == false)
                    return true;
            }
            return false;
        }

        void LoadGridAndTree()
        {
            LoadGrid();

            TreeNode[] arr = treeView1.Nodes.Find(CurrentFolderID.ToString(), true);
            if (arr.Length > 0)
                PopulateSubLevel(CurrentFolderID, arr[0]);
        }

        void ShowEditButton(bool val)
        {
            btnDelete.Visible = btnRename.Visible = pnlDocUpDown.Visible = val;
            grdMenuDelete.Visible = grdMenuRename.Visible = grdMenuCopy.Visible = grdMenuCut.Visible = grdMenuPaste.Visible = val;
        }

        void ButtonVisibility()
        {

            btnNewDrp_DMS.Visible = btnNewSheet.Visible = btnAddYearEndFolder.Visible = btnDelete.Visible = btnRename.Visible = btnCopy.Visible = btnCut.Visible = btnPaste.Visible = btnAddFold.Visible = pnlDocUpDown.Visible = btnLockUnlock.Visible = false;
            grdMenuDelete.Visible = grdMenuRename.Visible = grdMenuCopy.Visible = grdMenuCut.Visible = grdMenuPaste.Visible = false;

            //var openedFolder = repCntr.Find(CurrentFolderID);
            if (openedFolder == null)
                return;

            if (openedFolder.ParentID == 0)//If clients top most folder is opened
            {
                btnAddYearEndFolder.Visible = btnDelete.Visible = true;
                return;
            }
            if (openedFolder.IsTagged(openedFolder.ItemTag, Tags.TagType.YearEndFolder))//Disable edit buttons if Year End folder is opened
            {
                //noting should be visible
                return;
            }
            if (openedFolder.Name == AppConstants.LedgerFolder.LedgerReportFolder || openedFolder.Name == AppConstants.LedgerFolder.Trial_Balance)//Disable edit buttons if Year End folder is opened
            {
                //noting should be visible
                return;
            }
            if (openedFolder.Name == AppConstants.LedgerFolder.Payment || openedFolder.Name == AppConstants.LedgerFolder.Receipts || openedFolder.Name == AppConstants.LedgerFolder.SDB)
            {
                btnNewSheet.Visible = btnDelete.Visible = btnRename.Visible = btnCopy.Visible = btnCut.Visible = btnAddFold.Visible = pnlDocUpDown.Visible = btnLockUnlock.Visible = true;
                btnPaste.Visible = copySrcFolderID.HasValue;

                //if (IsSingleFileSelected())
                //{
                //    grdMenuCopy.Visible = grdMenuCut.Visible = true;
                //    grdMenuPaste.Visible = copySrcFolderID.HasValue;
                //}

                return;
            }

            if (openedFolder.Name == AppConstants.LedgerFolder.Notes || repCntr.IsChildOf(openedFolder.ID,Tags.TagType.NotesFolder))
            {
                btnNewDrp_DMS.Visible = btnDelete.Visible = btnRename.Visible = btnCopy.Visible = btnCut.Visible = btnAddFold.Visible = pnlDocUpDown.Visible = true;
                btnPaste.Visible = clipBoardList.Count > 0;
                return;
            }
            

            if (repCntr.IsChildOf(openedFolder.ID, AppConstants.LedgerFolder.Payment) || repCntr.IsChildOf(openedFolder.ID, AppConstants.LedgerFolder.Receipts) || repCntr.IsChildOf(openedFolder.ID, AppConstants.LedgerFolder.SDB))
            {
                btnNewSheet.Visible = btnDelete.Visible = btnRename.Visible = btnCopy.Visible = btnCut.Visible = btnAddFold.Visible = pnlDocUpDown.Visible = btnLockUnlock.Visible = true;
                btnPaste.Visible = copySrcFolderID.HasValue;

                //if (IsSingleFileSelected())
                //{
                //    grdMenuCopy.Visible = grdMenuCut.Visible = true;
                //    grdMenuPaste.Visible = copySrcFolderID.HasValue;
                //}
                return;
            }



        }


        void SetCurrentRow(long docID)
        {
            GridViewRowInfo r = grdDocuments.Rows.FirstOrDefault(x => ((tblDocumentItem)x.DataBoundItem).ID == docID);
            if (r != null)
                grdDocuments.CurrentRow = r;
        }

        #endregion

        #region Tree Load

        private void PopulateSubLevel(long parentid, TreeNode parentNode)
        {
            if (parentNode.Nodes != null && parentNode.Nodes.Count > 0)
            {
                parentNode.Nodes.Clear();
            }
            List<tblDocumentItem> lst = repCntr.FetchChildren((tblDocumentItem)parentNode.Tag);
            lst = lst.OrderByDescending(x => x.IsFolder).OrderBy(x => x.Name).ToList();
            PopulateNodes(lst, parentNode.Nodes);
        }

        private void PopulateNodes(List<tblDocumentItem> lst, TreeNodeCollection nodes)
        {
            lst = lst.OrderByDescending(x => x.IsFolder).OrderBy(x => x.Name).ToList();

            foreach (tblDocumentItem itm in lst)
            {
                TreeNode tn = new TreeNode();
                tn.Text = itm.Name.ToString();
                tn.Name = itm.ID.ToString();
                tn.Tag = itm;
                tn.SelectedImageIndex = tn.ImageIndex = (itm.IsFolder) ? 0 : 1;

                if (!itm.IsRootNode)
                {
                    TreeNode[] arr = treeView1.Nodes.Find(itm.ParentID.ToString(), true);
                    if (arr != null && arr.Length > 0)
                        arr[0].Nodes.Add(tn);
                }
                else
                    nodes.Add(tn);
            }
        }

        #endregion

        #region Events

        private void grdDocuments_DoubleClick(object sender, EventArgs e)
        {
            if (!IsRowSelected())
                return;

            tblDocumentItem itm = (tblDocumentItem)grdDocuments.CurrentRow.DataBoundItem;
            if (itm != null)
            {
                if (itm.IsFolder)
                {
                    CurrentFolderID = itm.ID;
                    LoadGrid();
                }
                else if (itm.IsVirtual == true)
                {
                    OpenVirtualFile(itm, this);

                    if (itm.IsTagged(itm.ItemTag, Tags.TagType.LedgerReport))
                    {
                        var clientID = repCntr.Find(CurrentFolderID).RecordID;
                        var rpt = new UcLedgerReport(CurrentFolderID, clientID);
                        DisplayManager.LoadControl(rpt, false, this);
                    }
                    else if (itm.IsTagged(itm.ItemTag, Tags.TagType.VatReport))
                    {
                        var rpt = new UcVATReport(CurrentFolderID);
                        DisplayManager.LoadControl(rpt, false, this);
                    }
                    else if (itm.IsTagged(itm.ItemTag, Tags.TagType.AnalysisReport))
                    {
                        var clientID = repCntr.Find(CurrentFolderID).RecordID;
                        var rpt = new UcAnalysisReport(CurrentFolderID, clientID);
                        DisplayManager.LoadControl(rpt, false, this);
                    }
                }
                else
                {
                    byte[] data = repCntr.GetFileContent(itm.ID);
                    TempraryDocuments.OpenMonitorFile(itm.Name, itm.ID, data, new Repository(AppConstants.RecordType.Client));
                }
            }
        }

        public static void OpenVirtualFile(tblDocumentItem itm, UserControlBase parentCntrl)
        {
            if (itm.IsTagged(itm.ItemTag, Tags.TagType.PettyCash_FileType))
            {
                DisplayManager.LoadControl(new UcPettyCash(itm.ID), false, parentCntrl);
            }
            if (itm.IsTagged(itm.ItemTag, Tags.TagType.Banking_FileType))
            {
                DisplayManager.LoadControl(new UcBanking(itm.ID), false, parentCntrl);
            }
            if (itm.IsTagged(itm.ItemTag, Tags.TagType.BankPayment_FileType))
            {
                DisplayManager.LoadControl(new UcBankPayment(itm.ID), false, parentCntrl);
            }
            if (itm.IsTagged(itm.ItemTag, Tags.TagType.PCB_FileType))
            {
                DisplayManager.LoadControl(new UcPCB(itm.ID), false, parentCntrl);
            }
            if (itm.IsTagged(itm.ItemTag, Tags.TagType.Ledger_FileType))
            {
                DisplayManager.LoadControl(new UcLedger(itm.ID, ""), false, parentCntrl);
            }
            if (itm.IsTagged(itm.ItemTag, Tags.TagType.BankReconcile_FileType))
            {
                DisplayManager.LoadControl(new UcBankReconcile(itm.ID), false, parentCntrl);
            }
            if (itm.IsTagged(itm.ItemTag, Tags.TagType.Payment_FileType))
            {
                DisplayManager.LoadControl(new UcExcelSheet(itm.ID, "Payment"), false, parentCntrl);
            }
            if (itm.IsTagged(itm.ItemTag, Tags.TagType.SDB_FileType))
            {
                DisplayManager.LoadControl(new UcSDB(itm.ID, "SDB"), false, parentCntrl);
            }
            if (itm.IsTagged(itm.ItemTag, Tags.TagType.Receipts_FileType))
            {
                DisplayManager.LoadControl(new UcExcelSheet(itm.ID, "Receipts"), false, parentCntrl);
            }
            if (itm.IsTagged(itm.ItemTag, Tags.TagType.TrialBal_FileType))
            {
                if (itm.IsTagged(itm.ItemTag, Tags.TagType.NextYearOpeningTrialBal))
                    DisplayManager.LoadControl(new UcRptNextYrOpnBal(itm.ID, ""), false, parentCntrl);


                if (itm.IsTagged(itm.ItemTag, Tags.TagType.OpeningTrialBal))
                    DisplayManager.LoadControl(new UcTrialBalance(itm.ID, Tags.TagType.OpeningTrialBal), false, parentCntrl);

                if (itm.IsTagged(itm.ItemTag, Tags.TagType.LastYearClosingTrialBal))
                    DisplayManager.LoadControl(new UcClosingTrialBalance(itm.ID, Tags.TagType.LastYearClosingTrialBal), false, parentCntrl);

                if (itm.IsTagged(itm.ItemTag, Tags.TagType.ThisYearClosingTrialBal))
                    DisplayManager.LoadControl(new UcRptThisYrClosingTrialBal(itm.ID), false, parentCntrl);
            }
            if (itm.IsTagged(itm.ItemTag, Tags.TagType.Journal_FileType))
            {
                if (itm.IsTagged(itm.ItemTag, Tags.TagType.DoubleEntriesJournal))
                    DisplayManager.LoadControl(new UcJournal(itm.ID, Tags.TagType.DoubleEntriesJournal), false, parentCntrl);

                else if (itm.IsTagged(itm.ItemTag, Tags.TagType.AccrualJournal))
                    DisplayManager.LoadControl(new UcJournal(itm.ID, Tags.TagType.AccrualJournal), false, parentCntrl);

                else if (itm.IsTagged(itm.ItemTag, Tags.TagType.PrepaymentJournal))
                    DisplayManager.LoadControl(new UcJournal(itm.ID, Tags.TagType.PrepaymentJournal), false, parentCntrl);

                else if (itm.IsTagged(itm.ItemTag, Tags.TagType.MultipleEntriesJournal))
                    DisplayManager.LoadControl(new UcMultipleEntJournal(itm.ID, Tags.TagType.MultipleEntriesJournal), false, parentCntrl);

            }

        }

        private void treeView1_NodeMouseClick(object sender, TreeNodeMouseClickEventArgs e)
        {
            if (!e.Node.IsExpanded)
            {
                tblDocumentItem itm = (tblDocumentItem)e.Node.Tag;
                if (itm.IsFolder)
                    PopulateSubLevel(long.Parse(e.Node.Name), e.Node);
            }
        }

        private void treeView1_AfterSelect(object sender, TreeViewEventArgs e)
        {
            tblDocumentItem selItm = ((tblDocumentItem)e.Node.Tag);
            if (selItm.IsFolder)
            {
                //load folder items
                CurrentFolderID = selItm.ID;
                SetDocList_FromDB();
                LoadGrid();
            }
            else
            {
                //load file parent folder
                CurrentFolderID = selItm.ParentID;
                SetDocList_FromDB();
                LoadGrid();
                //set current row to file
                SetCurrentRow(selItm.ID);
            }

            if (!fromLoadEvent)
            {
                TreeNode[] lst = treeView1.Nodes.Find(initialFolderID.ToString(), true);
                if (lst.Length > 0)
                {
                    TreeNode selectedNode = (treeView1.Nodes.Find(initialFolderID.ToString(), true))[0];
                    selectedNode.ForeColor = Color.Black;
                }
            }
        }

        private void btnBack_Click(object sender, EventArgs e)
        {
            if (CurrentFolderID == 0)//Allready on top hirarchy
            {
                return;
            }
            else
            {
                CurrentFolderID = repCntr.Find(CurrentFolderID).ParentID;
            }

            LoadGrid();
        }



        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (!AuthenticationService.IsAdminLogin())
            {
                DisplayManager.DisplayMessage("You are not allowed to delete the document(s). Please ask administrator for help.", MessageType.Error);
                return;
            }

            if (!IsRowSelected())
            {
                DisplayManager.DisplayMessage("Please select an item first.", MessageType.Error);
                return;
            }

            if (DisplayManager.DisplayMessage(CustomMessages.DeleteConfirmation, MessageType.Confirmation) != DialogResult.Yes)
                return;

            List<tblDocumentItem> itemList = GetSelected_GridItem();

            if (itemList != null && itemList.Count == 0)
            {
                DisplayManager.DisplayMessage("Please select a document first.", MessageType.Error);
                return;
            }

            foreach (tblDocumentItem doc in itemList)
            {
                CurrentFolderID = doc.ParentID;
                if (doc != null)
                {

                    if (doc.IsRootNode)
                    {
                        DisplayManager.DisplayMessage("You cannot delete this folder as it is directly mapped to client.", MessageType.Error);
                        return;
                    }

                    var lstLockedNames = new List<string>();
                    repCntr.MoveToRecycleBin(doc, lstLockedNames);
                    LoadGridAndTree();

                    DisplayManager.DisplayCrudMessage(CrudMessageType.EntityDeleted, this.crudMessage);
                    if (lstLockedNames.Count > 0)
                    {
                        var msg = string.Empty;
                        foreach (var item in lstLockedNames)
                        {
                            msg += item + ",";
                        }
                        var s = string.Format("Locked files ({0}) cannot be deleted. Please unlock these files before deleting.", msg.TrimEnd(",".ToCharArray()));
                        DisplayManager.DisplayMessage(s, MessageType.Error);
                    }
                }
                else
                    DisplayManager.DisplayMessage("Please select a document first.", MessageType.Error);
            }
        }

        private void btnAddFold_Click(object sender, EventArgs e)
        {
            tblDocumentItem doc = new tblDocumentItem();
            doc.RecordID = repCntr.Find(CurrentFolderID).RecordID;
            doc.ParentID = CurrentFolderID;

            UcSaveFolder cnt = new UserControls.UcSaveFolder();
            doc.ParentID = CurrentFolderID;
            cnt.SelectedDoc = doc;

            if (DisplayManager.ShowDialouge(cnt) == DialogResult.OK)
                LoadGrid();

        }

        private void btnAddYearEndFolder_Click(object sender, EventArgs e) 
        {

            tblDocumentItem doc = new tblDocumentItem();
            doc.RecordID = repCntr.Find(CurrentFolderID).RecordID;
            doc.ParentID = CurrentFolderID;

            UcCalender cnt = new UserControls.UcCalender();
            doc.ParentID = CurrentFolderID;
            cnt.SelectedDoc = doc;

            if (DisplayManager.ShowDialouge(cnt) == DialogResult.OK)
            {
                LoadGrid();
            }
        }

        private void btn_MouseEnter(object sender, EventArgs e)
        {
            Label cnt = (Label)sender;
            cnt.ForeColor = System.Drawing.SystemColors.Highlight;
        }

        private void btn_MouseLeave(object sender, EventArgs e)
        {
            Label cnt = (Label)sender;
            cnt.ForeColor = Color.Black;
        }

        private void btnOk_Click(object sender, EventArgs e)
        {
            //if (isPopupMode)
            //{
            //    tblDocumentItem itm = GetSelected_GridItem();
            //    if (!itm.IsFolder)
            //    {
            //        DisplayManager.DisplayMessage("Please select folder.", MessageType.Error);
            //        return;
            //    }
            //    else
            //    {
            //        PopupSelection = GetSelected_GridItem();
            //        Form parent = (Form)this.ParentForm;
            //        parent.DialogResult = DialogResult.OK;
            //    }
            //}
        }



        private void btnRename_Click(object sender, EventArgs e)
        {
            if (!IsRowSelected())
            {
                DisplayManager.DisplayMessage("Please select an item first.", MessageType.Error);
                return;
            }

            if (GetSelected_GridItem().Count > 1)
            {
                DisplayManager.DisplayMessage("Please select a single document to rename.", MessageType.Error);
                return;
            }

            tblDocumentItem doc = GetSelected_GridItem()[0];

            if (doc != null)
            {
                UcEdit cnt = new UcEdit(false);
                cnt.NewName = doc.Name;
                if (DisplayManager.ShowDialouge(cnt) == DialogResult.OK)
                {
                    doc.Name = cnt.NewName;
                    if (doc.IsFolder)
                        repCntr.UpdateFolder(doc);
                    else
                        repCntr.UpdateFile(doc);

                    LoadGrid();
                    DisplayManager.DisplayMessage("Item renamed successfully.", MessageType.Success);
                }
            }
        }

        private void treeView1_AfterCheck(object sender, TreeViewEventArgs e)
        {
            //ShowEditButton(e.Node.Checked);
        }



        private void grdDocuments_CurrentRowChanged(object sender, CurrentRowChangedEventArgs e)
        {
            //todo: current row
            ShowEditButton(e.CurrentRow != null);
            ButtonVisibility();
        }

        void grdDocuments_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                Point p = (sender as Control).PointToScreen(e.Location);
                grdContextMenu.Show(p.X, p.Y);
            }
        }

        private void grdMenuDelete_Click(object sender, EventArgs e)
        {
            btnDelete_Click(null, null);
        }

        private void grdMenuRename_Click(object sender, EventArgs e)
        {
            btnRename_Click(null, null);
        }

        private void cbShowFilter_ToggleStateChanged(object sender, StateChangedEventArgs args)
        {
            if (args.ToggleState == Telerik.WinControls.Enumerations.ToggleState.On)
                grdDocuments.ShowFilteringRow = true;
            else
                grdDocuments.ShowFilteringRow = false;
        }

        private void chkShowTreeView_ToggleStateChanged(object sender, StateChangedEventArgs args)
        {
            if (args.ToggleState == Telerik.WinControls.Enumerations.ToggleState.On)
            {
                if (this.radSplitContainer.Controls.Count < 2)
                {
                    this.radSplitContainer.Controls.Clear();
                    this.radSplitContainer.Controls.Add(this.splitPanel1);
                    this.radSplitContainer.Controls.Add(this.splitPanel2);
                }
            }
            else
            {
                this.radSplitContainer.Controls.Remove(this.splitPanel1);
            }
        }

        private void btnTreeSearch_Click(object sender, EventArgs e)
        {
            List<TreeNode> lst = new List<TreeNode>();
            string matchStr = txtTreeSearch.Text.Trim().ToLower();
            foreach (TreeNode node in treeView1.Nodes)
            {
                if (node != null && node.Text.ToLower().Contains(matchStr))
                    lst.Add(node);
            }

            treeView1.Nodes.Clear();

            foreach (TreeNode node in lst)
                treeView1.Nodes.Add(node);

            if (treeView1.Nodes.Count > 0)
                treeView1.SelectedNode = treeView1.Nodes[0];
        }

        private void btnTreeClear_Click(object sender, EventArgs e)
        {
            UcNavigation_Load(null, null);
        }

        private void grdDocuments_CellEndEdit(object sender, GridViewCellEventArgs e)
        {
            try
            {
                tblDocumentItem itm = (tblDocumentItem)e.Row.DataBoundItem;

                if (itm.IsFolder ? repCntr.UpdateFolder(itm) : repCntr.UpdateFile(itm))
                {
                    //LoadGrid();
                }
            }
            catch (Exception ecp)
            {
                HandleException(ecp);
            }
        }

        private void grdDocuments_CellValidating(object sender, CellValidatingEventArgs e)
        {
            if (e.Value != null && grdDocuments.CurrentCell != null && grdDocuments.CurrentCell.ColumnInfo.Name == "colOrder")
            {
                try
                {
                    int v;
                    if (!int.TryParse(e.Value.ToString(), out v))
                    {
                        DisplayManager.DisplayMessage("Please enter valid integer. Press 'Escape' to cancel.", MessageType.Error);
                        e.Cancel = true;
                        grdDocuments.CancelEdit();
                    }
                }
                catch (Exception ecp)
                {
                    e.Cancel = true;
                }
            }
        }

        private void btnUp_Click(object sender, EventArgs e)
        {
            if (grdDocuments.CurrentRow != null)
                SwapOrder(grdDocuments.CurrentRow.Index, grdDocuments.CurrentRow.Index - 1);
        }

        private void btnDown_Click(object sender, EventArgs e)
        {
            if (grdDocuments.CurrentRow != null)
                SwapOrder(grdDocuments.CurrentRow.Index, grdDocuments.CurrentRow.Index + 1);
        }

        private void grdDocuments_CellClick(object sender, GridViewCellEventArgs e)
        {
            if (e.RowIndex != -1 && e.Column.Name == "colStar")
            {
                int colInd = grdDocuments.Columns["colStar"].Index;

                tblDocumentItem itm = (tblDocumentItem)e.Row.DataBoundItem;

                itm.ToggleStar();

                e.Row.Cells[colInd].Value = itm.IsStarred() ? Properties.Resources.goldenStar : Properties.Resources.blackStar;
            }
        }

        private void drpDocPath_SelectedIndexChanged(object sender, Telerik.WinControls.UI.Data.PositionChangedEventArgs e)
        {
            if (drpDocPath.SelectedValue != null)
            {
                var itm = lstRecentDirs[drpDocPath.SelectedIndex];//(DropDownItem)drpDocPath.SelectedItem.Value;
                long folID = Convert.ToInt64(itm.Value);
                LoadFolder(folID);
            }
        }

        private void btnBackCutSrc_Click(object sender, EventArgs e)
        {
            if (copySrcFolderID.HasValue && copySrcFolderID > 0)
            {
                LoadFolder(copySrcFolderID.Value);
            }
        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            grdDocuments.MasterTemplate.SortDescriptors.Clear();
            LoadGrid();
        }

        #endregion

        tblDocumentItem AddNewVirtualDocument(string ext, Tags.TagType tagType, string docName = null)
        {
            tblDocumentItem doc = null;

            if (string.IsNullOrWhiteSpace(docName))
            {
                UcEdit cnt = new UcEdit(false);
                cnt.NewName = string.Empty;

                if (DisplayManager.ShowDialouge(cnt) == DialogResult.OK)
                {
                    docName = cnt.NewName;
                }
            }

            if (!string.IsNullOrWhiteSpace(docName))
            {
                doc = new tblDocumentItem();
                doc.RecordID = repCntr.Find(CurrentFolderID).RecordID;
                doc.ParentID = CurrentFolderID;

                if (!repCntr.DocumentExist(docName, doc.RecordID, doc.RecordID))
                {
                    doc.Name = docName + ext;
                    doc.IsFolder = false;
                    doc.TempByteData = new byte[0];
                    doc.AddTag(tagType);
                    repCntr.AddVirtualFile(doc);

                    DisplayManager.DisplayCrudMessage(CrudMessageType.EntitySaved, this.crudMessage);
                }
                LoadGrid();
            }

            return doc;
        }
        protected override void Dispose(bool disposing)
        {
            //dont dispose controls as the are used when this control is shown again
        }

        private void btnNew_Click(object sender, EventArgs e)
        {
            var ext = AppConstants.LedgerFolder.GetExtention(btnNewSheet.Tag.ToString());
            var tag = AppConstants.LedgerFolder.GetTag(btnNewSheet.Tag.ToString());
            if (!string.IsNullOrWhiteSpace(ext) && tag != Tags.TagType.None)
            {
                AddNewVirtualDocument(ext, tag);
            }
            else
            {
                DisplayManager.DisplayMessage("No settings found for new file.", MessageType.Error);
            }
        }

        void CopyPaymentReceipt(long srcID, string folName)
        {
            var ext = AppConstants.LedgerFolder.GetExtention(folName);
            var tag = AppConstants.LedgerFolder.GetTag(folName);

            tblDocumentItem srcDocItm = repCntr.Find(srcID);

            var docName = srcDocItm.Name.Replace(ext, string.Empty);

            LedgerRepository rp = new LedgerRepository();
            var yrEndFol = rp.GetYearEndFolder(srcDocItm.ID);

            tblItemInfoController infCnt = new tblItemInfoController();
            var itmInfSrc = infCnt.FetchAllByDocumentItemID(srcDocItm.ID)[0];


            if (!string.IsNullOrWhiteSpace(ext) && tag != Tags.TagType.None)
            {
                var desDocItem = AddNewVirtualDocument(ext, tag, docName);

                var itmInfDes = new tblItemInfo();
                itmInfDes.AccountNo = itmInfSrc.AccountNo;
                itmInfDes.AnalysisCodeID = itmInfSrc.AnalysisCodeID;
                itmInfDes.BankName = itmInfSrc.BankName;
                itmInfDes.Date = itmInfSrc.Date;
                itmInfDes.LockedByUserID = itmInfSrc.LockedByUserID;
                itmInfDes.OpeningBal = itmInfSrc.OpeningBal;
                itmInfDes.Period = itmInfSrc.Period;
                itmInfDes.ReconcileDate = itmInfSrc.ReconcileDate;
                //Modified values
                itmInfDes.DocumentItemID = desDocItem.ID;
                itmInfDes.YearEndFolderID = yrEndFol.ID;

                infCnt.Save(itmInfDes);

                if (folName == AppConstants.LedgerFolder.Payment || folName == AppConstants.LedgerFolder.Receipts)
                {
                    CopyPaymentRecData(srcDocItm, desDocItem);
                }
                else
                {
                    CopySDBData(srcDocItm, desDocItem);
                }
            }
        }

        void CopyPaymentRecData(tblDocumentItem srcDocItm, tblDocumentItem desDocItem)
        {
            Dictionary<long, long> dic = new Dictionary<long, long>();

            tblExcelSheetController cntExc = new tblExcelSheetController();
            var coll = cntExc.FetchAllByDocumentItemID(srcDocItm.ID);

            foreach (var oldRecord in coll)
            {
                var newRecord = new tblExcelSheet();
                newRecord.Date = oldRecord.Date;
                newRecord.Description = oldRecord.Description;
                newRecord.Reference = oldRecord.Reference;
                newRecord.Gross = oldRecord.Gross;
                newRecord.VATCode = oldRecord.VATCode;
                newRecord.VAT = oldRecord.VAT;
                newRecord.Net = oldRecord.Net;
                newRecord.NominalCode = oldRecord.NominalCode;
                newRecord.Comment = oldRecord.Comment;

                if (oldRecord.SplitParentID.HasValue)
                {
                    long newSplitParentID;
                    if (dic.TryGetValue(oldRecord.SplitParentID.Value, out newSplitParentID))//get id from dictionary
                    {
                        newRecord.SplitParentID = (int)newSplitParentID;
                    }
                }

                newRecord.Tick = null;
                newRecord.DocumentItemID = desDocItem.ID;

                cntExc.Save(newRecord);

                dic.Add(oldRecord.ID, newRecord.ID);//Add ids in dictionary
            }
        }

        void CopySDBData(tblDocumentItem srcDocItm, tblDocumentItem desDocItem)
        {
            tblSDBController cntExc = new tblSDBController();
            var coll = cntExc.FetchAllByDocumentItemID(srcDocItm.ID);

            foreach (var oldSDB in coll)
            {
                var exc = new tblSDB();
                exc.Date = oldSDB.Date;
                exc.Gross1 = oldSDB.Gross1;
                exc.Gross2 = oldSDB.Gross2;
                exc.Gross3 = oldSDB.Gross3;
                exc.Gross4 = oldSDB.Gross4;
                exc.Gross5 = oldSDB.Gross5;
                exc.Gross6 = oldSDB.Gross6;
                exc.Total = oldSDB.Total;

                exc.DocumentItemID = desDocItem.ID;
                cntExc.Save(exc);
            }

            tblSDBSettingsController cntSettings = new tblSDBSettingsController();
            var collSettings = cntSettings.FetchAllByDocumentItemID(srcDocItm.ID);

            foreach (tblSDBSetting oldSet in collSettings)
            {
                var newSet = new tblSDBSetting();
                newSet.Nominal1 = oldSet.Nominal1;
                newSet.Nominal2 = oldSet.Nominal2;
                newSet.Nominal3 = oldSet.Nominal3;
                newSet.Nominal4 = oldSet.Nominal4;
                newSet.Nominal5 = oldSet.Nominal5;
                newSet.Nominal6 = oldSet.Nominal6;

                newSet.VAT1 = oldSet.VAT1;
                newSet.VAT2 = oldSet.VAT2;
                newSet.VAT3 = oldSet.VAT3;
                newSet.VAT4 = oldSet.VAT4;
                newSet.VAT5 = oldSet.VAT5;
                newSet.VAT6 = oldSet.VAT6;

                newSet.TotalVAT1 = oldSet.TotalVAT1;
                newSet.TotalVAT2 = oldSet.TotalVAT2;
                newSet.TotalVAT3 = oldSet.TotalVAT3;
                newSet.TotalVAT4 = oldSet.TotalVAT4;
                newSet.TotalVAT5 = oldSet.TotalVAT5;
                newSet.TotalVAT6 = oldSet.TotalVAT6;

                newSet.DocumentItemID = desDocItem.ID;
                cntSettings.Save(newSet);
            }
        }

        private void grdContextMenu_Opening(object sender, System.ComponentModel.CancelEventArgs e)
        {
            ButtonVisibility();
        }
        private void btnCopy_Click(object sender, EventArgs e)
        {
            Copy(MenuAction.Copy);
        }

        private void Copy(MenuAction menuAction)
        {
            if (IsSingleRowSelected())
            {
                List<tblDocumentItem> itemList = GetSelected_GridItem();

                if (itemList[0].IsFolder)
                {
                    DisplayManager.DisplayMessage("Folder cannot be copied. Please select a file.", MessageType.Error);
                    return;
                }

                lastAction = menuAction;

                if (itemList[0].IsVirtual == true)
                {
                    copySrcFolderID = itemList[0].ID;
                    lstSrcTyp = LstSource.Virtual;
                }
                else
                {
                    CopyPhysical(menuAction);
                    lstSrcTyp = LstSource.Physical;
                }
            }
            else
            {
                DisplayManager.DisplayMessage("Select single document to copy.", MessageType.Error);
            }
        }

        private void btnCut_Click(object sender, EventArgs e)
        {
            Copy(MenuAction.Cut);

            //if (IsSingleRowSelected())
            //{
            //    List<tblDocumentItem> itemList = GetSelected_GridItem();
            //    lastAction = MenuAction.Cut;
            //    copySrcFolderID = itemList[0].ID;
            //}
            //else
            //{
            //    DisplayManager.DisplayMessage("Select single document to cut.", MessageType.Error);
            //}
        }

        private void btnPaste_Click(object sender, EventArgs e)
        {
            if (lstSrcTyp == LstSource.Virtual)
            {
                if (copySrcFolderID.HasValue)
                {
                    string folName = null;
                    if (repCntr.IsChildOf(copySrcFolderID.Value, AppConstants.LedgerFolder.Payment))
                    {
                        folName = AppConstants.LedgerFolder.Payment;
                    }
                    else if (repCntr.IsChildOf(copySrcFolderID.Value, AppConstants.LedgerFolder.Receipts))
                    {
                        folName = AppConstants.LedgerFolder.Receipts;
                    }
                    else if (repCntr.IsChildOf(copySrcFolderID.Value, AppConstants.LedgerFolder.SDB))
                    {
                        folName = AppConstants.LedgerFolder.SDB;
                    }

                    CopyPaymentReceipt(copySrcFolderID.Value, folName);


                    if (lastAction == MenuAction.Cut)
                    {
                        try
                        {
                            var old = repCntr.Find(copySrcFolderID.Value);
                            repCntr.DeleteRecursive(old, false);//dont check locked file in Cut command
                        }
                        catch { }
                    }

                    copySrcFolderID = null;

                    LoadGridAndTree();
                }
            }
            else if(lstSrcTyp == LstSource.Physical)
            {
                tblDocumentItem des = null;
                List<tblDocumentItem> selList = GetSelected_GridItem();

                if (selList == null || selList.Count == 0)//if nothing is selected then copy to opened folder
                {
                    des = repCntr.Find(CurrentFolderID);
                }
                else if (selList.Count == 1)//if single item is selected
                {
                    des = selList[0];
                }
                else if (selList.Count > 1)//if more then 1 item is selected
                {
                    DisplayManager.DisplayMessage("Please select a single folder.", MessageType.Error);
                    return;
                }

                PastePhysical(des);
            }

        }

        void ToggleLock()
        {
            if (IsSingleRowSelected())
            {
                List<tblDocumentItem> itemList = GetSelected_GridItem();
                var selItem = itemList[0];
                var lockedByID = selItem.FindLockedBy();
                if (lockedByID > 0)
                {
                    //Unlock
                    UserController userCnt = new UserController();
                    var usr = userCnt.Find(lockedByID.Value);

                    string msg = string.Format("This sheet is locked by {0}. Do you want to unlock?", usr.FirstName + " " + usr.LastName);

                    if (DisplayManager.DisplayMessage(msg, MessageType.Confirmation) == DialogResult.Yes)
                    {
                        UcUnLockSheet uc = new UcUnLockSheet();
                        if (DisplayManager.ShowDialouge(uc, "Unlock Sheet") == DialogResult.OK)
                        {
                            if (uc.InputString == usr.SheetPassword)
                            {
                                selItem.UpdateLock(null);//Unlock
                                DisplayManager.DisplayMessage("File has been unlocked.", MessageType.InfoDialogue);
                            }
                            else
                            {
                                DisplayManager.DisplayMessage("Password is incorrect. Please go to your profile in 'User Management' and recheck your 'Sheet Password'.", MessageType.Error);
                                return;
                            }
                        }
                    }
                }
                if (lockedByID == null)
                {
                    if (DisplayManager.DisplayMessage("Do you want to lock this file?", MessageType.Confirmation) == DialogResult.Yes)
                    {
                        //lock
                        selItem.UpdateLock(AuthenticationService.LoginUser.ID);
                        DisplayManager.DisplayMessage("File has been locked.", MessageType.InfoDialogue);
                    }
                }
                LoadGridAndTree();
            }
        }

        private void btnLockUnlock_Click(object sender, EventArgs e)
        {
            ToggleLock();
        }

        private void btnNewWord_Click(object sender, EventArgs e)
        {
            AddNewFile(AppConstants.WordDocExtention, new byte[0]);
        }

        private void btnNewExcel_Click(object sender, EventArgs e)
        {
            AddNewFile(".xlsx", FileHelper.GetByteArray(@".\empty.xlsx"));
        }

        private void btnNewText_Click(object sender, EventArgs e)
        {
            AddNewFile(".txt", new byte[0]);
        }

        void AddNewFile(string ext, byte[] data)
        {
            UcEdit cnt = new UcEdit(false);
            cnt.NewName = string.Empty;
            bool fileAdded = false;

            if (DisplayManager.ShowDialouge(cnt) == DialogResult.OK)
            {
                tblDocumentItem doc = new tblDocumentItem();

                doc.RecordID = repCntr.Find(CurrentFolderID).RecordID;
                doc.ParentID = CurrentFolderID;

                if (!repCntr.DocumentExist(cnt.NewName, doc.RecordID, doc.RecordID))
                {
                    doc.Name = cnt.NewName + ext;

                    doc.IsFolder = false;

                    if (cnt.SelTemplate != null && cnt.SelTemplate.ID != 0)
                    {
                        data = DocMerge.ExtractDocument(cnt.SelTemplate, doc.RecordID);
                    }

                    doc.TempByteData = data;

                    fileAdded = repCntr.AddFile(doc);

                    DisplayManager.DisplayCrudMessage(CrudMessageType.EntitySaved, this.crudMessage);
                }
                LoadGrid();

                if (fileAdded)
                {
                    SetCurrentRow(doc.ID);
                    grdDocuments_DoubleClick(null, null);
                }
            }
        }


        private void PastePhysical(tblDocumentItem des)
        {
            try
            {
                if (clipBoardList.Count > 0)
                {
                    if (des == null)
                    {
                        DisplayManager.DisplayMessage("Please select destination folder.", MessageType.Error);
                        return;
                    }

                    if (!des.IsFolder)
                    {
                        DisplayManager.DisplayMessage("Please select a single folder.", MessageType.Error);
                        return;
                    }

                    foreach (tblDocumentItem item in clipBoardList)
                    {
                        if (lastAction == MenuAction.Copy)
                        {
                            if (repCntr.Copy(item, des))
                            {
                                DisplayManager.DisplayMessage("Record copied to new place.", MessageType.Success);
                            }
                        }
                        else if (lastAction == MenuAction.Cut)
                        {
                            if (repCntr.Move(item, des.ID))
                            {
                                DisplayManager.DisplayMessage("Record moved to new place.", MessageType.Success);
                            }
                        }
                        LoadGridAndTree();
                    }
                }
                else
                {
                    DisplayManager.DisplayMessage("Please copy document(s) first.", MessageType.Error);
                }

            }
            finally
            {
                //clipBoardList.Clear(); //dont clear clipboard
            }
        }

        void CopyPhysical(MenuAction action)
        {
            lastAction = action;
            List<tblDocumentItem> itemList = GetSelected_GridItem();

            if (itemList != null && itemList.Count > 0)
            {
                copySrcFolderID = CurrentFolderID;

                if (itemList.Any(x => x.IsFolder))
                {
                    DisplayManager.DisplayMessage("You cannot copy folder only file(s) can be copied. Please un-select folder(s) and try again.", MessageType.Error);
                    return;
                }

                clipBoardList.Clear();

                itemList.ForEach(x =>
                {
                    if (!x.IsFolder)
                        clipBoardList.Add(x);
                });
            }
            else
                DisplayManager.DisplayMessage("Please select a document first.", MessageType.Error);
        }


    }


    public enum MenuAction
    {
        Cut,
        Copy,
    }
    public enum LstSource
    {
        Physical,
        Virtual,
    }

}

