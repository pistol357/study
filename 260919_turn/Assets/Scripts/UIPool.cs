using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIPool : MonoBehaviour
{
    [SerializeField] private int _size;
    [SerializeField] private GameObject _uiPrefab;

    private IPoolable[] _pool;

    private void Awake()
    {
        SetSingleton();
        Init();
    }

    private void OnDestroy() => _pool = null;

    private static UIPool _instance;
    public static UIPool Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = FindObjectOfType<BattleManager>();
            }
            return _instance;
        }
    }

    public IPoolable Take()
    {
        IPoolable poolable = null;

        for(int i = 0; i < _size; i++)
        {
            if (!_pool[i].Tr.gameObject.activeSelf)
            {
                poolable = _pool[i];
                poolable.Tr.gameObject.SetActive(true);
            }
        }

        return poolable;
    }

    private void Init()
    {
        _pool = new IPoolable[_size];

        for(int i = 0; i < _pool.Length; i++)
        {
            _pool[i] = Instantiate(_uiPrefab).GetComponent<IPoolable>();
        }
    }

    private void SetSingleton()
    {
        if (_instance != null && _instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            _instance = this;
        }
    }
}
