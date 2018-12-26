using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using DAL;
using DMS;

namespace DMS
{
    public partial class UcDescriptionList : UserControlBase
    {
        tblDescriptionController entCntrl = new tblDescriptionController();

        public UcDescriptionList()
        {
            InitializeComponent();
            this.Caption = "Description";
            this.crudMessage = new CustomMessages("Description");
        }

        private void UcTaskList_Load(object sender, EventArgs e)
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
            DisplayManager.LoadControl(new UcDescription());
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            if (grdItems.SelectedRows.Count > 0)
            {
                var itm = (tblDescription)grdItems.SelectedRows[0].DataBoundItem;
                UcDescription cnt = new UcDescription();
                cnt.SelectedItem = itm;
                DisplayManager.LoadControl(cnt);
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (grdItems.SelectedRows.Count > 0)
            {
                if (DisplayManager.DisplayMessage(CustomMessages.DeleteConfirmation, MessageType.Confirmation) == DialogResult.Yes)
                {
                    tblDescription itm = (tblDescription)grdItems.SelectedRows[0].DataBoundItem;
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

    }
}
