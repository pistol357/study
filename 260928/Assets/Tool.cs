using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using static UnityEditor.Experimental.GraphView.GraphView;

public class Tool : MonoBehaviour, IInteractable
{
    public int Weight { get; private set; }

    private Outline _outline;
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

    public void Selected()
    {
        Debug.Log($"{this.name} Selected");
    }

    public void Unselected()
    {
        Debug.Log($"{this.name} Unselected");
    }
}
