using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class SpinWings : MonoBehaviour
{
    [SerializeField] private GameObject _helicopter;
    [SerializeField] private float _spinSpeed;
    public float SpinSpeed
    {
        get
        {
            return _spinSpeed;
        }

        set
        {
            _spinSpeed = value;
            if(_spinSpeed > 2000)
            {
                _spinSpeed = 2000;
            }
            else if(_spinSpeed < 0)
            {
                _spinSpeed = 0;
            }
        }
    }
    [SerializeField] private float _takeoffSpeed;

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
            SpinSpeed++;
        }

        if (Input.GetKey(KeyCode.LeftShift))
        {
            SpinSpeed--;
        }
    }

    private void Spin()
    {
        transform.Rotate(Vector3.up, _spinSpeed * Time.deltaTime);
    }

    private void Fly()
    {
        if(1000 <= _spinSpeed && _spinSpeed <= 2000)
        {
            Vector3 position = _helicopter.transform.position;
            position.y = _spinSpeed / 100;
            _helicopter.transform.position = Vector3.MoveTowards(_helicopter.transform.position, position, _takeoffSpeed * Time.deltaTime);
        }
        else if(_spinSpeed < 1000)
        {
            Vector3 position = _helicopter.transform.position;
            position.y = 0;
            _helicopter.transform.position = Vector3.MoveTowards(_helicopter.transform.position, position, _takeoffSpeed * Time.deltaTime);
        }
    }
}