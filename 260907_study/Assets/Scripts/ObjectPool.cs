using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class ObjectPool : MonoBehaviour
{
    [SerializeField] GameObject _grenadePrefab;
    [field: SerializeField] public int Size { get; private set; }
    private IPoolable[] _pool;
    private int _count;
    public int Count
    {
        get
        {
            return _count;
        }

        set
        {
            _count = value;
            Debug.Log($"Count 변경됨 : {value}");
        }
    }

    public bool IsEmpty => Count == 0;

    private void Start() => Init();

    public IPoolable Take()
    {
        if (IsEmpty) return null;

        Count--;
        IPoolable poolable = _pool[Count];
        _pool[Count] = null;

        return poolable;
    }

    public void Return(IPoolable poolable)
    {
        if (Size <= Count) return;

        _pool[Count] = poolable;
        poolable.tr.gameObject.SetActive(false);
        Count++;
    }

    private void Init()
    {
        _pool = new IPoolable[Size];

        for(int i = 0; i < _pool.Length; i++)
        {
            GameObject go = Instantiate(_grenadePrefab);
            _pool[i] = go.GetComponent<IPoolable>();
            _pool[i].Pool = this;
            go.SetActive(false);
        }

        Count = Size;
    }
}
