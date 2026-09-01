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

    private Renderer _selfRenderer;
    private Renderer _farRenderer;

    private void Awake()
    {
        _selfRenderer = GetComponent<Renderer>();

        GameObject farObject = GameObject.FindWithTag(TAG_FAR);
        _farRenderer = farObject.GetComponent<Renderer>();

        Debug.Log($"VisibilitySwitch: 자신은 {_selfRenderer.name}입니다.");
        Debug.Log($"VisibilitySwitch: 연결된 자식은 {_childRenderer.name}입니다.");
        Debug.Log($"VisibilitySwitch: 태그로 찾은 것은 {_farRenderer.name}입니다.");
    }
}
