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
    
    private int _hasGrenadeCount;
    private bool _isPressedGrenadeKey => Input.GetKey(_grenadeKey);
    private bool _isReleasedGrenadeKey => Input.GetKeyUp(_grenadeKey);
    private bool _canThrowGrenade => _hasGrenadeCount > 0;
    private float _throwPower;

    public int MaxGrenadeCount
    {
        get
        {
            return _maxGrenadeCount;
        }
    }

    public int HasGrenadeCount
    {
        get
        {
            return _hasGrenadeCount;
        }
    }

    public float ThrowPower
    {
        get
        {
            return _throwPower;
        }

        set
        {
            _throwPower = value;
            if(_throwPower > _maxThrowPower)
            {
                _throwPower = _maxThrowPower;
            }
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

        if (_throwPower >= _maxThrowPower || _isReleasedGrenadeKey)
        {
            ThrowGrenade();
            _throwPower = 0;
        }

        if (!_isPressedGrenadeKey) return;

        _throwPower += Time.deltaTime * _maxThrowPower;
    }

    private void ThrowGrenade()
    {
        if (_throwPower >= _maxThrowPower / 2)
        {
            GrenadeController grenade = Instantiate(
            _grenadePrefab,
            transform.position,
            transform.rotation
            );

            Vector3 newVelocity = grenade.transform.forward * _throwPower;
            grenade.GetComponentInParent<Rigidbody>().velocity = newVelocity;
            _hasGrenadeCount--;
        }
    }

    private void Init()
    {
        _hasGrenadeCount = _maxGrenadeCount;
    }
}
