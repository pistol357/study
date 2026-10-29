# 코드 컨벤션

## 1. 코드 주석 규칙
- 클래스, 공개 메서드에는 `<summary>`를 쓴다.
- 비공개 메서드는 의도가 명확하지 않을 때만 주석을 작성한다.
- `TODO`는 `// TODO(이름): 내용` 형식으로 쓴다.
- 주석은 "왜" 이렇게 구현 했는지를 쓰고 "무엇"을 구현 했는지는 쓰지 않는다.(`<summary>`는 예외)

## 2. 네이밍 규칙
- 클래스, 메서드, 프로퍼티: `PascalCase`
- private 필드: `_camelCase`, 인스펙터 노출은 `[SerializeField] private`
- 상수: UPPER_SNAKE_CASE
- 이벤트 핸들러: `OnXxx`
- bool: `Is / Has / Can`으로 시작

## 3. Unity 규칙
- `Update`에서 `Find`, `GetComponent`, `Camera.main`을 호출하지 않는다.
- 컴포넌트 캐싱이 필요하다면 `CacheComponents()`메서드에서 캐싱후 `Awake`에서 사용한다.
- 초기값 설정 등이 필요하다면 `Init()`메서드에서 설정 후 `Start`에서 사용한다.
- `Awake`, `Start`등에 작성될 내용이 다른 메서드 호출 한 줄이라면 `Awake() => CacheComponents()`와 같은 형태로 작성한다.
- 런타임 `Instantiate`/`Destroy` 반복은 피하고 플래그 토글이나 풀링을 쓴다.
- 디버그 표시는 `OnDrawGizmos`를 우선한다.
- 설정하고자 하는 Attribute는 작성하지 않는 것이 불가능한 경우에만 기능의 설명과 설정하려는 이유를 밝히고 여부를 물은 뒤 답변에 따라 설정한다.

## 4. 성능 규칙
- 힙 할당(`new List`, LINQ, 문자열 조합)을 매 프레임 쓰지 않는다.
- 최적화 제안은 "측정 방법"과 함께 말한다. 측정 없는 최적화는 권하지 않는다.