using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    [SerializeField] private float _moveSpeed;

    private Stack<Vector3> _moveStack = new();
    private KeyCode _undoKey = KeyCode.Space;
    private bool _isPressedUndoKey => Input.GetKey(_undoKey);

    private void Update()
    {
        Move();
        UndoMove();
    }

    private void Move()
    {
        if (ReadMove() == Vector3.zero) return;

        Vector3 prevPosition = transform.position;
        _moveStack.Push(prevPosition);
        transform.position += ReadMove() * _moveSpeed * Time.deltaTime;
    }

    private Vector3 ReadMove()
    {
        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");

        return new Vector3(horizontal, 0f, vertical).normalized;
    }

    private void UndoMove()
    {
        if (!_isPressedUndoKey) return;

        Vector3 prevPosition;
        if(!_moveStack.TryPop(out prevPosition))
        {
            Debug.Log("더 이상 이동 취소 불가");
            return;
        }
        else
        {
            transform.position = prevPosition;
        }
    }
}
