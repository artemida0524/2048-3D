using System.Collections.Generic;
using UnityEngine;

public class ObjectPool<T> where T : MonoBehaviour
{
    private T instance;
    private List<T> list;

    private Transform container;

    public ObjectPool(T instance, Transform container)
    {
        this.instance = instance;
        this.container = container;

        list = new List<T>();
    }

    public ObjectPool(T instance, Transform container, int count) : this(instance, container)
    {
        for (int i = 0; i < count; i++)
        {
            CreateItem();
        }
    }


    public T Get(bool active)
    {
        foreach (var item in list)
        {
            if (!item.gameObject.activeSelf)
            {
                item.gameObject.SetActive(active);
                return item;
            }
        }

        return CreateItem();

    }

    private T CreateItem()
    {
        Debug.Log("fwefwef");

        T item = Object.Instantiate(instance, container);

        item.gameObject.SetActive(false);

        Push(item);

        return item;
    }


    private void Push(T item)
    {
        list.Add(item);
    }
}
