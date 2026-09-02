using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AxisMover : MonoBehaviour
{
    [SerializeField] private Vector3 _direction = Vector3.forward;
    [SerializeField] private float _meterPerSecond = 2f;

    private Vector3 _startPosition;
    private float _elapsed;
    private bool _reported;

    private void Awake()
    {
        
    }

    private void Update()
    {

    }

    private void CacheStart()
    {
        _startPosition = transform.position;
    }

    private void Move()
    {
        transform.position += _direction * _meterPerSecond * Time.deltaTime;
    }

    private void ReportOnce()
    {
        if (_reported)
        {
            return;
        }

        _elapsed += Time.deltaTime;

        if(_elapsed < 3f)
        {
            return;
        }

        _reported = true;
        float moved = (transform.position - _startPosition).magnitude;
        Debug.Log($"AxisMover: {name}이 3초 동안 움직인 거리는 {moved}입니다.");
    }
}