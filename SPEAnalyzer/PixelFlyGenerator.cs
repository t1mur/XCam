using System;
using System.Collections.Generic;
using System.Text;

namespace XCamera
{
    /// <summary>
    /// This class runs on seperate thread.
    /// called by PixelflyController
    /// And manage the pixelfly class.
    /// </summary>
    class PixelFlyGenerator
    {
        public static PixelFlyGenerator instance = null;
        public Pixelfly pf;
        public bool abort = false;

        public PixelFlyGenerator()
        {
            if (instance != null) throw new Exception("BUG in PixelFlyGenerator");
            instance = this;
            pf = new Pixelfly();
        }


        public bool takeOneSetOfImages(int NImages)
        {
            if (abort) return false;
            List<SingleImage> result = new List<SingleImage>();
            for (int i = 0; i < NImages; i++)
            {
                if (abort) return false;
                int err;
                err = pf.CameraSnapCamera();
                if (err != 0)
                {
                    PixelFlyController.instance.Invoke(PixelFlyController.thereIsAnErrorDelegate,
                        new Object[] { "SNAP=" + PixelFlyError.getErrorString(err) });
                    return false;
                }
                err = -1;
                for (int j = 0; j < 20; j++)
                {
                    if (err == 0) break;
                    err = pf.CameraGetImage();
                }
                if (err != 0)
                {
                    PixelFlyController.instance.Invoke(PixelFlyController.thereIsAnErrorDelegate,
                        new Object[] { "GETIMAGE=" + PixelFlyError.getErrorString(err) });
                    return false;
                }
                result.AddRange(pf.getImages());
            }
            if (abort) return false;
            PixelFlyController.instance.images = result;

            pictureIsTaken();
            return true;
        }

        /// <summary>
        /// This is the starting point of the thread.
        /// </summary>
        public void takeManySetsOfImages(int NImages)
        {
            if (abort) return;
            while (abort == false)
            {
                if (!takeOneSetOfImages(NImages)) return;
            }

        }

        /// <summary>
        /// Call this method after finishing the run.
        /// This method will notify the PixelflyController
        /// </summary>
        private void pictureIsTaken()
        {
            PixelFlyController.instance.Invoke(PixelFlyController.pictureIsTakenDelegate);
//            PixelFlyController.pictureIsTakenDelegate();
        }
    }
}
