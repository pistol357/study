using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EyeOnTarget : MonoBehaviour
{
    [SerializeField] private Transform _target;

    private void Update()
    {
        FaceTarget();
    }

    private void FaceTarget()
    {
        transform.LookAt(_target);
    }
}