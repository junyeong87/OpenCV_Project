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
    internal class OpenCVService
    {
        public Mat ProcessImage(String filePath)
        {
            Mat input = Cv2.ImRead(filePath);

            Mat gray = new Mat();
            Cv2.CvtColor(input, gray, ColorConversionCodes.BGR2GRAY);

            Mat blur = new Mat();
            Cv2.GaussianBlur(gray, blur, new Size(5, 5), 0);

            Mat edges = new Mat();
            Cv2.Canny(blur, edges, 50, 150);

            return edges;
        }







    }
}
