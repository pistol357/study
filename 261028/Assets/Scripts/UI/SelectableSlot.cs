using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

/// <summary>
/// 목록 카드의 공통 동작. 마우스를 올리면 선택/상세 정보 버튼을 보여주고, 선택 강조 표시를 관리한다.
/// </summary>
public abstract class SelectableSlot : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    // 상세 패널의 선택 버튼도 같은 글자를 쓰도록 공개한다.
    public const string SELECT_LABEL = "선택";
    public const string DESELECT_LABEL = "선택 취소";

    [SerializeField] private GameObject _hoverButtons;
    [SerializeField] private Button _selectButton;
    [SerializeField] private TMP_Text _selectButtonText;
    [SerializeField] private Button _detailButton;
    [SerializeField] private GameObject _selectedMark;

    /// <summary>
    /// 선택 강조 표시와 선택 버튼 글자를 선택 상태에 맞춘다.
    /// </summary>
    public void SetSelected(bool isSelected)
    {
        _selectedMark.SetActive(isSelected);
        _selectButtonText.text = isSelected ? DESELECT_LABEL : SELECT_LABEL;
    }

    /// <summary>
    /// 마우스가 슬롯 위에 올라오면 버튼을 보여준다.
    /// </summary>
    public void OnPointerEnter(PointerEventData eventData) => _hoverButtons.SetActive(true);

    /// <summary>
    /// 마우스가 슬롯을 벗어나면 버튼을 숨긴다.
    /// </summary>
    public void OnPointerExit(PointerEventData eventData) => _hoverButtons.SetActive(false);

    // 마우스가 올라간 채로 목록이 꺼지면 Exit 이벤트가 오지 않아, 다시 켜졌을 때 버튼이 남아 있는 것을 막는다.
    private void OnDisable() => _hoverButtons.SetActive(false);

    /// <summary>
    /// 버튼을 연결하고 표시 상태를 초기화한다. 자식 클래스의 Setup에서 호출한다.
    /// </summary>
    protected void BindButtons()
    {
        _selectButton.onClick.AddListener(OnSelectClicked);
        _detailButton.onClick.AddListener(OnDetailClicked);

        _hoverButtons.SetActive(false);
        SetSelected(false);
    }

    protected abstract void OnSelectClicked();

    protected abstract void OnDetailClicked();
}
