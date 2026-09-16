using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.Progress;

public class PlayerController : MonoBehaviour, IInteractor, IDamageable
{
    [SerializeField] private int _maxHp;
    [SerializeField] private Transform _cameraPivot;
    [SerializeField] private float _detectionRange;
    [SerializeField] private KeyCode _interactionKey = KeyCode.E;
    [SerializeField] private List<ItemBox> _usingItems;

    public ObservablHealthProperty Hp = new(0);
    public int MaxHp => _maxHp;
    private PlayerWeapon _weapon;
    private PlayerGrenade _grenade;
    private PlayerMovement _movement;
    private Transform _cameraTransform;
    private IInteractable _targetInteractable;
    private bool _hasDetectInteractable => _targetInteractable != null;
    private bool _isPressedInteractionKey => Input.GetKeyDown(_interactionKey);
    private bool _canInteraction => _hasDetectInteractable && _isPressedInteractionKey;
    private bool _isUsingItem;
    public Transform CameraPivot => _cameraPivot;
    public GameObject GameObject => gameObject;

    private void Awake() => CacheComponents();

    private void Start() => Init();

    private void FixedUpdate() => _movement.Move();

    private void Update()
    {
        _movement.Jump();
        _movement.Rotate();
        _weapon.Fire();
        _grenade.ThrowReady();
        _grenade.ThrowGrenade();
        DetectInteractable();
        TryInteract();
        UpdateItemTime();
        UsingItem();
    }

    private void LateUpdate()
    {
        SetCameraTransform();
        SetWeaponTransform();
        SetGrenadeTransform();
    }

    private void CacheComponents()
    {
        _movement = GetComponent<PlayerMovement>();
        _weapon = GetComponentInChildren<PlayerWeapon>();
        _grenade = GetComponentInChildren<PlayerGrenade>();
        _cameraTransform = Camera.main.transform;
    }

    private void SetWeaponTransform()
    {
        _weapon.transform.SetPositionAndRotation(
            _cameraPivot.position,
            _cameraPivot.rotation
            );
    }

    private void SetGrenadeTransform()
    {
        _grenade.transform.SetPositionAndRotation(
            _cameraPivot.position,
            _cameraPivot.rotation
            );
    }

    private void SetCameraTransform()
    {
        _cameraTransform.SetPositionAndRotation(
            _cameraPivot.position,
            _cameraPivot.rotation
            );
    }

    public void DetectInteractable()
    {
        Ray ray = new Ray(_cameraTransform.position, _cameraTransform.forward);
        RaycastHit hit;

        if (!Physics.Raycast(ray, out hit, _detectionRange))
        {
            if (_hasDetectInteractable)
            {
                _targetInteractable.Untargeting();
                _targetInteractable = null;
            }
            return;
        }

        if (_hasDetectInteractable)
        {
            if(hit.collider.gameObject == _targetInteractable.GameObject)
            {
                return;
            }
        }

        _targetInteractable?.Untargeting();
        _targetInteractable = hit.collider.GetComponent<IInteractable>();

        _targetInteractable?.Targeting();
    }

    public void TryInteract()
    {
        if (!_canInteraction) return;

        _targetInteractable.Interact(this);
        _targetInteractable = null;
    }

    private void UpdateItemTime()
    {
        if (!_isUsingItem) return;

        for(int i = 0; i < _usingItems.Count; i++)
        {
            _usingItems[i].ItemTime -= Time.deltaTime;
            if(_usingItems[i].ItemTime <= 0)
            {
                _usingItems.RemoveAt(i);
            }
        }
    }

    public void UseItem(ItemBox item)
    {
        if (item == null) return;

        _usingItems.Add(item);
        _isUsingItem = true;
    }

    private void UsingItem()
    {
        if (!_isUsingItem)
        {
            _movement.ResetSpeedBonus();
            _weapon.ResetCooldownBonus();
            return;
        }

        float maxSpeedBonus = 0;
        float maxCooldownBonus = 0;
        foreach(ItemBox item in _usingItems)
        {
            if(maxSpeedBonus < item.SpeedBonus)
            {
                maxSpeedBonus = item.SpeedBonus;
            }

            if(maxCooldownBonus < item.CooldownBonus)
            {
                maxCooldownBonus = item.CooldownBonus;
            }
        }

        _movement.SpeedBonus = maxSpeedBonus;
        _weapon.CooldownBonus = maxCooldownBonus;

        if(_usingItems.Count == 0)
        {
            _isUsingItem = false;
        }
    }

    public void TakeDamage(int damage)
    {
        Hp.Value -= damage;
    }

    private void GameOver()
    {
    }

    private void Init()
    {
        Hp.Value = _maxHp;
    }
}
