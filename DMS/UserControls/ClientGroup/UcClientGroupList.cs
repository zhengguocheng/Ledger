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

namespace DMS.UserControls
{
    public partial class UcClientGroupList : UserControlBase
    {
        GroupController grpCntr = new GroupController();
        ClientController cntrl = new ClientController();
        GroupClientController cntGrpClient = new GroupClientController();

        public tblGroup SelectedGroup = new tblGroup();
        
        public UcClientGroupList()
        {
            InitializeComponent();
            this.Caption = "Client Groups";
        }

        private void UcClientList_Load(object sender, EventArgs e)
        {

            tblGroup itm = new tblGroup() { GroupName = String.Empty, Description = String.Empty };
            List<tblGroup> lst = grpCntr.FetchAll();
            lst.Insert(0, itm);

            drpSelGrp.DataSource = lst;
            drpSelGrp.DisplayMember = "GroupName";
            drpSelGrp.ValueMember = "ID";

            this.DisableCellHighlighting(grdClients);
            RefreshGrid();
            grdClients.ShowFilteringRow = false;
            grdClients.Disposed += new EventHandler(grdClients_Disposed);
        }

        #region Column Chooser

        //save hidden columns in Settings 
        void grdClients_Disposed(object sender, EventArgs e)
        {
            if (!grdClients.IsDisposed)
            {
                StringBuilder sb = new StringBuilder();

                foreach (var col in grdClients.Columns)
                {
                    if(!col.IsVisible)
                        sb.Append(col.Name + ",");
                }

                DMSSettings.Default.ClientListHiddenColumns = sb.ToString();
                DMSSettings.Default.Save();
            }
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

        #endregion

        void RefreshGrid()
        {
            List<tblGroupClient> lst = cntGrpClient.FetchByGroupID(SelectedGroup.ID);

            grdClients.DataSource = cntrl.FetchAll().OrderBy(x => x.Client_Name).ToList();

            //Check all clients in grdClients if ClientID is in lst

            foreach (GridViewDataRowInfo row in grdClients.Rows)
            {
                int clntID = Convert.ToInt32(row.Cells[1].Value);
                if (lst.FirstOrDefault(x => x.ClientID == clntID) != null)
                {
                    row.Cells["colChkBox"].Value = true;
                }
            }
            
            SendColumnToColumnChooser();
        }

        private void cbShowFilter_ToggleStateChanged(object sender, StateChangedEventArgs args)
        {
            if (args.ToggleState == Telerik.WinControls.Enumerations.ToggleState.On)
                grdClients.ShowFilteringRow = true;
            else
                grdClients.ShowFilteringRow = false;
        }
       
        private void btnSave_Click(object sender, EventArgs e)
        {
            GroupClientController cnt = new GroupClientController();
            List<Client> selList = GetSelectedClients();

            foreach (GridViewDataRowInfo row in grdClients.Rows)
            {
                Client c = (Client)row.DataBoundItem;
                if (IsRowSelected(row))
                {
                    cnt.Insert(c.ID, SelectedGroup.ID);
                }
                else //if client is un selected then delete any existing client group record.
                {
                    cnt.Delete(c.ID, SelectedGroup.ID);
                }
            }
            DisplayManager.DisplayMessage("Selected clients are assigned to group " + SelectedGroup.GroupName, MessageType.Success);
            GoBack();
        }

        private void drpSelGrp_SelectedIndexChanged(object sender, Telerik.WinControls.UI.Data.PositionChangedEventArgs e)
        {
            RefreshGrid();
        }

        List<Client> GetSelectedClients()
        {
            List<Client> selList = new List<Client>();
            foreach (GridViewDataRowInfo row in grdClients.Rows)
            {
                if (IsRowSelected(row))
                {
                    Client c = (Client)row.DataBoundItem;
                    selList.Add(c);
                }
            }

            return selList;
        }

        bool IsRowSelected(GridViewDataRowInfo row)
        {
            //object obj = row.Cells["colChkBox"].Value;
            //if (obj != null)
            //{
            //    string value = obj.ToString();
            //    if (String.Compare(value, "true", true) == 0)
            //    {
            //        return true;
            //    }
            //}
            //return false;

            return Convert.ToBoolean(row.Cells["colChkBox"].Value);

        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            GoBack();
        }

        void GoBack()
        {
            DisplayManager.LoadControl(new UcGroupList());
        }

       
    }
}
