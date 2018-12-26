using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using DAL;
using Telerik.WinControls.UI;

namespace DMS
{
    public partial class UcUserList : UserControlBase
    {
        UserController cntrl = new UserController();
        
        public UcUserList()
        {
            InitializeComponent();
            this.Caption = "User List";
            crudMessage = new CustomMessages("User");
        }

        private void UcUserList_Load(object sender, EventArgs e)
        {
            this.DisableCellHighlighting(grdUsers);
            RefreshGrid();
            grdUsers.ShowFilteringRow = false;
        }

        void RefreshGrid()
        {
            grdUsers.DataSource = cntrl.FetchAll().OrderBy(x => x.UserName).ToList();
            DAL.Client cnt = new DAL.Client();
        }

        User GetSelectedItem()
        {
            if (grdUsers.SelectedRows.Count == 0)
            {
                return null;
            }
            else
            {
                User c = (User)grdUsers.SelectedRows[0].DataBoundItem;
                return c;
            }
        }

        private void cbShowFilter_ToggleStateChanged(object sender, StateChangedEventArgs args)
        {
            if (args.ToggleState == Telerik.WinControls.Enumerations.ToggleState.On)
                grdUsers.ShowFilteringRow = true;
            else
                grdUsers.ShowFilteringRow = false;
        }
       
        private void btnAdd_Click(object sender, EventArgs e)
        {
            UcUser uc = new UcUser();
            DisplayManager.LoadControl(uc);
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            UcUser uc = new UcUser();

            User clnt = GetSelectedItem();

            if (clnt == null)
            {
                return;
            }

            uc.SelectedItem = clnt;
            DisplayManager.LoadControl(uc);
        }

        private void grdClients_CellDoubleClick(object sender, GridViewCellEventArgs e)
        {
            btnUpdate_Click(null, null);
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (DisplayManager.DisplayMessage(crudMessage.GetEntityCrudMessage(CrudMessageType.EntitySureDelete), MessageType.Confirmation) == DialogResult.Yes)
            {
                if (cntrl.Delete(GetSelectedItem()))
                {
                    DisplayManager.DisplayMessage(crudMessage.GetEntityCrudMessage(CrudMessageType.EntityDeleted), MessageType.Success);
                    RefreshGrid();
                }
            }
        }

        private void grdUsers_CellDoubleClick(object sender, GridViewCellEventArgs e)
        {
            btnUpdate_Click(null, null);
        }

      
    }
}
