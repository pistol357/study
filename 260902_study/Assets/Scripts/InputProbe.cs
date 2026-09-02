using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputProbe : MonoBehaviour
{
    private void Update()
    {
        ReadKeys();
    }

    private void ReadKeys()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Debug.Log("InputProbe: GetKeyDown");
        }

        if (Input.GetKey(KeyCode.Space))
        {
            Debug.Log("InputProbe: GetKey");
        }

        if (Input.GetKeyUp(KeyCode.Space))
        {
            Debug.Log("InputProbe: GetKeyUp");
        }
    }
}
