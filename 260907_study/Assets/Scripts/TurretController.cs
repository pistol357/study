using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretController : MonoBehaviour, IDamageable
{
    [SerializeField] private string _name;
    [SerializeField] private int _maxHp;
    [SerializeField] private float _rotateSpeed;
    [SerializeField] private float _cooldown;
    [SerializeField] private Transform _headTransform;
    [SerializeField] private Transform _muzzlePoint;

    [Header("Bullet")]
    [SerializeField] private BulletController _bulletPrefab;
    [SerializeField] private int _bulletDamage;
    [SerializeField] private float _bulletSpeed;
    [SerializeField] private float _bulletDestroyDelay;

    public ObservableProperty<int> Hp = new(0);
    private float _currentCooldown;
    private Transform _playerTransform => _detectRange.GetCollider();
    private TurretTargetDetectController _detectRange;
    private bool _isPlayerInSight => _detectRange.RayShotToPlayer();
    private bool _isReadyToFire => _currentCooldown >= _cooldown;
    public GameObject GameObject => gameObject;
    public Transform MuzzlePoint => _muzzlePoint;
    public string Name => _name;
    public int MaxHp => _maxHp;

    private void Awake() => CacheComponents();

    private void Start() => Init();

    private void Update()
    {
        UpdateCurrentCooldown();
        Rotate();
        Fire();
    }

    private void Fire()
    {
        if (!_isPlayerInSight) return;

        Vector3 look = new Vector3(
            _playerTransform.position.x,
            _headTransform.position.y,
            _playerTransform.position.z);

        _headTransform.LookAt(look);

        if (!_isReadyToFire) return;

        SpawnBullet();

        _currentCooldown = 0f;
    }

    private void UpdateCurrentCooldown()
    {
        if (_isReadyToFire) return;

        _currentCooldown += Time.deltaTime;
    }

    private void SpawnBullet()
    {
        BulletController bullet = Instantiate(_bulletPrefab, _muzzlePoint.position, _muzzlePoint.rotation);

        bullet.SetData(_bulletDamage, _bulletSpeed, _bulletDestroyDelay);
    }

    private void Rotate()
    {
        if (_isPlayerInSight) return;

        _headTransform.Rotate(Vector3.up, _rotateSpeed * Time.deltaTime);
    }

    public void TakeDamage(int damage)
    {
        Hp.Value -= damage;
        if(Hp.Value <= 0)
        {
            Destroy(transform.gameObject);
        }
    }

    private void CacheComponents() => _detectRange = GetComponentInChildren<TurretTargetDetectController>();
    private void Init() => Hp.Value = _maxHp;
}
