using OpenCvSharp;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;

namespace OpenCV_Project.Services
{
    internal class ImageConverterService
    {
        public BitmapImage Convert(Mat mat)
        {
            byte[] imageBytes = mat.ImEncode(".png");

            using var ms = new MemoryStream(imageBytes);

            BitmapImage bitmap = new BitmapImage();
            bitmap.BeginInit();
            bitmap.CacheOption = BitmapCacheOption.OnLoad;
            bitmap.StreamSource = ms;
            bitmap.EndInit();
            bitmap.Freeze();

            return bitmap;
        }
    }
}
