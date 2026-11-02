using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// 캐릭터 한 명의 상세 정보(이름, 고유 마법, 패시브)를 보여주는 패널.
/// </summary>
public class CharacterDetailPanel : MonoBehaviour
{
    // TODO(pistol357): 소속, 소속 패시브, 두 번째 패시브, 설명 데이터가 생기면 표시 추가 (현재 씬에 임시 텍스트)
    [SerializeField] private TMP_Text _characterNameText;
    [SerializeField] private TMP_Text _uniqueSkill1Text;
    [SerializeField] private TMP_Text _uniqueSkill2Text;
    [SerializeField] private TMP_Text _passiveText;

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
    /// 캐릭터 정보를 채우고 패널을 연다.
    /// </summary>
    public void Show(CharacterData data, bool isSelected)
    {
        _characterNameText.text = data.CharacterName;
        _uniqueSkill1Text.text = data.UniqueSkill1.SkillName;
        _uniqueSkill2Text.text = data.UniqueSkill2.SkillName;
        _passiveText.text = data.Passive;
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
