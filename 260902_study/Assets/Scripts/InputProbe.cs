using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputProbe : MonoBehaviour
{
    private Renderer _renderer;

    private void Awake()
    {
        CacheComponents();
    }

    private void Update()
    {
        ReadToggleKey();
    }

    private void CacheComponents()
    {
        _renderer = GetComponent<Renderer>();
    }

    private void ReadToggleKey()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            _renderer.enabled = !_renderer.enabled;
            Debug.Log($"InputProbe: 보이기 상태는 {_renderer.enabled}입니다.");
        }
    }
}