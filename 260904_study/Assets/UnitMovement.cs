using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitMovement : MonoBehaviour, IMoveable, ISelectable
{
    public Vector3 Destination { get; set; }
    public bool IsMoving { get; set; }
    [field:SerializeField] public float MoveSpeed { get; set; }

    private void Update()
    {
        Move();
    }

    private void Move()
    {
        if (!IsMoving) return;

        transform.position = Vector3.MoveTowards(transform.position, Destination, MoveSpeed * Time.deltaTime);

        if(Vector3.Distance(transform.position, Destination) <= 0.5f)
        {
            IsMoving = false;
        }
    }

    public void SetDestination(Vector3 destination)
    {
        Destination = destination;
        IsMoving = true;
    }

    public void Selected()
    {
        Debug.Log($"{transform.name} 선택됨");
    }
}