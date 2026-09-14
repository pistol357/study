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

    private void OnEnable() => BindTurretUIEvent();

    private void Update()
    {
        SetUITransform();
    }

    private void BindTurretUIEvent()
    {
        _turret.Hp.AddListener(UpdateUI);
    }

    private void UpdateUI()
    {
        _nameText.text = $"{_turret.Name}";
        _hpText.text = $"{_turret.Hp.Value} / {_turret.MaxHp}";
        _hpImage.fillAmount = (float)_turret.Hp.Value / _turret.MaxHp;
    }

    private void SetUITransform() => transform.rotation = _player.CameraPivot.rotation;

    private void CacheComponents() => _turret = GetComponentInParent<TurretController>();
}
