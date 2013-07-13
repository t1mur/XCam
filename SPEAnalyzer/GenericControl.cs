using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;

namespace XCamera
{
    public delegate void DelegateRedraw(Graphics g); // delegate for redraw
    public partial class GenericControl : UserControl
    {
        private DelegateRedraw delegateRedraw = null;
        public GenericControl()
        {
            InitializeComponent();
        }

        public void registerDelegateRedraw(DelegateRedraw dr)
        {
            delegateRedraw = dr;
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            if (delegateRedraw != null)
            {
                delegateRedraw(e.Graphics);
            }
            else
            {
                base.OnPaint(e);
            }
        }
        protected override void OnResize(EventArgs e)
        {
            this.Invalidate();
        }
    }
}
