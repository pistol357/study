using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tool : MonoBehaviour
{
    public int Weight { get; private set; }

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
}
