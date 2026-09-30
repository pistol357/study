using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DetectRange : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other is IInteractable)
        {
            Debug.Log("hi");
            (other as IInteractable).Selected();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other is IInteractable)
        {
            (other as IInteractable).Unselected();
        }
    }
}
