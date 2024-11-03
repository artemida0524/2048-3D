using System;
using UnityEngine;

public interface IThrowingPlatform
{
    bool CanDrag { get; set; }
    
    ObjectPool<ThrowableItemBase> ObjectPool { get; }

    public event Action OnTouch;
    public event Action OnThrow;
    public event Action<int> OnActivatedDeactivated;


    void BeginDrag();
    void Drag();
    void EndDrag();
}