using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAction : MonoBehaviour
{
    private Rigidbody _playerBody;

    // ------------------------------
    private void Awake() => CacheComponents();
    // ------------------------------

    public void Move(Vector3 direction, float moveSpeed)
    {
        if (direction == Vector3.zero)
        {
            _playerBody.velocity = Vector3.zero;
            return;
        }

        _playerBody.MoveRotation(Quaternion.LookRotation(direction));
        _playerBody.velocity = _playerBody.transform.forward * moveSpeed;
    }

    public void Dash()
    {

    }

    public void Detect()
    {

    }

    public void TryInteract()
    {

    }

    public void Drop()
    {

    }

    private void CacheComponents()
    {
        _playerBody = GetComponent<Rigidbody>();
    }
}
