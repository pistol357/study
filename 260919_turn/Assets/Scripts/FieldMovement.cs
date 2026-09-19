using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FieldMovement : MonoBehaviour
{
    [SerializeField] private float _moveSpeed;

    private void Update() => Tick();

    private void Tick()
    {
        Vector3 movement = ReadMoveInput().normalized;

        transform.Translate(movement * _moveSpeed * Time.deltaTime, Space.World);
    }

    private Vector3 ReadMoveInput()
    {
        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");

        return new Vector3(horizontal, 0f, vertical);
    }
}
