using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpinRoot : MonoBehaviour
{
    [SerializeField] private float _degreePerSecond = 60;

    private void Update()
    {
        Spin();
    }

    private void Spin()
    {
        transform.Rotate(Vector3.up * _degreePerSecond * Time.deltaTime);
    }
}
