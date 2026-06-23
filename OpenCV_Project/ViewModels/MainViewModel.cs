    using CommunityToolkit.Mvvm.ComponentModel;
    using CommunityToolkit.Mvvm.Input;
    using System.Collections.ObjectModel;
    using System.Windows.Media;
    using System.Windows.Media.Imaging;
    using OpenCV_Project.Services;
    using OpenCvSharp;


namespace OpenCV_Project.ViewModels
    {

    public partial class MainViewModel : ObservableObject
        {


            private readonly OpenCVService _cvService = new();
            private readonly ImageConverterService _converterService = new();

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
                var processedMat = _cvService.ProcessImage(filePath);


                ProcessedImage = _converterService.Convert(processedMat);

                
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




            // =========================
            // 4. Mat → WPF 변환
            // =========================

            
        }
    }