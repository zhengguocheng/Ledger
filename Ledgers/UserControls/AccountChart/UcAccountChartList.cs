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

namespace Ledgers
{
    public partial class UcAccountChartList : UserControlBase
    {
        tblChartAccountController entCntrl = new tblChartAccountController();

        public UcAccountChartList()
        {
            InitializeComponent();
            this.Caption = "Account Chart";
            this.crudMessage = new CustomMessages("Account");
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
            DisplayManager.LoadControl(new UcAccountChart());
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            if (grdItems.SelectedRows.Count > 0)
            {
                tblChartAccount itm = (tblChartAccount)grdItems.SelectedRows[0].DataBoundItem;
                UcAccountChart cnt = new UcAccountChart();
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
                    tblChartAccount itm = (tblChartAccount)grdItems.SelectedRows[0].DataBoundItem;
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
