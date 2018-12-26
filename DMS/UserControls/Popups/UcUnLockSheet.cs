using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace DMS.UserControls
{
    public partial class UcUnLockSheet : UserControlBase
    {
        public string InputString;
        public UcUnLockSheet()
        {
            InitializeComponent();
        }

        private void btnOk_Click(object sender, EventArgs e)
        {
            if(!string.IsNullOrWhiteSpace(txtInput.Text))
            {
                InputString = txtInput.Text;
            }
            DisplayManager.CloseDialouge(DialogResult.OK);

        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            DisplayManager.CloseDialouge(DialogResult.Cancel);
        }

        private void UcUnLockSheet_Load(object sender, EventArgs e)
        {

        }

    }
}
