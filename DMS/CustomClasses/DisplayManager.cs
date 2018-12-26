using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.ComponentModel;
using DMS.UIForms;
using System.Drawing;
using DAL;
using System.IO;
using DMS.UserControls;

namespace DMS
{
    public enum MessageType
    {
        Error, Confirmation, Success, InfoDialogue
    }

    public enum ActionType
    {
        LoginComplete, SetCaption
    }

    public interface IMainForm
    {
        Control MainArea { get; }

        DialogResult DisplayMessage(string message, MessageType mType);

        void DisplayDeskTopAlert(string message, string caption = null);

        void CustomeAction(ActionType action, object val = null);

        void ShowMessageBar(bool show);

        void ShowProcessingBar(bool show);

    }


    public static class DisplayManager
    {
        static Form mainFrm;
        static UcClientList preClientListForm;
        static UcNavigation preUcNavigation;
        public static UserControlBase PreActiveForm;
        public static List<UserControlBase> lstPreActiveForms = new List<UserControlBase>();

        public delegate bool OnControlLoadingDelegate();
        public static OnControlLoadingDelegate OnControlLoading;//declare delegate variable

        public static Form MainForm
        {
            get
            {
                return mainFrm;
            }
            set
            {
                mainFrm = value;
            }
        }

        static DisplayManager()
        {

        }

        static Control GetMainPanel()
        {
            IMainForm frm = (IMainForm)MainForm;
            return frm.MainArea;
        }

        //public static bool LoadPreviousControl(Type typ)
        //{

        //    UserControlBase uc = null;

        //    if (typ == typeof(UcNavigation) && preUcNavigation != null)
        //        uc = preUcNavigation;

        //    else if (preActiveForm != null)
        //    {
        //        uc = preActiveForm;
        //    }

        //    if (uc == null)
        //        return false;

        //    Control p = GetMainPanel();

        //    if (p.Controls.Count > 0)
        //    {
        //        p.Controls[0].Dispose();//to eliminate memory leaks dispose previous control
        //    }

        //    p.Controls.Clear();
        //    uc.Dock = DockStyle.Fill;
        //    p.Controls.Clear();
        //    p.Controls.Add(uc);

        //    SetCaption(uc.Caption);

        //    if (uc.Caption.ToLower() == "Email Template".ToLower())
        //        ShowMessageBar(false);
        //    else
        //        ShowMessageBar(true);
        //}

        public static void LoadPrev(Type typ)
        {
            UserControlBase cnt = null;
            try
            {
                for (int i = lstPreActiveForms.Count - 1; i >= 0; i--)
                {
                    var item = lstPreActiveForms[i];
                    Type cntTyp = item.GetType();
                    if (cntTyp.IsAssignableFrom(typ))
                    {
                        cnt = item;

                        break;
                    }
                }

                if (cnt != null)
                {
                    int index = lstPreActiveForms.LastIndexOf(cnt);
                    lstPreActiveForms.RemoveRange(index, lstPreActiveForms.Count - index);
                    LoadControlInPanel(cnt, null);
                }
            }
            catch { }

            
        }
        public static void LoadControl(UserControlBase uc, bool loadPreviousCnt = false, UserControlBase preForm = null)
        {
            if (OnControlLoading != null)
            {
                if (OnControlLoading() == false)
                    return;
            }

            #region Everytime Restore Previous UcClientList

            if (uc is UcClientList)
            {
                if (preClientListForm == null)
                    preClientListForm = (UcClientList)uc;//initialize 
                else
                {
                    preClientListForm.SaveColumnSetting();
                    uc = preClientListForm;//load existing control
                }
            }

            #endregion

            #region load pre controls

            if (preForm != null && preForm is UcNavigation)
                preUcNavigation = (UcNavigation)preForm;


            if (preForm != null)
            {
                PreActiveForm = preForm;
                lstPreActiveForms.Add(preForm);
            }
            if (loadPreviousCnt)
            {
                if (uc is UcNavigation)
                {
                    uc = preUcNavigation;//load existing control
                }
                else
                {
                    //To be displayed properly This form must override dispose method
                    uc = PreActiveForm;
                }
            }

            #endregion

            LoadControlInPanel(uc, preForm);
        }

