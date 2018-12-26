using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using DAL;

using Telerik.WinControls.UI;
using DMS.CustomClasses;
using DMS.UserControls;

namespace DMS
{
    public static class GlobalLogger
    {
        public static Utilities.Logger logger;
        static GlobalLogger()
        {
            logger = new Utilities.Logger(AppConstants.LogDirPath);
        }
    }

    public class UserControlBase: UserControl
    {
        public string Caption { get; set; }
        public bool ShowToContributer { get; set; }
        protected CustomMessages crudMessage;
        protected List<Control> lstLedgerCntrl = new List<Control>();
        protected List<Control> lstDMSCntrl = new List<Control>();
        protected List<Control> lstFocusableCntrl = new List<Control>();

        public void AddLedgerControl(Control ctrl)
        {
            lstLedgerCntrl.Add(ctrl);
        }

        public void AddLedgerControl(params Control[] arr)
        {
            foreach (var ctrl in arr)
            {
                lstLedgerCntrl.Add(ctrl);
            }
        }

        public void AddDMSControl(Control ctrl)
        {
            lstDMSCntrl.Add(ctrl);
        }

        public void AddDMSControl(params Control[] arr)
        {
            foreach (var ctrl in arr)
            {
                lstDMSCntrl.Add(ctrl);
            }
        }

        ErrorProvider errorPrv;
        public UserControlBase PreviousForm;
        
        public UserControlBase()
        {
            this.Load +=new EventHandler(UserControlBase_Load);
            ShowToContributer = true;
        }

        void  UserControlBase_Load(object sender, EventArgs e)
        {
            errorPrv = new ErrorProvider();
            if (AppConstants.IsLedger)
            {
                foreach (var item in lstDMSCntrl)
                {
                    item.Visible = false;
                }
            }
            else
            {
                foreach (var item in lstLedgerCntrl)
                {
                    item.Visible = false;
                }
            }
        }

        public void AddKeyDownControls(params Control[] arr)
        {
            lstFocusableCntrl.Clear();
            arr[0].Focus();
            foreach (var item in arr)
            {
                item.KeyDown += new KeyEventHandler(Generic_KeyDown);
                lstFocusableCntrl.Add(item);
            }
        }

        Control GetNextControl(object sender)
        {
            Control current = (Control)sender;
            Control next = lstFocusableCntrl.FirstOrDefault(x => x.TabIndex == current.TabIndex + 1);
            if (next != null)
                return next;
            else
                return lstFocusableCntrl[0];
        }
        
        //int index = 0;
        //int GetNextIndex(object sender)
        //{
        //    index++;
        //    if (index >= lstFocusableCntrl.Count)
        //        index = 0;
        //    return index;
        //}

        public void Generic_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                #region commented
                //Control nextCnt = this.GetNextControl(ActiveControl, true);
                //if (nextCnt != null)
                //{
                //    e.Handled = true;
                //    if (lstFocusableCntrl.IndexOf(nextCnt) >= 0)
                //    {
                //        nextCnt.Focus();
                //    }
                //    else
                //        lstFocusableCntrl[0].Focus();
                //}
                //Control nextCnt = lstFocusableCntrl[GetNextIndex()];
                #endregion

                Control nextCnt = GetNextControl(sender);
                if (nextCnt != null)
                {
                    e.Handled = true;
                    nextCnt.Focus();
                }
            }
        }

        public static void HandleException(Exception ecp)
        {
            try
            {
                GlobalLogger.logger.LogException(ecp);

                DisplayManager.DisplayMessage(ecp.Message, MessageType.Error);
            }
            catch { }
        }

        //public static void UnhandledException(object sender, UnhandledExceptionEventArgs e)
        //{
        //    try
        //    {
        //        DBHelper.ReleaseResources();
        //        Exception ecp = (Exception)e.ExceptionObject;
        //        HandleException(ecp);
        //    }
        //    catch { }
        //}

        public static void UnhandledThreadException(object sender, System.Threading.ThreadExceptionEventArgs e)
        {
            try
            {
                Exception ecp = e.Exception;
                HandleException(ecp);
            }
            catch { }
        }

        protected void HideColumns(DataGridView grd, string[] arrNames)
        {
            foreach (string name in arrNames)
            {
                if (grd.Columns.Contains(name))
                {
                    grd.Columns[name].Visible = false;
                    grd.Columns[name].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
                }
            }

            string[] arr = new string[] {"CreatedBy","UpdatedBy","CreatedAt","UpdatedAt","IsNew"};
            foreach (string name in arr)
            {
                if (grd.Columns.Contains(name))
                    grd.Columns[name].Visible = false;
            }
        }

        protected void ShowColumns(DataGridView grd, string[] arrNames)
        {
            grd.Columns.Clear();
            int indx = 0;
            foreach (string str in arrNames)
            {
                DataGridViewTextBoxColumn col = new DataGridViewTextBoxColumn();
                col.DataPropertyName = str;
                col.HeaderText = GetColumnName(str);
                //col.AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells; 
                if(col.DataPropertyName.Trim().ToLower() == "clientname")
                    col.MinimumWidth = 150;
                grd.Columns.Insert(indx++,col);
            }
        }

        public static string GetColumnNameStatic(string str)
        {
            switch (str)
            {
                case "RollOverStatus":
                    return "Status";

                case "StrDue31_7":
                    return "Due 31 / 7";

                case "StrDue31_1":
                    return "Due 31 / 1";

                case "StrCTPayable":
                    return "CT Payable";
            }

            return str.CamelCaseToString();
        }

        string GetColumnName(string str)
        {
            return GetColumnNameStatic(str);
        }

        protected void ShowValidationError(Control c, string message)
        {
            errorPrv.Clear();
            errorPrv.SetError(c, message);
        }

        protected void ClearErrorProvider()
        {
            errorPrv.Clear();
        }
        
        protected void ShowSaveConfirmation()
        {
            DisplayManager.DisplayMessage(CustomMessages.GetCustomMessage(CustomMessages.SavedSuccessfully, this.Caption), MessageType.Success);
        }

        public bool TryNavigateBack()
        {
            if (PreviousForm == null)
                return false;
            return true;
        }

        public void LoadPreviousPage()//works if previous page is UcNavigation or UcLedgerReport
        {
            if(DisplayManager.PreActiveForm is UcNavigation)
            {
                DisplayManager.LoadControl(new UcNavigation(false), true);
            }
            else if (DisplayManager.PreActiveForm is UcLedgerReport)
            {
                DisplayManager.LoadControl(new UcLedgerReport(0, 0), true);
            }
            else if (DisplayManager.PreActiveForm is UcBankReconcile)
            {
                DisplayManager.LoadControl(new UcBankReconcile(0), true);
            }
            else
            {
                //if all above conditions failed then just move to UcNavigation
                DisplayManager.LoadControl(new UcNavigation(false), true);
            }

        }

        protected void DisableCellHighlighting(RadGridView grd)
        {
            grd.CellFormatting += new Telerik.WinControls.UI.CellFormattingEventHandler(GridView1_CellFormatting);
            grd.AllowRowResize = false;
        }

        protected void GridView1_CellFormatting(object sender, CellFormattingEventArgs e)
        {
            if (e.CellElement.IsCurrent)
            {
                e.CellElement.DrawFill = false;
            }
        }

        public virtual void ControlClosing()
        {

        }

        public virtual void ControlDisplayed()
        {

        }
    }
}

