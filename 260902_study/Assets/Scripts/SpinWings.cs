using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpinWings : MonoBehaviour
{
    [SerializeField] private GameObject _helicopter;
    [SerializeField] private float _spinSpeed;

    private void Update()
    {
        Read();
        Spin();
        Fly();
    }

    private void Read()
    {
        if (Input.GetKey(KeyCode.Space))
        {
            _spinSpeed++;
        }
    }

    private void Spin()
    {
        transform.Rotate(Vector3.up, _spinSpeed * Time.deltaTime);
    }

    private void Fly()
    {
        if(_spinSpeed >= 1000)
        {
            Vector3 position = _helicopter.transform.position;
            position.y = _spinSpeed / 100;
            _helicopter.transform.position = position;
        }
    }
}