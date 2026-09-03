using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerSpawner : MonoBehaviour
{
    [SerializeField] private GameObject _towerPrefab;
    private int _count;

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
        Vector3 spawnPosition = new Vector3(_count * 2f, 0f, 0f);
        Instantiate(_towerPrefab, spawnPosition, Quaternion.identity);
        _count += 1;
        Debug.Log($"TowerSpawner: {_count}번째를 만들었습니다.");
    }
}