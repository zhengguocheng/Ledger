using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using DAL;

namespace DMS.UserControls.Popups
{
    public partial class UcVATCodePopup : UserControlBase
    {
        public string SelectedText;
        public long yrEndFolID;

        //add by @zgc 
        //添加是否为第一次激活窗口标识
        bool m_bInit;

        public UcVATCodePopup(long _yrEndFolID)
        {
            InitializeComponent();
            yrEndFolID = _yrEndFolID;
            //add by @zgc 初始化
            m_bInit = true;
        }

        private void UcNominalCodePopup_Load(object sender, EventArgs e)
        {
            DropDownHelper.BindVATCode(drpVATCode,yrEndFolID);
        }

        private void btnOk_Click(object sender, EventArgs e)
        {
            SelectedText = drpVATCode.Text;
            if (!string.IsNullOrWhiteSpace(SelectedText) && SelectedText.Contains(UcExcelSheet.SeperatorChar))
            {
                SelectedText = SelectedText.Split(UcExcelSheet.SeperatorChar.ToCharArray(), StringSplitOptions.RemoveEmptyEntries)[0].TrimEnd();
            }
            DisplayManager.CloseDialouge(DialogResult.OK);
        }
        
        private void drpVATCode_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                btnOk_Click(null, null);
        }
        //add by @zgc  2018年11月18日23:10:14
        public string ToChar(Keys key)
        {
            char c = '\0';
            if ((key >= Keys.A) && (key <= Keys.Z))
            {
                //add by @zgc 2018年12月1日00:08:18
                //判断大写键是否按下
                if (IsKeyLocked(Keys.CapsLock))
                {
                    c = (char)((int)'A' + (int)(key - Keys.A));
                }
                else
                {
                    c = (char)((int)'a' + (int)(key - Keys.A));
                }
            }

            else if ((key >= Keys.D0) && (key <= Keys.D9))
            {
                c = (char)((int)'0' + (int)(key - Keys.D0));
            }//add by @zgc 添加小键盘处理工作 2018年11月30日23:49:16
            else if (key >= Keys.NumPad0 && key <= Keys.NumPad9)
            {
                c = (char)((int)'0' + (int)(key - Keys.NumPad0));
            }

            return c.ToString();
        }
        //add by @zgc 2018年11月18日23:10:29
        private void drpVATCode_KeyUp(object sender, KeyEventArgs e)
        {
            //add by @zgc 2018年11月30日23:38:11
            //添加小键盘按键事件
            if (m_bInit && ((e.KeyCode >= Keys.D0 && e.KeyCode <= Keys.D9) ||
               (e.KeyCode >= Keys.NumPad0 && e.KeyCode <= Keys.NumPad9) || (e.KeyCode >= Keys.A && e.KeyCode <= Keys.Z)))
            {
                m_bInit = false;
                string enteredVACodeChar = "{" + ToChar(e.KeyCode) + "}";
                SendKeys.Send(enteredVACodeChar);

            }
        }
    }
}
