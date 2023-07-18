using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class Reposition : MonoBehaviour
{
    Collider2D coll;
    Tilemap tilemap;
    int tileCellCountX = 0;
    int tileCellCountY = 0;
    private void Awake()
    {
        coll = GetComponent<Collider2D>();
        if (GetComponentInParent<Tilemap>() != null)
        {
            tilemap = GetComponentInParent<Tilemap>();
            tileCellCountX = tilemap.cellBounds.size.x;
            tileCellCountY = tilemap.cellBounds.size.y;
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (!collision.CompareTag("Area")) return;

        Vector3 playerPos = GameManager.instance.player.transform.position;
        Vector3 myPosition = transform.position;

        switch (transform.tag)
        {
            case "Ground":
                float diffX = playerPos.x - myPosition.x;
                float diffY = playerPos.y - myPosition.y;
                float dirX = diffX < 0 ? -1 : 1;
                float dirY = diffY < 0 ? -1 : 1;
                diffX = Mathf.Abs(diffX);
                diffY = Mathf.Abs(diffY);

                if (diffX > diffY)
                {
                    tilemap.transform.Translate(Vector3.right * dirX * tileCellCountX * 2);
                }
                else if (diffY > diffX)
                {
                    tilemap.transform.Translate(Vector3.up * dirY * tileCellCountY * 2);
                }
                break;
            case "Enemy":
                if (coll.enabled)
                {
                    Vector3 dist = playerPos - myPosition;
                    Vector3 ran = new Vector3(Random.Range(-3, 3), Random.Range(-3, 3));
                    transform.position = ran + dist * 2;
                }
                break;
        }
    }
}
