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
    public partial class UcGenericList : UserControlBase
    {
        tblGenericListController entCntrl = new tblGenericListController();
        GenericListType.TypeEnum type;

        public UcGenericList(GenericListType.TypeEnum ptype)
        {
            InitializeComponent();
            type = ptype;
            this.Caption = GenericListType.GetName(type);
            this.crudMessage = new CustomMessages(this.Caption);
        }

        private void UcTaskList_Load(object sender, EventArgs e)
        {
            this.DisableCellHighlighting(grdItems);
            RefreshGrid();
        }

        void RefreshGrid()
        {
            grdItems.DataSource = entCntrl.FetchByType(type); 
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            DisplayManager.LoadControl(new UcGenericItem(type));
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            if (grdItems.SelectedRows.Count > 0)
            {
                tblGenericList itm = (tblGenericList)grdItems.SelectedRows[0].DataBoundItem;
                UcGenericItem cnt = new UcGenericItem(type);
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
                    tblGenericList itm = (tblGenericList)grdItems.SelectedRows[0].DataBoundItem;
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
