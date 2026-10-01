using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.Experimental.GraphView.GraphView;

public class DetectRange : MonoBehaviour
{
    private List<IInteractable> _detecteds;
    private SphereCollider _collider;

    public List<IInteractable> Detecteds { get => _detecteds; }
    public float Range { get => _collider.radius; }

    // ------------------------------
    private void Awake() => CacheComponents();
    private void Start() => Init();
    // ------------------------------

    private void OnTriggerEnter(Collider other)
    {
        IInteractable interactable = other.GetComponent<IInteractable>();
        if (interactable != null)
        {
            interactable.Detected();
            _detecteds.Add(interactable);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        IInteractable interactable = other.GetComponent<IInteractable>();
        if (interactable != null)
        {
            interactable.Undetected();
            _detecteds.Remove(interactable);
        }
    }

    private void CacheComponents()
    {
        _collider = GetComponent<SphereCollider>();
    }

    private void Init()
    {
        _detecteds = new();
    }
}
