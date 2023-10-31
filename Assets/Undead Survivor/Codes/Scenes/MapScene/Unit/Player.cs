using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;
using System;
using Rewired;
public class Player : SkillOwner
{
    public Vector3 inputVec;
    public Vector3 playerVec;
    private Vector2 moveVector;
    public Vector3 vec;
    public PlayerHit playerHit;
    public int rotation;
    public Rigidbody2D rigid;
    public float speed;
    bool isMove = false;
    public SpriteRenderer spriter;
    public Animator animator;
    public Collider2D coll;
    Rewired.Player rewiredPlayer;
    public RuntimeAnimatorController[] animCon;
    Dictionary<SkillName, Sheild> _sheilds;
    public Dictionary<SkillName, Sheild> sheilds
    {
        get
        {
            if (_sheilds == null)
            {
                _sheilds = new Dictionary<SkillName, Sheild>();
            }
            return _sheilds;
        }
    }
    public ElementReactionObject elementReactionObject;
    public CharacterData characterData;
    public Weapon[] weapons;
    private Queue<Func<float>> hitQueue;
    List<UnityAction<float>> _onDamaged;
    public DamageAttach moraAttach;
    public List<UnityAction<float>> onDamaged
    {
        get
        {
            if (_onDamaged == null) _onDamaged = new List<UnityAction<float>>();
            return _onDamaged;
        }
    }

    void Awake()
    {
        rigid = GetComponent<Rigidbody2D>();
        spriter = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();
        coll = GetComponent<Collider2D>();
        stat = GameDataManager.instance.saveData.userData.selectChars[0];
        hitQueue = new Queue<Func<float>>();
        elementReactionObject = GetComponentInChildren<ElementReactionObject>();
        weapons = GetComponentsInChildren<Weapon>();
        moraAttach = GetComponentInChildren<DamageAttach>();
        rewiredPlayer = ReInput.players.GetPlayer(0);
        Init();
    }

    void Init()
    {
        if (stat.Name() == GameDataManager.instance.saveData.charactors[0].Name())//기본캐릭터일 경우
        {
            animator.runtimeAnimatorController = animCon[GameDataManager.instance.saveData.userData.defaultChar];
        }
        else
        {
            animator.runtimeAnimatorController = characterData.GetAnim(stat.charNum);
        }

        Init(stat);
        coll.enabled = true;
        rigid.simulated = true;
    }

    public void ResetHealth()
    {
        health = GameManager.instance.statCalcuator.Health;
        maxHealth = GameManager.instance.statCalcuator.Health;
    }

    private void Start()
    {
        InitWeapon();
        ResetHealth();
    }
    void InitWeapon()
    {
        int index = 0;
        foreach (Character character in GameDataManager.instance.saveData.userData.selectChars)
        {
            if (character == null)
            {
                index++;
                continue;
            }
            weapons[index].InitWeapon(GameManager.instance.weaponData.Get(character.weaponName).weaponType);
            index++;
        }

    }

    void FixedUpdate()
    {
        GetInput();
        ProcessInput();
        Vector2 nextVec = inputVec.normalized * GameManager.instance.statCalcuator.Speed * Time.fixedDeltaTime;
        rigid.MovePosition(rigid.position + nextVec);


        if (!CheckIsAlive())
        {
            OnDead();
        }
    }

    private void GetInput()
    {
        moveVector.x = rewiredPlayer.GetAxis("UIHorizontal");
        moveVector.y = rewiredPlayer.GetAxis("UIVertical");
    }

    private void ProcessInput()
    {
        if (moveVector != Vector2.zero)
        {
            OnMove(moveVector);
            isMove = true;
        }
        if (isMove && moveVector == Vector2.zero)
        {
            OnMove(moveVector);
            isMove = false;
        }
    }
    protected override bool CheckIsAlive()
    {

        if (GameManager.instance.statBuff.hutaoConstell5)
        {
            if (health / GameManager.instance.statCalcuator.Health <= 0.25f)
            {
                health = 1;
                ReceiveDamage(0, 10.0f);
                GameManager.instance.statBuff.allCritical = true;
                GameManager.instance.statBuff.hutaoConstell5 = false;
                return false;
            }
        }
        return base.CheckIsAlive();
    }
    void OnMove(InputValue value)
    {
        inputVec = value.Get<Vector2>();
        vec = value.Get<Vector2>();
        SetDir();
    }
    private void OnMove(Vector2 inputVec)
    {
        this.inputVec = inputVec;
        vec = inputVec;
        SetDir();
    }

