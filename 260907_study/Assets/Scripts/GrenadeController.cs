using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SubsystemsImplementation;

public class GrenadeController : MonoBehaviour, IPoolable
{
    [SerializeField] private float _delay;
    [SerializeField] private float _range;
    [SerializeField] private int _damage;
    [SerializeField] private GameObject _body;
    [SerializeField] private GameObject _effect;

    public ObjectPool Pool { get; set; }
    public Transform tr { get => transform; }
    private float _elapsedTime;
    private bool _isExplode;
    public GameObject Body => _body;
    public GameObject Effect => _effect;
    public float Delay
    {
        get => _delay;

        set
        {
            _delay = value;
        }
    }

    private void Update()
    {
        UpdateElapsedTime();
        Explosion();
    }

    private void Explosion()
    {
        if (_elapsedTime < Delay || _isExplode) return;

        _body.SetActive(false);
        _effect.SetActive(true);
        Collider[] colliders = Physics.OverlapSphere(transform.position, _range);

        foreach (Collider collider in colliders)
        {
            IDamageable damageable = collider.GetComponent<IDamageable>();

            if (damageable != null)
            {
                damageable.TakeDamage(_damage);
            }
        }

        _isExplode = true;
        StartCoroutine(ReturnToPoolRoutine());
    }

    private void UpdateElapsedTime()
    {
        _elapsedTime += Time.deltaTime;
    }

    private IEnumerator ReturnToPoolRoutine()
    {
        yield return new WaitForSeconds(1f);
        ReturnToPool();
    }

    public void ReturnToPool()
    {
        _elapsedTime = 0;
        _isExplode = false;
        Pool.Return(this);
    }
}
