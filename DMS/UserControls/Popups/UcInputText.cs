using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;

namespace DMS.UserControls.Popups
{
    public partial class UcInputText : UserControlBase
    {
        public string InputPath;
        public UcInputText()
        {
            InitializeComponent();
        }

        private void btnOk_Click(object sender, EventArgs e)
        {
            if(string.IsNullOrWhiteSpace(txtInput.Text))
            {
                ShowValidationError(txtInput, "Please enter file name.");
                return;
            }
            if (string.IsNullOrWhiteSpace(txtPath.Text))
            {
                ShowValidationError(btnSelectFolder, "Please enter file path.");
                return;
            }

            if(!string.IsNullOrWhiteSpace(txtInput.Text))
            {
                InputPath = Path.Combine(txtPath.Text,txtInput.Text);
            }
            DisplayManager.CloseDialouge(DialogResult.OK);
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            DisplayManager.CloseDialouge(DialogResult.Cancel);
        }

        

        private void btnSelectFolder_Click(object sender, EventArgs e)
        {
            if(folderBrowserDialog1.ShowDialog() == DialogResult.OK)
            {
                txtPath.Text = folderBrowserDialog1.SelectedPath;
            }
        }
    }
}
