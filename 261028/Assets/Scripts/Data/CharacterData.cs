using UnityEngine;

/// <summary>
/// 캐릭터 하나의 정보를 담는 데이터 에셋.
/// </summary>
public class CharacterData : ScriptableObject
{
    [SerializeField] private int _id;
    [SerializeField] private string _characterName;
    [SerializeField] private int _hp;
    [SerializeField] private int _mp;
    [SerializeField] private float _mpRegen;
    [SerializeField] private float _moveSpeed;
    // TODO(pistol357): 패시브 스킬 데이터 구조가 생기면 해당 타입으로 교체
    [SerializeField] private string _passive;
    [SerializeField] private SkillData _uniqueSkill1;
    [SerializeField] private SkillData _uniqueSkill2;

    public int Id => _id;
    public string CharacterName => _characterName;
    public int Hp => _hp;
    public int Mp => _mp;
    public float MpRegen => _mpRegen;
    public float MoveSpeed => _moveSpeed;
    public string Passive => _passive;
    public SkillData UniqueSkill1 => _uniqueSkill1;
    public SkillData UniqueSkill2 => _uniqueSkill2;

    /// <summary>
    /// CSV 변환 시 값을 한 번에 덮어쓴다.
    /// </summary>
    public void SetData(int id, string characterName, int hp, int mp, float mpRegen, float moveSpeed,
        string passive, SkillData uniqueSkill1, SkillData uniqueSkill2)
    {
        _id = id;
        _characterName = characterName;
        _hp = hp;
        _mp = mp;
        _mpRegen = mpRegen;
        _moveSpeed = moveSpeed;
        _passive = passive;
        _uniqueSkill1 = uniqueSkill1;
        _uniqueSkill2 = uniqueSkill2;
    }
}
