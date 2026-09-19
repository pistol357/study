using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    private static GameManager _instance;

    [field: SerializeField] public Unit[] PlayerParty { get; private set; }
    [field: SerializeField] public Unit[] MonsterParty { get; private set; }

    public static GameManager Instance
    {
        get
        {
            if(_instance == null)
            {
                _instance = FindObjectOfType<GameManager>();
                DontDestroyOnLoad(_instance.gameObject);
            }

            return _instance;
        }
    }

    private void Awake() => SetSingleton();

    public void StartBattle()
    {
        SceneManager.LoadScene("BattleScene");
    }

    public void SetBattleData(Party playerParty, Party monsterParty)
    {
        PlayerParty = playerParty.Units;
        MonsterParty = monsterParty.Units;
    }

    private void SetSingleton()
    {
        if(_instance != null && _instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            _instance = this;
            DontDestroyOnLoad(gameObject);
        }
    }
}
