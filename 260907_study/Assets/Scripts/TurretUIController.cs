using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class TurretUIController : MonoBehaviour
{
    [SerializeField] private Image _hpImage;
    [SerializeField] private PlayerController _player;

    private TextMeshProUGUI _hpText;
    private TurretController _turret;

    private void Awake() => CacheComponents();

    private void Update()
    {
        UpdateHpUI();
        SetUITransform();
    }

    private void UpdateHpUI()
    {
        _hpText.text = $"{_turret.Hp} / {_turret.MaxHp}";
        _hpImage.fillAmount = (float)_turret.Hp / _turret.MaxHp;
    }

    private void SetUITransform() => transform.rotation = _player.CameraPivot.rotation;

    private void CacheComponents()
    {
        _hpText = GetComponentInChildren<TextMeshProUGUI>();
        _turret = GetComponentInParent<TurretController>();
    }
}
