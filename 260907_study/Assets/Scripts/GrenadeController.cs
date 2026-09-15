using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrenadeController : MonoBehaviour
{
    private float _delay;
    private int _damage;
    private float _elapsedTime;
    private bool _isTimeUp => _elapsedTime >= _delay;

    private void Update()
    {
        UpdateElapsedTime();
        Explosion();
    }

    private void UpdateElapsedTime()
    {
        _elapsedTime += Time.deltaTime;
    }

    private void Explosion()
    {
        if (!_isTimeUp) return;


    }

    public void SetData(float delay, int damage)
    {
        _delay = delay;
        _damage = damage;
    }
}
