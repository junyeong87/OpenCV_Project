# 👁️ OpenCV Vision Inspection System

> C# + WPF + OpenCvSharp 기반 비전 검사(Vision Inspection) 프로그램

이미지를 불러와 객체를 검출하고, 면적 및 원형도를 계산하여 검사 결과를 판정하는 WPF 기반 비전 검사 프로그램입니다.

---

# 📌 프로젝트 소개

산업 현장에서 사용하는 머신비전 프로그램을 참고하여 제작한 프로젝트입니다.

OpenCV를 활용하여 이미지 내 객체를 분석하고, 면적과 원형도를 계산하여 OK/NG를 판정하도록 구현했습니다.

실제 산업용 카메라와 PLC는 연결되어 있지 않으며, 테스트 이미지를 이용하여 비전 검사 과정을 시뮬레이션했습니다.

---

# 🛠 개발 환경

* Visual Studio 2022
* .NET 8
* C#
* WPF
* OpenCvSharp
* CommunityToolkit.Mvvm

---

# ✨ 주요 기능

## ✅ 이미지 불러오기

* Open Image 버튼을 통한 이미지 선택
* 다양한 이미지 파일 로드
* 원본 이미지 화면 출력
 
<img width="1157" height="725" alt="image" src="https://github.com/user-attachments/assets/f61891f1-8c16-4602-8661-78dec868ef82" />


<img width="1159" height="722" alt="image" src="https://github.com/user-attachments/assets/4d4e2978-b1be-440c-849a-ba6c8486f62e" />



---

## ✅ 객체 검출

* 이미지 전처리
* 윤곽선(Contour) 검출
* 객체 추출


<img width="1162" height="725" alt="image" src="https://github.com/user-attachments/assets/ea862e67-49ba-4202-b464-a6714d611464" />


---

## ✅ 형상 분석 및 품질 판정

검출된 객체에 대해 다음 정보를 계산합니다.

* 형상(Shape)
* 면적(Area)
* 원형도(Circularity)


<img width="1164" height="726" alt="image" src="https://github.com/user-attachments/assets/1c43d72f-be48-451c-8b4c-72e7aa2d8266" />


측정된 값을 기준으로

* OK
* NG

를 판정하도록 구현했습니다.


<img width="1163" height="722" alt="image" src="https://github.com/user-attachments/assets/f212013b-70b9-4f97-a99e-2762d0281e49" />


---

## ✅ 검사 결과 표시

검사 결과를 테이블 형태로 표시합니다.

표시 항목

* 검사 시간
* 파일명
* Shape
* Area
* Circularity
* Result
* Reason



<img width="1164" height="725" alt="image" src="https://github.com/user-attachments/assets/56220472-58e7-4f47-9f90-df29d471eb3a" />



---

## ✅ 검사 결과 저장

검사 결과를 JSON 파일 형식으로 저장합니다.


<img width="1161" height="722" alt="image" src="https://github.com/user-attachments/assets/a75a2268-eb93-49de-bf96-00744aa768d7" />



<img width="1156" height="721" alt="image" src="https://github.com/user-attachments/assets/e1073920-8870-4956-a304-c7b891b943c6" />


---

# 📚 적용한 기술

## 🏗️ Architecture

### **MVVM Pattern**

View와 ViewModel을 분리하여 UI와 비즈니스 로직의 역할을 명확히 구분했습니다.

CommunityToolkit.Mvvm의 ObservableObject와 RelayCommand를 사용하여 코드 중복을 줄이고 유지보수성을 높였습니다.

---

### **Service Layer**

이미지 분석 기능을 InspectionService로 분리하여 ViewModel이 OpenCV 코드에 직접 의존하지 않도록 구성했습니다.

검사 로직을 독립적인 서비스로 관리하여 테스트와 기능 확장이 용이하도록 설계했습니다.

---

## ⚡ Image Processing

### **OpenCvSharp**

OpenCV 라이브러리를 활용하여

* 이미지 처리
* 윤곽선 검출
* 면적 계산
* 원형도 계산

기능을 구현했습니다.

---

### **Contour Detection**

Contour 기반 객체 검출을 이용하여 검사 대상을 찾고 필요한 형상 정보를 계산했습니다.

---

### **Quality Inspection**

측정된 값을 기준으로 품질을 판정하도록 구현하여 실제 머신비전 검사 흐름을 반영했습니다.

---

## 📊 UI & Data Management

### **Data Binding**

WPF Data Binding을 사용하여 검사 결과가 UI에 자동으로 반영되도록 구현했습니다.

---

### **ObservableCollection**

검사 결과를 ObservableCollection으로 관리하여 새로운 검사 결과가 DataGrid에 실시간으로 표시되도록 구현했습니다.

---


# 📖 배운 점

이번 프로젝트를 통해 OpenCV를 활용한 기본적인 비전 검사 과정을 구현하며 이미지 처리의 전체 흐름을 이해할 수 있었습니다.

특히 이미지 전처리, 객체 검출, 면적 계산, 원형도 계산 등의 기능을 구현하면서 OpenCV의 주요 기능을 익혔습니다.

또한 WPF와 MVVM 패턴을 적용하여 UI와 비즈니스 로직을 분리하는 구조를 경험했으며, Service 계층을 활용하여 검사 로직을 독립적으로 관리하는 설계 방식을 익힐 수 있었습니다.

검사 결과를 ObservableCollection과 Data Binding으로 연결하여 데이터 변경 사항이 UI에 자동으로 반영되는 구조를 구현하며 WPF의 데이터 흐름에 대한 이해를 높일 수 있었습니다.

이 프로젝트를 통해 단순한 이미지 처리 예제를 넘어 실제 산업용 비전 검사 프로그램과 유사한 구조를 설계하고 구현하는 경험을 쌓을 수 있었습니다.
