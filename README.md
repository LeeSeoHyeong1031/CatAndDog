# CatAndDog
기존 - 캐릭터 크기 (10,10) 에서 초당 10 움직임.  <- 맵에 비해 캐릭터가 너무 커보였음.

수정 - 캐릭터 크기 (5,5)로 수정.

그에 따른 공격 거리 10을 5로 수정. <- 캐릭터 크기가 줄어들었으니 사격 거리고 그 만큼 조절.

기존 - 캐릭터 스폰(플레이어 진영 기준) (50,0 ~ -20) <- UI 놓을 공간이 협소.

수정 - (50, 5 ~ -15)

기존 - 레벨업이 가능하면 노란색, 안된다면 회색

수정 - 비용Text를 레벨업이 가능하면 검은색, 안된다면 빨강으로 처리하였기 때문에 추가 안하였음.

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

-----------
남은 해야 할 일

필살기 사용 시 보여지는 image 추가.

캐릭터 다양화.

캐릭터 스폰 ui 만들기

캐릭터 스폰 로직 만들기(기존 꺼는 임시).

캐릭터 쿨타임 UI 및 로직 만들기.

캐릭터가 지금은 공격 시 모두가 광역 공격인데 단일 공격 로직 구현하기.

타이틀 화면 만들기.

패배, 승리 UI 만들기.
