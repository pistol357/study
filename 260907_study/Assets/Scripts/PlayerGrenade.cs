using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class PlayerGrenade : MonoBehaviour
{
    [SerializeField] private KeyCode _grenadeKey = KeyCode.Alpha3;
    [SerializeField] private float _maxThrowPower;

    [Header("Grenade")]
    [SerializeField] private GrenadeController _grenadePrefab;
    [SerializeField] private int _maxGrenadeCount;
    [SerializeField] private float _explosionDelay;
    [SerializeField] private int _damage;

    public ObservableProperty<int> HasGrenadeCount = new(0);
    public ObservableProperty<float> ThrowPower = new(0);
    private bool _isPressedGrenadeKey => Input.GetKey(_grenadeKey);
    private bool _isReleasedGrenadeKey => Input.GetKeyUp(_grenadeKey);
    private bool _canThrowGrenade => HasGrenadeCount.Value > 0;

    public int MaxGrenadeCount
    {
        get
        {
            return _maxGrenadeCount;
        }
    }

    public float MaxThrowPower
    {
        get
        {
            return _maxThrowPower;
        }
    }

    private void Start()
    {
        Init();
    }

    public void ThrowReady()
    {
        if (!_canThrowGrenade) return;

        if (ThrowPower.Value >= _maxThrowPower || _isReleasedGrenadeKey)
        {
            ThrowGrenade();
            ThrowPower.Value = 0;
        }

        if (!_isPressedGrenadeKey) return;

        ThrowPower.Value += Time.deltaTime * _maxThrowPower;
    }

    private void ThrowGrenade()
    {
        if (ThrowPower.Value >= _maxThrowPower / 2)
        {
            GrenadeController grenade = Instantiate(
            _grenadePrefab,
            transform.position,
            transform.rotation
            );

            Vector3 newVelocity = grenade.transform.forward * ThrowPower.Value;
            grenade.GetComponentInParent<Rigidbody>().velocity = newVelocity;
            grenade.SetData(_explosionDelay, _damage);
            HasGrenadeCount.Value--;
        }
    }

    private void Init()
    {
        HasGrenadeCount.Value = _maxGrenadeCount;
        ThrowPower.Value = 0;
    }
}
