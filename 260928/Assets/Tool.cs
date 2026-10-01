using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using static UnityEditor.Experimental.GraphView.GraphView;

public class Tool : MonoBehaviour, IInteractable
{
    public int Weight { get; private set; }
    public GameObject GameObject { get => gameObject; }

    private WaitForSeconds _wait = new WaitForSeconds(0.3f);


    public IEnumerator WaitUseRoutine()
    {
        yield return _wait;
        TryUse();
    }

    public void TryUse()
    {

    }

    public void Interact()
    {

    }

    public void Detected()
    {
        Debug.Log($"{this.name} Detected");
    }

    public void Undetected()
    {
        Debug.Log($"{this.name} Undetected");
        Unselect();
    }

    public void Select()
    {
        Debug.Log($"{this.name} Selected");
    }

    public void Unselect()
    {
        Debug.Log($"{this.name} Unselected");
    }
}
