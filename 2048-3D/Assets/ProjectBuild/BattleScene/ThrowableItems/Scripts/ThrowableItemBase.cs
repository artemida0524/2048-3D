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

                        itemView.View(dataItem.number);

                        OnDetectSameItem?.Invoke(dataItem.number);

                        DisableItem(item);
                    }
                }
            }
        }
    }

    private void OnDisable()
    {
        OnDetectSameItem = null;
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

        EnabledComponents(false);
    }

    public void InitializationDataItem(ThrowableItemData data, Action<int> OnDetectSameItem)
    {
        EnabledComponents(false);

        this.OnDetectSameItem = OnDetectSameItem;
        dataItem = data;
        dataItemInteraction = new ThrowableItemDataInteraction(dataItem);

        itemView.View(data.number);

    }

    protected virtual void EnabledComponents(bool active)
    {
        Rigidbody.isKinematic = !active;
    }
}
