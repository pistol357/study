using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] private MonsterStats _stats;
    [SerializeField] private int _id;

    private void Start()
    {
        _id = _stats.Id;

        Debug.Log(_id);
    }
}
