# DogKnights
- 2D 로그라이크 게임
<img src = "https://github.com/Freode/DogKnights/blob/main/ReadMeImage/DogKnights_Title_1.png?raw=true" alt="DogKnights_StageMove" width="500">


## 요약
- 2D 로그라이크 게임을 Unity로 구현했습니다.
- 강아지가 어떤 탑에 모험을 떠나는 이야기를 게임으로 구현했습니다.


## 기간
2023.04. ~ 2023.08.


## 참여 인원
- 성지윤(팀장), 최정호(본인), 김연경, 김종언


- 비중 : FE(25%)


---


팀원 Git 링크
- 성지윤 : https://github.com/ss-zun
- 김연경 : https://github.com/yeonkyeong1022
- 김종언 : 


## 언어
- C#


## 사용 툴
- Unity 2021.03.21f1


## 협업 툴
- Github : 협업 코드 저장소
- Notion : 기획 & 일정 관리


---
<table>
  <tr>
    <td style="text-align: center;">
      <figure>
        <img src = "https://github.com/Freode/DogKnights/blob/main/ReadMeImage/ReadMe_Image_3.png?raw=true" alt="Image 3" width="500">
        <figcaption>Github 협업 사진</figcaption>
      </figure>
    </td>
    <td>
      <figure>
        <img src = "https://github.com/Freode/DogKnights/blob/main/ReadMeImage/ReadMe_Image_4.png?raw=true" alt="Image 4" width="500">
        <figcaption>Notion : 명세서 작성</figcaption>
      </figure>
    </td>
  </tr>
</table>


## 플로우 차트
![플로우 차트](https://github.com/Freode/DogKnights/blob/main/ReadMeImage/ReadMe_Image_1.png)


## 개발 세부 파트
![본인 개발 플로우 차트](https://github.com/Freode/DogKnights/blob/main/ReadMeImage/ReadMe_Image_2.png)


### 스테이지 관리 기능 및 배치
<table>
  <tr>
    <td style="text-align: center;">
      <figure>
        <img src = "https://github.com/Freode/DogKnights/blob/main/ReadMeImage/DogKnights_VariousMap_1.gif?raw=true" alt="DogKnights_VariousMap_1" width="500">
        <figcaption>맵 예시 1</figcaption>
      </figure>
    </td>
    <td>
      <figure>
        <img src = "https://github.com/Freode/DogKnights/blob/main/ReadMeImage/DogKnights_VariousMap_2.gif?raw=true" alt="DogKnights_VariousMap_2" width="500">
        <figcaption>맵 예시 2</figcaption>
      </figure>
    </td>
  </tr>
</table>


- 해당 스테이지의 Visible 상태를 활성화하고 다른 스테이지의 Visible 상태를 비활성화함으로써 자원 사용률 절감
- 모든 스테이지 설계 및 배치

### 스테이지 구조물 상호작용 기능
<img src = "https://github.com/Freode/DogKnights/blob/main/ReadMeImage/DogKnights_StageInteraction.gif?raw=true" alt="DogKnights_StageInteraction" width="500">
- 몬스터를 모두 제거하거나 캐릭터가 문 근처로 이동하는 등, 여러 조건에 의해 문이 열리도록 설계


### 스테이지 이동
<img src = "https://github.com/Freode/DogKnights/blob/main/ReadMeImage/DogKnights_StageMove.gif?raw=true" alt="DogKnights_StageMove" width="500">
- 문 뒤로 이동하거나 특정 지점에 도착할 경우, 다음 스테이지로 이동
- 이전 스테이지의 Visible 비활성화


### 시야 조절 기능
<table>
  <tr>
    <td style="text-align: center;">
      <figure>
        <img src = "https://github.com/Freode/DogKnights/blob/main/ReadMeImage/DogKnights_VariousSight_1.png?raw=true" alt="DogKnights_VariousSight_1" width="500">
        <figcaption>맵 예시 1</figcaption>
      </figure>
    </td>
    <td>
      <figure>
        <img src = "https://github.com/Freode/DogKnights/blob/main/ReadMeImage/DogKnights_VariousSight_2.png?raw=true" alt="DogKnights_VariousSight_2" width="500">
        <figcaption>맵 예시 2</figcaption>
      </figure>
    </td>
  </tr>
</table>
- 스테이지 컨샙별로 다른 시야에서 확인되도록 구현


## 시연 영상
[![시연 영상](https://github.com/Freode/DogKnights/blob/main/ReadMeImage/DogKnights_Title_1.png)](https://youtu.be/aZ6FYSIx8iY)
- 클릭하시면, 시연 영상으로 이동합니다.


## 느낀점
- Unity 엔진과 협업을 처음으로 해본 프로젝트입니다.
- 첫 협업 프로젝트다 보니 코드 충돌, 버전 롤백 등 다양한 문제가 발생하고 이를 해결하기 위해 노력했습니다. 그에 따라, 협업 능력을 키울 수 있었습니다.
- 추후 프로젝트부터 코드 충돌을 방지하기 위한 규칙의 필요성을 깨닫게 되었습니다.

