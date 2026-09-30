using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    const int HAND_CAPACITY = 3;

    private PlayerAction _player;
    private Vector3 _direction;
    private GameObject _pickedItem;
    private int _pickedQuantity;
    private DetectRange _detectRange;

    private KeyCode _dashKey = KeyCode.LeftShift;
    private bool _isDashKeyPressed => Input.GetKeyDown(_dashKey);

    // ------------------------------
    private void Awake() => CacheComponents();
    private void Update()
    {
        ReadMove();
        ReadDash();
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
        
    }

    private void CacheComponents()
    {
        _player = GetComponent<PlayerAction>();
        _detectRange = GetComponentInChildren<DetectRange>();
    }
}
