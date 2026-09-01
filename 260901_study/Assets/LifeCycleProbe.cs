using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class LifeCycleProbe : MonoBehaviour
{
    private void Awake() => LogStep("Awake");
    private void OnEnable() => LogStep("OnEnable");
    private void Start() => LogStep("Start");
    private void OnDisable() => LogStep("OnDisable");
    private void Ondestroy() => LogStep("Ondestroy");

    private void LogStep(string stepName)
    {
        Debug.Log($"LifeCycleProbe: {stepName}");
    }
}