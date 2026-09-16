using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class PlayerGrenade : MonoBehaviour
{
    [SerializeField] private ObjectPool _grenadePool;
    [SerializeField] private KeyCode _grenadeKey = KeyCode.Alpha3;
    [SerializeField] private float _maxThrowPower;

    [Header("Grenade")]
    [SerializeField] private int _maxGrenadeCount;

    public ObservableProperty<int> HasGrenadeCount = new(0);
    public ObservableProperty<float> ThrowPower = new(0);
    private bool _isPressedGrenadeKey => Input.GetKey(_grenadeKey);
    private bool _isReleasedGrenadeKey => Input.GetKeyUp(_grenadeKey);
    private float _enoughPower => MaxThrowPower / 2;
    private bool _canThrowGrenade => HasGrenadeCount.Value > 0;
    public int MaxGrenadeCount => _maxGrenadeCount;
    public float MaxThrowPower => _maxThrowPower;

    private void Start() => Init();

    public void ThrowReady()
    {
        if (!_canThrowGrenade || !_isPressedGrenadeKey) return;

        ThrowPower.Value += Time.deltaTime * _maxThrowPower;
        ThrowPower.Value = Mathf.Clamp(ThrowPower.Value, 0f, MaxThrowPower);
    }

    public void ThrowGrenade()
    {
        if (!_isReleasedGrenadeKey) return;

        if (ThrowPower.Value >= _enoughPower)
        {
            IPoolable grenade = _grenadePool.Take();

            grenade.tr.position = transform.position + transform.forward;
            grenade.tr.rotation = transform.rotation;

            Vector3 newVelocity = grenade.tr.forward * ThrowPower.Value;
            grenade.tr.GetComponent<Rigidbody>().velocity = newVelocity;
            grenade.tr.gameObject.SetActive(true);
            ((GrenadeController)grenade).Body.SetActive(true);
            ((GrenadeController)grenade).Effect.SetActive(false);
            HasGrenadeCount.Value--;
        }

        ThrowPower.Value = 0;
    }

    private void Init()
    {
        HasGrenadeCount.Value = _maxGrenadeCount;
        ThrowPower.Value = 0;
    }
}
