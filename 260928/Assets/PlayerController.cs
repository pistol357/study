using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private const int HAND_CAPACITY = 3;
    private const float THRESHOLD = 0.7f;

    private PlayerAction _player;
    private Vector3 _direction;
    private GameObject _pickedItem;
    private int _pickedQuantity;
    private DetectRange _detectRange;
    private List<IInteractable> _detecteds => _detectRange.Detecteds;
    private List<IInteractable> _canTargetList;
    private Dictionary<IInteractable, float> _canTargetDict;
    private IInteractable _target;
    private KeyCode _dashKey = KeyCode.LeftShift;
    private bool _isDashKeyPressed => Input.GetKeyDown(_dashKey);

    // ------------------------------
    private void Awake() => CacheComponents();
    private void Start() => Init();
    private void Update()
    {
        ReadMove();
        ReadDash();
        Detect();
    }
    private void OnDrawGizmos()
    {
        if (_detectRange == null) return;

        Gizmos.color = Color.red;
        Gizmos.DrawRay(transform.position, transform.forward * _detectRange.Range);
        float angle = Mathf.Acos(THRESHOLD) * Mathf.Rad2Deg;

        Vector3 leftDir = Quaternion.Euler(0f, -angle, 0f) * transform.forward;
        Vector3 rightDir = Quaternion.Euler(0f, angle, 0f) * transform.forward;

        Gizmos.color = Color.yellow;
        Gizmos.DrawRay(transform.position, leftDir * _detectRange.Range);
        Gizmos.DrawRay(transform.position, rightDir * _detectRange.Range);
    }
    private void FixedUpdate()
    {
        _player.Move(_direction);
    }
    // ------------------------------

    private void ReadMove()
    {
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");

        _direction = new Vector3(horizontal, 0f, vertical).normalized;
    }

    private void ReadDash()
    {
        if (!_isDashKeyPressed) return;

        _player.Dash();
    }

    private void Detect()
    {
        foreach(IInteractable detected in _detecteds)
        {
            Vector3 playerDirection = transform.forward;
            Vector3 toDetectedDirection = (detected.GameObject.transform.position - transform.position);

            float lookPercentage = Vector3.Dot(toDetectedDirection, playerDirection);
            if(lookPercentage >= THRESHOLD)
            {
                _canTargetList.Add(detected);
                _canTargetDict.Add(detected, lookPercentage);
            }
        }

        IInteractable target = null;
        float targetLookPercentage = -1;
        for (int i = 0; i < _canTargetList.Count; i++)
        {
            if (_canTargetDict[_canTargetList[i]] > targetLookPercentage)
            {
                target = _canTargetList[i];
                targetLookPercentage = _canTargetDict[_canTargetList[i]];
            }
        }
        if (_target != target)
        {
            _target.Unselect();
            _target = target;
        }

        Vector3 toTargetDirection = (_target.GameObject.transform.position - transform.position);
        Ray ray = new Ray(transform.position, toTargetDirection);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, _detectRange.Range))
        {
            if(hit.transform.GetComponent<IInteractable>() == _target)
            {
                _target.Select();
            }
        }
    }

    private void CacheComponents()
    {
        _player = GetComponent<PlayerAction>();
        _detectRange = GetComponentInChildren<DetectRange>();
    }

    private void Init()
    {
        _canTargetList = new();
    }
}
