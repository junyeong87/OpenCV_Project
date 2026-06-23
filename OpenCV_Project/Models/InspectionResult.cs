using System;

namespace OpenCV_Project.Models
{
    public class InspectionResult
    {
        // 검사 시간
        public DateTime Time { get; set; }

        // 이미지 파일명
        public string File { get; set; }

        // 특징 값
        public double Area { get; set; }
        public double Circularity { get; set; }

        // 판정 결과
        public string Result { get; set; }   // OK / NG

        // NG 이유
        public string Reason { get; set; }

        // (선택) 추가 정보
        public string Shape { get; set; }
    }
}