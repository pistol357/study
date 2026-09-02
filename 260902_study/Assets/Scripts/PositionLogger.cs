using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PositionLogger : MonoBehaviour
{
    private float _elapsed;

    private void Update()
    {
        LogPeriodically();
    }

    private void LogPeriodically()
    {
        _elapsed += Time.deltaTime;

        if(_elapsed < 1f)
        {
            return;
        }

        _elapsed = 0f;
        Debug.Log($"PositionLogger: position은{transform.position}, localPosition은 {transform.localPosition}입니다.");
    }
}