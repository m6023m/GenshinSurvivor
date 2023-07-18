using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public enum Direction
{
    Vertical,
    Horizontal
}
public class BossMap : MonoBehaviour
{
    public float defalutDistance = 0;
    public Direction direction;
    public BossTrigger bossTrigger;
    Player player;
    public void Init()
    {
        player = GameManager.instance.player;
        bossTrigger.gameObject.SetActive(true);
        ResetPosition();
        SetEnable(true);
    }

    void ResetPosition()
    {
        Vector3 mapPosition = Vector3.zero;
        switch (direction)
        {
            case Direction.Vertical:
                if (player.playerVec.y >= 0)
                {
                    mapPosition = new Vector3(0, player.transform.position.y + defalutDistance);
                }
                else
                {
                    mapPosition = new Vector3(0, player.transform.position.y - defalutDistance);
                }
                break;
            case Direction.Horizontal:
                if (player.playerVec.x >= 0)
                {
                    mapPosition = new Vector3(player.transform.position.x + defalutDistance, 0);
                }
                else
                {
                    mapPosition = new Vector3(player.transform.position.x -defalutDistance, 0);
                }
                break;
        }

        transform.position = mapPosition;
    }

    public void SetEnable(bool enabled)
    {
        gameObject.SetActive(enabled);
    }
}
