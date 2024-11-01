using System;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public abstract class ThrowableItemBase : MonoBehaviour
{
    public Rigidbody Rigidbody{ get; protected set; }


    private void Awake()
    {
        Initialization();
    }


    protected virtual void Initialization()
    {
        Rigidbody = GetComponent<Rigidbody>();
    }

}
