﻿using System;
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
    public partial class UcAnalysisCodeList : UserControlBase
    {
        tblAnalysisCodeController entCntrl = new tblAnalysisCodeController();

        public UcAnalysisCodeList()
        {
            InitializeComponent();
            this.Caption = "Analysis Code";
            this.crudMessage = new CustomMessages("Analysis Code");
        }

        private void UcTaskList_Load(object sender, EventArgs e)
        {
            this.DisableCellHighlighting(grdItems);
            DropDownHelper.BindClientYearEnd(drpClient);
            RefreshGrid();
        }

        void RefreshGrid()
        {
            ClearErrorProvider();
            grdItems.DataSource = null;

            if (DropDownHelper.IsEmpty(drpClient))
                grdItems.DataSource = entCntrl.FetchByYearEndID(0);
            else
            {
                if (DropDownHelper.IsEmpty(drpYearEnd))
                {
                    ShowValidationError(drpYearEnd, CustomMessages.GetValidationMessage("Year End"));
                }
                else
                {
                    var id = Convert.ToInt64(DropDownHelper.GetSelectedValue(drpYearEnd));
                    grdItems.DataSource = entCntrl.FetchByYearEndID(id);
                }
            }
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            if (!DropDownHelper.IsEmpty(drpClient) && DropDownHelper.IsEmpty(drpYearEnd))
            {
                ClearErrorProvider();
                ShowValidationError(drpYearEnd, CustomMessages.GetValidationMessage("Year End"));
                return;
            }


            long yrEndFolID = 0;
            if (!DropDownHelper.IsEmpty(drpYearEnd))
            {
                yrEndFolID = Convert.ToInt64(DropDownHelper.GetSelectedValue(drpYearEnd));
            }

            var cnt = new UcAnalysisCode();
            cnt.yrEndFolID = yrEndFolID;
            DisplayManager.LoadControl(cnt, false, this);


            //DisplayManager.LoadControl(new UcAnalysisCode());
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            if (grdItems.SelectedRows.Count > 0)
            {
                tblAnalysisCode itm = (tblAnalysisCode)grdItems.SelectedRows[0].DataBoundItem;
                UcAnalysisCode cnt = new UcAnalysisCode();
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
                    tblAnalysisCode itm = (tblAnalysisCode)grdItems.SelectedRows[0].DataBoundItem;
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

        private void drpClient_SelectedIndexChanged(object sender, Telerik.WinControls.UI.Data.PositionChangedEventArgs e)
        {
            var id = DropDownHelper.GetSelectedValue(drpClient);
            DropDownHelper.BindYearEnd(drpYearEnd, id);
            RefreshGrid();
        }

        private void drpYearEnd_SelectedIndexChanged(object sender, Telerik.WinControls.UI.Data.PositionChangedEventArgs e)
        {
            RefreshGrid();
        }

        protected override void Dispose(bool disposing)
        {
            //dont dispose controls as the are used when this control is shown again
        }

       
      
    }
}