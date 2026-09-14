using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretTargetDetectController : MonoBehaviour
{
    private Transform _playerTransform;
    private TurretController _turret;
    private SphereCollider _sphereCollider;
    private LayerMask _layerMask;
    private bool _isPlayerInTrigger => _playerTransform != null;
    private bool _isPlayerInSight = false;

    private void Awake() => CacheComponents();

    private void Start() => Init();

    private void Update() => RayShotToPlayer();

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.layer == LayerMask.NameToLayer("Player"))
        {
            _playerTransform = other.transform;
        }
    }

    private void OnTriggerExit(Collider other) => _playerTransform = null;
    
    public Transform GetCollider() => _playerTransform;

    public bool RayShotToPlayer()
    {
        _isPlayerInSight = false;
        if (!_isPlayerInTrigger) return _isPlayerInSight;

        Vector3 from = new Vector3(
            transform.position.x,
            transform.position.y + _turret.MuzzlePoint.position.y,
            transform.position.z);
        Vector3 to = new Vector3(
            _playerTransform.position.x,
            _playerTransform.position.y + _turret.MuzzlePoint.position.y,
            _playerTransform.position.z);

        Ray ray = new Ray(from, (to - from).normalized);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, _sphereCollider.radius, _layerMask))
        {
            if (hit.transform.gameObject.layer == LayerMask.NameToLayer("Player"))
            {
                _isPlayerInSight = true;
            }
        }

        return _isPlayerInSight;
    }

    private void CacheComponents()
    {
        _turret = GetComponentInParent<TurretController>();
        _sphereCollider = GetComponent<SphereCollider>();
    }

    private void Init()
    {
        _layerMask = _layerMask.Everything();
        _layerMask = _layerMask.Remove(LayerMask.NameToLayer("Bullet"));
    }
}
