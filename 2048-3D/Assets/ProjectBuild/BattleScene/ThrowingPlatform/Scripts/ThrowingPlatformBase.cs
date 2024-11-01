using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class ThrowingPlatformBase : MonoBehaviour
{
    [Min(0)] public float widthZone;

    [SerializeField] protected ThrowableItemBase itemInstance;
    [SerializeField] protected Transform container;

    protected ObjectPool<ThrowableItemBase> ObjectPool { get; set; }


    private void Awake()
    {
        Initialization();
    }


    private void OnDrawGizmos/*Selected*/()
    {
        Gizmos.color = Color.red;

        Vector3 position = transform.position;

        Gizmos.DrawSphere(new Vector3(position.x + widthZone, position.y, position.z), .4f);
        Gizmos.DrawSphere(new Vector3(position.x - widthZone, position.y, position.z), .4f);
    }

    protected virtual void Initialization()
    {
        ObjectPool = new ObjectPool<ThrowableItemBase>(itemInstance, container, 10);
    }

}
