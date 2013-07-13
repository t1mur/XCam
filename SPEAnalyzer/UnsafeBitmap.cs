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

    public unsafe class UnsafeBitmap
    {
        Bitmap bitmap;

        // three elements used for MakeGreyUnsafe
        int width;
        BitmapData bitmapData = null;
        Byte* pBase = null;

        public UnsafeBitmap(Bitmap bitmap)
        {
            this.bitmap = new Bitmap(bitmap);
        }

        public UnsafeBitmap(int width, int height)
        {
            this.bitmap = new Bitmap(width, height, PixelFormat.Format24bppRgb);
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
            width = (int)boundsF.Width * sizeof(PixelData);
            if (width % 4 != 0)
            {
                width = 4 * (width / 4 + 1);
            }
            bitmapData =
             bitmap.LockBits(bounds, ImageLockMode.ReadWrite, PixelFormat.Format24bppRgb);

            pBase = (Byte*)bitmapData.Scan0.ToPointer();
        }

        public PixelData GetPixel(int x, int y)
        {
            PixelData returnValue = *PixelAt(x, y);
            return returnValue;
        }

        public void SetPixel(int x, int y, PixelData colour)
        {
            PixelData* pixel = PixelAt(x, y);
            *pixel = colour;
        }

        public void SetPixel(byte[,] data)
        {
            PixelData pd;
            PixelData* pixel;
            for (int i = 0; i < data.GetLength(0); i++)
            {
                for (int j = 0; j < data.GetLength(1); j++)
                {
                    byte b = data[i, j];
                    pd = new PixelData(b, b, b);
                    pixel = (PixelData*) (pBase + i * width + j * sizeof(PixelData));
                    *pixel = pd;
                }
            }
        }
        public void SetPixel(float[,] data)
        {
            PixelData pd;
            PixelData* pixel;

            int val;
            byte a, b, c;

            for (int i = 0; i < data.GetLength(0); i++)
            {
                for (int j = 0; j < data.GetLength(1); j++)
                {
                    val = (int)data[i, j];
                    a = (byte)(val/65536);
                    b = (byte)((val/256)%256);
                    c = (byte)(val%256);
                    pd = new PixelData(a, b, c);
                    pixel = (PixelData*)(pBase + i * width + j * sizeof(PixelData));
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
        public PixelData* PixelAt(int x, int y)
        {
            return (PixelData*)(pBase + y * width + x * sizeof(PixelData));
        }

        public static Bitmap generateBitmap(byte[,] data)
        {
            UnsafeBitmap ub = new UnsafeBitmap(data.GetLength(1), data.GetLength(0));
            ub.LockBitmap();
            ub.SetPixel(data);
            ub.UnlockBitmap();
            return ub.Bitmap;
        }
        public static Bitmap generateBitmap(float[,] data)
        {
            UnsafeBitmap ub = new UnsafeBitmap(data.GetLength(1), data.GetLength(0));
            ub.LockBitmap();
            ub.SetPixel(data);
            ub.UnlockBitmap();
            return ub.Bitmap;
        }
    }
    public struct PixelData
    {
        public byte blue;
        public byte green;
        public byte red;
        public PixelData(byte blue, byte green, byte red)
        {
            this.blue = blue;
            this.green = green;
            this.red = red;
        }
    }

   
}
