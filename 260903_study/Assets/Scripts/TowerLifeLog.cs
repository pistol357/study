using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerLifeLog : MonoBehaviour
{
    private void OnDestroy()
    {
        Debug.Log($"TowerLifeLog: {gameObject.name}이 사라집니다.");
    }
}