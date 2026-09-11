using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemBox : MonoBehaviour, IInteractable
{
    [SerializeField] private float _itemTime;
    [SerializeField] private float _speedBonus;
    [SerializeField] private float _cooldownBonus;

    public GameObject GameObject { get => gameObject; }
    private Outline _outline;

    public float ItemTime
    {
        get
        {
            return _itemTime;
        }

        set
        {
            _itemTime = value;
        }
    }

    public float SpeedBonus
    {
        get
        {
            return _speedBonus;
        }
    }

    public float CooldownBonus
    {
        get
        {
            return _cooldownBonus;
        }
    }

    private void Awake()
    {
        CacheComponents();
    }

    private void Start()
    {
        Init();
    }

    public void Targeting()
    {
        _outline.enabled = true;
    }

    public void Untargeting()
    {
        _outline.enabled = false;
    }

    public void Interact(IInteractor owner)
    {
        if (!(owner is PlayerController)) return;

        PlayerController player = (PlayerController)owner;
        player.UseItem(this);

        Destroy(gameObject);
    }

    private void Init()
    {
        _outline.enabled = false;
    }

    private void CacheComponents()
    {
        _outline = gameObject.GetComponent<Outline>();
    }
}
