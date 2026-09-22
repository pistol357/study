using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Summon : MonoBehaviour
{
    [SerializeField] private GameObject _prefab1;
    [SerializeField] private GameObject _prefab2;
    [SerializeField] private GameObject _prefab3;

    private Queue<GameObject> _summonQueue = new();
    private KeyCode _summonKey1 = KeyCode.Alpha1;
    private KeyCode _summonKey2 = KeyCode.Alpha2;
    private KeyCode _summonKey3 = KeyCode.Alpha3;
    private bool _isPressedSummonKey1 => Input.GetKeyDown(_summonKey1);
    private bool _isPressedSummonKey2 => Input.GetKeyDown(_summonKey2);
    private bool _isPressedSummonKey3 => Input.GetKeyDown(_summonKey3);

    private void Update()
    {
        ReadSummonInput();
    }

    private void ReadSummonInput()
    {
        if (_isPressedSummonKey1)
        {
            _summonQueue.Enqueue(_prefab1);
        }
        if (_isPressedSummonKey2)
        {
            _summonQueue.Enqueue(_prefab2);
        }
        if (_isPressedSummonKey3)
        {
            _summonQueue.Enqueue(_prefab3);
        }
    }

    private void SummonPrefab()
    {
        
    }
}
