using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using Telerik.WinControls;
using Telerik.WinControls.UI;
using DAL;
using DMS.UserControls;
using System.Diagnostics;
using FrontEnd;
using DMS.CustomClasses;
using System.Collections.Generic;
using System.IO;
using DMS;

namespace DMS
{
    public partial class LedgerMainForm : Telerik.WinControls.UI.RadForm, IMainForm
    {
        DataTable dsMessages = new DataTable();
        public static event RefreshGridHandler RefreshGrid;
        public delegate void RefreshGridHandler(object obj, EventArgs e);

        public LedgerMainForm()
        {
            InitializeComponent();
            ThemeResolutionService.ApplicationThemeName = "Desert";
        }

        private void NewMainForm_Load(object sender, EventArgs e)
        {
            lblLoginStatus1.Text = "";
            SetLoginStatus(false);
            var version= Application.ProductVersion.Replace(".0.0", string.Empty);
            this.Text = "Account Ledgers " + version;
            //this.Text = "Test Release ";
            //DisplayManager.DisplayMessage("Please note this is a test release.", MessageType.InfoDialogue);

            if (string.Compare(version.Trim(), AppConstants.LatestVersion.Trim(), true) != 0)
            {
                DisplayManager.DisplayMessage(string.Format("Ledger software has dectected that a newer version is available. Please use version {0} of Ledger software.", AppConstants.LatestVersion), MessageType.Error);
                this.Close();
            }
        }

        void SetLoginStatus(bool isVisible)
        {
            pnlLoginStatus1.Visible = isVisible;
        }

        #region IMainForm

        public Control MainArea
        {
            get
            {
                return pnlMainAreaBody;
            }
        }

        public void OnRefreshGrid(EventArgs e)
        {
            if (RefreshGrid != null)
                RefreshGrid(this, e);
        }

        public void CustomeAction(ActionType action, object val)
        {
            switch (action)
            {
                case ActionType.SetCaption:
                    activeWindow.Text = " " + val.ToString() + "  ";
                    break;
                case ActionType.LoginComplete:
                    SetLoginStatus(true);
                    lblLoginStatus1.Text = "Welcome: " + AuthenticationService.LoginUser.UserName;
                    break;
            }
        }

