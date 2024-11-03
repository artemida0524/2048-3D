using System;
using UnityEngine;


[RequireComponent(typeof(BoxCollider), typeof(MeshRenderer), typeof(MeshFilter))]
public abstract class WallBase : MonoBehaviour, IWall
{
    [field: NonSerialized] public BoxCollider BoxCollider { get; protected set; }
    [field: NonSerialized] public MeshRenderer MeshRenderer { get; protected set; }

    public event Action OnDetect;

    protected void Awake()
    {
        Initialization();
    }

    protected void OnCollisionEnter(Collision collision)
    {
        CheckItem(collision);
    }

    protected void Initialization()
    {
        BoxCollider = GetComponent<BoxCollider>();
        MeshRenderer = GetComponent<MeshRenderer>();
    }

    //change
    protected virtual void CheckItem(Collision collision)
    {
        if (collision.gameObject.TryGetComponent(out ThrowableItemBase throwingItem))
        {
            OnDetect?.Invoke();
        }
    }

}
