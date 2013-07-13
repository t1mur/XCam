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
    public partial class MultiImageViewer : UserControl
    {
        private List<SingleImage> images;
        private List<SingleImage> imageHistory;
        public const int NHistory = 20;
        public const int NDisplayPixelValue = 5;
        public const int NNCount = 4;

        private bool newImages = false;
        private bool isDraging ;
        private bool isMovingRectangle = false;
        private int binning = 1;

        private Graphics graphics;

        private int curMouseX = 0;
        private int curMouseY = 0;

        private Size pictureBoxSize = new Size(696, 512);
        private List<RadioButton> countRButtons;
        private List<Rectangle> countRectangles;
        
        private List<Pen> countPens;
        public static Pen defaultPen = new Pen(System.Drawing.Color.Red);
        private List<Brush> countBurshes;
        public static Brush defaultBrush = new HatchBrush(HatchStyle.Percent20, Color.Red, Color.Transparent);
        private List<Color> countColors;
        private Color[] countColorConstants = new Color[] 
            { Color.Green, Color.Lime, Color.Blue,Color.Cyan, Color.Purple, Color.Magenta, Color.Gray, Color.White };

        private Rectangle defaultRectangle = new Rectangle(0, 0, 0, 0);
        private Point startPoint = new Point(0, 0);
        private Bitmap currentBitmap = null;

        private List<Single> NCounts;
        private Single defaultNCount = 0;

   

        public Rectangle currentROI = new Rectangle(0, 0, 1392, 1024);
        public ImageViewType imageViewType = ImageViewType.Absolute;

        public MultiImageViewer()
        {
            InitializeComponent();
            InitializeComponent2();
            pictureBox.Size = pictureBoxSize;
            isDraging = false;
            graphics = pictureBox.CreateGraphics();
        }

        public void setPictureBoxSize(Size pbSize)
        {
            pictureBox.Size = pbSize;
            pictureBoxSize = pbSize;
            // 140 is the size of the top menu
            this.Size = new Size(Math.Max(385, pbSize.Width), 97+ 100 +44 + pbSize.Height);
            crossSectionViewer.Location = new Point(0, 97 + pbSize.Height);
            crossSectionViewer.MySize = new Size( pbSize.Width, 100);
        }

        public int Binning
        {
            get { return binning; }
            set { binning = value; }
        }

        private void InitializeComponent2()
        {
            this.inputCountGB.SuspendLayout();
            this.SuspendLayout();

            countRButtons = new List<RadioButton>();
            countRectangles = new List<Rectangle>();
            countColors = new List<Color>();
            countBurshes = new List<Brush>();
            countPens = new List<Pen>();
            NCounts = new List<Single>();

            
            this.Controls.Add(crossSectionViewer);

            //creating brushes and pens
            for (int i = 0; i < NNCount*2; i++)
            {
                countPens.Add(new Pen(countColorConstants[i]));
                countBurshes.Add( new HatchBrush(HatchStyle.Percent20, countColorConstants[i], Color.Transparent));
                if (i % 2 == 0) NCounts.Add(0);
            }

            for (int i = 0; i < NNCount*2; i++)
            {
                RadioButton rb = new RadioButton();
                rb.Click += new EventHandler(NCountRB_Click);
                countRButtons.Add(rb);
                countRectangles.Add(new Rectangle(0, 0, 0, 0));
                
                inputCountGB.Controls.Add(rb);
                rb.AutoSize = true;
                rb.Name = "radioButton" + i;
                rb.ForeColor = countColorConstants[i];
                rb.BackColor = Color.Black;
                int x = 6; int y = ((int)(i/2)) * 19 + 17;
                if (i % 2 == 0) // left
                {
                    rb.Text = "N Count #" + ((int) i/2);
                }
                else // right
                {
                    x = 140;
                    rb.Text = "Background #" + ((int) i/2);
                }
                rb.Location = new System.Drawing.Point(x, y);
            }
            this.inputCountGB.ResumeLayout(false);
            this.inputCountGB.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

            displayNCounts();
        }

        private void calculateNCounts()
        {
            defaultNCount = getNCount(defaultRectangle);
            for (int i = 0; i < NNCount * 2; i++)
            {
                int countID = (int)i / 2;
                NCounts[countID] = getNCountMinBackground(countID);
            }
            displayNCounts();
        }

        private void displayNCounts()
        {
            string result = " ";
            string s = "";
            for (int i = 0; i < NNCount; i++)
            {
                s = NCounts[i] + "";
                if (s.Length > 5) s = s.Substring(0, 5);
                result += "N" + i + "=" + s + " | "; 
            }
            s = defaultNCount + "";
            if (s.Length > 5) s = s.Substring(0, 5);
            result += "N=" + s;
            countLabel.Text = result;
        }

        private Single getNCount(Rectangle rec)
        {
            if (images == null) return 0;
            int ID = currentImageID;
            SingleImage image = images[ID];
            return image.getNCount(rec);
        }

        private Single getNCountMinBackground(int countID)
        {
            Rectangle atomRec = countRectangles[countID * 2];
            Rectangle backgroundRec = countRectangles[countID * 2 + 1];
            return images[currentImageID].getNCountMinBackground(atomRec, backgroundRec);
        }

        void NCountRB_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < NNCount*2; i++)
            {
                if (countRButtons[i].Checked)
                {
                    countRectangles[i] = defaultRectangle;
                    countRButtons[i].Checked = false;
                    redraw();
                    int countID = (int)i / 2;
                    NCounts[countID] = getNCountMinBackground(countID);
                    displayNCounts();
                }
            }
        }

        public void displayImages(SingleImage image)
        {
            if (image == null)
            {
                Console.WriteLine("In MultiImageViewer.displayImages(). image == null");
                return;
            }
            if (imageHistory == null) imageHistory = new List<SingleImage>();
            if (imageHistory.Count > NHistory) imageHistory.RemoveAt(imageHistory.Count - 1);
            imageHistory.Reverse();
            imageHistory.Add(image);
            imageHistory.Reverse();
            displayImages(imageHistory);
        }

        public void displayImages(List<SingleImage> images)
        {
            if ((images == null) || (images.Count == 0))
            {

                return;
            }
            if (images != null)
            {
                for (int i = 0; i < images.Count; i++)
                {
                    if (images[i] == null)
                    {
                        Console.WriteLine("In MultiImageViewer.displayImages(). images[i] == null");
                        return;
                    }
                    images[i].MaxBitmapSize = this.pictureBoxSize;
                    images[i].setAbsoluteROI(currentROI);
                    //images[i].getBitmap();
                }
            }
            newImages = true;
            imageIDNUP.Minimum = -1;
            imageIDNUP.Value = -1;
            this.images = images;
            imageIDNUP.Maximum = images.Count - 1;

            newImages = false;
            imageIDNUP.Value = 0;
            imageIDNUP.Minimum = 0;
            
        }

        public int selectedCountIndex
        {
            get
            {
                for (int i = 0; i < NNCount; i++)
                {
                    if (countRButtons[i].Checked) return i;
                }
                return -1;
            }
        }

        public int currentImageID
        {
            get { return (int)imageIDNUP.Value; }
        }

        private void imageIDNUP_ValueChanged(object sender, EventArgs e)
        {
            if (newImages) return;
            if (images == null) return;
            int ID = (int)imageIDNUP.Value;
            if (ID < 0) return;
            if (ID >= images.Count) return;
            currentBitmap = images[ID].getBitmapMust(imageViewType);
            redraw();
            calculateNCounts();
            displayNCounts();
        }

        private void setAllImagesAbsoluteROI(Rectangle ROI)
        {
            if (ROI.Width ==0) return;
            if (ROI.Height == 0) return;
            currentROI = ROI;
            if (images == null) return;
            for (int i = 0; i < images.Count; i++)
            {
                images[i].setAbsoluteROI(ROI);
            }
            defaultRectangle = new Rectangle(0, 0, 0, 0);
            for (int i = 0; i < NNCount * 2; i++)
            {
                int countID = (int)i / 2;
                NCounts[countID] = 0;
                countRectangles[i] = new Rectangle(0, 0, 0, 0);
            }
            displayNCounts();
        }

        private void redraw(Graphics graphics)
        {
            //if (currentBWBitmap == null) return;
            //drawBWBitmap(currentBWBitmap, 0, 0);
            if (currentBitmap == null) return;
            //graphics.FillRectangle(Brushes.Black, new Rectangle(0,0,pictureBoxSize.Width,pictureBoxSize.Height));
            graphics.DrawImage(currentBitmap, 0, 0);
            graphics.FillRectangle(defaultBrush, defaultRectangle);
            graphics.DrawRectangle(defaultPen, defaultRectangle);
            for (int i = 0; i < NNCount*2; i++)
            {
                graphics.DrawRectangle(countPens[i], countRectangles[i]);
                graphics.FillRectangle(countBurshes[i], countRectangles[i]);
            }
        }
        private void redraw()
        {
            redraw(graphics);
        }
        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
            redraw();
        }

        private void displayPixelValue(int y, int x)
        {
            Single val;
            string result;
            iLabel.Text = "";
            for (int i = 0; i < NDisplayPixelValue; i++)
            {
                if (images.Count <= i) break;
                val = images[i].getDataRelativePoint(new Point(x,y));
                result = val + "";
                if (result.Length > 5) result = result.Substring(0, 5);
                if (i != 0) result = result + ", ";
                iLabel.Text += "I" + i + "=" + result;
            }
        }

        private void pictureBox_MouseMove(object sender, MouseEventArgs e)
        {
            int x = e.X; int y = e.Y;
            if (e.X < 0) x = 0; if (e.X >= pictureBoxSize.Width) x = pictureBoxSize.Width - 1;
            if (e.Y < 0) y = 0; if (e.Y >= pictureBoxSize.Height) y = pictureBoxSize.Height - 1;
            xLabel.Text = "x=" + e.X;
            yLabel.Text = "y=" + e.Y;
            curMouseX = e.X;
            curMouseY = e.Y ;
            if (images != null)
                if (images.Count > currentImageID)
                {
                    displayPixelValue(y,x);
                    Point p = images[currentImageID].fromRelativeToAbsolutePoint(new Point(x, y));
                    xLabel.Text = "x=" + p.X;
                    yLabel.Text = "y=" + p.Y;
                }
            if (isDraging)
            {
                Size recSize = new Size(Math.Abs(x - startPoint.X), Math.Abs(y - startPoint.Y));
                Point start = new Point(Math.Min(x, startPoint.X), Math.Min(y, startPoint.Y));
                defaultRectangle = new Rectangle(start, recSize);
                defaultNCount = getNCount(defaultRectangle);
                displayNCounts();
                redraw();
            }
            if (isMovingRectangle)
            {
                defaultRectangle.Offset(x - startPoint.X, y - startPoint.Y);
                startPoint = new Point(x, y);
                defaultNCount = getNCount(defaultRectangle);
                displayNCounts();
                redraw();
            }
        }

        private void pictureBox_MouseDown(object sender, MouseEventArgs e)
        {
            int x = e.X; int y = e.Y;
            if (e.X < 0) x = 0; if (e.X >= pictureBoxSize.Width) x = pictureBoxSize.Width - 1;
            if (e.Y < 0) y = 0; if (e.Y >= pictureBoxSize.Height) y = pictureBoxSize.Height - 1;
            if (e.Button == MouseButtons.Left)
            {
                if (defaultRectangle.Contains(x, y)) // drag the old box
                {
                    startPoint = new Point(x, y);
                    isMovingRectangle = true;
                }
                else // create new box
                {
                    isDraging = true;
                    startPoint = new Point(x, y);
                }
            }
            if (e.Button == MouseButtons.Right)
            {
                if (images != null)
                {
                    contextMenuStrip.Show(pictureBox, new Point(x, y));
                }
            }
        }

        private void pictureBox_MouseUp(object sender, MouseEventArgs e)
        {
            pictureBox_MouseMove(sender, e);
            isDraging = false;
            isMovingRectangle = false;
            defaultNCount = getNCount(defaultRectangle);
            displayNCounts();
        }

        private void pictureBox_Click(object sender, EventArgs e)
        {

        }

        private void MultiImageViewer_Load(object sender, EventArgs e)
        {

        }

        private void button595_Click(object sender, EventArgs e)
        {
            if (images == null) return;
            int ID = (int)imageIDNUP.Value;
            if (ID < 0) return;
            if (ID >= images.Count) return;
            imageViewType = ImageViewType.Relative;
            currentBitmap = images[ID].getBitmapMust(imageViewType);
            images[ID].prefferedViewType = imageViewType;
            redraw();
        }

        private void button0135_Click(object sender, EventArgs e)
        {
            if (images == null) return;
            int ID = (int)imageIDNUP.Value;
            if (ID < 0) return;
            if (ID >= images.Count) return;
            imageViewType = ImageViewType.Absolute;
            currentBitmap = images[ID].getBitmapMust(imageViewType);
            images[ID].prefferedViewType = imageViewType;
            redraw();
        }

        private void fullROIButton_Click(object sender, EventArgs e)
        {
            if (images == null) return;
            currentROI = new Rectangle(0, 0, 1392, 1024);
            setAllImagesAbsoluteROI(currentROI);
            currentBitmap = images[currentImageID].getBitmap(imageViewType);
            pictureBox.Size = new Size(currentBitmap.Width, currentBitmap.Height);
            redraw();
        }

        private void setROIToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // setting ROI
            if ((defaultRectangle.Height > 0) && (defaultRectangle.Width > 0))
            {
                images[currentImageID].setRelativeROI(defaultRectangle);
                currentROI = images[currentImageID].getROI();
                setAllImagesAbsoluteROI(currentROI);
                currentBitmap = images[currentImageID].getBitmap(imageViewType);
                pictureBox.Size = new Size(currentBitmap.Width, currentBitmap.Height);
                defaultRectangle = new Rectangle(0, 0, 0, 0);
                redraw();
            }
        }

        private void fullROIToolStripMenuItem_Click(object sender, EventArgs e)
        {
            fullROIButton_Click(this, null);
        }

        private void crossSectionToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (defaultRectangle.Width == 0) return;
            if (defaultRectangle.Height == 0) return;
            if (images == null) return;
            SingleImage image = images[currentImageID];
            crossSectionViewer.displayCrossSection(image, image.fromRelativeToAbsolute(defaultRectangle));
        }

    }
    [Serializable]
    public class ImageViewType
    {
        public const int N = 3;
        public int type;
        public static string[] names = { "Absolute", "Relative", "Default" };
        public static ImageViewType[] allViewTypes;
        public static readonly ImageViewType Absolute = new ImageViewType(0);
        public static readonly ImageViewType Relative = new ImageViewType(1);
        public static readonly ImageViewType Default = new ImageViewType(2);

        public ImageViewType() { }
        private ImageViewType(int type)
        {
            this.type = type;
            if (allViewTypes == null) allViewTypes = new ImageViewType[N];
            allViewTypes[type] = this;

        }
        public string Name() { if (type < N) { return names[type]; } else  return "No name"; }
        public int Value { get { return type; } }
        public static bool operator ==(ImageViewType cm1, ImageViewType cm2) { if (((object)cm1 == null) || ((object)cm2 == null))return false; return (cm1.type == cm2.type); }
        public static bool operator !=(ImageViewType cm1, ImageViewType cm2) { if (((object)cm1 == null) || ((object)cm2 == null))return true; return (cm1.type != cm2.type); }
        public override bool Equals(object obj)
        {
            if (obj == null) return false;
            if (obj.GetType() == this.GetType())
                return (((ImageViewType)obj) == this);
            return false;
        }
        public override int GetHashCode() { return 1; }
        public override string ToString() { return names[type]; }
    }
}
