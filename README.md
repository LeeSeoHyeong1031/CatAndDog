# CatAndDog
기존 - 캐릭터 크기 (10,10) 에서 초당 10 움직임.  <- 맵에 비해 캐릭터가 너무 커보였음.

수정 - 캐릭터 크기 (5,5)로 수정.

그에 따른 공격 거리 10을 5로 수정. <- 캐릭터 크기가 줄어들었으니 사격 거리고 그 만큼 조절.

기존 - 캐릭터 스폰(플레이어 진영 기준) (50,0 ~ -20) <- UI 놓을 공간이 협소.

수정 - (50, 5 ~ -15)

기존 - 레벨업이 가능하면 노란색, 안된다면 회색

수정 - 비용Text를 레벨업이 가능하면 검은색, 안된다면 빨강으로 처리하였음.

2024.10.30

적을 감지하면 적을 때리는 로직 완성.

아군끼리 공격하는 현상 수정 완료.

SPUM 캐릭터 에셋 적용. (픽셀 배경 && UI 없음)

2024.10.31

캐릭터 y좌표 별 자동 정렬 구현 완성.

기지, 캐릭터, 배경화면 추가.

UI 미구현.

2024.11.01

UI 레벨업 구현 완성. 돈, 비용 관련 로직 완성.

필살기 로직 구현, 필살기 사용했을 때 보여지는 image 없음.

2024.11.02 ~ 2024.11.03

~~캐릭터 다양화. (하는 중)~~

~~적 캐릭터 스폰 로직 만들기(기존 꺼는 임시).~~

~~캐릭터 스폰 ui 만들기 (하는 중)~~

~~캐릭터가 지금은 공격 시 모두가 광역 공격인데 단일 공격 로직 구현하기.~~

2024.11.04

~~캐릭터 쿨타임 UI 및 로직 만들기. (임시)~~

2024.11.05

패배, 승리 UI 구현

애니메이션 SingleAttack, AreaAttack을 만들고 Event기능 추가.

타이틀 화면 구현

2024.11.06

전체 게임 구현 완료.

씬 넘어가는 과정에서 생긴 에러 해결

타이틀 애니메이션 구현

작은 에러들 고침

코드 리팩토링 중

2024.11.07

레벨업 버튼, 궁극기 버튼이 게임을 다시 시작했을 때 재기능을 못하던 버그 고침

displayCoin 초기화가 작동 안하던 에러 해결