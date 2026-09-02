using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyMover : MonoBehaviour
{
    private const string AXIS_HORIZONTAL = "Horizontal";
    private const string AXIS_VERTICAL = "Vertical";

    [SerializeField] private float _meterPerSecond = 3f;

    private void Update()
    {
        MoveByKey();
    }

    private void MoveByKey()
    {
        float horizontal = Input.GetAxisRaw(AXIS_HORIZONTAL);
        float vertical = Input.GetAxisRaw(AXIS_VERTICAL);
        Vector3 direction = new Vector3(horizontal, 0f, vertical);
        transform.position += direction.normalized * _meterPerSecond * Time.deltaTime;
    }
}