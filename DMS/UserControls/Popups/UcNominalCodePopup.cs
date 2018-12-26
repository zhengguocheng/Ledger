using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace DMS.UserControls.Popups
{
    public partial class UcNominalCodePopup : UserControlBase
    {
        public string NominalCode;
        public int NominalCodeID;
        public long yrEndFolID;
        string enteredChar;
        //add by @zgc 
        //添加是否为第一次激活窗口标识
        bool m_bInit;
        public UcNominalCodePopup(long _yrEndFolID, string _enteredChr = null)
        {
            InitializeComponent();
            yrEndFolID = _yrEndFolID;
            enteredChar = _enteredChr;
            //add by @zgc 初始化
            m_bInit = true;
        }

        private void UcNominalCodePopup_Load(object sender, EventArgs e)
        {
            DropDownHelper.BindNominalCode(drpNominalCode, yrEndFolID);
            //del by @zgc
            // 统一在keyup里处理
/*            if (!string.IsNullOrWhiteSpace(enteredChar))
            {
                drpNominalCode.SelectedText = enteredChar;
                //add by @zgc sj2018/11/18 13:57
                drpNominalCode.ShowDropDown();
            }*/
        }

        private void btnOk_Click(object sender, EventArgs e)
        {
            NominalCode = drpNominalCode.Text;
            var id  = DropDownHelper.GetSelectedValue(drpNominalCode);
            NominalCodeID = AppConverter.ToInt(id, -1);

            if (!string.IsNullOrWhiteSpace(NominalCode) && NominalCode.Contains(UcExcelSheet.SeperatorChar))
            {
                NominalCode = NominalCode.Split(UcExcelSheet.SeperatorChar.ToCharArray(), StringSplitOptions.RemoveEmptyEntries)[0].TrimEnd();
            }
            DisplayManager.CloseDialouge(DialogResult.OK);
        }

        
        private void drpNominalCode_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                DropDownHelper.CorrectInvalidSearchTerm(drpNominalCode);//Focus changed will not call in this case we need to call CorrectInvalidSearchTerm manually 
                
                btnOk_Click(null, null);
            }
            //del by @zgc 
//            else
//            {
//               if (!string.IsNullOrWhiteSpace(enteredChar))
//                {
//                    drpNominalCode.SelectedText = enteredChar + ToChar(e.KeyCode);
//                    enteredChar = null;//set null so this is not keep on adding
//                }
//            }
        }

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
        //add by @zgc 2018年11月18日22:48:24
        //解决Nominalcode获得焦点时不能自动匹配问题。
        private void drpNominalCode_KeyUp(object sender, KeyEventArgs e)
        {
            //add by @zgc 2018年11月30日23:38:11
            //添加小键盘按键事件
            if (m_bInit && ((e.KeyCode >= Keys.D0 && e.KeyCode <= Keys.D9) ||
               (e.KeyCode >= Keys.NumPad0 && e.KeyCode <= Keys.NumPad9) || (e.KeyCode >= Keys.A && e.KeyCode <= Keys.Z)))
            {
                m_bInit = false;
                enteredChar = "{" + ToChar(e.KeyCode) + "}";
                SendKeys.Send(enteredChar);

                enteredChar = null;
            }

        }
    }
}
