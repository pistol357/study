using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// 스킬 하나의 상세 정보(아이콘, 종류, 이름, 쿨타임, 효과)를 보여주는 패널.
/// </summary>
public class SkillDetailPanel : MonoBehaviour
{
    // TODO(pistol357): 스킬 배경 스토리 데이터가 생기면 표시 추가 (현재 씬에 임시 텍스트)
    [SerializeField] private Image _iconImage;
    [SerializeField] private TMP_Text _typeText;
    [SerializeField] private TMP_Text _skillNameText;
    [SerializeField] private TMP_Text _cooldownText;
    [SerializeField] private TMP_Text _effectText;

    [SerializeField] private Button _selectButton;
    [SerializeField] private TMP_Text _selectButtonText;
    [SerializeField] private Button _backButton;

    /// <summary>
    /// 선택 버튼과 뒤로가기 버튼을 눌렀을 때 실행할 동작을 설정한다.
    /// </summary>
    public void Setup(Action onSelect, Action onBack)
    {
        _selectButton.onClick.AddListener(() => onSelect());
        _backButton.onClick.AddListener(() => onBack());
    }

    /// <summary>
    /// 스킬 정보를 채우고 패널을 연다.
    /// </summary>
    public void Show(SkillData data, bool isSelected)
    {
        _iconImage.sprite = data.Icon;
        _iconImage.enabled = data.Icon != null;
        _typeText.text = data.Type.ToDisplayName();
        _skillNameText.text = data.SkillName;
        _cooldownText.text = $"{data.Cooldown}초";
        _effectText.text = data.Description;
        SetSelected(isSelected);
        gameObject.SetActive(true);
    }

    /// <summary>
    /// 선택 버튼 글자를 선택 상태에 맞춘다.
    /// </summary>
    public void SetSelected(bool isSelected) =>
        _selectButtonText.text = isSelected ? SelectableSlot.DESELECT_LABEL : SelectableSlot.SELECT_LABEL;

    /// <summary>
    /// 패널을 닫는다.
    /// </summary>
    public void Hide() => gameObject.SetActive(false);
}
