using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class TurretUIController : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _nameText;
    [SerializeField] private TextMeshProUGUI _hpText;
    [SerializeField] private Image _hpImage;
    [SerializeField] private PlayerController _player;

    private TurretController _turret;

    private void Awake() => CacheComponents();

    private void Update()
    {
        UpdateUI();
        SetUITransform();
    }

    private void UpdateUI()
    {
        _nameText.text = $"{_turret.Name}";
        _hpText.text = $"{_turret.Hp} / {_turret.MaxHp}";
        _hpImage.fillAmount = (float)_turret.Hp / _turret.MaxHp;
    }

    private void SetUITransform() => transform.rotation = _player.CameraPivot.rotation;

    private void CacheComponents() => _turret = GetComponentInParent<TurretController>();
}