        private static void LoadControlInPanel(UserControlBase uc, UserControlBase preForm)
        {
            if(uc == null)
            {
                return;
            }
            Control p = GetMainPanel();

            if (p.Controls.Count > 0)
            {
                var cnt = (UserControlBase)p.Controls[0];
                cnt.ControlClosing();

                p.Controls[0].Dispose();//to eliminate memory leaks dispose previous control
            }

            p.Controls.Clear();
            uc.Dock = DockStyle.Fill;
            uc.PreviousForm = preForm;
            p.Controls.Clear();
            p.Controls.Add(uc);

            uc.ControlDisplayed();

            SetCaption(uc.Caption);

            if (uc.Caption.ToLower() == "Email Template".ToLower())
                ShowMessageBar(false);
            else
                ShowMessageBar(true);
        }

        public static void SetCaption(string cap)
        {
            IMainForm frm = (IMainForm)MainForm;
            frm.CustomeAction(ActionType.SetCaption, cap);

        }

        public static DialogResult DisplayMessage(string message, MessageType mType)
        {
            IMainForm frm = (IMainForm)MainForm;
            return frm.DisplayMessage(message, mType);
        }

        public static DialogResult DisplayCrudMessage(CrudMessageType crudType, CustomMessages cm)
        {
            IMainForm frm = (IMainForm)MainForm;
            return frm.DisplayMessage(cm.GetEntityCrudMessage(crudType), MessageType.Success);
        }

        public static void CustomeAction(ActionType act, object val = null)
        {
            IMainForm frm = (IMainForm)MainForm;
            frm.CustomeAction(act, val);
        }

        public static void DisplayDeskTopAlert(string message, string caption = null)
        {
            IMainForm frm = (IMainForm)MainForm;
            frm.DisplayDeskTopAlert(message, caption);
        }

        public static void ShowMessageBar(bool show)
        {
            IMainForm frm = (IMainForm)MainForm;
            frm.ShowMessageBar(show);
        }

        static FrmDialougeBox dialogueForm;
        public static DialogResult ShowDialouge(UserControlBase cntrl, string caption = null)
        {
            dialogueForm = new FrmDialougeBox(cntrl);

            if (!string.IsNullOrEmpty(caption))
                dialogueForm.SetCaption(caption);

            DialogResult dr = dialogueForm.ShowDialog();
            return dr;
        }

        public static void CloseDialouge(DialogResult dr)
        {
            if (dialogueForm != null)//this check is for dialougue box null
                dialogueForm.DialogResult = dr;
        }

        public static void ShowProcessingBar(bool show)
        {
            IMainForm frm = (IMainForm)MainForm;
            frm.ShowProcessingBar(show);
        }

        public static void SetDocumentIcon(iDocumentItemUI item)
        {
            item.StarImg = item.IsStarred() ? Properties.Resources.goldenStar : Properties.Resources.blackStar;

            if (item.IsFolder)
            {
                item.DocImg = Properties.Resources.imgFolder16;
                //if (item.IsLedgerYearEnd()) dont use custom image for yearend folder
                //{
                //    item.DocImg = Properties.Resources.imgCalender16;
                //}
            }
            else
            {
                item.DocImg = Properties.Resources.imgFile16;//set default

                if (item.IsLocked() == true)
                {
                    item.DocImg = Properties.Resources.imgLock;//set default
                }

                string ext = Path.GetExtension(item.Name);
                if (ext == ".pdf")
                    item.DocImg = Properties.Resources.imgPdf16;
                else if (ext == ".doc" || ext == ".docx")
                    item.DocImg = Properties.Resources.imgWord16;
                else if (ext == ".jpeg" || ext == ".jpg" || ext == ".bmp" || ext == ".png" ||
                        ext == ".tif" || ext == ".dib" || ext == ".tiff" || ext == ".tif" ||
                        ext == ".jiff" || ext == ".jif")
                    item.DocImg = Properties.Resources.bitmap16;
                else if (ext == ".xls" || ext == ".xlsx")
                    item.DocImg = Properties.Resources.imgExcel16;
                else if (ext == ".txt")
                    item.DocImg = Properties.Resources.imgtext;
            }
        }
    }
}
