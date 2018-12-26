
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using Telerik.WinControls;

namespace DMS.UIForms
{
    public partial class FrmAutoUpdate : Telerik.WinControls.UI.RadForm
    {
        public FrmAutoUpdate()
        {
            InitializeComponent();
            ThemeResolutionService.ApplicationThemeName = "Desert";
        }

        private void FrmAutoUpdate_Load(object sender, EventArgs e)
        {
            radWaitingBar1.StartWaiting();
            timer1.Interval = new Random().Next(5, 8) * 1000;
            timer1.Enabled = true;
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            Application.Exit();
            System.Diagnostics.Process.Start("Ledger.exe");
        }

       
    }
}
