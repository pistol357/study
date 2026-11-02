# Tech Specification

> 이 프로젝트의 기술 스택 관련 결정 사항이다.
>  
> Claude Code는 코드를 작성할 때 이 문서를 기준으로 한다.

---

## 프로젝트 제원

- 프로젝트명 : UnityAISetting
- 장르 : 2D 뱀서라이크 디펜스(3D 모델로 2D처럼 보이는 표현)
- 물리 방식 : 2D로 XY 평면에서 처리
- 팀 구성 : 1인

## Unity 환경

- 버전 : 6000.3.9f1 LTS
- 렌더 파이프라인 : URP
- 빌드 플랫폼 : Windows

## 유저 입력 시스템

- new InputSystem
  - 구 `InputManager`는 사용하지 않는다.
- 특정 입력에 대한 기능 구현 시 `InputActionReference`를 사용해 InputAction을 참조하는 형태로 구현한다.
- OnEnable에서 InputAction에 대한 Enable(), OnDisable에서 Disable()을 사용한다.

## UI

- 텍스트는 TextMesh Pro(`TMP_Text`)를 사용한다.
- 기본 폰트 : `LevelUp-Regular SDF` (SIL OFL 1.1, Bold는 Font Weight 700에 `LevelUp-Bold SDF` 등록)
  - 폰트 에셋은 Dynamic 방식이며 `Clear Dynamic Data On Build`를 켠다.
  - Play 후 폰트 에셋이 Git 변경으로 잡히면, 커밋 전에 `Clear Dynamic Data`로 비운다.
- 외부에서 받은 에셋은 `Assets/Imports/{종류}/{에셋 이름}/` 에 두고, 라이선스 문서를 함께 넣는다. (예: `Assets/Imports/Fonts/LevelUp/`)