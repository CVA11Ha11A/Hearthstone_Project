### Hearthstone_Project
유니티 3D 프로젝트로 제작한 하스스톤 입니다.

## 개발환경
* 유니티 2021.3.27f1
* VisualStudio 2022
* PhotonNetwork
* SourceTree

## 개발 기간
* 1인
* 2024.02.15 ~ 2024.05.22 (약 18주)

## 주요기능
+ 매칭시스템
  + 포톤 네트워크의 콜백함수와 유니티의 코루틴을 이용해서 매칭 시스템을 제작하였고 유저에게는 매칭 UI만 보이지만 실제로는 방을 찾아 들어가거나 방을 생성후 대기하는 기능이 실행됨
  <p align="center">
  <img width = "70%" alt="Matching" src="https://github.com/user-attachments/assets/ec770264-1310-476e-8295-486e543eadcd">
  </p>
+ 카드관리
  + 카드는 한개의 자료구조(Dictionary)에서 관리가 되며 Card의 고유적인 ID값을 이용해서 관리가 됨
  <p align="center">
  <img width = "70%" alt="CardCollection" src="https://github.com/user-attachments/assets/17276096-a87c-4e42-84a2-caa11e1bcb8e">
  </p>
  
+ 플레이어 덱 생성및 저장
  + 유니티의 JsonClass이용하였으며 Application.persistentDataPath을 이용해서 프로젝트의 Path를 지정해줌
  + 저장하는 것으로는 Deck을 저장하는데  Deck속 카드의 갯수, 덱의 직업, 카드들의 ID가 존재    
+ 게임 사이클
  + 단 1회 진행되는것 : 선공 후공 지정, 멀리건(카드를 섞어 드로우할지 그대로 갈지)
  + 게임 사이클 : 턴시작, 카드내기, 하수인 공격, 영웅능력, 턴종료를 반복
  + 게임 종료 조건 : 두 영웅중 한명의 영웅의 체력이 0 이하일 경우 게임 결과 출력
  + 자신의 턴에서 카드를 내거나 하수인의 공격등 액션을 취할수 있음
  + 조건으로 하수인을 소환한 턴에는 즉시 공격 불가능하도록 설정
+ 멀리건 사진
  <p align="center">
  <img width = "70%" alt="Mulligan" src="https://github.com/user-attachments/assets/4be3d982-257e-4019-bdd1-b1c40a02fddd">
  </p>  
+ 하수인 공격 사진
  <p align="center">
  <img width = "70%" alt="MinionAttack" src="https://github.com/user-attachments/assets/e5d99f71-a8a3-4bfb-bc7f-76627bf89482">
  </p>

