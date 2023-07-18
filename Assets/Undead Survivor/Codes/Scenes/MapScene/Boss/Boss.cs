using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Boss : Enemy
{
    public bool isEnable = false;
    protected Transform playerTransform;
    public enum BossName
    {
        Dvalin,
        Andrius,
        Tartaglia0,
        Tartaglia1,
        Tartaglia2
    }
    public BossName bossName;


    public void OnHit()
    {
        LiveCheck();
    }

    public virtual void InitBoss(SpawnData spawnData)
    {
        Enable();
        base.Init(spawnData);
        isLive = true;
        playerTransform = GameManager.instance.player.transform;
    }

    protected override void LiveCheck()
    {
        if (!isLive) return;
        if (health >= 0)
        {
            AudioManager.instance.PlaySFX(AudioManager.SFX.Hit);
        }
        else
        {
            isLive = false;
            GameManager.instance.gameInfoData.kill++;
            GameManager.instance.battleResult.kill++;
            GameDataManager.instance.saveData.record.killBossCount++;
            GameManager.instance.GetExp(exp);
            DropQniqueBox();
            AudioManager.instance.PlaySFX(AudioManager.SFX.Dead);
            GameManager.instance.gameInfoData.gameLevel++;
            Disable();
        }
    }


    void DropQniqueBox()
    {
        GameObject dropItem = GameManager.instance.poolManager.Get(PoolManager.Type.DropItem);
        dropItem.transform.position = gameObject.transform.position;
        DropItem drop = dropItem.GetComponent<DropItem>();
        drop.Init(DropItem.Name.Box_Unique);
    }

    public void Enable()
    {
        isEnable = true;
        gameObject.SetActive(true);
    }

    public virtual void Disable()
    {
        isEnable = false;
        gameObject.SetActive(false);
    }

    public virtual void ResetPosition()
    { 
        transform.position = playerTransform.position + new Vector3(0, 10.0f);
        LookPlayer();
    }

    public Tweener ResetPositionDoTween()
    {
        Vector3 targetPosition = playerTransform.position + new Vector3(0, 6.0f);

        Vector2 direction = transform.CalcTarget(targetPosition);
        transform.rotation = Quaternion.FromToRotation(Vector3.up, direction);
        return transform.DOMove(targetPosition, 4.0f).SetEase(Ease.Linear).OnComplete(() =>
        {
            LookPlayer();
        });
    }


    protected void LookPlayer()
    {
        Vector2 direction = transform.CalcTarget(playerTransform);
        transform.rotation = Quaternion.FromToRotation(Vector3.up, direction);
    }

}