using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class DamageUI : MonoBehaviour, IPoolable
{
    [SerializeField] private Vector3 _maxForce;

    private Rigidbody _rigidbody;
    private Camera _camera;
    private TextMeshProUGUI _tmp;

    private void Awake() => CacheComponents();
    private void OnEnable() => AddRandomForce();
    private void LateUpdate() => SetRotate();

    public void SetData(int value)
    {
        _tmp.text = value.ToString();
    }

    private void SetRotate()
    {
        transform.forward = _camera.transform.forward;
    }

    private void AddRandomForce()
    {
        _rigidbody.velocity = GetRandomForce();
    }

    private Vector3 GetRandomForce()
    {
        return new Vector3(
            Random.Range(-_maxForce.x, _maxForce.x),
            _maxForce.y,
            Random.Range(-_maxForce.z, _maxForce.z)
            );
    }

    private void CacheComponents()
    {
        _rigidbody = GetComponent<Rigidbody>();
        _tmp = GetComponent<TextMeshProUGUI>();
        _camera = Camera.main;
    }
}
