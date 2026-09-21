using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Monster : MonoBehaviour
{
    public bool IsTurnRunning { get; set; }

    public void OnTurnStart()
    {
        Debug.Log($"{gameObject.name} : 턴 시작");
    }

    public void OnTurnRunning()
    {
        Debug.Log($"{gameObject.name} : 턴 진행중");
    }

    public void OnTurnEnd()
    {
        Debug.Log($"{gameObject.name} : 턴 끝");
    }
}
