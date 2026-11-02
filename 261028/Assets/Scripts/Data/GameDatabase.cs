using UnityEngine;

/// <summary>
/// 모든 캐릭터와 스킬 에셋을 모아 둔 목록 에셋. CSV 변환 시 자동으로 갱신된다.
/// </summary>
public class GameDatabase : ScriptableObject
{
    [SerializeField] private CharacterData[] _characters;
    [SerializeField] private SkillData[] _skills;

    public CharacterData[] Characters => _characters;
    public SkillData[] Skills => _skills;

    /// <summary>
    /// CSV 변환 시 목록을 한 번에 덮어쓴다.
    /// </summary>
    public void SetData(CharacterData[] characters, SkillData[] skills)
    {
        _characters = characters;
        _skills = skills;
    }
}
