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
    public partial class UcSupplierList : UserControlBase
    {
        tblSupplierController entCntrl = new tblSupplierController();

        public UcSupplierList()
        {
            InitializeComponent();
            this.Caption = "Supplier";
            this.crudMessage = new CustomMessages("Supplier");
        }

        private void UcTaskList_Load(object sender, EventArgs e)
        {
            this.DisableCellHighlighting(grdItems);
            RefreshGrid();
        }

        void RefreshGrid()
        {
            ClearErrorProvider();
            grdItems.DataSource = entCntrl.FetchView();
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            var cnt = new UcSupplier();
            DisplayManager.LoadControl(cnt, false, this);
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            if (grdItems.SelectedRows.Count > 0)
            {
                var vw = (VwSupplier)grdItems.SelectedRows[0].DataBoundItem;
                var itm = entCntrl.Find(vw.ID);
                UcSupplier cnt = new UcSupplier();
                cnt.SelectedItem = itm;
                DisplayManager.LoadControl(cnt, false, this);
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (grdItems.SelectedRows.Count > 0)
            {
                if (DisplayManager.DisplayMessage(CustomMessages.DeleteConfirmation, MessageType.Confirmation) == DialogResult.Yes)
                {
                    VwSupplier itm = (VwSupplier)grdItems.SelectedRows[0].DataBoundItem;
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

        protected override void Dispose(bool disposing)
        {
            //dont dispose controls as the are used when this control is shown again
        }

        
    }
}