    public void SetDir()
    {
        if (inputVec != Vector3.zero)
        {
            Vector3 dir = inputVec;
            playerVec = dir;
            rotation = (int)Quaternion.FromToRotation(Vector3.up, inputVec).eulerAngles.z;
            rotation -= (rotation % 45);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
    }
    private void LateUpdate()
    {
        animator.SetFloat("Speed", inputVec.magnitude);

        if (inputVec.x != 0)
        {
            spriter.flipX = inputVec.x < 0;
        }
    }

    public void Surrender()
    {
        GameManager.instance.gameInfoData.rebirth = (int)GameManager.instance.statCalcuator.Rebirth;
        OnDead();
    }

    public override void OnDead()
    {
        base.OnDead();
        animator.SetTrigger("Dead");
    }

    private void Dead()
    {
        GameDataManager.instance.saveData.record.deadCount++;
        if (GameManager.instance.gameInfoData.rebirth < (int)GameManager.instance.statCalcuator.Rebirth)
        {
            Init();
            ResetHealth();
            GameManager.instance.gameInfoData.rebirth++;
            animator.SetTrigger("Rebirth");
            Hit(0, 3);
            GameDataManager.instance.saveData.record.rebirthCount++;
        }
        else
        {
            GameManager.instance.Defeat();
        }
    }

    public void Hit(float damage, float noDamageTime, Enemy enemy)
    {
        if (noDamage) return;
        if (CheckSheild(damage, noDamageTime, enemy)) return;
        if (CheckHitQueue(damage, noDamageTime)) return;
        float damageResult = CalcDamage(damage);
        ReceiveDamage(damageResult, noDamageTime);
        playerHit.PlayHitAnimation();
    }
    bool CheckSheild(float damage, float noDamageTime, Enemy enemy)
    {
        bool result = false;
        foreach (KeyValuePair<SkillName, Sheild> keyValue in sheilds)
        {
            Sheild sheild = keyValue.Value;
            if (sheild != null)
            {
                if (sheild.gameObject.activeSelf)
                {
                    sheild.ReceiveDamage(damage, enemy);
                    result = true;
                }
            }
        }
        return result;
    }

    bool CheckHitQueue(float damage, float noDamageTime)
    {
        if (hitQueue.Count > 0)
        {
            float damageResult = damage * hitQueue.Dequeue().Invoke();
            ReceiveDamage(CalcDamage(damage), noDamageTime);
            return true;
        }
        return false;
    }


    public float CalcDamage(float damage)
    {
        float armor = stat.armor + GameManager.instance.artifactData.Armor;
        armor = armor + (armor * GameManager.instance.artifactData.ArmorMultiplier);
        float armorMultiplier = 4 / (5 + armor);
        float damageResult = damage * armorMultiplier
         * (1.0f + GameManager.instance.statBuff.Claymore_Serpent_SpineStack
         * GameManager.instance.statBuff.Claymore_Serpent_SpineReceiveDamage);
        if (damageResult <= 0) damageResult = 0.2f;

        return damageResult;

    }


    public void ReceiveDamage(float damage, float noDamageTime)
    {
        health -= damage;
        OnReceiveDamage(damage);
        noDamage = true;
        StartCoroutine(NoDamage(noDamageTime));
    }

    void OnReceiveDamage(float damage)
    {
        GameManager.instance.battleResult.receiveDamage += damage;
        GameDataManager.instance.saveData.record.receiveTotalDamage += damage;
        foreach (UnityAction<float> action in onDamaged)
        {
            action.Invoke(damage);
        }
    }

    public void HealHealth(float value)
    {
        float maxHealth = GameManager.instance.statCalcuator.Health;
        float healResult = value + (value * GameManager.instance.statCalcuator.HealBonus);

        health += healResult;
        GameManager.instance.battleResult.healHealth += healResult;
        GameDataManager.instance.saveData.record.healTotalHealth += healResult;

        if (health > maxHealth) health = maxHealth;

        AudioManager.instance.PlaySFX(AudioManager.SFX.Heal);
    }
    IEnumerator NoDamage(float noDamageTime)
    {
        if (noDamageTime > 0f)
        {
            yield return new WaitForSecondsRealtime(noDamageTime);
            noDamage = false;
        }
        else
        {
            noDamage = false;
            yield return null;
        }
    }

    public void AddHitQueue(Func<float> action)
    {
        hitQueue.Enqueue(action);
    }

    public void AddSheild(Sheild sheild)
    {
        sheilds.AddOrUpdate(sheild.parameterWithKey.name, sheild);
    }

    public void RemoveSheild(Sheild sheild)
    {
        sheilds.Remove(sheild.parameterWithKey.name);
    }
}
