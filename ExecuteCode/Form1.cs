using DMS;
using DMS.UserControls;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ExecuteCode
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            UcAccountChart c = new UcAccountChart();
            c.ImportYearEndCodeData();
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            UcCalender.ModifyFolderNames();
        }
    }
}
