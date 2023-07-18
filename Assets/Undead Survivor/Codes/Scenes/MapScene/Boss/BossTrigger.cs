using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossTrigger : MonoBehaviour
{
    public Spawner spawner;
    public GameObject bossWall;
    public GameObject[] normalWalls;
    public bool isTriggered
    {
        get
        {
            return !gameObject.activeSelf;
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!gameObject.activeSelf) return;
        if (collision.CompareTag("Player"))
        {
            RemoveNormalWall();
            SpwanBoss();
            gameObject.SetActive(false);
        }
    }
    void RemoveNormalWall()
    {
        EnableNormalWall(false);
    }

    void SpwanBoss()
    {
        if (spawner == null) return;
        spawner.SpawnBoss();
    }

    public void EnableNormalWall(bool enabled)
    {
        if (normalWalls == null) return;
        if (normalWalls.Length == 0) return;
        foreach (GameObject obj in normalWalls)
        {
            obj.SetActive(enabled);
        }
        if (bossWall == null) return;
        bossWall.SetActive(!enabled);
    }
}
