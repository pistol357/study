using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerWeapon : MonoBehaviour
{
    [SerializeField] private KeyCode _fireKey = KeyCode.Mouse0;
    [SerializeField] private KeyCode _reloadKey = KeyCode.R;
    [SerializeField] private float _range;
    [SerializeField] private int _damage;
    [SerializeField] private float _cooldown;
    [SerializeField] private int _maxBullet;
    [SerializeField] private FlameEffect _flameEffect;
    [SerializeField] private FlameEffect _bulletImpactEffectPrefab;

    public ObservableProperty<int> HasBullet = new(0);
    private Transform _cameraTransform;
    private bool _isPressedFire => Input.GetKey(_fireKey);
    private bool _isPressedReload => Input.GetKey(_reloadKey);
    private float _currentCooldown;
    private float _cooldownBonus = 0;

    public int MaxBullet
    {
        get
        {
            return _maxBullet;
        }
    }

    private bool _isReadyToFire
    {
        get
        {
            return _currentCooldown >= FinalCooldown;
        }
    }

    public float FinalCooldown
    {
        get
        {
            return _cooldown * (1 - _cooldownBonus);
        }
    }

    public float CooldownBonus
    {
        get
        {
            return _cooldownBonus;
        }

        set
        {
            _cooldownBonus = value;

            if (_cooldownBonus > 1)
            {
                _cooldownBonus = 1;
            }
        }
    }

    private void Awake()
    {
        CacheComponents();
    }

    private void Start()
    {
        Init();
    }

    private void Update()
    {
        UpdateCurrentCooldown();
        ReadReloadInput();
    }

    private void UpdateCurrentCooldown()
    {
        if (_isReadyToFire) return;

        _currentCooldown += Time.deltaTime;
    }

    public void ResetCooldownBonus()
    {
        _cooldownBonus = 0;
    }

    public void Fire()
    {
        if (!_isReadyToFire || !_isPressedFire) return;
        if (HasBullet.Value <= 0)
        {
            return;
        }

        _currentCooldown = 0f;
        HasBullet.Value--;
        PlayFlameEffect();

        IDamageable damageable = GetDamageable();

        if (damageable == null) return;

        damageable.TakeDamage(_damage);
    }

    private void PlayFlameEffect()
    {
        _flameEffect.gameObject.SetActive(true);
        _flameEffect.Play();
    }

    private void PlayBulletImpactEffect(RaycastHit hit)
    {
        Transform effectTransform = Instantiate(_bulletImpactEffectPrefab).transform;
        effectTransform.position = hit.point;
        effectTransform.forward = hit.normal;
    }

    public void Reload()
    {
        HasBullet.Value = _maxBullet;
    }

    private void ReadReloadInput()
    {
        if (!_isPressedReload) return;

        Reload();
    }

    private IDamageable GetDamageable()
    {
        Ray ray = new Ray(_cameraTransform.position, _cameraTransform.forward);
        RaycastHit hit;

        IDamageable damageable = null;

        if(Physics.Raycast(ray, out hit, _range))
        {
            PlayBulletImpactEffect(hit);
            damageable = hit.transform.GetComponent<IDamageable>();
        }

        return damageable;
    }

    private void CacheComponents()
    {
        _cameraTransform = Camera.main.transform;
    }

    private void Init()
    {
        Reload();
    }
}
