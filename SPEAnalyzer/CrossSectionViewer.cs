using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Data;
using System.Text;
using System.Windows.Forms;
namespace XCamera
{
    class CrossSectionViewer :PictureBox
    {
        private Size size = new Size(300, 200);
        private Single[] crossSectionData = null;
        private Graphics graphics;

        private Rectangle defaultRectangle = new Rectangle(0, 0, 0, 0);
        private Point startPoint = new Point(0, 0);
        private bool isMovingRectangle = false;
        private bool isDragging = false;
        public CrossSectionViewer()
        {
            InitializeComponent2();
        }
        private void InitializeComponent2()
        {
            this.BackColor = Color.Aqua;
            graphics = this.CreateGraphics();
            this.MouseMove += new MouseEventHandler(CrossSectionViewer_MouseMove);
            this.MouseDown += new MouseEventHandler(CrossSectionViewer_MouseDown);
            this.MouseUp += new MouseEventHandler(CrossSectionViewer_MouseUp);
            
        }

        void CrossSectionViewer_MouseUp(object sender, MouseEventArgs e)
        {
            CrossSectionViewer_MouseMove(sender, e);
            isDragging = false;
            isMovingRectangle = false;
        }

        void CrossSectionViewer_MouseDown(object sender, MouseEventArgs e)
        {
            int x = e.X; int y = e.Y;
            if (e.X < 0) x = 0; if (e.X >= this.Width) x = this.Width - 1;
            if (e.Y < 0) y = 0; if (e.Y >= this.Height) y = this.Height - 1;
            if (e.Button == MouseButtons.Left)
            {
                if (defaultRectangle.Contains(x, y)) // drag the old box
                {
                    startPoint = new Point(x, y);
                    isMovingRectangle = true;
                }
                else // create new box
                {
                    isDragging = true;
                    startPoint = new Point(x, y);
                }
            }
        }

        void CrossSectionViewer_MouseMove(object sender, MouseEventArgs e)
        {
            int x = e.X; int y = e.Y;
            if (e.X < 0) x = 0; if (e.X >= this.Width) x = this.Width - 1;
            if (e.Y < 0) y = 0; if (e.Y >= this.Height) y = this.Height - 1;
            //xLabel.Text = "x=" + e.X;
            //yLabel.Text = "y=" + e.Y;
            if (isDragging)
            {
                Size recSize = new Size(Math.Abs(x - startPoint.X), Math.Abs(y - startPoint.Y));
                Point start = new Point(Math.Min(x, startPoint.X), Math.Min(y, startPoint.Y));
                defaultRectangle = new Rectangle(start, recSize);
                redraw();
            }
            if (isMovingRectangle)
            {
                defaultRectangle.Offset(x - startPoint.X, y - startPoint.Y);
                startPoint = new Point(x, y);
                redraw();
            }
        }
        public Size MySize
        {
            get { return size; }
            set
            {
                size = value;
                //this.Size = size;
                this.Size = size;
                graphics = this.CreateGraphics();
                redraw();
            }
        }

        public static Single[] calculateCrossSection(SingleImage image, Rectangle absoluteROI)
        {
            bool xDirection = absoluteROI.Width > absoluteROI.Height;
            int len;
            if (xDirection) len = absoluteROI.Width; else len = absoluteROI.Height;
            Single[] result = new Single[len];
            if (xDirection)
            {
                for (int x = absoluteROI.X; x < absoluteROI.X + absoluteROI.Width; x++)
                {
                    for (int y = absoluteROI.Y; y < absoluteROI.Y + absoluteROI.Height; y++)
                    {
                        result[x - absoluteROI.X] += image.data[y, x];
                    }
                }
            }
            else
            {
                for (int x = absoluteROI.X; x < absoluteROI.X + absoluteROI.Width; x++)
                {
                    for (int y = absoluteROI.Y; y < absoluteROI.Y + absoluteROI.Height; y++)
                    {
                        result[y - absoluteROI.Y] += image.data[y, x];
                    }
                }
            }
            return result;
        }

        //display the cross section
        public void displayCrossSection(SingleImage image, Rectangle absoluteROI)
        {
            crossSectionData = calculateCrossSection(image, absoluteROI);
            redraw();
        }

        public static double Min(Single[] data)
        {
            double min = double.MaxValue;
            for (int i = 0; i < data.Length; i++)
            {
                if (min > data[i]) min = data[i];
            }
            return min;
        }
        public static double Max(Single[] data)
        {
            double max = double.MinValue;
            for (int i = 0; i < data.Length; i++)
            {
                if (max < data[i]) max = data[i];
            }
            return max;
        }
        public void redraw(Graphics graphics)
        {
            graphics.FillRectangle(Brushes.Black, new Rectangle(0,0,size.Width,size.Height));
            //graphics.DrawLine(Pens.Blue, new PointF(0, 0), new PointF(size.Width, size.Height));
            if (!((crossSectionData == null) || (crossSectionData.Length < 2))) 
            {
                double xScale = ((double)size.Width) / (crossSectionData.Length);
                double max = Max(crossSectionData);
                double min = Min(crossSectionData);
                if (max == min) return;
                double yScale = this.Height / (max - min);
                double x1 = 0;
                double y1 = (crossSectionData[0] - min) * yScale;
                for (int i = 1; i < crossSectionData.Length; i++)
                {
                    double x2 = x1 + xScale;
                    double y2 = (crossSectionData[i] - min) * yScale;
                    graphics.DrawLine(Pens.Blue, new Point((int)x1, (int)y1), new Point((int)x2, (int)y2));
                    x1 = x2; y1 = y2;
                }
            }
            graphics.FillRectangle(MultiImageViewer.defaultBrush, defaultRectangle);
            graphics.DrawRectangle(MultiImageViewer.defaultPen, defaultRectangle);
        }
        public void redraw()
        {
            redraw(graphics);
        }
        
        protected override void OnPaint(PaintEventArgs e)
        {
            //base.OnPaint(e);
            redraw(e.Graphics);
        }
    }
}
