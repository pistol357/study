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
}