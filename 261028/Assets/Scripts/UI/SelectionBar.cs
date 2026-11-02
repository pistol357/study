using System;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 화면 하단에 선택한 캐릭터, 고유 마법 2개, 스킬 5개를 보여주는 바.
/// </summary>
public class SelectionBar : MonoBehaviour
{
    [SerializeField] private BarSlot _characterSlot;
    [SerializeField] private BarSlot _uniqueSkill1Slot;
    [SerializeField] private BarSlot _uniqueSkill2Slot;
    [SerializeField] private BarSlot[] _skillSlots;

    /// <summary>
    /// 스킬 칸을 눌렀을 때 몇 번째 칸인지 알려줄 동작을 설정한다.
    /// </summary>
    public void Setup(Action<int> onSkillClicked)
    {
        for (int i = 0; i < _skillSlots.Length; i++)
        {
            // 람다가 반복 변수 i를 그대로 잡으면 모든 칸이 마지막 값을 쓰게 되므로 복사해 둔다.
            int index = i;
            _skillSlots[i].SetClickAction(() => onSkillClicked(index));
        }
    }

    /// <summary>
    /// 선택한 캐릭터와 고유 마법을 표시한다. null이면 비운다.
    /// </summary>
    public void ShowCharacter(CharacterData character)
    {
        if (character == null)
        {
            _characterSlot.Clear();
            _uniqueSkill1Slot.Clear();
            _uniqueSkill2Slot.Clear();
            return;
        }

        _characterSlot.Show(character.CharacterName, null);
        _uniqueSkill1Slot.Show(character.UniqueSkill1.SkillName, character.UniqueSkill1.Icon);
        _uniqueSkill2Slot.Show(character.UniqueSkill2.SkillName, character.UniqueSkill2.Icon);
    }

    /// <summary>
    /// 선택한 스킬을 앞 칸부터 채우고 남은 칸은 비운다.
    /// </summary>
    public void ShowSkills(List<SkillSlot> selectedSkills)
    {
        for (int i = 0; i < _skillSlots.Length; i++)
        {
            if (i < selectedSkills.Count)
            {
                _skillSlots[i].Show(selectedSkills[i].Data.SkillName, selectedSkills[i].Data.Icon);
            }
            else
            {
                _skillSlots[i].Clear();
            }
        }
    }
}
