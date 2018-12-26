using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using DAL;
using DMS.UserControls;

namespace DMS
{
    public partial class UcGroupList : UserControlBase
    {
        GroupController entCntrl = new GroupController();

        public UcGroupList()
        {
            InitializeComponent();
            this.Caption = "Group List";
            this.crudMessage = new CustomMessages("UcGroup");
        }

        private void UcGroupList_Load(object sender, EventArgs e)
        {
            this.DisableCellHighlighting(grdItems);
            RefreshGrid();
        }

        void RefreshGrid()
        {
            grdItems.DataSource = entCntrl.FetchAll(); 
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            DisplayManager.LoadControl(new UcGroup());
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            if (grdItems.SelectedRows.Count > 0)
            {
                UcGroup uc = new UcGroup();
                uc.SelectedGroup = (tblGroup)grdItems.SelectedRows[0].DataBoundItem;
                DisplayManager.LoadControl(uc);
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            
            if (grdItems.SelectedRows.Count > 0)
            {
                if (DisplayManager.DisplayMessage(CustomMessages.DeleteConfirmation, MessageType.Confirmation) == DialogResult.Yes)
                {
                    tblGroup itm = (tblGroup)grdItems.SelectedRows[0].DataBoundItem;
                    if (entCntrl.Delete(itm.ID))
                    {
                        DisplayManager.DisplayCrudMessage(CrudMessageType.EntityDeleted, crudMessage);
                        RefreshGrid();
                    }
                }
            }
        }

        private void grdItems_DoubleClick(object sender, EventArgs e)
        {
            btnUpdate_Click(sender, e);
        }

        private void btnGroupClients_Click(object sender, EventArgs e)
        {
            if (grdItems.SelectedRows.Count > 0)
            {
                UcClientGroupList uc = new UcClientGroupList();
                uc.SelectedGroup = (tblGroup)grdItems.SelectedRows[0].DataBoundItem;
                DisplayManager.LoadControl(uc);
            }
            
        }
    }
}
