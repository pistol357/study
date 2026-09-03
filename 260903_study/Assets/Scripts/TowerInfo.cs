using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerInfo : MonoBehaviour
{
    [SerializeField] private string _label = "기본";

    private void Start()
    {
        LogLabel();
    }

    private void LogLabel()
    {
        Debug.Log($"TowerInfo: 이 오브젝트의 이름표는 {_label}입니다.");
    }
}