using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class PoolManager : MonoBehaviour
{
    public PoolingObject[] poolingObjects;
    List<PoolingObject> poolList;

    void Awake()
    {
        poolList = new List<PoolingObject>();
    }


    public T GetObject<T>() where T : PoolingObject
    {
        T select = null;

        foreach (T item in poolList.OfType<T>())
        {
            if (item != null && !item.gameObject.activeSelf)
            {
                select = item;
                select.gameObject.SetActive(true);
                break;
            }
        }
        if (!select)
        {
            PoolingObject prefab = poolingObjects.FirstOrDefault(p => p is T);
            select = (T)Instantiate(prefab, transform);
            poolList.Add(select);
        }

        return select;
    }
}
