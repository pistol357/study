using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class BattleManager : MonoBehaviour
{
    [SerializeField] private Transform[] _spawnPoints;

    private List<Unit> _playerUnits = new List<Unit>();
    private List<Unit> _monsterUnits = new List<Unit>();

    public List<Unit> PlayerUnits => _playerUnits;

    private static BattleManager _instance;
    public static BattleManager Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = FindObjectOfType<BattleManager>();
            }
            return _instance;
        }
    }

    private void Awake() => SetSingleton();

    private void Start()
    {
        Init();
        StartCoroutine(TurnRoutine());
    }

    public void Attack(Unit unit)
    {
        unit.TakeDamage(10);
    }

    public void Die(Unit unit)
    {
        if (_playerUnits.Contains(unit))
        {
            _playerUnits.Remove(unit);
        }
        if (_monsterUnits.Contains(unit))
        {
            _monsterUnits.Remove(unit);
        }

        Destroy(unit.gameObject);
    }

    private IEnumerator TurnRoutine()
    {
        while (_playerUnits.Count > 0 && _monsterUnits.Count > 0)
        {
            List<Unit> units = GetRunningList();
            int index = 0;

            while (units.Count > 0)
            {
                float maxSpeed = float.MinValue;
                for (int i = 0; i < units.Count; i++)
                {
                    if (units[i] == null) continue;

                    if (maxSpeed < units[i].Speed)
                    {
                        index = i;
                        maxSpeed = units[i].Speed;
                    }
                }

                BattleModule currentModule = units[index].GetComponent<BattleModule>();
                units.RemoveAt(index);
                currentModule.ResetPhases();

                yield return StartCoroutine(currentModule.PhasesRoutine());
                yield return new WaitForSeconds(2f);
            }
        }
    }

    private List<Unit> GetRunningList()
    {
        List<Unit> list = new List<Unit>();

        foreach(Unit unit in _playerUnits)
        {
            list.Add(unit);
        }
        foreach(Unit unit in _monsterUnits)
        {
            list.Add(unit);
        }

        return list;
    }

    private void Init()
    {
        foreach(Unit unit in GameManager.Instance.PlayerParty)
        {
            _playerUnits.Add(Instantiate(unit));
        }

        foreach(Unit unit in GameManager.Instance.MonsterParty)
        {
            _monsterUnits.Add(Instantiate(unit));
        }

        for(int i = 0; i < _playerUnits.Count; i++)
        {
            _playerUnits[i].transform.position = _spawnPoints[i].position;
        }

        for (int i = 0; i < _monsterUnits.Count; i++)
        {
            _monsterUnits[i].transform.position = _spawnPoints[i + 3].position;
        }
    }

    private void SetSingleton()
    {
        if (_instance != null && _instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            _instance = this;
        }
    }
}
