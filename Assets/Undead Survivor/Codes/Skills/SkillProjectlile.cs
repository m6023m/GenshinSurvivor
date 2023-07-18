using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillProjectlile : MonoBehaviour
{

    public int penetrate = -1;
    void Awake()
    {
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (!collision.CompareTag("Enemy") || penetrate == -1) return;

        penetrate--;

        if (penetrate == -1)
        {
            gameObject.SetActive(false);
        }
    }
}