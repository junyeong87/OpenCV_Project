using OpenCV_Project.Models;
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
        private Mat? _sourceImage;
        private Mat? _processedImage;
        private string _fileName = "";

        // =========================
        // 전처리
        // =========================
        public Mat ProcessImage(string filePath)
        {
            _fileName = Path.GetFileName(filePath);

            _sourceImage = Cv2.ImRead(filePath);

            Mat gray = new();
            Cv2.CvtColor(_sourceImage, gray, ColorConversionCodes.BGR2GRAY);

            Mat blur = new();
            Cv2.GaussianBlur(gray, blur, new Size(5, 5), 0);

            _processedImage = new Mat();
            Cv2.Canny(blur, _processedImage, 50, 150);

            return _processedImage;
        }





        /////////////////////////////////////////////////////
        /// 검사
        /////////////////////////////////////////////////////
        public InspectionResult Inspect()
        {
            if (_processedImage == null)
                throw new InvalidOperationException("이미지를 먼저 불러오세요.");

            // 외곽선 검출
            Cv2.FindContours(
                _processedImage,
                out Point[][] contours,
                out HierarchyIndex[] hierarchy,
                RetrievalModes.External,
                ContourApproximationModes.ApproxSimple);

            if (contours.Length == 0)
            {
                return new InspectionResult
                {
                    Time = DateTime.Now,
                    File = _fileName,
                    Result = "NG",
                    Reason = "Contour를 찾을 수 없습니다."
                };
            }



            // 가장 큰 외곽선 선택
            Point[] contour = contours
                .OrderByDescending(c => Cv2.ContourArea(c))
                .First();

            // 면적
            double area = Cv2.ContourArea(contour);

            // 둘레
            double perimeter = Cv2.ArcLength(contour, true);

            // 원형도
            double circularity =
                perimeter == 0
                ? 0
                : 4 * Math.PI * area / (perimeter * perimeter);

            // 도형 단순화
            Point[] approx = Cv2.ApproxPolyDP(
                contour,
                0.02 * perimeter,
                true);

            // 도형 판별
            string shape;

            switch (approx.Length)
            {
                case 3:
                    shape = "Triangle";
                    break;

                case 4:
                    shape = "Rectangle";
                    break;

                default:
                    shape = circularity > 0.85
                        ? "Circle"
                        : "Unknown";
                    break;
            }

            // 판정
            string result;
            string reason;

            if (shape == "Circle" && circularity >= 0.90)
            {
                result = "OK";
                reason = "-";
            }
            else
            {
                result = "NG";
                reason = "형상이 기준과 다릅니다.";
            }

            return new InspectionResult
            {
                Time = DateTime.Now,
                File = _fileName,
                Shape = shape,
                Area = Math.Round(area, 2),
                Circularity = Math.Round(circularity, 3),
                Result = result,
                Reason = reason
            };
        }
    }
}