using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IWannaSleep : MonoBehaviour
{
    private SphereCollider b;

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log($"{gameObject.name} : 트리거 안에 {other.gameObject.name} 들어옴");

        IDamageable d = other.gameObject.GetComponent<IDamageable>();

        if (d != null)
        {
            d.TakeDamage(10);
        }
    }

    private void OnTriggerStay(Collider other)
    {
        // Debug.Log($"{gameObject.name} : 트리거 안에 {other.gameObject.name} 있음");
    }

    private void OnTriggerExit(Collider other)
    {
        Debug.Log($"{gameObject.name} : 트리거에서 {other.gameObject.name} 나감");
    }
}