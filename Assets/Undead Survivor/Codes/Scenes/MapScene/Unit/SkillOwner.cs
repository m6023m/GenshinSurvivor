using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
public class SkillOwner : MonoBehaviour
{
    public float health;
    public Character stat;
    public bool isLive = true;
    public float maxHealth;
    public bool noDamage;
    public Scanner _scanner;
    public Scanner scanner
    {
        get
        {
            if (_scanner == null) _scanner = GetComponent<Scanner>();
            return _scanner;
        }
    }
    private void Awake()
    {
        Init(GameDataManager.instance.saveData.userData.selectChars[0]);
    }
    public void Init(Character stat)
    {
        this.stat = JsonUtility.FromJson<Character>(JsonUtility.ToJson(stat));

        isLive = true;
        noDamage = false;
        health = stat.hp;
        maxHealth = stat.hp;
    }

    public void InitImmune(Character stat)
    {
        Init(stat);
        health = 9999999;
        maxHealth = 9999999;
    }

    protected virtual bool CheckIsAlive()
    {
        if (health > 0)
        {// 아직 살아있으면
            return true;
        }
        else if (isLive)
        {//죽었으면
            return false;
        }
        return true;
    }

    public virtual void OnDead()
    {
        isLive = false;
    }

    public void Hit(float damage, float noDamageTime)
    {
        if (noDamage) return;
        health -= damage;
        noDamage = true;
        StartCoroutine(receiveDamage(damage, noDamageTime));
    }

    IEnumerator receiveDamage(float damage, float noDamageTime)
    {
        yield return new WaitForSecondsRealtime(noDamageTime);
        noDamage = false;
    }
}
