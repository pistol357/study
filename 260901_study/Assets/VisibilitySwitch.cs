using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VisibilitySwitch : MonoBehaviour
{
    private const string TAG_FAR = "FarTarget";

    [SerializeField] private Renderer _childRenderer;

    [SerializeField] private bool _showSelf = true;
    [SerializeField] private bool _showChild = true;
    [SerializeField] private bool _showFar = true;
}
