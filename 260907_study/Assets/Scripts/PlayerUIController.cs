using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PlayerUIController : MonoBehaviour
{
    [SerializeField] private Image _hpImage;
    [SerializeField] private TextMeshProUGUI _hpText;
    [SerializeField] private TextMeshProUGUI _bullet;
    [SerializeField] private TextMeshProUGUI _grenadeCount;
    [SerializeField] private TextMeshProUGUI _grenadeCharge;
    [SerializeField] private GameObject _gameOverUI;

    private PlayerController _player;
    private PlayerWeapon _weapon;
    private PlayerGrenade _grenade;

    private void Awake() => CacheComponents();

    private void OnEnable() => BindPlayerUIEvent();

    private void BindPlayerUIEvent()
    {
        _player.Hp.AddListener(UpdateHpUI);
        _weapon.HasBullet.AddListener(UpdateBulletUI);
        _grenade.HasGrenadeCount.AddListener(UpdateGrenadeCountUI);
        _grenade.ThrowPower.AddListener(UpdateGrenadeChargeUI);
        GameManager.Instance.GameOver += ShowGameOverUI;
    }

    private void UpdateHpUI()
    {
        _hpImage.fillAmount = (float)_player.Hp.Value / _player.MaxHp;
        _hpText.text = $"{_player.Hp.Value} / {_player.MaxHp}";
    }

    private void ShowGameOverUI()
    {
        if (_player.Hp.Value > 0) _gameOverUI.SetActive(false);
        else _gameOverUI.SetActive(true);
    }

    private void UpdateBulletUI()
    {
        _bullet.text = $"{_weapon.HasBullet.Value} / {_weapon.MaxBullet}";
    }

    private void UpdateGrenadeCountUI()
    {
        _grenadeCount.text = $"{_grenade.HasGrenadeCount.Value} / {_grenade.MaxGrenadeCount}";
    }

    private void UpdateGrenadeChargeUI()
    {
        if(_grenade.ThrowPower.Value >= _grenade.MaxThrowPower / 2)
        {
            _grenadeCharge.text = $"{(int)(_grenade.ThrowPower.Value) - (int)(_grenade.MaxThrowPower / 2)} / {(int)(_grenade.MaxThrowPower / 2)}";
        }
        else
        {
            _grenadeCharge.text = "";
        }
    }

    private void CacheComponents()
    {
        _player = GetComponent<PlayerController>();
        _weapon = GetComponentInChildren<PlayerWeapon>();
        _grenade = GetComponentInChildren<PlayerGrenade>();
    }
}
