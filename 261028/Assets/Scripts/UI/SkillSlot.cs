using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// 스킬 목록의 슬롯 하나. 스킬 아이콘과 이름을 보여준다.
/// </summary>
public class SkillSlot : SelectableSlot
{
    [SerializeField] private Image _iconImage;
    [SerializeField] private TMP_Text _nameText;

    private SkillData _data;
    private Action<SkillSlot> _onSelect;
    private Action<SkillSlot> _onDetail;

    public SkillData Data => _data;

    /// <summary>
    /// 슬롯에 표시할 스킬과 버튼을 눌렀을 때 실행할 동작을 설정한다.
    /// </summary>
    public void Setup(SkillData data, Action<SkillSlot> onSelect, Action<SkillSlot> onDetail)
    {
        _data = data;
        _onSelect = onSelect;
        _onDetail = onDetail;

        _nameText.text = data.SkillName;
        _iconImage.sprite = data.Icon;
        // 아이콘이 없는 스킬은 흰 사각형이 보이지 않도록 이미지를 끈다.
        _iconImage.enabled = data.Icon != null;
        BindButtons();
    }

    protected override void OnSelectClicked() => _onSelect(this);

    // 상세 화면에서 선택할 때 어느 슬롯인지 알아야 하므로 데이터가 아닌 슬롯을 넘긴다.
    protected override void OnDetailClicked() => _onDetail(this);
}
