# Omok

Unity를 이용해 한게임 오목을 2인용으로 클론 코딩하였습니다.

### 개발 기간, 인원

- 개발 4일
- 1인 

### 개발 환경

Unity 2021.3.01f

### 기능

- 오목 로직
   - 바둑판 안에 고정된 바둑알에서 시작해 4방향(오른쪽, 아래, 오른쪽 위, 오른쪽 아래)으로 탐색하면서 정확히 5개가 같은 색일 경우 승리

#### 클래스 설명

- `Constants` 클래스 
   - 상수 값들을 정의하는 클래스
   - 바둑판의 색상, 크기 등과 같은 상수들을 저장

- [`GameManager 클래스`](https://github.com/youhyeoneee/Omok/blob/main/Assets/Sciprts/GameManager.cs)
   - 게임의 주요 로직을 관리하는 클래스
   - 게임 상태 관리 (준비, 시작, 플레이 중, 종료)
   - 플레이어 전환 및 게임 종료 검사
   - 바둑판 배열 및 오목 검사
   - 게임 시작, 진행 중, 종료 관련 메서드

- [`Player 클래스`](https://github.com/youhyeoneee/Omok/blob/main/Assets/Sciprts/Player.cs)
   - 각 플레이어의 정보와 동작을 관리하는 클래스

- [`BadukButton 클래스`](https://github.com/youhyeoneee/Omok/blob/main/Assets/Sciprts/BadukButton.cs) 
   - 각 바둑알 버튼의 동작과 상태를 관리하는 클래스

- [`UIManager 클래스`](https://github.com/youhyeoneee/Omok/blob/main/Assets/Sciprts/UIManager.cs)
    - 게임 내 UI를 관리하는 클래스

- [`SoundManager 클래스`](https://github.com/youhyeoneee/Omok/blob/main/Assets/Sciprts/SoundManager.cs)
    - 게임 내 사운드를 관리하는 클래스

### DEMO 영상

[![Video Label](http://img.youtube.com/vi/ufKmqsl52G4/0.jpg)](https://www.youtube.com/watch?v=ufKmqsl52G4)

### 프로젝트를 진행하면서 

#### 어려웠던 점

- 바둑알을 생성할 때 두 가지 방법 중에서 고민했었는데,
  초기 로딩 시간이 조금 더 길더라도 이미지 교체가 자주 일어나 게임 중에 성능 문제가 발생하는 것 보다는 낫다고 생각하여
  a번 방법을 선택했다. 
   - 1. 미리 흑, 백 바둑알 버튼을 모두 만들어놓고 비활성화 활성화할지 :
     - 장점 : 미리 만들어 놓기 때문에 활성화, 비활성화만 하면 됨
     - 단점 : 메모리 사용량이 더 많고 초기 로딩 시간이 더 길어진다. 
   - 2. 플레이어가 변경될 때마다 이미지를 모두 교체할지
     - 장점 : 메모리 사용량이 적고, 초기 로딩 시간이 짧다.
     - 단점 : 플레이어가 많이 바뀌면 이미지 교체가 자주 일어나 성능 문제가 발생할 수 있다. 
 

- 오목 로직을 구현하기 위해 비슷한 알고리즘 문제가 있는지 찾아보고, 해당 문제를 풀면서 습득해나갔다.
   - [[백준] 2615번 : 오목](https://velog.io/@youhyeoneee/%EB%B0%B1%EC%A4%80-2615%EB%B2%88-%EC%98%A4%EB%AA%A9)
     
#### 깨달은 점

- 혼자 진행한 프로젝트라도 남이 보고 읽기 쉽게 주석을 달고, 가독성이 좋도록 리팩토링 하는 습관을 가져야 겠다.
