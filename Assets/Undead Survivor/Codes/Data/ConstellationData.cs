using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class ConstellationData
{
    Player player
    {
        get
        {
            return GameManager.instance.player;
        }
    }
    SkillData skillData
    {
        get
        {
            return GameManager.instance.skillData;
        }
    }

    CharacterData characterData
    {
        get
        {
            return GameManager.instance.characterData;
        }
    }
    StatBuff statBuff;
    public void InitConstellation(Character character)
    {
        statBuff = GameManager.instance.statBuff;
        if (character == null) return;
        switch (character.charNum)
        {
            case CharacterData.Name.Travler_MALE:
            case CharacterData.Name.Travler_FEMALE:
                Travler(character);
                break;
            case CharacterData.Name.Amber:
                Amber(character);
                break;
            case CharacterData.Name.Xiangling:
                Xiangling(character);
                break;
            case CharacterData.Name.Bennet:
                Bennet(character);
                break;
            case CharacterData.Name.Diluc:
                Diluc(character);
                break;
            case CharacterData.Name.Klee:
                Klee(character);
                break;
            case CharacterData.Name.Barbara:
                Barbara(character);
                break;
            case CharacterData.Name.Xingqiu:
                Xingqiu(character);
                break;
            case CharacterData.Name.Mona:
                Mona(character);
                break;
            case CharacterData.Name.Sucrose:
                Sucrose(character);
                break;
            case CharacterData.Name.Jean:
                Jean(character);
                break;
            case CharacterData.Name.Venti:
                Venti(character);
                break;
            case CharacterData.Name.Lisa:
                Lisa(character);
                break;
            case CharacterData.Name.Razor:
                Razor(character);
                break;
            case CharacterData.Name.Beidou:
                Beidou(character);
                break;
            case CharacterData.Name.Fischl:
                Fischl(character);
                break;
            case CharacterData.Name.Keqing:
                Keqing(character);
                break;
            case CharacterData.Name.Kaeya:
                Kaeya(character);
                break;
            case CharacterData.Name.Chongyun:
                Chongyun(character);
                break;
            case CharacterData.Name.Qiqi:
                Qiqi(character);
                break;
            case CharacterData.Name.Ningguang:
                Ningguang(character);
                break;
            case CharacterData.Name.Noelle:
                Noelle(character);
                break;
            case CharacterData.Name.Tartaglia:
                Tartaglia(character);
                break;
            case CharacterData.Name.Diona:
                Diona(character);
                break;
            case CharacterData.Name.Zhongli:
                Zhongli(character);
                break;
            case CharacterData.Name.Xinyan:
                Xinyan(character);
                break;
            case CharacterData.Name.Albedo:
                Albedo(character);
                break;
            case CharacterData.Name.Ganyu:
                Ganyu(character);
                break;
            case CharacterData.Name.Xiao:
                Xiao(character);
                break;
            case CharacterData.Name.Hutao:
                Hutao(character);
                break;
            case CharacterData.Name.Rosaria:
                Rosaria(character);
                break;
            case CharacterData.Name.Yanfei:
                Yanfei(character);
                break;
            case CharacterData.Name.Eula:
                Eula(character);
                break;
            case CharacterData.Name.Kazuha:
                Kazuha(character);
                break;
            case CharacterData.Name.Ayaka:
                Ayaka(character);
                break;
            case CharacterData.Name.Yoimiya:
                Yoimiya(character);
                break;
            case CharacterData.Name.Sayu:
                Sayu(character);
                break;
            case CharacterData.Name.Raiden:
                Raiden(character);
                break;
            case CharacterData.Name.Sara:
                Sara(character);
                break;
            case CharacterData.Name.Kokomi:
                Kokomi(character);
                break;
            case CharacterData.Name.Ito:
                Ito(character);
                break;
            case CharacterData.Name.Goro:
                Goro(character);
                break;
            case CharacterData.Name.Thoma:
                Thoma(character);
                break;
            case CharacterData.Name.Shenhe:
                Shenhe(character);
                break;
            case CharacterData.Name.Yunjin:
                Yunjin(character);
                break;
            case CharacterData.Name.Miko:
                Miko(character);
                break;
            case CharacterData.Name.Ayato:
                Ayato(character);
                break;
            case CharacterData.Name.Yelan:
                Yelan(character);
                break;
            case CharacterData.Name.Heizo:
                Heizo(character);
                break;
            case CharacterData.Name.Shinobu:
                Shinobu(character);
                break;
        }
    }



    void Travler(Character character)
    {
        switch (character.elementType)
        {
            case Element.Type.Anemo:
                Travler_Anemo(character);
                break;
            case Element.Type.Geo:
                Travler_Geo(character);
                break;
            case Element.Type.Electro:
                Travler_Electro(character);
                break;
        }
    }

    void Travler_Anemo(Character character)
    {
        SkillData.ParameterWithKey baseAttack = GameManager.instance.baseAttack.parameterWithKey;
        SkillData.ParameterWithKey skill = skillData.skills[SkillName.E_Travler_Anemo];
        SkillData.ParameterWithKey burst = skillData.skills[SkillName.EB_Travler_Anemo];
        if (character.constellation[0])
        { //회오리검의 끌어당기는 범위가 20% 증가
            skill.parameter.magnet *= 1.5f;
        }
        if (character.constellation[1])
        { //원소 충전 효율이 20% 증가
            statBuff.Regen += 0.2f;
        }
        if (character.constellation[2])
        { //격동의 바람의 대미지 20% 증가
            burst.parameter.damage *= 1.2f;
        }
        if (character.constellation[3])
        { //회오리검을 시전하는 동안 방어력이 2증가
            skill.AddStartListener(() =>
            {
                statBuff.Armor += 2;
            });
            skill.AddEndListener(() =>
            {
                statBuff.Armor -= 2;
            });
        }
        if (character.constellation[4])
        { //회오리검의 대미지 20% 증가
            skill.parameter.damage *= 1.2f;
        }
        if (character.constellation[5])
        { //격동의 바람에 피격된 적은 바람 원소 내성이 20% 감소
            burst.parameter.isDebuffable = true;
        }
    }

    void Travler_Geo(Character character)
    {
        SkillData.ParameterWithKey baseAttack = GameManager.instance.baseAttack.parameterWithKey;
        SkillData.ParameterWithKey skill = skillData.skills[SkillName.E_Travler_Geo];
        SkillData.ParameterWithKey burst = skillData.skills[SkillName.EB_Travler_Geo];
        if (character.constellation[0])
        { // 첩첩산중의 돌벽 안쪽에서 행운 3 증가
            skill.constellations.num0 = true;
        }
        if (character.constellation[1])
        { // 성운검의 재사용 대기시간 20% 감소
            skill.parameter.coolTime *= 0.8f;
        }
        if (character.constellation[2])
        { // 첩첩산중 대미지 20% 증가
            burst.parameter.damage *= 1.2f;
        }
        if (character.constellation[3])
        { // 첩첩산중의 최대 원소 에너지 20 감소
            burst.parameter.elementGaugeMax -= 20;
        }
        if (character.constellation[4])
        { // 성운검 대미지 20% 증가
            skill.parameter.damage *= 1.2f;
        }
        if (character.constellation[5])
        { // 첩첩산중과 성운검의 지속시간 30% 증가
            skill.parameter.duration *= 1.3f;
            burst.parameter.duration *= 1.3f;
        }
    }


    private void Travler_Electro(Character character)
    {
        SkillData.ParameterWithKey baseAttack = GameManager.instance.baseAttack.parameterWithKey;
        SkillData.ParameterWithKey skill = skillData.skills[SkillName.E_Travler_Electro];
        SkillData.ParameterWithKey burst = skillData.skills[SkillName.EB_Travler_Electro];
        skill.AddStartListener(() =>
        {
            int regen = 0;
            if (character.constellation[3])
            { // 원소폭발의 원소에너지 합계가 35% 이하일 경우 뇌영검의 원소에너지 회복이 5 증가
                float burstGaugeMax = 0;
                float burstGauge = 0;
                foreach (KeyValuePair<SkillName, SkillObject> skills in GameManager.instance.ownBursts)
                {
                    SkillData.ParameterWithKey param = skills.Value.parameterWithKey;
                    burstGauge += param.parameter.elementGauge;
                    burstGaugeMax += param.parameter.elementGaugeMax;
                }
                if (burstGauge / burstGaugeMax >= 0.35f)
                {
                    regen = 5;
                }
            }
            GameManager.instance.AddElementGauge(10 + regen);
            if (character.constellation[0])
            { // 뇌영검 발동 시 회복되는 원소 에너지가 두배가 됨
                GameManager.instance.AddElementGauge(10 + regen);
            }
        });

        burst.AddHitListener((colider2D) =>
        {
            GameManager.instance.AddElementGauge(0.1f);
        });
        if (character.constellation[0])
        { // 뇌영검 발동 시 회복되는 원소 에너지가 두배가 됨
            statBuff.E_Travler_Electro = 2;
        }
        if (character.constellation[1])
        { // 요동치는 우렛소리가 명중하면 적의 번개 원소 내성이 15% 감소. 지속 8초
            skill.parameter.isDebuffable = true;
            skill.constellations.num1 = true;
            //Enemy.cs
        }
        if (character.constellation[2])
        { // 요동치는 우렛소리의 대미지 20% 증가
            burst.parameter.damage *= 1.2f;
        }
        if (character.constellation[4])
        { // 뇌영검의 대미지 20% 증가
            skill.parameter.damage *= 1.2f;
        }
        if (character.constellation[5])
        { // 요동치는 우렛소리의 개수 2 증가
            burst.parameter.count += 2;
        }
    }

    void Amber(Character character)
    {
        SkillData.ParameterWithKey baseAttack = GameManager.instance.baseAttack.parameterWithKey;
        SkillData.ParameterWithKey skill = skillData.skills[SkillName.E_Amber];
        SkillData.ParameterWithKey burst = skillData.skills[SkillName.EB_Amber];
        if (character.constellation[0])
        { //일반공격의 개수가 1 증가
            baseAttack.parameter.count++;
        }
        if (character.constellation[1])
        { //폭탄인형의 대미지 20% 증가
            skill.parameter.damage *= 1.2f;
        }
        if (character.constellation[2])
        { //화살비 대미지 20% 증가
            burst.parameter.damage *= 1.2f;
        }
        if (character.constellation[3])
        { //폭탄 인형의 쿨타임 50% 감소
            skill.parameter.coolTime *= 0.5f;
        }
        if (character.constellation[4])
        { //폭탄 인형의 대미지 20% 증가
            skill.parameter.damage *= 1.2f;
        }
        if (character.constellation[5])
        { //화살비 발동 후 10초동안 이동속도 15%, 공격력 15% 증가
            burst.AddEndListener(() =>
            {
                player.StartCoroutine(AmborBuff());
            });
        }
    }

    IEnumerator AmborBuff()
    {
        float addSpeed = player.stat.speed * 0.15f;
        float addAtk = player.stat.atk * 0.15f;
        statBuff.Speed += addSpeed;
        statBuff.Atk += addAtk;
        yield return new WaitForSecondsRealtime(10.0f);
        statBuff.Speed -= addSpeed;
        statBuff.Atk -= addAtk;
    }


    void Xiangling(Character character)
    {
        SkillData.ParameterWithKey baseAttack = GameManager.instance.baseAttack.parameterWithKey;
        SkillData.ParameterWithKey skill = skillData.skills[SkillName.E_Xiangling];
        SkillData.ParameterWithKey burst = skillData.skills[SkillName.EB_Xiangling];
        if (character.constellation[0])
        { //누룽지의 공격에 피격된 적은 불원소 내성이 15% 감소, 지속 6초
            skill.parameter.isDebuffable = true;
        }
        if (character.constellation[1])
        { //일반공격 대미지 20% 증가
            baseAttack.parameter.damage *= 1.2f;
        }
        if (character.constellation[2])
        { //화륜의 대미지 20% 증가
            burst.parameter.damage *= 1.2f;
        }
        if (character.constellation[3])
        { //화륜의 지속시간 40% 증가
            burst.parameter.duration *= 0.4f;
        }
        if (character.constellation[4])
        { //누룽지 출격의 대미지 20% 증가
            skill.parameter.damage *= 1.2f;
        }
        if (character.constellation[5])
        { //화륜의 지속시간 동안 불 원소 피해 15% 증가
            burst.AddStartListener(() =>
            {
                player.StartCoroutine(XianglingBuff(burst));
            });
        }
    }

    IEnumerator XianglingBuff(SkillData.ParameterWithKey burst)
    {
        float addPyroDamage = 0.15f;
        statBuff.PyroDmg += addPyroDamage;
        yield return new WaitForSecondsRealtime(burst.parameter.duration);
        statBuff.PyroDmg -= addPyroDamage;
    }

    void Bennet(Character character)
    {
        SkillData.ParameterWithKey baseAttack = GameManager.instance.baseAttack.parameterWithKey;
        SkillData.ParameterWithKey skill = skillData.skills[SkillName.E_Bennet];
        SkillData.ParameterWithKey burst = skillData.skills[SkillName.EB_Bennet];
        if (character.constellation[0])
        { //공격력 1 증가
            statBuff.Atk += 1.0f;
        }
        if (character.constellation[1])
        { //원소충전 효율 20% 상승
            statBuff.Regen += 0.2f;
        }
        if (character.constellation[2])
        { //열정 과부하 대미지 20% 증가
            skill.parameter.damage *= 1.2f;
        }
        if (character.constellation[3])
        { //열정 과부하의 쿨타임 20% 감소
            skill.parameter.coolTime *= 0.8f;
        }
        if (character.constellation[4])
        { //아름다운 여정의 대미지 20% 증가
            burst.parameter.damage *= 1.2f;
        }
        if (character.constellation[5])
        { //아름다운 여정 영영 내에 있는 물리피해 기본공격은 불 원소 부여 효과를 얻고 불원소 피해 15% 증가
            //Buff.cs
            skill.constellations.num5 = true;
        }
    }

    void Diluc(Character character)
    {
        SkillData.ParameterWithKey baseAttack = GameManager.instance.baseAttack.parameterWithKey;
        SkillData.ParameterWithKey skill = skillData.skills[SkillName.E_Diluc];
        SkillData.ParameterWithKey burst = skillData.skills[SkillName.EB_Diluc];
        if (character.constellation[0])
        { //불원소 피해 15% 증가
            statBuff.PyroDmg += 0.15f;
        }
        if (character.constellation[1])
        { //공격력 20%증가, 공격속도 10% 증가
            baseAttack.parameter.coolTime *= 0.9f;
            statBuff.Atk += player.stat.atk * 1.2f;
        }
        if (character.constellation[2])
        { //역날의 화염 대미지 20% 증가
            skill.parameter.damage *= 1.2f;
        }
        if (character.constellation[3])
        { //역날의 화염 대미지 40% 증가
            skill.parameter.damage *= 1.4f;
        }
        if (character.constellation[4])
        { //여명의 대미지 20% 증가
            burst.parameter.damage *= 1.2f;
        }
        if (character.constellation[5])
        { //일반공격의 쿨타임이 20% 감소하고 피해 20% 증가
            baseAttack.parameter.coolTime *= 0.8f;
            baseAttack.parameter.damage *= 1.2f;
        }
    }

    void Klee(Character character)
    {
        SkillData.ParameterWithKey baseAttack = GameManager.instance.baseAttack.parameterWithKey;
        SkillData.ParameterWithKey skill = skillData.skills[SkillName.E_Klee];
        SkillData.ParameterWithKey burst = skillData.skills[SkillName.EB_Klee];
        if (character.constellation[0])
        { //쾅쾅 불꽃의 개수가 1 증가
            burst.parameter.count++;
        }
        if (character.constellation[1])
        { //통통 폭탄은 적의 방어력을 20% 감소 시킴. 지속시간 10초
            skill.parameter.isDebuffable = true;
        }
        if (character.constellation[2])
        { //통통폭탄의 대미지 20% 증가
            skill.parameter.damage *= 1.2f;
        }
        if (character.constellation[3])
        { //쾅쾅 불꽃의 관통 1증가
            burst.parameter.penetrate++;
        }
        if (character.constellation[4])
        { //쾅쾅불꽃의 대미지 20% 증가
            burst.parameter.damage *= 1.2f;
        }
        if (character.constellation[5])
        { //쾅쾅 불꽃 발동 시 3초마다 원소 에너지 3pt를 지속해서 회복, 불 원소 피해 보너스 20%증가. 지속시간 12초
            burst.AddStartListener(() =>
            {
                player.StartCoroutine(KleeBuff());
            });
        }
    }

    IEnumerator KleeBuff()
    {
        statBuff.PyroDmg += 0.2f;
        for (int i = 0; i < 4; i++)
        {
            GameManager.instance.AddElementGauge(3.0f);
            yield return new WaitForSecondsRealtime(3.0f);
        }
        statBuff.PyroDmg -= 0.2f;
    }
    void Barbara(Character character)
    {
        SkillData.ParameterWithKey baseAttack = GameManager.instance.baseAttack.parameterWithKey;
        SkillData.ParameterWithKey skill = skillData.skills[SkillName.E_Barbara];
        SkillData.ParameterWithKey burst = skillData.skills[SkillName.EB_Barbara];
        if (character.constellation[0])
        { //10초마다 원소 에너지를 1pt 회복
            player.StartCoroutine(BarbaraBuff0());
        }
        if (character.constellation[1])
        { //공연, 시작♪의 재사용 대기시간이 15% 감소
            skill.parameter.coolTime *= 0.85f;
            skill.constellations.num1 = true;
        }
        if (character.constellation[2])
        { //빛나는 기적♪의 회복량 20% 증가
            burst.parameter.healPer *= 1.2f;
        }
        if (character.constellation[3])
        { //원소충전 효율 20% 증가
            statBuff.Regen += 0.2f;
        }
        if (character.constellation[4])
        { //공연, 시작♪의 대미지 20% 증가
            burst.parameter.healPer *= 1.2f;
        }
        if (character.constellation[5])
        { //부활 회수 1 증가
            statBuff.Resurraction++;
        }
    }

    IEnumerator BarbaraBuff0()
    {
        while (true)
        {
            GameManager.instance.AddElementGauge(1.0f);
            yield return new WaitForSecondsRealtime(10.0f);
        }
    }
    void Xingqiu(Character character)
    {
        SkillData.ParameterWithKey baseAttack = GameManager.instance.baseAttack.parameterWithKey;
        SkillData.ParameterWithKey skill = skillData.skills[SkillName.E_Xingqiu];
        SkillData.ParameterWithKey burst = skillData.skills[SkillName.EB_Xingqiu];
        if (character.constellation[0])
        { //우령검의 개수 1 증가
            skill.parameter.count++;
            burst.parameter.count++;
        }
        if (character.constellation[1])
        { //고화검 · 재우유홍의 지속시간 20% 증가, 적 피격 시 물 원소 내성 15% 감소, 지속시간 4초
            burst.parameter.duration *= 1.2f;
            burst.parameter.isDebuffable = true;
        }
        if (character.constellation[2])
        { //고화검 · 재우유홍의 대미지 20% 증가
            burst.parameter.damage *= 1.2f;
        }
        if (character.constellation[3])
        { //고화검 · 재우유홍의 효과가 지속되는 동안 고화검 · 화우농산으로 가하는 피해가 50% 증가한다.
            float damage = skill.parameter.damage *= 1.5f;
            burst.AddStartListener(() =>
            {
                skill.parameter.damage += damage;
            });

            burst.AddEndListener(() =>
            {
                skill.parameter.damage -= damage;
            });
        }
        if (character.constellation[4])
        { //고화검 · 화우농산의 대미지 20% 증가
            skill.parameter.damage *= 1.2f;
        }
        if (character.constellation[5])
        { //고화검 · 재우유홍 사용 시 원소 에너지 10 획득, 대미지 20% 증가
            burst.AddStartListener(() =>
                {
                    GameManager.instance.AddElementGauge(10.0f);
                });
        }
    }

    void Mona(Character character)
    {
        SkillData.ParameterWithKey baseAttack = GameManager.instance.baseAttack.parameterWithKey;
        SkillData.ParameterWithKey skill = skillData.skills[SkillName.E_Mona];
        SkillData.ParameterWithKey burst = skillData.skills[SkillName.EB_Mona];
        if (character.constellation[0])
        { //별의 운명의 내성감소 20% 증가
            burst.constellations.num0 = true;
            //EnemyDebuff.cs
        }
        if (character.constellation[1])
        { //일반공격의 대미지 20% 증가
            baseAttack.parameter.damage *= 1.2f;
        }
        if (character.constellation[2])
        { //별의 운명 대미지 20% 증가
            burst.parameter.damage *= 1.2f;
        }
        if (character.constellation[3])
        { //별의 운명의 내성감소 20% 증가
            burst.constellations.num3 = true;
            //EnemyDebuff.cs
        }
        if (character.constellation[4])
        { //수중 환원의 대미지 20% 증가
            skill.parameter.damage *= 1.2f;
        }
        if (character.constellation[5])
        { //이동 중일때 대미지 20% 증가
            player.StartCoroutine(MonaBuff());
        }
    }

    IEnumerator MonaBuff()
    {
        bool isBuff = false;
        float buffValue = 0.2f;
        while (true)
        {
            Vector3 movePosition = player.inputVec;
            if (movePosition != Vector3.zero && !isBuff)
            {
                isBuff = true;
                statBuff.PhysicsDmg += buffValue;
                statBuff.PyroDmg += buffValue;
                statBuff.HydroDmg += buffValue;
                statBuff.AnemoDmg += buffValue;
                statBuff.DendroDmg += buffValue;
                statBuff.ElectroDmg += buffValue;
                statBuff.CyroDmg += buffValue;
                statBuff.GeoDmg += buffValue;
            }

            if (movePosition == Vector3.zero && isBuff)
            {
                isBuff = false;
                statBuff.PhysicsDmg -= buffValue;
                statBuff.PyroDmg -= buffValue;
                statBuff.HydroDmg -= buffValue;
                statBuff.AnemoDmg -= buffValue;
                statBuff.DendroDmg -= buffValue;
                statBuff.ElectroDmg -= buffValue;
                statBuff.CyroDmg -= buffValue;
                statBuff.GeoDmg -= buffValue;
            }
            yield return null;
        }
    }
    void Sucrose(Character character)
    {
        SkillData.ParameterWithKey baseAttack = GameManager.instance.baseAttack.parameterWithKey;
        SkillData.ParameterWithKey skill = skillData.skills[SkillName.E_Sucrose];
        SkillData.ParameterWithKey burst = skillData.skills[SkillName.EB_Sucrose];
        if (character.constellation[0])
        { //풍령 작성 · 육삼공팔의 쿨타임 20% 감소
            skill.parameter.coolTime *= 0.8f;
        }
        if (character.constellation[1])
        { //금기 · 풍령 작성 · 칠오 동구 이형의 지속시간 20% 증가
            burst.parameter.duration *= 1.2f;
        }
        if (character.constellation[2])
        { //풍령 작성 · 육삼공팔의 대미지 20% 증가
            skill.parameter.damage *= 1.2f;
        }
        if (character.constellation[3])
        { //풍령 작성 · 육삼공팔의 끌어당기는 범위와 속도 20% 증가
            skill.parameter.magnet *= 1.2f;
            skill.parameter.magnetSpeed *= 1.2f;
        }
        if (character.constellation[4])
        { //금기 · 풍령 작성 · 칠오 동구 이형의 대미지 20% 증가
            burst.parameter.damage *= 1.2f;
        }
        if (character.constellation[5])
        { //금기 · 풍령 작성 · 칠오 동구 이형이 원소 전환이 발생하면 대응하는 원소 피해 보너스를 20% 획득한다. 지속시간 10초
            burst.AddElementChangeListener((elementType) =>
            {
                player.StartCoroutine(SucroseBuff(elementType));
            });
        }
    }
    IEnumerator SucroseBuff(Element.Type elementType)
    {
        Element.Type type = elementType;
        float buffValue = 0.2f;
        switch (type)
        {
            case Element.Type.Pyro:
                statBuff.PyroDmg += buffValue;
                break;
            case Element.Type.Hydro:
                statBuff.HydroDmg += buffValue;
                break;
            case Element.Type.Electro:
                statBuff.ElectroDmg += buffValue;
                break;
            case Element.Type.Cyro:
                statBuff.CyroDmg += buffValue;
                break;
        }
        yield return new WaitForSecondsRealtime(10.0f);

        switch (type)
        {
            case Element.Type.Pyro:
                statBuff.PyroDmg -= buffValue;
                break;
            case Element.Type.Hydro:
                statBuff.HydroDmg -= buffValue;
                break;
            case Element.Type.Electro:
                statBuff.ElectroDmg -= buffValue;
                break;
            case Element.Type.Cyro:
                statBuff.CyroDmg -= buffValue;
                break;
        }
    }

    void Jean(Character character)
    {
        SkillData.ParameterWithKey baseAttack = GameManager.instance.baseAttack.parameterWithKey;
        SkillData.ParameterWithKey skill = skillData.skills[SkillName.E_Jean];
        SkillData.ParameterWithKey burst = skillData.skills[SkillName.EB_Jean];
        if (character.constellation[0])
        { //풍압검의 크기 40% 증가
            skill.parameter.area *= 1.4f;
        }
        if (character.constellation[1])
        { //일반공격 쿨타임 10% 감소, 이동속도 10% 증가
            baseAttack.parameter.coolTime *= 0.9f;
            statBuff.Speed += 0.1f;
        }
        if (character.constellation[2])
        { //민들레 바람 대미지 20% 증가
            burst.parameter.damage *= 1.2f;
        }
        if (character.constellation[3])
        { //민들레 바람 영역 내에서 모든 적의 바람 원소 내성 40% 감소
            burst.constellations.num3 = true;
        }
        if (character.constellation[4])
        { //풍압검 대미지 20% 증가
            skill.parameter.damage *= 1.2f;
        }
        if (character.constellation[5])
        { //민들레 바람 영역 내에서 방어력 3 증가
            player.StartCoroutine(AmborBuff());
        }
    }

    void Venti(Character character)
    {
        SkillData.ParameterWithKey baseAttack = GameManager.instance.baseAttack.parameterWithKey;
        SkillData.ParameterWithKey skill = skillData.skills[SkillName.E_Venti];
        SkillData.ParameterWithKey burst = skillData.skills[SkillName.EB_Venti];
        if (character.constellation[0])
        { //일반공격의 개수가 2 증가
            GameManager.instance.baseAttack.parameterWithKey.parameter.count += 2;
        }
        if (character.constellation[1])
        { //높은 하늘의 노래는 적의 바람원소 내성과 물리 내성을 12% 감소시킨다
            skill.constellations.num1 = true;
            skill.parameter.isDebuffable = true;
        }
        if (character.constellation[2])
        { //바람신의 시 대미지 20% 증가
            burst.parameter.damage *= 1.2f;
        }
        if (character.constellation[3])
        { //바람 원소 피해 20% 증가
            statBuff.AnemoDmg += 0.2f;
        }
        if (character.constellation[4])
        { //높은 하늘의 노래 대미지 20% 증가
            skill.parameter.damage *= 1.2f;
        }
        if (character.constellation[5])
        { //바람 신의  시에 피격된 적은 모든 원소 내성이 20% 감소
            burst.constellations.num5 = true;
            burst.parameter.isDebuffable = true;
        }
    }

    void Lisa(Character character)
    {
        SkillData.ParameterWithKey baseAttack = GameManager.instance.baseAttack.parameterWithKey;
        SkillData.ParameterWithKey skill = skillData.skills[SkillName.E_Lisa];
        SkillData.ParameterWithKey burst = skillData.skills[SkillName.EB_Lisa];
        if (character.constellation[0])
        { //원소 충전 효율 10% 증가
            statBuff.Regen += 0.1f;
        }
        if (character.constellation[1])
        { //창뢰의 대미지 주기 0.1초 감소
            skill.parameter.skillTick -= 0.1f;
        }
        if (character.constellation[2])
        { //장미의 뇌광의 범위 20% 증가
            burst.parameter.area *= 1.2f;
        }
        if (character.constellation[3])
        { //장미의 뇌광의 범위 40% 증가
            burst.parameter.area *= 1.4f;
        }
        if (character.constellation[4])
        { //창뢰의 범위 20% 증가
            skill.parameter.area *= 1.2f;
        }
        if (character.constellation[5])
        { //창뢰의 범위 40% 증가
            skill.parameter.area *= 1.4f;
        }
    }

    void Razor(Character character)
    {
        SkillData.ParameterWithKey baseAttack = GameManager.instance.baseAttack.parameterWithKey;
        SkillData.ParameterWithKey skill = skillData.skills[SkillName.E_Razor];
        SkillData.ParameterWithKey burst = skillData.skills[SkillName.EB_Razor];
        if (character.constellation[0])
        { //공격력 1 증가
            statBuff.Atk++;
        }
        if (character.constellation[1])
        { //행운이 1 증가
            statBuff.Luck++;
        }
        if (character.constellation[2])
        { //뇌아의 대미지 20% 증가
            burst.parameter.damage *= 1.2f;
        }
        if (character.constellation[3])
        { //날카로운 발톱과 창뢰에 맞은 적은 방어력이 2 감소한다. 지속 7초
            skill.constellations.num3 = true;
        }
        if (character.constellation[4])
        { //날카로운 발톱과 창뢰의 대미지 20% 증가
            skill.parameter.damage *= 1.2f;
        }
        if (character.constellation[5])
        { //뇌아의 최대 원소 게이지 20 감소
            burst.parameter.elementGaugeMax -= 20;
        }
    }

    void Beidou(Character character)
    {
        SkillData.ParameterWithKey baseAttack = GameManager.instance.baseAttack.parameterWithKey;
        SkillData.ParameterWithKey skill = skillData.skills[SkillName.E_Beidou];
        SkillData.ParameterWithKey burst = skillData.skills[SkillName.EB_Beidou];
        if (character.constellation[0])
        { //작뢰의 최대 원소게이지 20 감소
            burst.parameter.elementGaugeMax -= 20;
        }
        if (character.constellation[1])
        { //작뢰의 대미지 20% 증가
            burst.parameter.damage *= 1.2f;
        }
        if (character.constellation[2])
        { //파도잡이의 대미지 20% 증가
            skill.parameter.damage *= 1.2f;
        }
        if (character.constellation[3])
        { //작뢰의 대미지 20% 증가
            burst.parameter.damage *= 1.2f;
        }
        if (character.constellation[4])
        { //작뢰의 대미지 20% 증가
            burst.parameter.damage *= 1.2f;
        }
        if (character.constellation[5])
        { //작뢰가 지속되는 동안 번개 원소 피해 20% 증가
            burst.AddStartListener(() =>
            {
                statBuff.ElectroDmg += 0.2f;
            });
            burst.AddEndListener(() =>
            {
                statBuff.ElectroDmg -= 0.2f;
            });
        }
    }
    void Fischl(Character character)
    {
        SkillData.ParameterWithKey baseAttack = GameManager.instance.baseAttack.parameterWithKey;
        SkillData.ParameterWithKey skill = skillData.skills[SkillName.E_Fischl];
        SkillData.ParameterWithKey burst = skillData.skills[SkillName.EB_Fischl];
        burst.AddEndListener(() =>
        {
            skill.parameter.elementGauge = 10000;
        });
        if (character.constellation[0])
        { //일반공격의 대미지 20% 증가
            baseAttack.parameter.damage *= 1.2f;
        }
        if (character.constellation[1])
        { //밤을 순찰하는 그림자 날개의 관통 1 증가
            skill.parameter.penetrate++;
        }
        if (character.constellation[2])
        { //밤을 순찰하는 그림자 날개의 대미지 20% 증가
            skill.parameter.damage *= 1.2f;
        }
        if (character.constellation[3])
        { //암야의 환상 발동 시 HP 20% 회복, 암야의 환상 범위 50% 증가
            burst.AddStartListener(() =>
            {
                float healValue = player.maxHealth * 0.2f;
                player.HealHealth(healValue);
            });
        }
        if (character.constellation[4])
        { //암야의 환상 대미지 20% 증가
            burst.parameter.damage *= 1.2f;
        }
        if (character.constellation[5])
        { //암야의 환상과 밤을 순찰하는 그림자 날개 지속시간 20% 증가
            burst.parameter.duration *= 1.2f;
            skill.parameter.duration *= 1.2f;
        }
    }


    void Keqing(Character character)
    {
        SkillData.ParameterWithKey baseAttack = GameManager.instance.baseAttack.parameterWithKey;
        SkillData.ParameterWithKey skill = skillData.skills[SkillName.E_Keqing];
        SkillData.ParameterWithKey burst = skillData.skills[SkillName.EB_Keqing];
        if (character.constellation[0])
        { //성신회귀의 범위 50% 증가
            skill.parameter.area *= 1.5f;
        }
        if (character.constellation[1])
        { //원소 충전 효율 20% 증가
            statBuff.Regen += 0.2f;
        }
        if (character.constellation[2])
        { //천가 순유의 대미지 20% 증가
            burst.parameter.damage *= 1.2f;
        }
        if (character.constellation[3])
        { //공격력 2 증가
            statBuff.Atk += 2;
        }
        if (character.constellation[4])
        { //성신 회귀의 대미지 20% 증가
            skill.parameter.damage *= 1.2f;
        }
        if (character.constellation[5])
        { //번개 원소 피해 보너스 30% 증가
            statBuff.ElectroDmg += 0.3f;
        }
    }

    void Kaeya(Character character)
    {
        SkillData.ParameterWithKey baseAttack = GameManager.instance.baseAttack.parameterWithKey;
        SkillData.ParameterWithKey skill = skillData.skills[SkillName.E_Kaeya];
        SkillData.ParameterWithKey burst = skillData.skills[SkillName.EB_Kaeya];
        if (character.constellation[0])
        { //행운이 1 증가
            statBuff.Luck++;
        }
        if (character.constellation[1])
        { //살을 에는 윤무의 지속시간 20% 증가
            burst.parameter.duration *= 1.2f;
        }
        if (character.constellation[2])
        { //서리 엄습의 대미지 20% 증가
            skill.parameter.damage *= 1.2f;
        }
        if (character.constellation[3])
        { //
            skill.parameter.coolTime *= 0.5f;
        }
        if (character.constellation[4])
        { //살을 에는 윤무의 대미지 20% 증가
            burst.parameter.damage *= 1.2f;
        }
        if (character.constellation[5])
        { //살을 에는 윤무의 개수 1증가, 최대 원소 에너지 15 감소
            burst.parameter.count++;
            burst.parameter.elementGaugeMax -= 15;
        }
    }

    void Chongyun(Character character)
    {
        SkillData.ParameterWithKey baseAttack = GameManager.instance.baseAttack.parameterWithKey;
        SkillData.ParameterWithKey skill = skillData.skills[SkillName.E_Chongyun];
        SkillData.ParameterWithKey burst = skillData.skills[SkillName.EB_Chongyun];
        if (character.constellation[0])
        { //일반공격의 대미지의 10%의 얼음원소 추가피해가 생김(피해는 영도 · 중첩의 서리의 대미지로 계산)
            baseAttack.parameter.AddExtendDamage(skill.name, baseAttack.parameter.damage / 10.0f);
        }
        if (character.constellation[1])
        { //재사용 대기시간이 10% 감소
            statBuff.Cooltime += 0.1f;
        }
        if (character.constellation[2])
        { //영도 · 떨어지는 별의 대미지 20% 증가
            burst.parameter.damage *= 1.2f;
        }
        if (character.constellation[3])
        { //원소 충전 효율 10% 증가
            statBuff.Regen += 0.1f;
        }
        if (character.constellation[4])
        { //영도 · 중첩의 서리의 범위 20% 증가
            skill.parameter.area *= 1.2f;
        }
        if (character.constellation[5])
        { //영도 · 떨어지는 별의 지속시간 30% 증가
            burst.parameter.duration *= 1.3f;
        }
    }

    void Qiqi(Character character)
    {
        SkillData.ParameterWithKey baseAttack = GameManager.instance.baseAttack.parameterWithKey;
        SkillData.ParameterWithKey skill = skillData.skills[SkillName.E_Qiqi];
        SkillData.ParameterWithKey burst = skillData.skills[SkillName.EB_Qiqi];
        if (character.constellation[0])
        { //선법 · 한병의 귀차의 발동 시 원소 에너지를 2초당 2씩 5번 회복한다.
            skill.AddStartListener(() =>
            {
                player.StartCoroutine(QiqiBuff());
            });
        }
        if (character.constellation[1])
        { //일반공격의 대미지 20% 증가
            baseAttack.parameter.damage *= 1.2f;
        }
        if (character.constellation[2])
        { //선법 · 구고도액의 대미지 20% 증가
            burst.parameter.damage *= 1.2f;
        }
        if (character.constellation[3])
        { //선법 · 한병의 귀차에 피격당한 적은 공격력이 20% 감소함
            skill.parameter.isDebuffable = true;
        }
        if (character.constellation[4])
        { //선법 · 한병의 귀차의 회복량 20% 증가
            skill.parameter.healPer *= 1.2f;
        }
        if (character.constellation[5])
        { //부활 회수 2 증가
            statBuff.Resurraction += 2;
        }
    }
    IEnumerator QiqiBuff()
    {
        for (int i = 0; i < 5; i++)
        {
            GameManager.instance.AddElementGauge(2.0f);
            yield return new WaitForSecondsRealtime(2.0f);
        }
    }
    void Ningguang(Character character)
    {
        SkillData.ParameterWithKey baseAttack = GameManager.instance.baseAttack.parameterWithKey;
        SkillData.ParameterWithKey skill = skillData.skills[SkillName.E_Ninguang];
        SkillData.ParameterWithKey burst = skillData.skills[SkillName.EB_Ninguang];
        if (character.constellation[0])
        { //관통이 있는 일반공격의 관통 2 증가
            baseAttack.parameter.penetrate += 2;
        }
        if (character.constellation[1])
        { //선기 병풍의 재사용 대기시간 20% 감소
            skill.parameter.coolTime *= 0.8f;
        }
        if (character.constellation[2])
        { //천권 봉옥의 대미지 20% 증가
            burst.parameter.damage *= 1.2f;
        }
        if (character.constellation[3])
        { //선기 병풍의 지속시간 동안 방어력 2 증가
            skill.AddStartListener(() =>
            {
                statBuff.Armor += 2;
            });
            skill.AddEndListener(() =>
            {
                statBuff.Armor -= 2;
            });
        }
        if (character.constellation[4])
        { //선기 병풍의 대미지 20% 증가
            skill.parameter.damage *= 1.2f;
        }
        if (character.constellation[5])
        { //천권 붕옥의 개수 7개 증가
            burst.parameter.count += 7;
        }
    }

    void Noelle(Character character)
    {
        SkillData.ParameterWithKey baseAttack = GameManager.instance.baseAttack.parameterWithKey;
        SkillData.ParameterWithKey skill = skillData.skills[SkillName.E_Noelle];
        SkillData.ParameterWithKey burst = skillData.skills[SkillName.EB_Noelle];
        if (character.constellation[0])
        { //대청소 또는 호심경 사용시 HP 20% 회복
            float healValue = 0;
            burst.AddStartListener(() =>
            {
                healValue = player.maxHealth * 0.2f;
                player.HealHealth(healValue);
            });
            skill.AddStartListener(() =>
            {
                healValue = player.maxHealth * 0.2f;
                player.HealHealth(healValue);
            });
        }
        if (character.constellation[1])
        { //일반공격 피해 20% 증가
            baseAttack.parameter.damage *= 1.2f;
        }
        if (character.constellation[2])
        { //호심경의 대미지 20% 증가
            skill.parameter.damage *= 1.2f;
        }
        if (character.constellation[3])
        { //호심경의 범위, 대미지 50% 증가
            skill.parameter.area *= 1.5f;
            skill.parameter.damage *= 1.5f;
        }
        if (character.constellation[4])
        { //대청소의 대미지 20% 증가
            burst.parameter.damage *= 1.2f;
        }
        if (character.constellation[5])
        { //대청소 발동 시 추가로 노엘 방어력의 50%만큼 공격력 증가, 지속시간 20% 증가
            float buffValue = 0;
            burst.parameter.duration *= 1.2f;
            burst.AddStartListener(() =>
            {
                buffValue = GameManager.instance.statCalcuator.Armor * 0.5f;
                statBuff.Atk += buffValue;
            });

            burst.AddEndListener(() =>
            {
                statBuff.Atk -= buffValue;
            });
        }
    }
    void Tartaglia(Character character)
    {
        SkillData.ParameterWithKey baseAttack = GameManager.instance.baseAttack.parameterWithKey;
        SkillData.ParameterWithKey skill = skillData.skills[SkillName.E_Tartaglia];
        SkillData.ParameterWithKey burst = skillData.skills[SkillName.EB_Tartaglia];
        if (character.constellation[0])
        { //마왕 무장 · 광란의 재사용 대기시간이 20% 감소한다
            skill.parameter.coolTime *= 0.8f;
        }
        if (character.constellation[1])
        { //적 처치 시 극악기 · 진멸섬의 원소 에너지 0.5 회복
            //EnemyNormal.cs
        }
        if (character.constellation[2])
        { //마왕 무장 · 광란의 지속시간 20% 증가
            skill.parameter.duration *= 1.2f;
        }
        if (character.constellation[3])
        { //마왕 무장 · 광란 지속시간 동안 일반 공격의 관통 1 증가, 범위 20% 증가
            skill.AddStartListener(() =>
            {
                baseAttack.parameter.area += 0.2f;
                baseAttack.parameter.penetrate += 1;
            });
            skill.AddEndListener(() =>
            {
                baseAttack.parameter.area -= 0.2f;
                baseAttack.parameter.penetrate -= 1;
            });
        }
        if (character.constellation[4])
        { //극악기 · 진멸섬의 대미지 20% 증가
            burst.parameter.damage *= 1.2f;
        }
        if (character.constellation[5])
        { //극악기 · 진멸섬 발동 후 마왕 무장 · 광란의 재사용 대기시간이 초기화됨
            burst.AddEndListener(() =>
            {
                skill.parameter.elementGauge = 10000;
            });
        }

        SkillData.ParameterWithKey skill2 = skillData.skills[SkillName.E_Tartaglia];
        skill2.constellations.num0 = true;
    }
    void Diona(Character character)
    {
        SkillData.ParameterWithKey baseAttack = GameManager.instance.baseAttack.parameterWithKey;
        SkillData.ParameterWithKey skill = skillData.skills[SkillName.E_Diona];
        SkillData.ParameterWithKey burst = skillData.skills[SkillName.EB_Diona];
        if (character.constellation[0])
        { //특제 칵테일의 최대 원소 에너지 15 감소
            burst.parameter.elementGaugeMax -= 15;
        }
        if (character.constellation[1])
        { //꽁꽁 젤리의 관통 1 증가
            skill.parameter.penetrate += 1;
        }
        if (character.constellation[2])
        { //특제 칵테일의 회복량 20% 증가
            burst.parameter.healPer *= 1.2f;
        }
        if (character.constellation[3])
        { //특제 칵테일의 영역 내에서 일반 공격 재사용 대기시간이 20% 감소
            burst.constellations.num3 = true;
            //Buff.cs
        }
        if (character.constellation[4])
        { //꽁꽁젤리의 대미지 20% 증가
            skill.parameter.damage *= 1.2f;
        }
        if (character.constellation[5])
        { //특제 칵테일 영역 내의 캐릭터는 치유 보너스 30% 증가, 원소마스터리 200 증가
            burst.constellations.num5 = true;
            burst.parameter.isDebuffable = true;
            //Buff.cs
        }
    }
    void Zhongli(Character character)
    {
        SkillData.ParameterWithKey baseAttack = GameManager.instance.baseAttack.parameterWithKey;
        SkillData.ParameterWithKey skill = skillData.skills[SkillName.E_Zhongli];
        SkillData.ParameterWithKey burst = skillData.skills[SkillName.EB_Zhongli];
        if (character.constellation[0])
        { //지핵의 재사용 대기시간 20% 감소
            skill.parameter.coolTime *= 0.8f;
        }
        if (character.constellation[1])
        { //천성의 발동 후 지핵의 재사용 대기시간 초기화됨
            burst.AddEndListener(() =>
            {
                skill.parameter.elementGauge = 10000;
            });
        }
        if (character.constellation[2])
        { //지핵의 대미지 20% 증가
            skill.parameter.damage *= 1.2f;
        }
        if (character.constellation[3])
        { //천성의 범위가 20%증가, 또한 석화 지속시간 2초 증가
            burst.parameter.area *= 1.2f;
            burst.constellations.num3 = true;
            //EmenyNormal.cs
        }
        if (character.constellation[4])
        { //천성의 대미지 20% 증가
            burst.parameter.damage *= 1.2f;
        }
        if (character.constellation[5])
        { //지핵의 보호막이 피해를 받은 경우 받은 피해의 100% 만큼 적에게 바위 원소 피해를 준다
            skill.constellations.num5 = true;
            //Sheild.cs
        }
    }
    void Xinyan(Character character)
    {
        SkillData.ParameterWithKey baseAttack = GameManager.instance.baseAttack.parameterWithKey;
        SkillData.ParameterWithKey skill = skillData.skills[SkillName.E_Xinyan];
        SkillData.ParameterWithKey burst = skillData.skills[SkillName.EB_Xinyan];
        if (character.constellation[0])
        { //일반 공격의 대미지 12% 증가
            baseAttack.parameter.damage *= 1.12f;
        }
        if (character.constellation[1])
        { //반항의 피치카토로 가하는 피해는 반드시 치명타로 적용 됨
            burst.constellations.num1 = true;
            //StatCalculator.cs
        }
        if (character.constellation[2])
        { //정열의 연주의 범위 20% 증가
            skill.parameter.area *= 1.2f;
        }
        if (character.constellation[3])
        { //정열의 연주의 피해는 적의 물리 내성을 15% 감소 시킴
            skill.constellations.num3 = true;
            skill.parameter.isDebuffable = true;
            //EnemyDebuff.cs
        }
        if (character.constellation[4])
        { //반항의 피치카토의 범위 20% 증가
            burst.parameter.area *= 1.2f;
        }
        if (character.constellation[5])
        { //일반 공격의 피해량이 방어력의 20% 만큼 증가함
            //StatCalculator.cs
        }
    }
    void Albedo(Character character)
    {
        SkillData.ParameterWithKey baseAttack = GameManager.instance.baseAttack.parameterWithKey;
        SkillData.ParameterWithKey skill = skillData.skills[SkillName.E_Albedo];
        SkillData.ParameterWithKey burst = skillData.skills[SkillName.EB_Albedo];
        if (character.constellation[0])
        { //창생법 · 모조 태양꽃 사용 시 탄생식 · 대지의 파동의 원소 에너지 5 증가
            skill.AddStartListener(() =>
            {
                burst.parameter.elementGauge += 5;
            });
        }
        if (character.constellation[1])
        { //창생법 · 모조 태양꽃의 범위 내에서 방어력 3 증가
            skill.constellations.num1 = true;
            //Buff.cs
        }
        if (character.constellation[2])
        { //창생법 · 모조 태양꽃의 대미지 20% 증가
            skill.parameter.damage *= 1.2f;
        }
        if (character.constellation[3])
        { //창생법 · 모조 태양꽃의 범위 20% 증가
            skill.parameter.area *= 1.2f;
        }
        if (character.constellation[4])
        { //탄생식 · 대지의 파동의 대미지 20% 증가
            burst.parameter.damage *= 1.2f;
        }
        if (character.constellation[5])
        { //창생법 · 모조 태양꽃의 범위 내에서 재사용 대기시간 20% 감소
            skill.constellations.num5 = true;
            //Buff.cs
        }
    }
    void Ganyu(Character character)
    {
        SkillData.ParameterWithKey baseAttack = GameManager.instance.baseAttack.parameterWithKey;
        SkillData.ParameterWithKey skill = skillData.skills[SkillName.E_Ganyu];
        SkillData.ParameterWithKey burst = skillData.skills[SkillName.EB_Ganyu];
        if (character.constellation[0])
        { //"감우의 일반공격에 피격당한 적은 얼음 원소 내성이 15% 감소함. 지속 6초 또한 일반공격 사용 시 원소 에너지 0.5 회복"
            if (baseAttack.name == SkillName.Basic_Ganyu)
            {
                baseAttack.constellations.num0 = true;
                baseAttack.parameter.isDebuffable = true;
            }
            baseAttack.AddStartListener(() =>
            {
                GameManager.instance.AddElementGauge(0.5f);
            });
        }
        if (character.constellation[1])
        { //산과 강의 기린 흔적의 재사용 대기시간이 20% 감소함
            skill.parameter.coolTime *= 0.8f;
        }
        if (character.constellation[2])
        { //쏟아지는 천화의 대미지 20% 증가
            burst.parameter.damage *= 1.2f;
        }
        if (character.constellation[3])
        { //쏟아지는 천화 영역 내에서 적의 모든 내성이 20% 감소
            burst.constellations.num3 = true;
            burst.parameter.isDebuffable = true;
            //EnemyDebuff.cs
        }
        if (character.constellation[4])
        { //산과 강의 기린 흔적의 대미지 20% 증가
            skill.parameter.damage *= 1.2f;
        }
        if (character.constellation[5])
        { //감우의 일반공격의 쿨타임 50% 감소
            if (baseAttack.name == SkillName.Basic_Ganyu)
            {
                baseAttack.parameter.coolTime *= 0.5f;
            }
        }
    }


    void Xiao(Character character)
    {
        SkillData.ParameterWithKey baseAttack = GameManager.instance.baseAttack.parameterWithKey;
        SkillData.ParameterWithKey skill = skillData.skills[SkillName.E_Xiao];
        SkillData.ParameterWithKey burst = skillData.skills[SkillName.EB_Xiao];
        if (character.constellation[0])
        { //"풍륜양립의 재사용 대기 시간이 20% 감소
            skill.parameter.coolTime *= 0.8f;
        }
        if (character.constellation[1])
        { //원소 충전 효율이 25% 증가
            statBuff.Regen += 0.25f;
        }
        if (character.constellation[2])
        { //풍륜양립의 대미지 20% 증가
            skill.parameter.damage *= 1.2f;
        }
        if (character.constellation[3])
        { //HP가 50% 미만일 때 방어력이 100% 증가함
            player.StartCoroutine(XiaoBuff());
        }
        if (character.constellation[4])
        { //나자의 춤의 대미지 20% 증가
            burst.parameter.damage *= 1.2f;
        }
        if (character.constellation[5])
        { //나자의 춤 지속시간 동안 풍륜양립의 재사용 대기시간이 1초가 됨
            burst.constellations.num5 = true;
            //Buff.cs
        }
    }

    IEnumerator XiaoBuff()
    {
        bool isBuff = false;
        float buffValue = player.stat.armor;
        while (true)
        {
            float healthPer = player.health / GameManager.instance.statCalcuator.Health;
            if (healthPer <= 0.5f && !isBuff)
            {
                isBuff = true;
                buffValue = GameManager.instance.statCalcuator.Armor;
                statBuff.Armor += buffValue;
            }

            if (healthPer > 0.5f && isBuff)
            {
                isBuff = false;
                statBuff.Armor -= buffValue;
            }
            yield return null;
        }
    }

    void Hutao(Character character)
    {
        SkillData.ParameterWithKey baseAttack = GameManager.instance.baseAttack.parameterWithKey;
        SkillData.ParameterWithKey skill = skillData.skills[SkillName.E_Hutao];
        SkillData.ParameterWithKey burst = skillData.skills[SkillName.EB_Hutao];
        if (character.constellation[0])
        { //나비의 서 지속시간 동안 일반 공격의 재사용 대기시간이 20% 감소함
            float baseAttackCooltime = baseAttack.parameter.coolTime;
            skill.AddStartListener(() =>
            {
                baseAttackCooltime = baseAttack.parameter.coolTime * 0.2f;
                baseAttack.parameter.coolTime -= baseAttackCooltime;
            });
            skill.AddEndListener(() =>
            {
                baseAttack.parameter.coolTime += baseAttackCooltime;
            });
        }
        if (character.constellation[1])
        { //일반 공격이 가하는 피해량이 HP의 2% 만큼 증가함
            //Enemy.cs
        }
        if (character.constellation[2])
        { //나비의 서 지속시간 20% 증가
            skill.parameter.duration *= 1.2f;
        }
        if (character.constellation[3])
        { //행운 3증가
            statBuff.Luck += 3;
        }
        if (character.constellation[4])
        { //평안의 서 대미지 20% 증가
            burst.parameter.damage *= 1.2f;
        }
        if (character.constellation[5])
        { //HP가 25% 이하로 떨어지거나 전투 불능이 될 정도의 피해를 입으면 10초동안 무적이 되며 모든 공격이 치명타가 됨 쿨타임 60초
            statBuff.hutaoConstell5 = true;
            player.StartCoroutine(HutaoBuff());
            //Player.cs
            //StatCalculator.cs
        }
    }

    IEnumerator HutaoBuff()
    {
        float buffResetTime = 60.0f;
        float buffTime = 0;
        while (true)
        {
            if (!statBuff.hutaoConstell5)
            {
                buffTime = 0;
                while (true)
                {
                    buffTime += Time.deltaTime;
                    if (buffTime >= 10.0f)
                    {
                        statBuff.allCritical = false;
                    }
                    if (buffTime >= buffResetTime)
                    {
                        statBuff.hutaoConstell5 = true;
                        break;
                    }
                    yield return null;
                }
            }
            yield return null;
        }
    }


    void Rosaria(Character character)
    {
        SkillData.ParameterWithKey baseAttack = GameManager.instance.baseAttack.parameterWithKey;
        SkillData.ParameterWithKey skill = skillData.skills[SkillName.E_Rosaria];
        SkillData.ParameterWithKey burst = skillData.skills[SkillName.EB_Rosaria];
        if (character.constellation[0])
        { //일반 공격 피해 10%증가, 일반 공격 재사용 대기시간 10% 감소
            baseAttack.parameter.damage *= 1.1f;
            baseAttack.parameter.coolTime *= 0.9f;
        }
        if (character.constellation[1])
        { //죽음의 성례의 지속시간 20% 증가
            burst.parameter.duration *= 1.2f;
        }
        if (character.constellation[2])
        { //죄를 삼키는 고해의 대미지 20% 증가
            skill.parameter.damage *= 1.2f;
        }
        if (character.constellation[3])
        { //죄를 삼키는 고해 사용 시 죽음의 성례의 원소 게이지 5 증가
            skill.AddStartListener(() =>
            {
                burst.parameter.elementGaugeMax += 5;
            });
        }
        if (character.constellation[4])
        { //죽음의 성례의 대미지 20% 증가
            burst.parameter.damage *= 1.2f;
        }
        if (character.constellation[5])
        { //죽음의 성례의 공격은 적의 물리 내성을 20% 감소시킴. 지속 10초
            burst.parameter.isDebuffable = true;
            //EnemyDebuff.cs
        }
    }


    void Yanfei(Character character)
    {
        SkillData.ParameterWithKey baseAttack = GameManager.instance.baseAttack.parameterWithKey;
        SkillData.ParameterWithKey skill = skillData.skills[SkillName.E_Yanfei];
        SkillData.ParameterWithKey burst = skillData.skills[SkillName.EB_Yanfei];
        if (character.constellation[0])
        { //HP가 20% 증가
            statBuff.Hp += player.stat.hp * 0.2f;
        }
        if (character.constellation[1])
        { //행운 3증가
            statBuff.Luck += 3;
        }
        if (character.constellation[2])
        { //단홍의 계약 대미지 20% 증가
            skill.parameter.damage *= 1.2f;
        }
        if (character.constellation[3])
        { //계약 성립 발동 시 HP최대치의 30%에 해당하는 피해를 흡수하는 보호막 생성. 지속 15초
            burst.constellations.num3 = true;
            //SkillObject.cs
        }
        if (character.constellation[4])
        { //계약 성립의 대미지 20% 증가
            burst.parameter.damage *= 1.2f;
        }
        if (character.constellation[5])
        { //단홍의 계약의 일반 공격 추가대미지 50% 증가
            skill.constellations.num5 = true;
            //Skill.cs
        }
    }


    void Eula(Character character)
    {
        SkillData.ParameterWithKey baseAttack = GameManager.instance.baseAttack.parameterWithKey;
        SkillData.ParameterWithKey skill = skillData.skills[SkillName.E_Eula];
        SkillData.ParameterWithKey burst = skillData.skills[SkillName.EB_Eula];

        if (character.constellation[0])
        { //얼음 파도의 와류의 3스택 공격 시 물리피해가 30% 증가함 지속. 12초
            skill.AddEndListener(() =>
            {
                if (statBuff.eulaSkillStack == 0)
                {
                    player.StartCoroutine(EulaBuff());
                }
            });
        }
        if (character.constellation[1])
        { //얼음 파도의 와류의 재사용 대기시간 20% 감소
            skill.parameter.coolTime *= 0.8f;
        }
        if (character.constellation[2])
        { //파도를 얼리는 광검의 대미지 20% 증가
            burst.parameter.damage *= 1.2f;
        }
        if (character.constellation[3])
        { //파도를 얼리는 광검의 대미지 30% 증가
            burst.parameter.damage *= 1.3f;
        }
        if (character.constellation[4])
        { //얼음 파도의 와류 대미지 20% 증가
            skill.parameter.damage *= 1.2f;
        }
        if (character.constellation[5])
        { //파도를 얼리는 광검 시전시 즉시 5스택 획득, 일반공격으로 획득하는 스택이 1추가로 증가함
          //Buff.cs, SkillObject.cs 
        }
    }

    IEnumerator EulaBuff()
    {
        statBuff.PhysicsDmg += 0.3f;
        yield return new WaitForSecondsRealtime(10.0f);
        statBuff.PhysicsDmg -= 0.3f;
    }

    void Kazuha(Character character)
    {
        SkillData.ParameterWithKey baseAttack = GameManager.instance.baseAttack.parameterWithKey;
        SkillData.ParameterWithKey skill = skillData.skills[SkillName.E_Kazuha];
        SkillData.ParameterWithKey burst = skillData.skills[SkillName.EB_Kazuha];
        InitializeDictionaries();
        if (character.constellation[0])
        { //치하야부루의 재사용 대기시간이 10% 감소. 카즈하의 일도를 발동하면 치하야부루의 재사용 대기시간이 초기화됨
            skill.parameter.coolTime *= 0.9f;
            burst.AddStartListener(() =>
            {
                skill.parameter.elementGauge = 10000;
            });
        }
        if (character.constellation[1])
        { //원소마스터리 200 증가. 치하야부루 또는 카즈하의 일도의 원소전환 시 해당하는 원소의 원소피해 20% 증가. 지속 12초
            statBuff.ElementMastery += 200;
            skill.AddElementChangeListener((type) =>
            {
                player.StartCoroutine(KazuhaBuff(type));
            });
            burst.AddElementChangeListener((type) =>
            {
                player.StartCoroutine(KazuhaBuff(type));
            });
        }
        if (character.constellation[2])
        { //치하야부루의 대미지 20% 증가
            skill.parameter.damage *= 1.2f;
        }
        if (character.constellation[3])
        { //카즈하의 일도의 원소 에너지가 45 이하일 경우 치하야부루 사용시 카즈하의 일도의 원소에너지 10 회복
            skill.AddStartListener(() =>
            {
                if (burst.parameter.elementGauge <= 45)
                {
                    burst.parameter.elementGauge += 10;
                }
            });
        }
        if (character.constellation[4])
        { //카즈하의 일도의 대미지 20% 증가
            burst.parameter.damage *= 1.2f;
        }
        if (character.constellation[5])
        { //일반공격 피해가 원소마스터리의 2%만큼 증가함. 치하야부루 또는 카즈하의 일도 지속시간동안 일반공격에 바람원소 부여 효과를 얻음. 
          //StatCalculator.cs
            skill.AddStartListener(() =>
            {
                StartChangeElementType();
            });
            skill.AddEndListener(() =>
            {
                EndChangeElementType();
            });
            burst.AddStartListener(() =>
            {
                StartChangeElementType();
            });
            burst.AddEndListener(() =>
            {
                EndChangeElementType();
            });
        }
    }
    private int elementTypeChangeCount = 0;

    private void StartChangeElementType()
    {
        SkillData.ParameterWithKey baseAttack = GameManager.instance.baseAttack.parameterWithKey;
        if (elementTypeChangeCount == 0)
        {
            baseAttack.parameter.SetElementType(baseAttack, Element.Type.Anemo);
        }
        elementTypeChangeCount++;
    }

    private void EndChangeElementType()
    {
        SkillData.ParameterWithKey baseAttack = GameManager.instance.baseAttack.parameterWithKey;
        elementTypeChangeCount--;

        if (elementTypeChangeCount <= 0)
        {
            baseAttack.parameter.SetElementType(baseAttack, Element.Type.Physics);
            elementTypeChangeCount = 0;  // 음수 방지
        }
    }

    private Dictionary<Element.Type, bool> buffActiveFlags = new Dictionary<Element.Type, bool>();
    private Dictionary<Element.Type, float> buffDurations = new Dictionary<Element.Type, float>();

    private void InitializeDictionaries()
    {
        foreach (Element.Type type in Enum.GetValues(typeof(Element.Type)))
        {
            buffActiveFlags[type] = false;
            buffDurations[type] = 0f;
        }
    }

    IEnumerator KazuhaBuff(Element.Type elementType)
    {
        if (buffActiveFlags[elementType])
        {
            buffDurations[elementType] = 10.0f;
            yield break;
        }

        buffActiveFlags[elementType] = true;

        float buffValue = 0.2f;
        ApplyBuff(elementType, buffValue);
        buffDurations[elementType] = 10.0f;

        while (buffDurations[elementType] > 0)
        {
            yield return new WaitForSecondsRealtime(1.0f);
            buffDurations[elementType] -= 1.0f;
        }

        RemoveBuff(elementType, buffValue);
        buffActiveFlags[elementType] = false;
    }

    void ApplyBuff(Element.Type elementType, float buffValue)
    {
        switch (elementType)
        {
            case Element.Type.Pyro:
                statBuff.PyroDmg += buffValue;
                break;
            case Element.Type.Hydro:
                statBuff.HydroDmg += buffValue;
                break;
            case Element.Type.Electro:
                statBuff.ElectroDmg += buffValue;
                break;
            case Element.Type.Cyro:
                statBuff.CyroDmg += buffValue;
                break;
        }
    }

    void RemoveBuff(Element.Type elementType, float buffValue)
    {
        switch (elementType)
        {
            case Element.Type.Pyro:
                statBuff.PyroDmg -= buffValue;
                break;
            case Element.Type.Hydro:
                statBuff.HydroDmg -= buffValue;
                break;
            case Element.Type.Electro:
                statBuff.ElectroDmg -= buffValue;
                break;
            case Element.Type.Cyro:
                statBuff.CyroDmg -= buffValue;
                break;
        }
    }


    private void Ayaka(Character character)
    {
        SkillData.ParameterWithKey baseAttack = GameManager.instance.baseAttack.parameterWithKey;
        SkillData.ParameterWithKey baseAttackAyaka = skillData.skills[SkillName.Basic_Ayaka];
        SkillData.ParameterWithKey skill = skillData.skills[SkillName.E_Ayaka];
        SkillData.ParameterWithKey burst = skillData.skills[SkillName.EB_Ayaka];
        SkillObject skillObject = GameManager.instance.ownSkills[SkillName.E_Ayaka];
        if (character.constellation[0])
        { // 일반공격 발동 시 50% 확률로 카미사토류 · 얼음꽃의 재사용 대기시간 0.3초 감소
            baseAttack.AddStartListener(() =>
            {
                if (0.5f.CheckSuccessByRate())
                {
                    skillObject.skillTime += 0.3f;
                }
            });
        }
        if (character.constellation[1])
        { // 카미사토류 · 멸망의 서리의 개수 2 증가
            skill.parameter.count += 2;
        }
        if (character.constellation[2])
        { // 카미사토류 · 멸망의 서리의 대미지 20% 증가
            burst.parameter.damage *= 1.2f;
        }
        if (character.constellation[3])
        { // 카미사토류 · 멸망의 서리는 적의 방어력을 5 감소 시킴. 지속 6초
            burst.constellations.num3 = true;
            burst.parameter.isDebuffable = true;
            //EnemyDebuff.cs
        }
        if (character.constellation[4])
        { // 카미사토류 · 얼음꽃의 대미지 20% 증가
            skill.parameter.damage *= 1.2f;
        }
        if (character.constellation[5])
        { // 일반공격-아야카의 대미지 100% 증가
            baseAttack.parameter.damage *= 2.0f;
        }
    }
    private void Yoimiya(Character character)
    {
        SkillData.ParameterWithKey baseAttack = GameManager.instance.baseAttack.parameterWithKey;
        SkillData.ParameterWithKey skill = skillData.skills[SkillName.E_Yoimiya];
        SkillData.ParameterWithKey burst = skillData.skills[SkillName.EB_Yoimiya];
        if (character.constellation[0])
        { // 염초 정화(庭火)의 춤의 개수 2 증가
            skill.parameter.count += 2;
        }
        if (character.constellation[1])
        { // 염초 정화(庭火)의 춤이 항상 치명타로 적중함
            skill.constellations.num1 = true;
            //StatCalulator.cs
        }
        if (character.constellation[2])
        { // 염초 정화(庭火)의 춤의 대미지 20% 증가
            burst.parameter.damage *= 1.2f;
        }
        if (character.constellation[3])
        { // 염초 정화(庭火)의 춤의 재사용 대기시간 20% 감소
            skill.parameter.coolTime *= 0.8f;
        }
        if (character.constellation[4])
        { // 유금 운간초의 대미지 20% 증가
            skill.parameter.damage *= 1.2f;
        }
        if (character.constellation[5])
        { // 염초 정화(庭火)의 춤의 재사용 대기시간 30% 감소
            skill.parameter.coolTime *= 0.7f;
        }
    }
    private void Sayu(Character character)
    {
        SkillData.ParameterWithKey baseAttack = GameManager.instance.baseAttack.parameterWithKey;
        SkillData.ParameterWithKey skill = skillData.skills[SkillName.E_Travler_Geo];
        SkillData.ParameterWithKey burst = skillData.skills[SkillName.EB_Travler_Geo];
        if (character.constellation[0])
        { // 유후류 · 너구리 분신술의 회복량과 대미지 20% 증가
            burst.parameter.healPer *= 1.2f;
            burst.parameter.damage *= 1.2f;
        }
        if (character.constellation[1])
        { // 유후류 · 풍은 돌진의 대미지 주기가 0.1초 감소
            skill.parameter.skillTick -= 0.1f;
        }
        if (character.constellation[2])
        { // 유후류 · 너구리 분신술의 대미지 20% 증가
            burst.parameter.damage *= 1.2f;
        }
        if (character.constellation[3])
        { // 원소충전 효율 20% 증가
            statBuff.Regen += 0.2f;
        }
        if (character.constellation[4])
        { // 유후류 · 풍은 돌진의 대미지 20%증가
            skill.parameter.damage *= 1.2f;
        }
        if (character.constellation[5])
        { // 유후류 · 너구리 분신술의 대미지와 회복량이 원소마스터리의 2% 만큼 증가함
            burst.constellations.num5 = true;
            //StatCalculator.cs
        }
    }
    private void Raiden(Character character)
    {
        SkillData.ParameterWithKey baseAttack = GameManager.instance.baseAttack.parameterWithKey;
        SkillData.ParameterWithKey skill = skillData.skills[SkillName.E_Raiden];
        SkillData.ParameterWithKey burst = skillData.skills[SkillName.EB_Raiden];
        if (character.constellation[0])
        { // 초월 · 악요개안로 증가하는 공격력이 20% 증가함
            skill.constellations.num0 = true;
        }
        if (character.constellation[1])
        { // 오의 · 몽상진설의 대미지가 내성의 영향을 받지 않음
            foreach (SkillSet.SkillSequence skillSequence in skill.skillSet.sequences)
            {
                skillSequence.elementType = Element.Type.Fix;
            }
        }
        if (character.constellation[3])
        { // 오의 · 몽상진설의 무적시간이 2초 증가
            statBuff.immuneTimeRaiden += 2;
        }
        if (character.constellation[2])
        { // 오의 · 몽상진설의 대미지 20% 증가
            burst.parameter.damage *= 1.2f;
        }
        if (character.constellation[4])
        { // 초월 · 악요개안의 대미지 20% 증가
            skill.parameter.damage *= 1.2f;
        }
        if (character.constellation[5])
        { // 오의 · 몽상진설의 최대 원소 에너지가 200 감소
            burst.parameter.elementGaugeMax -= 200;
        }
    }
    private void Sara(Character character)
    {
        SkillData.ParameterWithKey baseAttack = GameManager.instance.baseAttack.parameterWithKey;
        SkillData.ParameterWithKey skill = skillData.skills[SkillName.E_Sara];
        SkillData.ParameterWithKey burst = skillData.skills[SkillName.EB_Sara];
        if (character.constellation[0])
        { // 까마귀 깃 텐구의 뇌정 소환의 재사용 대기시간 20% 감소
            skill.parameter.coolTime *= 0.8f;
        }
        if (character.constellation[1])
        { // 까마귀 깃 텐구의 뇌정 소환의 공격력 증가효과 20% 증가
            skill.constellations.num1 = true;
            burst.constellations.num1 = true;
            //buff.cs
        }
        if (character.constellation[2])
        { // 빛나는 천도의 진법의 대미지 20% 증가
            burst.parameter.damage *= 1.2f;
        }
        if (character.constellation[3])
        { // 빛나는 천도의 진법의 번개 개수가 2 증가
            burst.parameter.count += 2;
        }
        if (character.constellation[4])
        { // 까마귀 깃 텐구의 뇌정 소환의 대미지 20% 증가
            skill.parameter.damage *= 1.2f;
        }
        if (character.constellation[5])
        { // 공격력 증가효과에 행운 5증가가 추가됨
            skill.constellations.num5 = true;
            burst.constellations.num5 = true;
            //buff.cs
        }
    }
    private void Kokomi(Character character)
    {
        SkillData.ParameterWithKey baseAttack = GameManager.instance.baseAttack.parameterWithKey;
        SkillData.ParameterWithKey skill = skillData.skills[SkillName.E_Kokomi];
        SkillData.ParameterWithKey burst = skillData.skills[SkillName.EB_Kokomi];
        if (character.constellation[0])
        { // 해인화우(海人化羽) 상태에서 대미지가 체력의 2% 추가로 증가
            skill.constellations.num0 = true;
            //Buff.cs
        }
        if (character.constellation[1])
        { // 해파리의 서약과 해인화우(海人化羽)의 회복량이 20% 증가
            skill.parameter.healPer *= 1.2f;
            burst.parameter.healPer *= 1.2f;
        }
        if (character.constellation[2])
        { // 해인화우(海人化羽)의 회복량 20% 증가
            burst.parameter.healPer *= 1.2f;
        }
        if (character.constellation[3])
        { // 해인화우(海人化羽) 상태에서 일반공격 재사용 대기시간 10% 감소. 일반공격 사용 시 원소 에너지 1 회복
            burst.AddStartListener(() =>
            {
                statBuff.BaseCooltime += 0.1f;
                baseAttack.AddStartListener(() =>
                {
                    GameManager.instance.AddElementGauge(1.0f);
                });
            });
        }
        if (character.constellation[4])
        { // 해파리의 서약 대미지 20% 증가
            skill.parameter.damage *= 1.2f;
        }
        if (character.constellation[5])
        { // 해인화우(海人化羽) 상태에서 물 원소 피해 보너스 40% 증가
            burst.AddStartListener(() =>
            {
                statBuff.HydroDmg += 0.4f;
            });
        }
    }
    private void Ito(Character character)
    {
        SkillData.ParameterWithKey baseAttack = GameManager.instance.baseAttack.parameterWithKey;
        SkillData.ParameterWithKey skill = skillData.skills[SkillName.E_Ito];
        SkillData.ParameterWithKey burst = skillData.skills[SkillName.EB_Ito];
        if (character.constellation[0])
        { // 방어력 5 증가
            statBuff.Armor += 5;
        }
        if (character.constellation[1])
        { // 파티의 바위원소 캐릭터 1명당 방어력 2 증가
            foreach (Character checkCharacter in GameDataManager.instance.saveData.userData.selectChars)
            {
                if (checkCharacter.elementType == Element.Type.Geo)
                {
                    statBuff.Armor += 2;
                }
            }
        }
        if (character.constellation[2])
        { // 마살 절기 · 적우발파!의 대미지 20% 증가
            skill.parameter.damage *= 1.2f;
        }
        if (character.constellation[3])
        { // 일반공격 범위 20% 증가
            baseAttack.parameter.area *= 1.2f;
        }
        if (character.constellation[4])
        { // 방어력 5 증가
            statBuff.Armor += 5;
        }
        if (character.constellation[5])
        { // 행운 5 증가
            statBuff.Luck += 5;
        }
    }
    private void Goro(Character character)
    {
        SkillData.ParameterWithKey baseAttack = GameManager.instance.baseAttack.parameterWithKey;
        SkillData.ParameterWithKey skill = skillData.skills[SkillName.E_Goro];
        SkillData.ParameterWithKey burst = skillData.skills[SkillName.EB_Goro];
        if (character.constellation[0])
        { // 이누자카 멍멍 방원진의 재사용 대기시간 20% 감소
            skill.parameter.coolTime *= 0.8f;
        }
        if (character.constellation[1])
        { // 이누자카 멍멍 방원진의 지속시간 20% 증가
            skill.parameter.duration *= 1.2f;
        }
        if (character.constellation[2])
        { // 이누자카 멍멍 방원진의 대미지 20% 증가
            skill.parameter.damage *= 1.2f;
        }
        if (character.constellation[3])
        { // 이누자카 멍멍 방원진 발동 시 방어력의 50% 만큼 체력을 회복
            skill.AddStartListener(() =>
            {
                player.HealHealth(GameManager.instance.statCalcuator.Armor * 0.5f);
            });
        }
        if (character.constellation[4])
        { // 야수 이빨 돌격형 승전법의 지속시간 20% 증가
            burst.parameter.duration *= 1.2f;
        }
        if (character.constellation[5])
        { // 이누자카 멍멍 방원진효과에 캐릭터 하나당 행운이 3씩 증가함
            skill.constellations.num5 = true;
            burst.constellations.num5 = true;
        }
    }
    private void Thoma(Character character)
    {
        SkillData.ParameterWithKey baseAttack = GameManager.instance.baseAttack.parameterWithKey;
        SkillData.ParameterWithKey skill = skillData.skills[SkillName.E_Thoma];
        SkillData.ParameterWithKey burst = skillData.skills[SkillName.EB_Thoma];
        if (character.constellation[0])
        { // 타오르는 우명의 보호와 진홍 열화의 오오요로이의 재사용 대기시간이 20% 감소
            skill.parameter.coolTime *= 0.8f;
            burst.parameter.coolTime *= 0.2f;
        }
        if (character.constellation[1])
        { // 진홍 열화의 오오요로이의 지속시간 20% 증가
            burst.parameter.coolTime *= 1.2f;
        }
        if (character.constellation[2])
        { // 타오르는 우명의 보호의 대미지 20% 증가
            skill.parameter.damage *= 1.2f;
        }
        if (character.constellation[3])
        { // 진홍 열화의 오오요로이의 최대 원소 에너지 15 감소
            burst.parameter.elementGaugeMax -= 15;
        }
        if (character.constellation[4])
        { // 진홍 열화의 오오요로이의 대미지 20% 증가
            burst.parameter.damage *= 1.2f;
        }
        if (character.constellation[5])
        { // 일반공격 피해 20% 증가
            baseAttack.parameter.damage *= 1.2f;
        }
    }
    private void Shenhe(Character character)
    {
        SkillData.ParameterWithKey baseAttack = GameManager.instance.baseAttack.parameterWithKey;
        SkillData.ParameterWithKey skill = skillData.skills[SkillName.E_Shenhe];
        SkillData.ParameterWithKey burst = skillData.skills[SkillName.EB_Shenhe];
        if (character.constellation[0])
        { // 위령 소환 구사술의 재사용 대기시간 20% 감소
            skill.constellations.num0 = true;
        }
        if (character.constellation[1])
        { // 신녀 강령 비결의 지속시간 30% 증가 얼음원소 내성 감소량 15% 추가
            skill.parameter.coolTime *= 0.8f;
        }
        if (character.constellation[2])
        { // 위령 소환 구사술의 대미지 20% 증가
            skill.parameter.damage *= 1.2f;
        }
        if (character.constellation[3])
        { // 위령 소환 구사술의 피해증가 효과가 20% 추가
            burst.parameter.elementGaugeMax -= 20;
        }
        if (character.constellation[4])
        { // 신녀 강경 비결 대미지 20% 증가
            burst.parameter.damage *= 1.2f;
        }
        if (character.constellation[5])
        { // 위령 소환 구사술의 피해증가 효과가 영구 지속 됨
            statBuff.E_Shenhe_Stack_Minus = 0;
        }
    }
    private void Yunjin(Character character)
    {
        SkillData.ParameterWithKey baseAttack = GameManager.instance.baseAttack.parameterWithKey;
        SkillData.ParameterWithKey skill = skillData.skills[SkillName.E_Yunjin];
        SkillData.ParameterWithKey burst = skillData.skills[SkillName.EB_Yunjin];
        if (character.constellation[0])
        { // 선운개상의 재사용 대기시간 20% 감소함
            skill.parameter.coolTime *= 0.8f;
        }
        if (character.constellation[1])
        { // 절벽을 깎는 깃발 효과에 일반공격 피해증가 15%가 추가됨
            burst.AddStartListener(() =>
            {
                statBuff.BaseDamage += 0.15f;
            });
            burst.AddEndListener(() =>
            {
                statBuff.BaseDamage -= 0.15f;
            });
        }
        if (character.constellation[2])
        { // 선운개상의 재사용 대기시간 20% 감소
            skill.parameter.coolTime *= 0.8f;
        }
        if (character.constellation[3])
        { // 방어력 5 증가
            statBuff.Armor += 5;
        }
        if (character.constellation[4])
        { // 선운개상의 대미지 20% 증가
            skill.parameter.damage *= 1.2f;
        }
        if (character.constellation[5])
        { // 절벽을 깎는 깃발 효과에 일반공격 재사용 대기시간 12% 감소가 추가됨
            burst.AddStartListener(() =>
            {
                statBuff.BaseCooltime += 0.12f;
            });
            burst.AddEndListener(() =>
            {
                statBuff.BaseCooltime -= 0.12f;
            });
        }
    }
    private void Miko(Character character)
    {
        SkillData.ParameterWithKey baseAttack = GameManager.instance.baseAttack.parameterWithKey;
        SkillData.ParameterWithKey skill = skillData.skills[SkillName.E_Miko];
        SkillData.ParameterWithKey burst = skillData.skills[SkillName.EB_Miko];
        if (character.constellation[0])
        { // 대비법 · 천호(天狐) 현신의 최대 원소에너지가 24 감소함
            burst.parameter.elementGaugeMax -= 24;
        }
        if (character.constellation[1])
        { // 야칸의 소환 · 살생앵의 범위가 40% 증가하고 재사용 대기시간이 20% 감소함
            skill.parameter.coolTime *= 0.8f;
            skill.parameter.area *= 1.4f;
        }
        if (character.constellation[2])
        { // 야칸의 소환 · 살생앵의 대미지 20% 증가
            skill.parameter.damage *= 1.2f;
        }
        if (character.constellation[3])
        { // 번개 원소 피해 20% 증가
            statBuff.ElectroDmg += 0.2f;
        }
        if (character.constellation[4])
        { // 대비법 · 천호(天狐)현신의 대미지 20% 증가
            burst.parameter.damage *= 1.2f;
        }
        if (character.constellation[5])
        { // 야칸의 소환 · 살생앵의 재사용 대기시간 20% 감소
            skill.parameter.coolTime *= 0.8f;
        }
    }
    private void Ayato(Character character)
    {
        SkillData.ParameterWithKey baseAttack = GameManager.instance.baseAttack.parameterWithKey;
        SkillData.ParameterWithKey skill = skillData.skills[SkillName.E_Ayato];
        SkillData.ParameterWithKey burst = skillData.skills[SkillName.EB_Ayato];
        if (character.constellation[0])
        { // 카미사토류 · 거울꽃의 대미지가 40% 증가함
            skill.parameter.damage *= 1.4f;
        }
        if (character.constellation[1])
        { // 체력이 50% 증가함
            statBuff.Hp += player.stat.hp * 0.5f;
        }
        if (character.constellation[2])
        { // 카미사토류 · 거울꽃의 대미지가 20% 증가함
            skill.parameter.damage *= 1.2f;
        }
        if (character.constellation[3])
        { // 카미사토류 · 거울꽃의 재사용 대기시간 20% 감소
            skill.parameter.coolTime *= 0.8f;
        }
        if (character.constellation[4])
        { // 카미사토류 · 물의 영역의 대미지 20% 증가
            burst.parameter.damage *= 1.2f;
        }
        if (character.constellation[5])
        { // 카미사토류 · 거울꽃의 대미지 40% 증가
            skill.parameter.damage *= 1.4f;
        }
    }
    private void Yelan(Character character)
    {
        SkillData.ParameterWithKey baseAttack = GameManager.instance.baseAttack.parameterWithKey;
        SkillData.ParameterWithKey skill = skillData.skills[SkillName.E_Yelan];
        SkillData.ParameterWithKey burst = skillData.skills[SkillName.EB_Yelan];
        if (character.constellation[0])
        { // 뒤얽힌 생명줄의 재사용 대기시간 20% 감소
            skill.parameter.coolTime *= 0.8f;
        }
        if (character.constellation[1])
        { // 심오하고 영롱한 주사위의 투사체 발사 개수 1 증가
            burst.parameter.count += 1;
        }
        if (character.constellation[2])
        { // 심오하고 영롱한 주사위 대미지 20% 증가
            burst.parameter.damage *= 1.2f;
        }
        if (character.constellation[3])
        { // 뒤얽힌 생명줄 지속시간동안 적을 통과할수 있으며 무적상태가 됨
            skill.AddStartListener(() =>
            {
                float duration = skill.skillSet.sequences[0].duration
                * GameManager.instance.statCalcuator.Duration
                * skill.parameter.duration;
                player.ReceiveDamage(0, duration);
            });
        }
        if (character.constellation[4])
        { // 뒤얽힌 생명줄의 대미지 20% 증가
            skill.parameter.damage *= 1.2f;
        }
        if (character.constellation[5])
        { // 심오하고 영롱한 주사위에 적에게 맞을시 폭발하며 물원소 범위피해를 주는 효과가 추가됨
            burst.constellations.num5 = true;
        }
    }
    private void Heizo(Character character)
    {
        SkillData.ParameterWithKey baseAttack = GameManager.instance.baseAttack.parameterWithKey;
        SkillData.ParameterWithKey skill = skillData.skills[SkillName.E_Heizo];
        SkillData.ParameterWithKey burst = skillData.skills[SkillName.EB_Heizo];
        if (character.constellation[0])
        { // 일반공격-헤이조로 변격 스택이 쌓일 경우 1개 더 획득함
            statBuff.E_Heizo_Stack_Plus++;
        }
        if (character.constellation[1])
        { // 선풍각의 끌어당기는 범위, 속도 20% 증가
            burst.parameter.magnet *= 1.2f;
            burst.parameter.magnetSpeed *= 1.2f;
        }
        if (character.constellation[2])
        { // 쇄심권의 대미지 20% 증가
            skill.parameter.damage *= 1.2f;
        }
        if (character.constellation[3])
        { // 선풍각의 최대 원소 에너지 20 감소, 재사용 대기시간 20% 감소
            burst.parameter.elementGaugeMax -= 20;
            burst.parameter.coolTime *= 0.8f;
        }
        if (character.constellation[4])
        { // 선풍각의 대미지 20% 증가
            burst.parameter.damage *= 1.2f;
        }
        if (character.constellation[5])
        { // 행운 5 증가
            statBuff.Luck += 5;
        }
    }
    private void Shinobu(Character character)
    {
        SkillData.ParameterWithKey baseAttack = GameManager.instance.baseAttack.parameterWithKey;
        SkillData.ParameterWithKey skill = skillData.skills[SkillName.E_Shinobu];
        SkillData.ParameterWithKey burst = skillData.skills[SkillName.EB_Shinobu];
        if (character.constellation[0])
        { // 교에 나루카미 카리야마 의식의 범위 50% 증가
            burst.parameter.area *= 1.5f;
        }
        if (character.constellation[1])
        { // 제액의 뇌초지륜 지속시간이 20% 증가함
            skill.parameter.duration *= 1.2f;
        }
        if (character.constellation[2])
        { // 제액의 뇌초지륜의 대미지 20% 증가
            skill.parameter.damage *= 1.2f;
        }
        if (character.constellation[3])
        { // 제액의 뇌초지륜의 범위 20% 증가
            skill.parameter.area *= 1.2f;
        }
        if (character.constellation[4])
        { // 교에 나루카미 카리야마 의식의 대미지 20% 증가
            burst.parameter.damage *= 1.2f;
        }
        if (character.constellation[5])
        { // 원소마스터리가 100 증가, 전투불능 상태의 대미지를 받을 경우 전투불능 상태가 되지 않고 3초간 무적이 됨. 재사용 대기시간 60초
            statBuff.ElementMastery += 100;
            player.onDamaged.Add((damage) =>
            {
                ShinobuBuff();
            });
        }
    }
    bool isShinobu = false;
    void ShinobuBuff()
    {
        if (isShinobu) return;
        isShinobu = true;
        player.ReceiveDamage(0, 3.0f);
        player.StartCoroutine(ShinobuBuffCooltime());
    }

    IEnumerator ShinobuBuffCooltime()
    {
        yield return new WaitForSeconds(60.0f);
        isShinobu = false;
    }
}