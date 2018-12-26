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
    public partial class UcGridSearch : UserControlBase
    {
        public string SearchTerm;
        public UcGridSearch()
        {
            InitializeComponent();
        }

        private void btnOk_Click(object sender, EventArgs e)
        {
            if(!string.IsNullOrWhiteSpace(txtSearchTerm.Text))
            {
                SearchTerm = txtSearchTerm.Text;
            }
            DisplayManager.CloseDialouge(DialogResult.OK);

        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            DisplayManager.CloseDialouge(DialogResult.Cancel);
        }
    }
}
