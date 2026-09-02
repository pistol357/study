using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoalController : MonoBehaviour
{
    [SerializeField] private float _revolutionSpeed;
    [SerializeField] private Transform _target;

    private void Update()
    {
        transform.RotateAround(_target.position, Vector3.up, _revolutionSpeed * Time.deltaTime);
    }
}