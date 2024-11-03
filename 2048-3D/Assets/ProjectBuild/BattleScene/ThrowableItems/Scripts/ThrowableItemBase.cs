using System;
using UnityEngine;

[SelectionBase, RequireComponent(typeof(Rigidbody))]
public abstract class ThrowableItemBase : MonoBehaviour
{
    public Rigidbody Rigidbody { get; protected set; }

    [SerializeField] protected ThrowableItemView itemView;
    protected ThrowableItemData dataItem;
    protected ThrowableItemDataInteraction dataItemInteraction;

    public event Action<int> OnDetectSameItem;
    public event Action<int> OnActivatedDeactivated;

    private void Awake()
    {
        Initialization();
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.TryGetComponent(out ThrowableItemBase item))
        {
            if (this.dataItem.number == item.dataItem.number)
            {
                if (collision.impulse.magnitude > dataItem.impulse)
                {
                    if (this.Rigidbody.velocity.magnitude > item.Rigidbody.velocity.magnitude)
                    {
                        dataItemInteraction.LevelUp();

                        OnDetectSameItem?.Invoke(dataItem.number);
                        OnActivatedDeactivated?.Invoke(dataItem.number);
                        

                        DisableItem(item);
                    }
                }
            }
        }
    }

    private void OnDisable()
    {
        OnActivatedDeactivated = null;
    }

    private void DisableItem(ThrowableItemBase item)
    {
        item.gameObject.SetActive(false);
        item.EnabledComponents(false);
        item.transform.rotation = Quaternion.Euler(0, 0, 0);
    }

    public void Throw(Vector3 velocity)
    {
        EnabledComponents(true);
        Rigidbody.AddForce(velocity);
    }

    protected virtual void Initialization()
    {
        Rigidbody = GetComponent<Rigidbody>();
        OnDetectSameItem += itemView.View;

        EnabledComponents(false);
    }

    public void InitializationDataItem(ThrowableItemData data, Action<int> OnActivatedDeactivated)
    {
        EnabledComponents(false);

        this.OnActivatedDeactivated = OnActivatedDeactivated;
        dataItem = data;
        dataItemInteraction = new ThrowableItemDataInteraction(dataItem);

        itemView.View(data.number);

    }

    protected virtual void EnabledComponents(bool active)
    {
        Rigidbody.isKinematic = !active;
    }
}
