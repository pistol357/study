using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Party : MonoBehaviour
{
    [field: SerializeField] public Unit[] Units { get; private set; }
}
