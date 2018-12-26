using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using DMS.CustomClasses;
using DAL;
using Telerik.WinControls.UI;
using System.Collections;
using Telerik.WinControls.Enumerations;

namespace DMS.UserControls
{
    public partial class UcClientList : UserControlBase
    {
        GroupController grpCntr = new GroupController();
        ClientController cntrl = new ClientController();
        GroupClientController cntGrpClient = new GroupClientController();
        Repository repCntr = new Repository(AppConstants.RecordType.Client);

        public static long? PreSelectedClientID = null;

        public RadGridView ClientGrid { get; set; }

        public UcClientList()
        {
            InitializeComponent();
            this.Caption = "Client List";
            AddLedgerControl(pnlLedger);
            AddDMSControl(pnlDMS);
        }

        protected override void Dispose(bool disposing)
        {
            //dont dispose controls as the are used when this control is shown again
        }

        private void UcClientList_Load(object sender, EventArgs e)
        {
            tblGroup itm = new tblGroup() { GroupName = String.Empty, Description = String.Empty };
            List<tblGroup> lst = grpCntr.FetchAll();
            lst.Insert(0, itm);

            drpSelGrp.DataSource = lst;
            drpSelGrp.DisplayMember = "GroupName";
            drpSelGrp.ValueMember = "ID";

            ClientGrid = grdClients;
            this.DisableCellHighlighting(grdClients);
            RefreshGrid();
            grdClients.ShowFilteringRow = false;
            grdClients.Disposed += new EventHandler(grdClients_Disposed);
        }

        void grdClients_Disposed(object sender, EventArgs e)
        {
            if (!grdClients.IsDisposed)
            {
                SaveColumnSetting();
            }
        }

        public void SaveColumnSetting()
        {
            StringBuilder sb = new StringBuilder();

            foreach (var col in grdClients.Columns)
            {
                if (!col.IsVisible)
                    sb.Append(col.Name + ",");
            }

            DMSSettings.Default.ClientListHiddenColumns = sb.ToString();
            DMSSettings.Default.Save();
        }

        void SendColumnToColumnChooser()
        {
            try
            {
                string[] arrColNames = DMSSettings.Default.ClientListHiddenColumns.Split(",".ToCharArray());

                foreach (var colName in arrColNames)
                {
                    if (grdClients.Columns.Contains(colName))
                    {
                        grdClients.Columns[colName].IsVisible = false;
                    }    
                }
            }
            catch { }
        }

        void RefreshGrid()
        {
            if (drpSelGrp.SelectedIndex > 0)
            {
                tblGroup grp = (tblGroup)drpSelGrp.SelectedItem.DataBoundItem;
                List<int> clntIDs = new List<int>();
                List<tblGroupClient> lst = cntGrpClient.FetchByGroupID(grp.ID);
                lst.ForEach(x => clntIDs.Add(x.ClientID));
                grdClients.DataSource = cntrl.FetchAll(clntIDs).OrderBy(x => x.Client_Name).ToList();
            }
            else
            {
                grdClients.DataSource = cntrl.FetchAll().OrderBy(x => x.Client_Name).ToList();
            }

            SendColumnToColumnChooser();

            DAL.Client cnt = new DAL.Client();

            if (PreSelectedClientID.HasValue)
            {
                GridViewRowInfo r = grdClients.Rows.FirstOrDefault(x => ((Client)x.DataBoundItem).ID == PreSelectedClientID);
                if (r != null)
                    grdClients.CurrentRow = r;
            }
            
            PreSelectedClientID = null;
        }

        private void grdClients_SelectionChanged(object sender, EventArgs e)
        {
            //UcClientDocuments cd = (UcClientDocuments)this.Parent.Parent.Parent;
            //cd.LoadDocuments();
        }

        private void btnOpenDocument_Click(object sender, EventArgs e)
        {
            Repository rep = new Repository(AppConstants.RecordType.Client);

            Client c = GetSelectedClient();
            
            if (c == null)
            {
                DisplayManager.DisplayMessage("Please select client to view his documents",MessageType.Error);

                return;
            }                      
            
            List<tblDocumentItem> lst = rep.FetchRootNode(c.ID);

            if (lst != null || lst.Count == 0)
            {
                DefaultFolders df = new DefaultFolders();
                df.CreateDefaultFolders(c.ID, c.Client_Name, AppConstants.RecordType.Client);
                DisplayManager.DisplayMessage("Directory structure created.", MessageType.Success);
            }

            lst = rep.FetchRootNode(c.ID);
            if (lst != null && lst.Count > 0)
            {
                UcNavigation nav = new UcNavigation(false, lst[0].ID);
                DisplayManager.LoadControl(nav);
            }
            
        }

        Client GetSelectedClient()
        {
            if (grdClients.SelectedRows.Count == 0)
            {
                DisplayManager.DisplayMessage("Please select client first.", MessageType.Error);
                return null;
            }
            else if (grdClients.SelectedRows.Count > 1)
            {
                DisplayManager.DisplayMessage("Please select single client.", MessageType.Error);
                return null;
            }
            else
            {
                Client c = (Client)grdClients.SelectedRows[0].DataBoundItem;
                return c;
            }
        }

