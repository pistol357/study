using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class LifeCycleProbe : MonoBehaviour
{
    private void Awake()
    {
        Debug.Log("LIfeCycleProbe: Awake");
    }

    private void OnEnable()
    {
        Debug.Log("LIfeCycleProbe: OnEnable");
    }

    private void Start()
    {
        Debug.Log("LIfeCycleProbe: Start");
    }

    private void FixedUpdate()
    {
        Debug.Log("LifeCycleProbe: FixedUpdate");
    }

    private void Update()
    {
        Debug.Log("LifeCycleProbe: Update");
    }

    private void LateUpdate()
    {
        Debug.Log("LifeCycleProbe: LateUpdate");
    }
}