using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Runtime.CompilerServices;
using UnityEngine;

public class CubeController : MonoBehaviour
{
    private Camera _cam;
    [SerializeField] private ISelectable _target;
    [SerializeField] private float _detectRange;

    private void Start()
    {
        _cam = Camera.main;
    }

    private void Update()
    {
        RayShot();
        MoveTarget();
        DetectMonster();
    }

    private void DetectMonster()
    {
        if (!Input.GetKeyDown(KeyCode.Alpha1)) return;

        Collider[] cols = Physics.OverlapSphere(transform.position, _detectRange);

        foreach(Collider col in cols)
        {
            if (col.gameObject.CompareTag("Monster"))
            {
                Debug.Log(col.gameObject.name);
            }
        }
    }

    private void MoveTarget()
    {
        if (!Input.GetMouseButtonDown(1) || _target == null || !(_target is IMoveable)) return;

        Ray ray = _cam.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        if(Physics.Raycast(ray, out hit))
        {
            if (hit.transform.CompareTag("Ground"))
            {
                (_target as IMoveable).SetDestination(hit.point);
            }
            else if(hit.transform.GetComponent<ISelectable>() != null)
            {
                (_target as IMoveable).SetDestination(hit.transform.position);
            }
            else
            {
                return;
            }
        }
    }

    private void RayShot()
    {
        if (!Input.GetMouseButtonDown(0)) return;

        Ray ray = _cam.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        if(Physics.Raycast(ray, out hit))
        {
            _target = hit.transform.GetComponent<ISelectable>();

            if (_target != null)
            {
                _target.Selected();
            }
        }
        else
        {
            _target = null;
        }
        return;
    }
}