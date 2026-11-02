using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// 하단 선택 바의 한 칸. 선택한 캐릭터나 스킬의 이름과 아이콘을 보여준다.
/// </summary>
public class BarSlot : MonoBehaviour
{
    [SerializeField] private Image _iconImage;
    [SerializeField] private TMP_Text _nameText;
    // 클릭으로 취소하는 스킬 칸에만 연결한다. 캐릭터, 고유 마법 칸은 비워 둔다.
    [SerializeField] private Button _button;

    /// <summary>
    /// 칸을 눌렀을 때 실행할 동작을 설정한다.
    /// </summary>
    public void SetClickAction(Action onClick) => _button.onClick.AddListener(() => onClick());

    /// <summary>
    /// 이름과 아이콘을 표시한다.
    /// </summary>
    public void Show(string displayName, Sprite icon)
    {
        _nameText.text = displayName;
        _iconImage.sprite = icon;
        _iconImage.enabled = icon != null;
    }

    /// <summary>
    /// 빈 칸으로 비운다.
    /// </summary>
    public void Clear() => Show(string.Empty, null);
}
