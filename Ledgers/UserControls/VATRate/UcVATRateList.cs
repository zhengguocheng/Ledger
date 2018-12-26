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
    public partial class UcVATRateList : UserControlBase
    {
        tblVAT_RateController entCntrl = new tblVAT_RateController();

        public UcVATRateList()
        {
            InitializeComponent();
            this.Caption = "VAT Rate List";
            this.crudMessage = new CustomMessages("VAT Rate");
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
            DisplayManager.LoadControl(new UcVATRate());
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            if (grdItems.SelectedRows.Count > 0)
            {
                tblVAT_Rate itm = (tblVAT_Rate)grdItems.SelectedRows[0].DataBoundItem;
                UcVATRate cnt = new UcVATRate();
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
                    tblVAT_Rate itm = (tblVAT_Rate)grdItems.SelectedRows[0].DataBoundItem;
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
