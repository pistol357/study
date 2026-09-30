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

    // ------------------------------
    private void Awake() => CacheComponents();
    private void Start() => Init();
    // ------------------------------

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
        _outline.enabled = true;
    }

    public void Unselected()
    {
        _outline.enabled = false;
    }

    private void CacheComponents()
    {
        _outline = GetComponentInChildren<Outline>();
    }

    private void Init()
    {
        _outline.enabled = false;
    }
}
