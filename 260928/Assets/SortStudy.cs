using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SortStudy : MonoBehaviour
{
    [SerializeField] private List<Enemy> _enemies = new();

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            _enemies.Sort((a, b) => a.Health.CompareTo(b.Health));
        }
    }
}
