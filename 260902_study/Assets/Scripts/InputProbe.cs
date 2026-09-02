using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputProbe : MonoBehaviour
{
    private const string AXIS_HORIZONTAL = "Horizontal";
    private Renderer _renderer;

    private void Awake()
    {
        CacheComponents();
    }

    private void Update()
    {
        ReadToggleKey();
        ReadAxes();
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

    private void ReadAxes()
    {
        float smoothed = Input.GetAxis(AXIS_HORIZONTAL);
        float raw = Input.GetAxisRaw(AXIS_HORIZONTAL);
        Debug.Log($"InputProbe: GetAxis는 {smoothed}, GetAxisRaw는 {raw}입니다.");
    }
}