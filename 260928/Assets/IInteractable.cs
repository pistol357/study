using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IInteractable
{
    public GameObject GameObject { get; }

    public void Detected();
    public void Undetected();
    public void Select();
    public void Unselect();
    public void Interact();
}
