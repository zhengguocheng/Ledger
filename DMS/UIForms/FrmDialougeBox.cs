using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using Telerik.WinControls;
using DMS.CustomClasses;

namespace DMS.UIForms
{
    public partial class FrmDialougeBox : Telerik.WinControls.UI.RadForm
    {
        UserControlBase childCntrl = null;

        protected FrmDialougeBox()
        {
            InitializeComponent();
        }

        public FrmDialougeBox(UserControlBase cntrl)
        {
            InitializeComponent();
            childCntrl = cntrl;
        }

        private void FrmDialougeBox_Load(object sender, EventArgs e)
        {
            this.Height = childCntrl.Height + 50;
            this.Width = childCntrl.Width + 15;
            
            this.groupBox1.Controls.Add(childCntrl);
            childCntrl.Dock = DockStyle.Fill;

            groupBox1.Text = childCntrl.Caption;
            CenterMe();
        }

        private void CenterMe()
        {
            int boundWidth = Screen.PrimaryScreen.Bounds.Width;
            int boundHeight = Screen.PrimaryScreen.Bounds.Height;
            int x = boundWidth - this.Width;
            int y = boundHeight - this.Height;
            this.Location = new Point(x / 2, y / 2);
        }

        public void SetCaption(string caption)
        {
            this.Text = caption;
        }

        public void SetControl()
        {
            
        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }
    }
}
