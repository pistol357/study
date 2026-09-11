using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletController : MonoBehaviour
{
    private int _damage;
    private float _speed;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("죽어라");
        }

        Destroy(gameObject);
    }

    private void Update()
    {
        MoveForward();
    }

    private void MoveForward()
    {
        transform.Translate(Vector3.forward * _speed * Time.deltaTime);
    }

    public void SetData(int damage, float speed, float destroyDelay)
    {
        _damage = damage;
        _speed = speed;

        Destroy(gameObject, destroyDelay);
    }
}
