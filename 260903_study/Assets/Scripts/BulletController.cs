using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletController : MonoBehaviour
{
    [SerializeField] private float _speed;
    [SerializeField] private float _destroyTime;

    [SerializeField] private AudioSource _audioSource;
    [SerializeField] private AudioClip _audioClip;

    private void Awake()
    {
        _audioSource = GetComponent<AudioSource>();
        _audioSource.loop = false;
        _audioSource.Stop();
        _audioSource.clip = _audioClip;
        _audioSource.Play();

        Destroy(gameObject, _destroyTime);
    }

    private void Update()
    {
        MoveForward();
    }

    private void MoveForward()
    {
        transform.Translate(Vector3.forward * _speed * Time.deltaTime);
    }
}