using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;
using System.Drawing.Imaging;


namespace XCamera
{
    /// <summary>
    /// UnsafeBitmap your_fast_bitmap = new UnsafeBitmap(old_safe_bitmap);
    /// your_fast_bitmap.LockBitmap();
    /// PixelData pixel = your_fast_bitmap.getPixel(3, 4);
    /// //more setpixel/getpixel calls here!
    /// your_fast_bitmap.UnlockBitmap();

    /// </summary>

    public unsafe class UnsafeBitmapTIF
    {
        Bitmap bitmap;
        

        // three elements used for MakeGreyUnsafe
        int width;
        BitmapData bitmapData = null;
        Byte* pBase = null;
        
        public UnsafeBitmapTIF(Bitmap bitmap)
        {
            this.bitmap = new Bitmap(bitmap);
        }

        public UnsafeBitmapTIF(int width, int height)
        {
            this.bitmap = new Bitmap(width, height, PixelFormat.Format48bppRgb);
        }

        public void Dispose()
        {
            bitmap.Dispose();
        }

        public Bitmap Bitmap
        {
            get
            {
                return (bitmap);
            }
        }

        private Point PixelSize
        {
            get
            {
                GraphicsUnit unit = GraphicsUnit.Pixel;
                RectangleF bounds = bitmap.GetBounds(ref unit);
                return new Point((int)bounds.Width, (int)bounds.Height);
            }
        }

        public void LockBitmap()
        {
            GraphicsUnit unit = GraphicsUnit.Pixel;
            RectangleF boundsF = bitmap.GetBounds(ref unit);
            Rectangle bounds = new Rectangle((int)boundsF.X,
             (int)boundsF.Y,
             (int)boundsF.Width,
             (int)boundsF.Height);

            // Figure out the number of bytes in a row
            // This is rounded up to be a multiple of 4
            // bytes, since a scan line in an image must always be a multiple of 4 bytes
            // in length. 
            width = (int)boundsF.Width * sizeof(PixelDataG);
            if (width % 4 != 0)
            {
                width = 4 * (width / 4 + 1);
            }
            bitmapData = bitmap.LockBits(bounds, ImageLockMode.ReadWrite, PixelFormat.Format48bppRgb);
            pBase = (Byte*)bitmapData.Scan0.ToPointer();
        }

        public PixelDataG GetPixel(int x, int y)
        {
            PixelDataG returnValue = *PixelAt(x, y);
            return returnValue;
        }

        public void SetPixel(int x, int y, PixelDataG colour)
        {
            PixelDataG* pixel = PixelAt(x, y);
            *pixel = colour;
        }

        public void SetPixel(float[,] data)
        {
            PixelDataG pd;
            PixelDataG* pixel;
            ushort value;  
            for (int i = 0; i < data.GetLength(0); i++)
            {
                for (int j = 0; j < data.GetLength(1); j++)
                {
                    value = (ushort)data[i,j];
                    pd = new PixelDataG(value, value, value);
                    pixel = (PixelDataG*) (pBase + i * width + j * sizeof(PixelDataG));
                    *pixel = pd;
                }
            }
        }
        public void UnlockBitmap()
        {
            bitmap.UnlockBits(bitmapData);
            bitmapData = null;
            pBase = null;
        }
        public PixelDataG* PixelAt(int x, int y)
        {
            return (PixelDataG*)(pBase + y * width + x * sizeof(PixelDataG));
        }

        public static Bitmap generateBitmap(float[,] data)
        {
            UnsafeBitmapTIF ub = new UnsafeBitmapTIF(data.GetLength(1), data.GetLength(0));
            ub.LockBitmap();
            ub.SetPixel(data);
            ub.UnlockBitmap();
            return ub.Bitmap;
        }
    }
    public struct PixelDataG
    {
        public ushort r;
        public ushort g;
        public ushort b;
        public PixelDataG(ushort r, ushort g, ushort b)
        {
            this.r = r;
            this.g = g;
            this.b = b;
        }
    }

   
}
