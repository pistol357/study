using UnityEngine;

[CreateAssetMenu(fileName = "MonsterStats", menuName = "Data/MonsterStats")]
public class MonsterStats : ScriptableObject
{
    [SerializeField] private int _id;
    [SerializeField] private string _name;
    [SerializeField] private int _maxHp;
    [SerializeField] private float _detectRange;
    [SerializeField] private float _attackRange;
    [SerializeField] private float _speed;
    [SerializeField] private int _attackValue;

    public int Id => _id;
    public string Name => _name;
    public int MaxHp => _maxHp;
    public float DetectRange => _detectRange;
    public float AttackRange => _attackRange;
    public float Speed => _speed;
    public int AttackValue => _attackValue;

    public void SetData(int id, string name, int maxHp, float detectRange, float attackRange, float speed, int attackValue)
    {
        _id = id;
        _name = name;
        _maxHp = maxHp;
        _detectRange = detectRange;
        _attackRange = attackRange;
        _speed = speed;
        _attackValue = attackValue;
    }
}
