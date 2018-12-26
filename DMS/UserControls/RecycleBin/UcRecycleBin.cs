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
using System.Collections.Generic;
using System.Data.SqlClient;

namespace DMS
{
    public partial class UcRecycleBin : UserControlBase
    {
        DocumentItemController cntrl = new DocumentItemController();
        List<tblDocumentItem> ListAllRecords;
        Repository repCntr = new Repository(AppConstants.RecordType.Ledger);

        public UcRecycleBin()
        {
            InitializeComponent();
            this.Caption = "Recycle Bin";
            crudMessage = new CustomMessages("Item");
        }

        private void UcUserList_Load(object sender, EventArgs e)
        {
            this.DisableCellHighlighting(grdUsers);
            RefreshGrid();
            grdUsers.ShowFilteringRow = false;
        }

        void RefreshGrid()
        {
            var lst = GetListFromDB();
            foreach (var item in lst)
            {
                DisplayManager.SetDocumentIcon(item);
            }

            grdUsers.DataSource = lst;
            grdUsers.Columns["colImg"].FieldName = "DocImg";
            grdUsers.Columns["colImg"].IsVisible = true;
        }

        List<tblDocumentItem> GetListFromDB()
        {
            ListAllRecords = cntrl.FetchDeletedItems().OrderBy(x => x.ID).ToList();

            //remove all child items from displaying
            var lstNew = new List<tblDocumentItem>();

            ListAllRecords.ForEach(x => lstNew.Add(x));

            var lstToRemove = new List<tblDocumentItem>();

            foreach (var record in ListAllRecords)
            {
                if (record.IsFolder)
                {
                    var lstCh = new List<tblDocumentItem>();
                    FindAll_DeletedChildRecursive(record, lstCh);

                    lstCh.Remove(record);//remove parent folder from lstChild

                    foreach (var child in lstCh)
                    {
                        lstToRemove.Add(child);
                    }
                }
            }

            foreach (var item in lstToRemove)
            {
                lstNew.Remove(item);
            }

            return lstNew;
        }

        public void FindAll_DeletedChildRecursive(tblDocumentItem record, List<tblDocumentItem> lstChild)
        {
            if (record.IsFolder)
            {
                var lst = new List<tblDocumentItem>();

                lst = ListAllRecords.Where(x => x.IsDeleted == true && x.RecordID == record.RecordID && x.RecordTypeID.Value == (int)AppConstants.RecordType.Ledger && x.ParentID == record.ID).ToList();

                foreach (var item in lst)
                {
                    FindAll_DeletedChildRecursive(item, lstChild);
                }
            }

            lstChild.Add(record);
        }

        tblDocumentItem GetSelectedItem()
        {
            if (grdUsers.SelectedRows.Count == 0)
            {
                return null;
            }
            else
            {
                tblDocumentItem c = (tblDocumentItem)grdUsers.SelectedRows[0].DataBoundItem;
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

        private void btnDelete_Click(object sender, EventArgs e)
        {
            var selectedItm = GetSelectedItem();
            if (selectedItm.IsFolder)
            {
                var lstDeletedChild = new List<tblDocumentItem>();
                FindAll_DeletedChildRecursive(selectedItm, lstDeletedChild);

                if (lstDeletedChild.Count > 0)
                {
                    var names = "";
                    foreach (var item in lstDeletedChild)
                    {
                        names += item.Name + ", ";
                    }

                    names = names.TrimEnd(",".ToCharArray());
                    var msg = string.Format("This folder contains {0} ({1}) files. Do you want to proceed?.", lstDeletedChild.Count, names);
                    if (DisplayManager.DisplayMessage(msg, MessageType.Confirmation) == DialogResult.No)
                    {
                        return;
                    }
                }
            }

            if (DisplayManager.DisplayMessage("Once an item is deleted it can never be recovered. Are you sure you want to delete selected record?", MessageType.Confirmation) == DialogResult.Yes)
            {
                var lstDeletedChild = new List<tblDocumentItem>();
                FindAll_DeletedChildRecursive(selectedItm, lstDeletedChild);

                foreach (var child in lstDeletedChild)
                {
                    cntrl.Delete(child);
                    DBHelper.ExecuteSP_NoRead(SPNames.SpRemoveDeletedItem, new SqlParameter("@docItemID", child.ID));
                }

                DisplayManager.DisplayMessage(crudMessage.GetEntityCrudMessage(CrudMessageType.EntityDeleted), MessageType.Success);
                RefreshGrid();
            }
        }

        private void btnRestore_Click(object sender, EventArgs e)
        {
            if (DisplayManager.DisplayMessage("Are you sure you want to restore selected record?", MessageType.Confirmation) == DialogResult.Yes)
            {
                var selectedItm = GetSelectedItem();

                var lstDeletedChild = new List<tblDocumentItem>();
                FindAll_DeletedChildRecursive(selectedItm, lstDeletedChild);

                foreach (var child in lstDeletedChild)
                {
                    repCntr.RestoreFromRecycleBin(child);
                }

                DisplayManager.DisplayMessage("Item restored successfully.", MessageType.Success);
                RefreshGrid();

            }
        }


    }
}
