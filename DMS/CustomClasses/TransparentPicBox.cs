using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Drawing;

namespace DMS.CustomClasses
{
    //class TransparentPicBox : PictureBox
    //{
    //}

    class TransPictureBox : Control
    {
        private Image _image = null;

        public Image Image
        {
            get
            {
                return _image;
            }
            set
            {
                _image = value;
            }
        }
        public TransPictureBox()
        {
        }

        protected override void OnPaintBackground(PaintEventArgs pevent)
        {
        }

        protected override void OnPaint(PaintEventArgs pe)
        {
            if (Image != null)
                pe.Graphics.DrawImage(Image, 0, 0);
        }
        protected override CreateParams CreateParams
        {
            get
            {
                CreateParams cp = base.CreateParams;
                cp.ExStyle |= 0x20;
                return cp;
            }
        }
    }
}
