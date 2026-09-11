using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PlayerUIController : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _bullet;
    [SerializeField] private TextMeshProUGUI _grenadeCount;
    [SerializeField] private TextMeshProUGUI _grenadeCharge;

    private PlayerWeapon _weapon;
    private PlayerGrenade _grenade;

    private void Awake()
    {
        CacheComponents();
    }

    private void Update()
    {
        UpdateBulletUI();
        UpdateGrenadeCountUI();
        UpdateGrenadeChargeUI();
    }

    private void UpdateBulletUI()
    {
        _bullet.text = $"{_weapon.HasBullet} / {_weapon.MaxBullet}";
    }

    private void UpdateGrenadeCountUI()
    {
        _grenadeCount.text = $"{_grenade.HasGrenadeCount} / {_grenade.MaxGrenadeCount}";
    }

    private void UpdateGrenadeChargeUI()
    {
        if(_grenade.ThrowPower >= _grenade.MaxThrowPower / 2)
        {
            _grenadeCharge.text = $"{(int)(_grenade.ThrowPower) - (int)(_grenade.MaxThrowPower / 2)} / {(int)(_grenade.MaxThrowPower / 2)}";
        }
        else
        {
            _grenadeCharge.text = "";
        }
    }

    private void CacheComponents()
    {
        _weapon = GetComponentInChildren<PlayerWeapon>();
        _grenade = GetComponentInChildren<PlayerGrenade>();
    }
}
