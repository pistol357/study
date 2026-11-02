using System;
using TMPro;
using UnityEngine;

/// <summary>
/// 캐릭터 목록의 슬롯 하나. 캐릭터 이름을 보여준다.
/// </summary>
public class CharacterSlot : SelectableSlot
{
    [SerializeField] private TMP_Text _nameText;

    private CharacterData _data;
    private Action<CharacterSlot> _onSelect;
    private Action<CharacterSlot> _onDetail;

    public CharacterData Data => _data;

    /// <summary>
    /// 슬롯에 표시할 캐릭터와 버튼을 눌렀을 때 실행할 동작을 설정한다.
    /// </summary>
    public void Setup(CharacterData data, Action<CharacterSlot> onSelect, Action<CharacterSlot> onDetail)
    {
        _data = data;
        _onSelect = onSelect;
        _onDetail = onDetail;

        _nameText.text = data.CharacterName;
        BindButtons();
    }

    protected override void OnSelectClicked() => _onSelect(this);

    // 상세 화면에서 선택할 때 어느 슬롯인지 알아야 하므로 데이터가 아닌 슬롯을 넘긴다.
    protected override void OnDetailClicked() => _onDetail(this);
}
