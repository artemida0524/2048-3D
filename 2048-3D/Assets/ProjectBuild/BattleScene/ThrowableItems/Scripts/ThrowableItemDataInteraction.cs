

using UnityEngine;

public class ThrowableItemDataInteraction
{
    private readonly ThrowableItemData data;
    public ThrowableItemDataInteraction(ThrowableItemData data)
    {
        this.data = data;
    }

    public void LevelUp()
    {
        data.number *= 2;

        

    }
}