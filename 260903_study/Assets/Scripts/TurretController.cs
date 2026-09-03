using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretController : MonoBehaviour
{
    [SerializeField] private GameObject _bulletPrefab;
    [SerializeField] private Transform _muzzleTransform;
    [SerializeField] private float _coolTime;
    [SerializeField] private Transform _playerTransform;
    [SerializeField] private float _rotateSpeed;
    [SerializeField] private float _detectionRange;
    private float _cool;


    private void Update()
    {
        float distance = Vector3.Distance(transform.position, _playerTransform.position);
        _cool += Time.deltaTime;

        if (distance <= _detectionRange)
        {
            LookAtPlayer();
            if(_cool >= _coolTime)
            {
                SpawnBullet();
                _cool = 0;
            }
        }
        else
        {
            RotateTurret();
        }
    }

    private void RotateTurret()
    {
        transform.Rotate(Vector3.up, _rotateSpeed * Time.deltaTime);
    }

    private void LookAtPlayer()
    {
        transform.LookAt(_playerTransform);
    }

    private void SpawnBullet()
    {
        GameObject bullet = Instantiate(_bulletPrefab);
        bullet.transform.position = _muzzleTransform.position;
        bullet.transform.rotation = _muzzleTransform.rotation;
    }
}