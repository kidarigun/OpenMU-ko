# 한국어 용어집 (Korean terminology)

OpenMU의 한국어 리소스(`*.ko.resx`)는 이 용어를 따릅니다. 게임 콘텐츠는 한국 뮤
온라인에서 익숙한 용어를, 관리 화면은 간결한 한국어를 씁니다. 화면 표시용 번역일
뿐이며 프로토콜 식별자나 저장된 값은 바꾸지 않습니다.

| 분야 | English | 한국어 |
|---|---|---|
| 클래스 | Dark Wizard / Soul Master / Grand Master | 흑마법사 / 소울마스터 / 그랜드마스터 |
| 클래스 | Dark Knight / Blade Knight / Blade Master | 흑기사 / 블레이드나이트 / 블레이드마스터 |
| 클래스 | Fairy Elf / Muse Elf / High Elf | 요정 / 뮤즈엘프 / 하이엘프 |
| 클래스 | Magic Gladiator / Duel Master | 마검사 / 듀얼마스터 |
| 클래스 | Dark Lord / Lord Emperor | 다크로드 / 로드엠퍼러 |
| 클래스 | Summoner / Bloody Summoner / Dimension Master | 소환술사 / 블러디서머너 / 디멘션마스터 |
| 클래스 | Rage Fighter / Fist Master | 레이지파이터 / 피스트마스터 |
| 스탯 | Strength / Agility / Vitality / Energy / Leadership | 힘 / 민첩 / 체력 / 에너지 / 통솔 |
| 스탯 | Health / Mana / Shield / Ability Gauge | 생명력 / 마나 / SD / AG |
| 스탯 | Damage / Defense / Experience / Level | 데미지 / 방어력 / 경험치 / 레벨 |
| 스탯 | Excellent Damage / Critical Damage | 엑설런트 데미지 / 크리티컬 데미지 |
| 스탯 | Attack Speed / Attack Rate / Defense Rate | 공격 속도 / 공격 성공률 / 방어 성공률 |
| 아이템 | Jewel of Bless / Soul / Chaos / Life / Creation | 축복의 보석 / 영혼의 보석 / 혼돈의 보석 / 생명의 보석 / 창조의 보석 |
| 아이템 | Jewel of Guardian / Harmony | 수호의 보석 / 조화의 보석 |
| 아이템 | Excellent / Ancient / Socket / Durability | 엑설런트 / 세트 / 소켓 / 내구도 |
| 아이템 | Inventory / Vault / Personal Store | 인벤토리 / 창고 / 개인 상점 |
| 아이템 | Zen / Wings / Cloak / Pendant / Ring | 젠 / 날개 / 망토 / 목걸이 / 반지 |
| 맵 | Lorencia / Noria / Devias / Dungeon | 로랜시아 / 노리아 / 데비아스 / 던전 |
| 맵 | Lost Tower / Atlans / Tarkan / Icarus | 로스트타워 / 아틀란스 / 타르칸 / 이카루스 |
| 맵 | Aida / Kanturu / Kalima / Raklion | 아이다 / 칸투르 / 칼리마 / 라클리온 |
| 맵 | Valley of Loren / Land of Trials / Crywolf Fortress | 로렌 협곡 / 시련의 땅 / 크라이울프 요새 |
| 맵 | Barracks of Balgass / Balgass Refuge | 발가스의 병영 / 발가스의 안식처 |
| 이벤트 | Blood Castle / Devil Square / Chaos Castle | 블러드 캐슬 / 데빌 스퀘어 / 카오스 캐슬 |
| 이벤트 | Illusion Temple / Doppelgaenger / Imperial Guardian | 환영의 사원 / 도플갱어 / 제국 수호군 |
| 이벤트 | Castle Siege / Golden Invasion | 공성전 / 황금 몬스터 침공 |
| 서버 | Account / Character / Player / Guild / Alliance | 계정 / 캐릭터 / 플레이어 / 길드 / 연합 |
| 서버 | Game Server / Connect Server | 게임 서버 / 접속 서버 |
| 서버 | Party / Trade / Drop / Spawn / Reset | 파티 / 거래 / 드롭 / 스폰 / 리셋 |
| 설정 | Configuration / Definition / Attribute | 구성 / 정의 / 속성 |
| 설정 | Plug-in / Plug-in Point / Caption | 플러그인 / 플러그인 확장점 / 표시 이름 |
| 관리 | Create / Edit / Save / Delete / Cancel | 만들기 / 편집 / 저장 / 삭제 / 취소 |
| 관리 | Start / Stop / Restart / Disconnect | 시작 / 중지 / 다시 시작 / 연결 해제 |
| 관리 | Ban / Unban / Install / Update | 차단 / 차단 해제 / 설치 / 업데이트 |
| 관리 | Two-factor authentication / Recovery code | 2단계 인증 / 복구 코드 |

## 번역 규칙

- 키, 주석, `{0}` 같은 자리표시자와 그 개수, HTML 태그, 채팅 명령(`/discord link`
  등)은 원문 그대로 둡니다. `tests/MUnique.OpenMU.Tests/Localization`의 테스트가
  이를 검사합니다.
- 브랜드와 고유 명사(OpenMU, Discord), 표준 약어(API, NPC, ID), 파일 이름과
  코드 식별자는 번역하지 않습니다.
- 존댓말 평서문(“~합니다”, “~하세요”)을 쓰고, “당신/귀하”는 가능한 한 생략합니다.
- 서버 로그는 영어로 남깁니다.
