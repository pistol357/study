using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputProbe : MonoBehaviour
{
    private const string AXIS_HORIZONTAL = "Horizontal";
    private const int MOUSE_BUTTON_LEFT = 0;
    [SerializeField] private float _amountPerSecond = 3f;
    private Renderer _renderer;
    private float _total;

    private void Awake()
    {
        CacheComponents();
    }

    private void Update()
    {
        ReadToggleKey();
        // ReadAxes();
        ReadMouseButton();
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
        float raw = Input.GetAxisRaw(AXIS_HORIZONTAL);
        _total += raw * _amountPerSecond * Time.deltaTime;
        Debug.Log($"InputProbe: 누적값은 {_total}입니다.");
    }

    private void ReadMouseButton()
    {
        if (Input.GetMouseButtonDown(MOUSE_BUTTON_LEFT))
        {
            Debug.Log($"InputProbe: 좌표는 {Input.mousePosition}입니다.");
        }
    }
}