        private void cbShowFilter_ToggleStateChanged(object sender, StateChangedEventArgs args)
        {
            grdClients.EnableFiltering = grdClients.ShowFilteringRow = (args.ToggleState == ToggleState.On);
        }

       
        

        private void btnAdd_Click(object sender, EventArgs e)
        {
            UcClient uc = new UcClient();
            DisplayManager.LoadControl(uc);
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            UcClient uc = new UcClient();

            Client clnt = GetSelectedClient();

            if (clnt == null)
            {
                //DisplayManager.DisplayMessage("Please select client to view his documents", MessageType.Error);
                return;
            }

            uc.SelectedClient = clnt;
            DisplayManager.LoadControl(uc);
        }

        private void grdClients_CellDoubleClick(object sender, GridViewCellEventArgs e)
        {
            if (AppConstants.IsLedger)
                btnAccount_Click(null, null);
            else
                btnUpdate_Click(null, null);
        }

        private void btnGroup_Click(object sender, EventArgs e)
        {
            //GroupClientController cnt = new GroupClientController();
            //UcClientGroup uc = new UcClientGroup();
            //if (DisplayManager.ShowDialouge(uc) == DialogResult.OK)
            //{
            //    int grpID = uc.SelectedGroup.ID;
            //    foreach (var dgRow in grdClients.SelectedRows)
            //    {
            //        Client c = (Client)dgRow.DataBoundItem;
            //        int clntID = c.ID;
            //        cnt.Insert(clntID, grpID);
            //    }
            //}
        }

        private void drpSelGrp_SelectedIndexChanged(object sender, Telerik.WinControls.UI.Data.PositionChangedEventArgs e)
        {
            //tblGroup grp = (tblGroup)drpSelGrp.SelectedItem.DataBoundItem;
            //if (grp.ID > 0)
            //{
                RefreshGrid();
            //}
        }

        private void btnDocMerge_Click(object sender, EventArgs e)
        {
            UcEdit cnt = new UcEdit(true);
            cnt.NewName = string.Empty;

            if (DisplayManager.ShowDialouge(cnt) == DialogResult.OK)
            {
                string mess = string.Format("{0} clients selected. Do you want to proceed document merge process?", grdClients.Rows.Count);
                if (DisplayManager.DisplayMessage(mess, MessageType.Confirmation) == DialogResult.Yes)
                {
                    foreach (var dgRow in grdClients.Rows)
                    {
                        Client clnt = (Client)dgRow.DataBoundItem;
                        AddDocument(clnt, cnt.SelTemplate, cnt.NewName);
                    }
                }
            }
        }

        void AddDocument(Client clnt, tblDocumentTemplate tmplate, string docName)
        {
            try
            {
                List<tblDocumentItem> docList = repCntr.FetchRootNode(clnt.ID);

                if (docList != null && docList.Count > 0)
                {
                    tblDocumentItem doc = new tblDocumentItem();
                    doc.RecordID = clnt.ID;
                    doc.ParentID = docList[0].ID;//for adding doc in root node

                    if (!repCntr.DocumentExist(docName, doc.ParentID, doc.RecordID))
                    {
                        doc.Name = docName;
                        doc.IsFolder = false;
                        if (tmplate != null)
                        {
                            byte[] data = data = DocMerge.ExtractDocument(tmplate, doc.RecordID);
                            doc.TempByteData = data;
                            repCntr.AddFile(doc);
                        }
                    }
                }
            }
            catch (Exception ecp)
            {
                
            }
        }

        private void btnAccount_Click(object sender, EventArgs e)
        {
            var clnt = GetSelectedClient();
            if (clnt != null)
            {
                UcAccount acc = new UcAccount(clnt.ID);
                DisplayManager.LoadControl(acc);
            }
        }

        private void btnLedger_Click(object sender, EventArgs e)
        {
            Repository rep = new Repository(AppConstants.RecordType.Ledger);

            Client c = GetSelectedClient();
            if (c == null)
                return;

            List<tblDocumentItem> lst = rep.FetchRootNode(c.ID);
            if (lst == null || lst.Count == 0)
            {
                DefaultFolders f = new DefaultFolders();
                f.CreateDefaultFolders(c.ID, c.Client_Name, AppConstants.RecordType.Client);
            }

            lst = rep.FetchRootNode(c.ID);
            if (lst != null && lst.Count > 0)
            {
                UcNavigation nav = new UcNavigation(false, lst[0].ID);
                DisplayManager.LoadControl(nav);
            }
            else
            {
                DisplayManager.DisplayMessage("No root folder found for this client.", MessageType.Error);
            }
        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            drpSelGrp.SelectedIndex = 0;
            cbShowFilter.Checked = false;
            RefreshGrid();
        }
    }

   
}
