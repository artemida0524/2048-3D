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
            CreateItem(false);
        }
    }


    public T Get(bool active)
    {
        foreach (var item in list)
        {
            try
            {
                if (!item.gameObject.activeSelf)
                {
                    item.gameObject.SetActive(active);
                    return item;
                }
            }
            catch (System.Exception)
            {
                continue;
            }
        }

        return CreateItem(active);

    }

    private T CreateItem(bool active)
    {

        T item = Object.Instantiate(instance, container);

        item.gameObject.SetActive(active);

        Push(item);

        return item;
    }

    private void Push(T item)
    {
        list.Add(item);
    }

    public void Remove(T itemRemove)
    {
        itemRemove.gameObject.SetActive(false);
    }

    public List<T> GetAll()
    {
        return list;
    }
}
