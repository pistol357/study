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
    private bool _canThrowGrenade => HasGrenadeCount.Value > 0;
    public int MaxGrenadeCount => _maxGrenadeCount;
    public float MaxThrowPower => _maxThrowPower;

    private void Start() => Init();

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
            IPoolable grenade = _grenadePool.Take();
            Debug.Log(grenade);

            grenade.tr.position = this.transform.position;
            Debug.Log($"{grenade} / {transform}");
            grenade.tr.rotation = transform.rotation;

            Vector3 newVelocity = grenade.tr.forward * ThrowPower.Value;
            grenade.tr.GetComponentInParent<Rigidbody>().velocity = newVelocity;
            grenade.tr.gameObject.SetActive(true);
            HasGrenadeCount.Value--;
        }
    }

    private void Init()
    {
        HasGrenadeCount.Value = _maxGrenadeCount;
        ThrowPower.Value = 0;
    }
}
