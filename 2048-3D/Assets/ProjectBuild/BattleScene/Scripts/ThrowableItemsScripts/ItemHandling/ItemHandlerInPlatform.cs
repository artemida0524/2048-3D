using System;
using System.Collections;
using UnityEngine;

public class ItemHandlerInPlatform
{
    private readonly ThrowingPlatformBase platform;
    public ItemHandlerInPlatform(ThrowingPlatformBase platform)
    {
        this.platform = platform;
    }

    public ThrowableItemBase CreateNewItem(int probality, float impulse, Action<int> OnDetectSameItem)
    {

        int cubeNumber = ProbabilityGenerator.GenerateWithProbability(probality, 4, 2);

        ThrowableItemBase item = platform.ObjectPool.Get(true);

        item.InitializationDataItem(new ThrowableItemData()
        {
            impulse = impulse,
            number = cubeNumber
        }, OnDetectSameItem);

        return item;
    }

    public ThrowableItemBase AttachItemToTarget(ThrowableItemBase item, Transform itemTarget)
    {
        item.transform.parent = itemTarget;

        item.transform.localPosition = Vector3.zero;


        return item;
    }

    public void ThrowItem(ThrowableItemBase item, Vector3 direction)
    {
        item.Throw(direction);
    }


    public void ResetItemDetachAndResetItem(ThrowableItemBase item, Transform itemTarget)
    {
        item.transform.parent = null;

        itemTarget.localPosition = Vector3.zero;

    }


}
