using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoolManager : MonoBehaviour
{
    public enum Type
    {
        Enemy,
        Skill,
        Damage,
        SkillObject,
        Summon,
        SkillIcon,
        ElementalSphere,
        DropItem,
        ArtifactIcon,
        MapObject,
        Buff,
        Sheild,
        EnemyAttack,
        Crystalize,
        DvalinCrackArea,
        SkillEffect
    }
    public GameObject[] prefabs;
    List<GameObject>[] pools;
    public List<GameObject> enemys;
    public List<GameObject> elementalSpheres;

    void Awake()
    {
        pools = new List<GameObject>[prefabs.Length];

        for (int i = 0; i < pools.Length; i++)
        {
            pools[i] = new List<GameObject>();
        }
        pools[(int)Type.Enemy] = enemys;
        pools[(int)Type.ElementalSphere] = elementalSpheres;
    }

    public GameObject Get(Type type)
    {
        GameObject select = null;

        foreach (GameObject item in pools[(int)type])
        {
            if (item != null && !item.activeSelf)
            {
                select = item;
                select.SetActive(true);
                break;
            }
        }
        if (!select)
        {
            select = Instantiate(prefabs[(int)type], transform);
            pools[(int)type].Add(select);
        }

        return select;
    }


    public GameObject Get(Type type, List<GameObject> parents)
    {
        GameObject select = null;
        foreach (GameObject item in parents)
        {
            if (item != null && !item.activeSelf && CheckType(item, type))
            {
                select = item;
                select.SetActive(true);
                break;
            }
        }
        if (!select)
        {
            select = Instantiate(prefabs[(int)type], transform);
            parents.Add(select);
        }

        return select;
    }

    bool CheckType(GameObject item, Type type)
    {
        bool result = false;
        switch (type)
        {
            case Type.Skill:
                result = item.GetComponentInChildren<Skill>(true) != null;
                break;
            case Type.Buff:
                result = item.GetComponentInChildren<Buff>(true) != null;
                break;
            case Type.Sheild:
                result = item.GetComponentInChildren<Sheild>(true) != null;
                break;
            case Type.Summon:
                result = item.GetComponentInChildren<Summon>(true) != null;
                break;
            case Type.SkillEffect:
                result = item.GetComponentInChildren<SkillEffect>(true) != null;
                break;
        }
        return result;
    }
}
