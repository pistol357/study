using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// 로비 화면의 탭 전환, 목록 슬롯 생성, 캐릭터/스킬 선택 상태와 게임 시작 버튼을 관리한다.
/// </summary>
public class LobbyUI : MonoBehaviour
{
    private const int MAX_SKILL_COUNT = 5;

    [SerializeField] private GameDatabase _database;

    [SerializeField] private CharacterSlot _characterSlotPrefab;
    [SerializeField] private SkillSlot _skillSlotPrefab;
    [SerializeField] private Transform _characterContent;
    [SerializeField] private Transform _skillContent;

    [SerializeField] private Button _characterTabButton;
    [SerializeField] private Button _skillTabButton;
    [SerializeField] private Color _activeTabColor;
    [SerializeField] private Color _inactiveTabColor;

    [SerializeField] private GameObject _characterListPanel;
    [SerializeField] private GameObject _skillListPanel;
    [SerializeField] private CharacterDetailPanel _characterDetailPanel;
    [SerializeField] private SkillDetailPanel _skillDetailPanel;

    [SerializeField] private SelectionBar _selectionBar;
    [SerializeField] private Button _startButton;

    private CharacterSlot _selectedCharacterSlot;
    // 고른 순서대로 하단 바에 표시하기 위해 List로 관리한다.
    private readonly List<SkillSlot> _selectedSkillSlots = new List<SkillSlot>();

    // 상세 패널의 선택 버튼이 어느 슬롯을 선택할지 알기 위해 기억한다.
    private CharacterSlot _detailCharacterSlot;
    private SkillSlot _detailSkillSlot;

    private void Start()
    {
        CreateSlots();
        BindButtons();
        ShowCharacterList();
        RefreshSelection();
    }

    private void CreateSlots()
    {
        foreach (CharacterData character in _database.Characters)
        {
            CharacterSlot slot = Instantiate(_characterSlotPrefab, _characterContent);
            slot.Setup(character, OnCharacterSelected, OnCharacterDetailRequested);
        }

        foreach (SkillData skill in _database.Skills)
        {
            // 고유 마법은 캐릭터에 딸려 오므로 직접 고르는 목록에서 뺀다.
            if (skill.IsUnique)
            {
                continue;
            }

            SkillSlot slot = Instantiate(_skillSlotPrefab, _skillContent);
            slot.Setup(skill, OnSkillSelected, OnSkillDetailRequested);
        }
    }

    private void BindButtons()
    {
        _characterTabButton.onClick.AddListener(ShowCharacterList);
        _skillTabButton.onClick.AddListener(ShowSkillList);
        _startButton.onClick.AddListener(OnStartClicked);
        _selectionBar.Setup(OnBarSkillClicked);
        _characterDetailPanel.Setup(() => OnCharacterSelected(_detailCharacterSlot), ShowCharacterList);
        _skillDetailPanel.Setup(() => OnSkillSelected(_detailSkillSlot), ShowSkillList);
    }

    // 탭을 눌러도 상세 패널을 닫고 목록으로 돌아간다.
    private void ShowCharacterList()
    {
        SetTabColor(true);
        _characterListPanel.SetActive(true);
        _skillListPanel.SetActive(false);
        HideDetailPanels();
    }

    private void ShowSkillList()
    {
        SetTabColor(false);
        _characterListPanel.SetActive(false);
        _skillListPanel.SetActive(true);
        HideDetailPanels();
    }

    private void HideDetailPanels()
    {
        _characterDetailPanel.Hide();
        _skillDetailPanel.Hide();
    }

    private void SetTabColor(bool isCharacterTab)
    {
        _characterTabButton.image.color = isCharacterTab ? _activeTabColor : _inactiveTabColor;
        _skillTabButton.image.color = isCharacterTab ? _inactiveTabColor : _activeTabColor;
    }

    private void OnCharacterSelected(CharacterSlot slot)
    {
        if (_selectedCharacterSlot != null)
        {
            _selectedCharacterSlot.SetSelected(false);
        }

        // 이미 고른 캐릭터를 다시 누르면 선택 취소
        _selectedCharacterSlot = _selectedCharacterSlot == slot ? null : slot;

        if (_selectedCharacterSlot != null)
        {
            _selectedCharacterSlot.SetSelected(true);
        }

        RefreshSelection();
    }

    private void OnCharacterDetailRequested(CharacterSlot slot)
    {
        _detailCharacterSlot = slot;
        _characterListPanel.SetActive(false);
        _characterDetailPanel.Show(slot.Data, slot == _selectedCharacterSlot);
    }

    private void OnSkillSelected(SkillSlot slot)
    {
        if (_selectedSkillSlots.Contains(slot))
        {
            DeselectSkill(slot);
            return;
        }

        if (_selectedSkillSlots.Count >= MAX_SKILL_COUNT)
        {
            return;
        }

        _selectedSkillSlots.Add(slot);
        slot.SetSelected(true);
        RefreshSelection();
    }

    private void OnSkillDetailRequested(SkillSlot slot)
    {
        _detailSkillSlot = slot;
        _skillListPanel.SetActive(false);
        _skillDetailPanel.Show(slot.Data, _selectedSkillSlots.Contains(slot));
    }

    // 빈 칸을 누르면 취소할 스킬이 없으므로 무시한다.
    private void OnBarSkillClicked(int index)
    {
        if (index >= _selectedSkillSlots.Count)
        {
            return;
        }

        DeselectSkill(_selectedSkillSlots[index]);
    }

    private void DeselectSkill(SkillSlot slot)
    {
        _selectedSkillSlots.Remove(slot);
        slot.SetSelected(false);
        RefreshSelection();
    }

    private void RefreshSelection()
    {
        CharacterData character = _selectedCharacterSlot != null ? _selectedCharacterSlot.Data : null;

        _selectionBar.ShowCharacter(character);
        _selectionBar.ShowSkills(_selectedSkillSlots);
        _startButton.interactable = character != null && _selectedSkillSlots.Count == MAX_SKILL_COUNT;

        // 상세 화면에 머문 채 선택하거나 하단 바에서 취소해도 버튼 글자가 맞도록 함께 갱신한다.
        _characterDetailPanel.SetSelected(_detailCharacterSlot != null && _detailCharacterSlot == _selectedCharacterSlot);
        _skillDetailPanel.SetSelected(_selectedSkillSlots.Contains(_detailSkillSlot));
    }

    // TODO(pistol357): 게임 씬이 생기면 선택 정보를 넘기고 씬을 전환하도록 교체
    private void OnStartClicked()
    {
        List<string> skillNames = new List<string>();

        foreach (SkillSlot slot in _selectedSkillSlots)
        {
            skillNames.Add(slot.Data.SkillName);
        }

        Debug.Log($"게임 시작 - 캐릭터 : {_selectedCharacterSlot.Data.CharacterName}, 스킬 : {string.Join(", ", skillNames)}");
    }
}