        public DialogResult DisplayMessage(string message, MessageType mType)
        {
            if (mType == MessageType.InfoDialogue)
                return MessageBox.Show(message, "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);


            if (mType == MessageType.Confirmation)
                return MessageBox.Show(message, "Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (mType == MessageType.Error)
            {
                AddStatusMessage(message, mType);
                return MessageBox.Show(message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }

            if (mType == MessageType.Success)
            {
                AddStatusMessage(message, mType);
                //return MessageBox.Show(message, "Message", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }


            return System.Windows.Forms.DialogResult.Yes;
        }

        void AddStatusMessage(string message, MessageType mType)
        {
            this.Invoke((MethodInvoker)delegate
            {
                Bitmap img = DMS.Properties.Resources.imgInfo;
                if (mType == MessageType.Error)
                    img = DMS.Properties.Resources.imgError;

                //grdMessages.Rows.Insert(0, img, DateTime.Now.ToString(Constants.DateFormatFull), message);


                if (grdMessages.Rows.Count > AppConstants.MessageMaxLimit)//remove last rows
                    grdMessages.Rows.RemoveAt(0);

                grdMessages.Rows.Add(img, DateTime.Now.ToString("dd/MM/yyyy hh:mm tt"), message);

                #region sort desc

                for (int i = 0; i < grdMessages.Rows.Count; i++)
                    for (int j = 0; j < grdMessages.Rows.Count; j++)
                    {
                        try
                        {
                            if (Convert.ToDateTime(grdMessages.Rows[i].Cells[1].Value) > Convert.ToDateTime(grdMessages.Rows[j].Cells[1].Value))
                                grdMessages.Rows.Move(i, j);
                        }
                        catch (Exception ecp)
                        { }
                    }

                #endregion

                grdMessages.ClearSelection();
                if (grdMessages.Rows.Count > 0)
                {
                    grdMessages.Rows[0].IsCurrent = grdMessages.Rows[0].IsSelected = true;
                    grdMessages.TableElement.ScrollToRow(grdMessages.Rows[0]);
                }
            });
        }

        void DisplayForm(UserControlBase frm)
        {
        }


        int StatusBar_MinHeight = 25;
        public void ShowMessageBar(bool show)
        {
            //if (toolTabStrip2.Collapsed && show)
            //{
            //    toolTabStrip2.AutoHidePosition = Telerik.WinControls.UI.Docking.AutoHidePosition.Left;

            //    toolTabStrip2.Collapsed = !show;
            //}

            if (!toolTabStrip2.Collapsed)
            {
                if (show)
                    this.toolTabStrip2.SizeInfo.AbsoluteSize = new System.Drawing.Size(toolTabStrip2.Width, 100);
                else
                    this.toolTabStrip2.SizeInfo.AbsoluteSize = new System.Drawing.Size(toolTabStrip2.Width, StatusBar_MinHeight);
            }
        }

        #endregion

        bool IsDocOpened()
        {
            string fileNames = string.Empty;
            string mess = string.Empty;

            List<string> unDeletedLst = TempraryDocuments.TryDeleteTempFiles();

            if (unDeletedLst.Count > 0)
            {
                if (unDeletedLst.Count > 10) //show only 10 oened file names remove all other
                {
                    try
                    {
                        unDeletedLst.RemoveRange(10, unDeletedLst.Count - 10);
                    }
                    catch (Exception ecp)
                    {
                        GlobalLogger.logger.LogException(ecp);
                    }
                }

                unDeletedLst.ForEach(x => fileNames += Environment.NewLine + Path.GetFileName(x));
                mess = string.Format("You have opened file(s) {0} from DMS. Please close these files before loging out. " + Environment.NewLine + "Do you still want to logout?", fileNames);
                if (DisplayManager.DisplayMessage(mess, MessageType.Confirmation) == System.Windows.Forms.DialogResult.Yes)
                    return false;
                return true;
            }
            else
            {
                return false;
            }
        }

        public void DisplayDeskTopAlert(string message, string caption = null)
        {
            this.Invoke((MethodInvoker)delegate
            {
                radAlert.CaptionText = (caption == null) ? CustomMessages.DesktopAlertCaption : caption;
                radAlert.ContentText = message;
                radAlert.Show();
            });
        }

        public void ShowProcessingBar(bool show)
        { }

        #region Control Events

       

        //private void NewMainForm_FormClosing(object sender, FormClosingEventArgs e)
        //{
        //    if (IsDocOpened() == true)
        //    {
        //        e.Cancel = true;
        //        return;
        //    }
        //    else
        //        Application.Exit();
        //}



        private void btnGroup_Click(object sender, EventArgs e)
        {
            if (AuthenticationService.IsAuthenticated)
                DisplayManager.LoadControl(new UcGroupList());
        }

        private void btnStarredDocument_Click(object sender, EventArgs e)
        {
            if (AuthenticationService.IsAuthenticated)
                DisplayManager.LoadControl(new UcStarDocumentList());
        }

        private void btnChangeTheme_Click(object sender, EventArgs e)
        {
            RadMenuButtonItem btn = (RadMenuButtonItem)sender;
            ThemeResolutionService.ApplicationThemeName = btn.Text;
            drpChangeTheme.Text = btn.Text;
        }

        private void radGridView1_CellFormatting(object sender, CellFormattingEventArgs e)
        {
            if (e.CellElement.IsCurrent)
            {
                e.CellElement.DrawFill = false;
            }
        }

        private void btnTaskList_Click(object sender, EventArgs e)
        {
            if (AuthenticationService.IsAuthenticated)
                DisplayManager.LoadControl(new UcAccountChart());
        }

        private void btnPdfMatch_Click(object sender, EventArgs e)
        {
            if (AuthenticationService.IsAuthenticated)
            {
                ProcessMatchedPDF.StartJob();
                GlobalLogger.logger.LogMessage("Going to exe PDF Analyzer from  = " + AppConstants.PDFAnalyzerExePath);
                Process.Start(AppConstants.PDFAnalyzerExePath);
            }
        }

        private void btnLogout_Click(object sender, EventArgs e)
        {
            if (DisplayManager.DisplayMessage("Are you sure you want to logout?", MessageType.Confirmation) == System.Windows.Forms.DialogResult.Yes)
            {
                if (IsDocOpened())
                {
                    return;
                }

                AuthenticationService.Logout();
                DisplayManager.LoadControl(new UcLogin());
                SetLoginStatus(false);
            }
        }

        #endregion

        private void btnClientDocument_Click_1(object sender, EventArgs e)
        {
            if (AuthenticationService.IsAuthenticated)
                DisplayManager.LoadControl(new UcClientList());
        }

        private void btnVATRate_Click(object sender, EventArgs e)
        {
            if (AuthenticationService.IsAuthenticated)
                DisplayManager.LoadControl(new UcVATRateList());
        }

        private void btnAccountChart_Click(object sender, EventArgs e)
        {
            if (AuthenticationService.IsAuthenticated)
                DisplayManager.LoadControl(new UcAccountChartList());
        }

        private void btnAnalysisCode_Click(object sender, EventArgs e)
        {
            if (AuthenticationService.IsAuthenticated)
                DisplayManager.LoadControl(new UcAnalysisCodeList());
        }

        private void btnAccountGroup_Click(object sender, EventArgs e)
        {
            if (AuthenticationService.IsAuthenticated)
                DisplayManager.LoadControl(new UcAccountGroupList());
        }

        private void btnDescription_Click(object sender, EventArgs e)
        {
            if (AuthenticationService.IsAuthenticated)
                DisplayManager.LoadControl(new UcFindReplaceDesc());
        }

        private void btnUserList_Click(object sender, EventArgs e)
        {
            if (AuthenticationService.IsAuthenticated && AuthenticationService.IsAdminLogin())
                DisplayManager.LoadControl(new UcUserList());
            else
                DisplayManager.DisplayMessage("Only administrator is allowed to view this page. Please ask administrator for help.", MessageType.Error);
        }

        private void btnSupplier_Click(object sender, EventArgs e)
        {
            if (AuthenticationService.IsAuthenticated)
                DisplayManager.LoadControl(new UcSupplierList());
        }

        private void btnRecycleBin_Click(object sender, EventArgs e)
        {
            if (AuthenticationService.IsAuthenticated)
                DisplayManager.LoadControl(new UcRecycleBin());
        }

    }

}
