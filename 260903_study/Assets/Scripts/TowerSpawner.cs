using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerSpawner : MonoBehaviour
{
    [SerializeField] private GameObject _towerPrefab;

    private void Update()
    {
        ReadSpawnKey();
    }

    private void ReadSpawnKey()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            SpawnOne();
        }
    }

    private void SpawnOne()
    {
        Instantiate(_towerPrefab);
        Debug.Log("TowerSpawner: 하나 만들었습니다.");
    }
}