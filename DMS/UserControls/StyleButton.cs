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
    public partial class StyleButton : UserControl
    {
        public StyleButton()
        {
            InitializeComponent();
        }

        private void StyleButton_Load(object sender, EventArgs e)
        {
            pictureBox3.Location = new Point(0, 3);
            pictureBox3.Size = new Size(24, 24);
            lblLine.Location = new Point(30, 22);
            lblHeader.Location = new Point(30, 3);
        }

        [Browsable(true)]
        public string ButtonCaption
        {
            get
            {
                return lblHeader.Text;
            }
            set
            {
                lblHeader.Text = value;
            }
        }

        public new event EventHandler ClickCommand
        {
            add
            {
                base.Click += value;
                foreach (Control control in Controls)
                {
                    control.Click += value;
                }
            }
            remove
            {
                base.Click -= value;
                foreach (Control control in Controls)
                {
                    control.Click -= value;
                }
            }
        }

        private void panel3_Click(object sender, EventArgs e)
        {

        }
    }
}
