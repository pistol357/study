using UnityEngine;

/// <summary>
/// 스킬 하나의 정보를 담는 데이터 에셋.
/// </summary>
public class SkillData : ScriptableObject
{
    [SerializeField] private int _id;
    [SerializeField] private SkillType _type;
    [SerializeField] private string _skillName;
    [SerializeField] private Sprite _icon;
    [SerializeField] private string _description;
    [SerializeField] private float _cooldown;

    public int Id => _id;
    public SkillType Type => _type;
    public string SkillName => _skillName;
    public Sprite Icon => _icon;
    public string Description => _description;
    public float Cooldown => _cooldown;

    /// <summary>
    /// CSV 변환 시 값을 한 번에 덮어쓴다.
    /// </summary>
    public void SetData(int id, SkillType type, string skillName, Sprite icon, string description, float cooldown)
    {
        _id = id;
        _type = type;
        _skillName = skillName;
        _icon = icon;
        _description = description;
        _cooldown = cooldown;
    }
}
