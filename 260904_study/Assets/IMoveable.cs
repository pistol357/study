using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IMoveable
{
    public Vector3 Destination { get; set; }
    public bool IsMoving { get; set; }
    public float MoveSpeed { get; set; }

    public void SetDestination(Vector3 destination);
}