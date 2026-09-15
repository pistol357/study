using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SubsystemsImplementation;

public class GrenadeController : MonoBehaviour, IPoolable
{
    [SerializeField] private float _delay;
    [SerializeField] private float _range;
    [SerializeField] private int _damage;

    public ObjectPool Pool { get; set; }
    public Transform tr { get; }
    private WaitForSeconds _wait;
    public float Delay
    {
        get => _delay;

        set
        {
            _delay = value;
        }
    }

    private void Awake()
    {
        new WaitForSeconds(Delay);
    }

    private void Start()
    {
        StartCoroutine(ExplosionRoutine());
    }

    private IEnumerator ExplosionRoutine()
    {
        yield return _wait;
        Explosion();
    }

    private void Explosion()
    {
        Collider[] colliders = Physics.OverlapSphere(transform.position, _range);

        foreach (Collider collider in colliders)
        {
            IDamageable damageable = collider.GetComponent<IDamageable>();

            if (damageable != null)
            {
                damageable.TakeDamage(_damage);
            }
        }

        ReturnToPool();
    }

    public void ReturnToPool()
    {
        Pool.Return(this);
    }
}
