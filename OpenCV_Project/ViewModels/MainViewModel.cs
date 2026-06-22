using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using OpenCvSharp;
using System.Collections.ObjectModel;
using System.IO;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace OpenCV_Project.ViewModels
{
    public partial class MainViewModel : ObservableObject
    {
        // =========================
        // 1. UI 상태
        // =========================

        [ObservableProperty]
        private ImageSource originalImage;

        [ObservableProperty]
        private ImageSource processedImage;

        [ObservableProperty]
        private string resultText = "Ready";

        [ObservableProperty]
        private string shapeText;

        [ObservableProperty]
        private string areaText;

        [ObservableProperty]
        private string circularityText;

        [ObservableProperty]
        private string reasonText;

        [ObservableProperty]
        private ObservableCollection<object> results = new();


        // =========================
        // 2. Commands
        // =========================

        [RelayCommand]
        private void OpenImage()
        {
            var dialog = new Microsoft.Win32.OpenFileDialog
            {
                Filter = "Image Files|*.png;*.jpg;*.jpeg;*.bmp"
            };

            if (dialog.ShowDialog() != true)
                return;

            string filePath = dialog.FileName;

            // 1) 원본 표시
            OriginalImage = new BitmapImage(new Uri(filePath));

            // 2) OpenCV 처리
            Mat inputMat = Cv2.ImRead(filePath);

            Mat resultMat = ProcessImage(inputMat);

            // 3) 결과 표시

            ProcessedImage = MatToBitmapImage(resultMat);
        }


        [RelayCommand]
        private void Inspect()
        {
            ResultText = "Inspecting...";
        }


        [RelayCommand]
        private void Reset()
        {
            OriginalImage = null;
            ProcessedImage = null;

            ResultText = "Ready";
            ShapeText = "";
            AreaText = "";
            CircularityText = "";
            ReasonText = "";
        }


        [RelayCommand]
        private void SaveResult()
        {
            // JSON 저장 or 로그 저장
        }


        // =========================
        // 3. OpenCV 처리 로직
        // =========================

        private Mat ProcessImage(Mat input)
        {
            Mat processingImage = new Mat();
            Cv2.CvtColor(input, processingImage, ColorConversionCodes.BGR2GRAY);

            Mat blur = new Mat();
            Cv2.GaussianBlur(processingImage, blur, new Size(5, 5), 0);

            Mat edges = new Mat();
            Cv2.Canny(blur, edges, 50, 150);

            return edges;
        }


        // =========================
        // 4. Mat → WPF 변환
        // =========================

        private BitmapImage MatToBitmapImage(Mat mat)
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