using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using DG.Tweening;


public class WeaponStarter
{
    StatBuff statBuff;
    WeaponData.Parameter weaponParameter;
    Weapon weapon;
    Character character;
    Player player;
    SkillData skillData;
    public void InitWeapon(Character character, Weapon weapon)
    {
        weaponParameter = GameManager.instance.weaponData.Get(character.weaponName);
        if (weaponParameter.weaponName == WeaponName.None_Weapoen) return;
        this.weapon = weapon;
        this.character = character;
        skillData = GameManager.instance.skillData;
        player = GameManager.instance.player;
        statBuff = GameManager.instance.statBuff;
        switch (weaponParameter.weaponName)
        {
            case WeaponName.Sword_Favonius:
                Sword_Favonius();
                break;
            case WeaponName.Sword_The_Flute:
                Sword_The_Flute();
                break;
            case WeaponName.Sword_Sacrificial:
                Sword_Sacrificial();
                break;
            case WeaponName.Sword_Lions_Loar:
                Sword_Lions_Loar();
                break;
            case WeaponName.Sword_The_Alley_Flash:
                Sword_The_Alley_Flash();
                break;
            case WeaponName.Sword_The_Black:
                Sword_The_Black();
                break;
            case WeaponName.Sword_Aquila_Favonia:
                Sword_Aquila_Favonia();
                break;
            case WeaponName.Sword_Skyword_Blade:
                Sword_Skyword_Blade();
                break;
            case WeaponName.Sword_Summit_Shaper:
                Sword_Summit_Shaper();
                break;
            case WeaponName.Sword_Primordial_Jade_Cutter:
                Sword_Primordial_Jade_Cutter();
                break;
            case WeaponName.Sword_Freedom_Sworn:
                Sword_Freedom_Sworn();
                break;
            case WeaponName.Claymore_Favonius:
                Claymore_Favonius();
                break;
            case WeaponName.Claymore_The_Bell:
                Claymore_The_Bell();
                break;
            case WeaponName.Claymore_Sacrificial:
                Claymore_Sacrificial();
                break;
            case WeaponName.Claymore_Rainslasher:
                Claymore_Rainslasher();
                break;
            case WeaponName.Claymore_Lithic_Blade:
                Claymore_Lithic_Blade();
                break;
            case WeaponName.Claymore_Serpent_Spine:
                Claymore_Serpent_Spine();
                break;
            case WeaponName.Claymore_Skyward_Pride:
                Claymore_Skyward_Pride();
                break;
            case WeaponName.Claymore_Wolfs_Gravestone:
                Claymore_Wolfs_Gravestone();
                break;
            case WeaponName.Claymore_The_Unforged:
                Claymore_The_Unforged();
                break;
            case WeaponName.Claymore_Song_Of_Broken_Pines:
                Claymore_Song_Of_Broken_Pines();
                break;
            case WeaponName.Spear_Dragons_Bane:
                Spear_Dragons_Bane();
                break;
            case WeaponName.Spear_Favonius:
                Spear_Favonius();
                break;
            case WeaponName.Spear_Lithic_Spear:
                Spear_Lithic_Spear();
                break;
            case WeaponName.Spear_Deathmatch:
                Spear_Deathmatch();
                break;
            case WeaponName.Spear_Skyward_Spine:
                Spear_Skyward_Spine();
                break;
            case WeaponName.Spear_Primordial_Jade_Spear:
                Spear_Primordial_Jade_Spear();
                break;
            case WeaponName.Spear_Vortex_Vanquisher:
                Spear_Vortex_Vanquisher();
                break;
            case WeaponName.Spear_Staff_Of_Homa:
                Spear_Staff_Of_Homa();
                break;
            case WeaponName.Catalist_Favonius:
                Catalist_Favonius();
                break;
            case WeaponName.Catalist_Widsith:
                Catalist_Widsith();
                break;
            case WeaponName.Catalist_Sacrificial:
                Catalist_Sacrificial();
                break;
            case WeaponName.Catalist_Eye_Of_Perception:
                Catalist_Eye_Of_Perception();
                break;
            case WeaponName.Catalist_Wine_And_Song:
                Catalist_Wine_And_Song();
                break;
            case WeaponName.Catalist_Solar_Pearl:
                Catalist_Solar_Pearl();
                break;
            case WeaponName.Catalist_Skyward_Atlas:
                Catalist_Skyward_Atlas();
                break;
            case WeaponName.Catalist_Lost_Prayer_To_The_Sacered_Winds:
                Catalist_Lost_Prayer_To_The_Sacered_Winds();
                break;
            case WeaponName.Catalist_Memory_Of_Dust:
                Catalist_Memory_Of_Dust();
                break;
            case WeaponName.Bow_Favonius:
                Bow_Favonius();
                break;
            case WeaponName.Bow_The_Stringless:
                Bow_The_Stringless();
                break;
            case WeaponName.Bow_Sacrificial_Bow:
                Bow_Sacrificial_Bow();
                break;
            case WeaponName.Bow_Rust:
                Bow_Rust();
                break;
            case WeaponName.Bow_Alley_Hunter:
                Bow_Alley_Hunter();
                break;
            case WeaponName.Bow_Mitternachts_Waltz:
                Bow_Mitternachts_Waltz();
                break;
            case WeaponName.Bow_Viridescent_Hunt:
                Bow_Viridescent_Hunt();
                break;
            case WeaponName.Bow_SkyWard_Harp:
                Bow_SkyWard_Harp();
                break;
            case WeaponName.Bow_Amos_Bow:
                Bow_Amos_Bow();
                break;
            case WeaponName.Bow_Elegy_For_The_End:
                Bow_Elegy_For_The_End();
                break;
            case WeaponName.Bow_Polar_Star:
                Bow_Polar_Star();
                break;
        }
    }
    void WeaponFade(bool fade)
    {
        weapon.OutlineFade(fade);
    }
    IEnumerator WeaponFadeCoroutine()
    {
        WeaponFade(true);
        yield return new WaitForSecondsRealtime(1.0f);
        WeaponFade(false);
    }
    void WeaponFade()
    {
        weapon.StartCoroutine(WeaponFadeCoroutine());
    }

    IEnumerator WeaponCooltime(float coolTime, float time, UnityAction start, UnityAction end)
    {
        start.Invoke();
        while (true)
        {
            if (!GameManager.instance.IsPause) time += 0.5f;
            if (time > coolTime)
            {
                end.Invoke();
                break;
            }
            yield return new WaitForSecondsRealtime(0.5f);
        }
        yield return null;
    }

    void Sword_Favonius() //{0}초마다 {1:P}의 확률로 원소 에너지를 6pt 회복함
    {
        weapon.StartCoroutine(FavoniusBuff(weaponParameter.valueSums[0], weaponParameter.valueSums[1]));
    }

    IEnumerator FavoniusBuff(float coolTime, float percentage)
    {
        float time = 0;
        while (true)
        {
            if (!GameManager.instance.IsPause) time += 0.5f;
            if (time > coolTime)
            {
                int random = Random.Range(0, 10);
                if (percentage * 10 > random)
                {
                    GameManager.instance.AddElementGauge(6.0f);
                }
                time = 0;
                WeaponFade();
            }
            yield return new WaitForSecondsRealtime(0.5f);
        }
    }

    void Sword_The_Flute() //5초마다 주변 적에게 공격력 {0:P}의 피해를 줌
    {
        SkillData.ParameterWithKey skillParameter = skillData.skills[SkillName.Weapon_Sword_The_Flute];
        skillParameter.parameter.damage = weaponParameter.valueSums[0];
        skillParameter.AddStartListener(() =>
        {
            WeaponFade(true);
        });
        skillParameter.AddEndListener(() =>
        {
            WeaponFade(false);
        });
        weapon.AddWeaponSkill(SkillName.Weapon_Sword_The_Flute);
    }

    void Sword_Sacrificial()
    {
        Sacrificial();
    }
    void Sacrificial() //원소전투 스킬을 사용할 때 {0:P}의 확률로 해당 스킬의 재사용 대기시간이 초기화됨. 해당 효과는 {1}초마다 발동가능
    {
        weapon.weaponCheck = true;
        weapon.weaponAction = () =>
        {
            if (weapon.weaponCheck == true)
            {
                int random = Random.Range(0, 10);
                bool result = (weaponParameter.valueSums[0] * 10 > random);
                if (result)
                {
                    WeaponFade();
                    weapon.weaponCheck = false;
                    weapon.StartCoroutine(SacrificialBuff(weaponParameter.valueSums[1]));
                }
                return result;
            }
            return false;
        };
    }
    IEnumerator SacrificialBuff(float coolTime)
    {
        float time = 0;
        while (true)
        {
            if (!GameManager.instance.IsPause) time += 0.5f;
            if (time > coolTime)
            {
                weapon.weaponCheck = true;
                break;
            }
            yield return new WaitForSecondsRealtime(0.5f);
        }
        yield return null;
    }
    void Sword_Lions_Loar() //불 원소 또는 번개 원소가 부착된 적에게 가하는 피해가 {0:P}증가함
    {
        statBuff.Sword_Lions_Loar = weaponParameter.valueSums[0];
        //StatCalculator.cs
    }
    bool isSword_The_Alley_Flash = false;
    float timeSword_The_Alley_Flash = 0;
    void Sword_The_Alley_Flash() //피해가 {0:P}증가함 적에게 체력이 감소된 경우 해당 효과가 5초동안 사라짐
    {
        statBuff.allDamageAdd(weaponParameter.valueSums[0]);
        player.onDamaged.Add((damage) =>
        {
            timeSword_The_Alley_Flash = 0;
            weapon.StartCoroutine(The_Alley_FlashBuff(5.0f));
        });
    }
    IEnumerator The_Alley_FlashBuff(float coolTime)
    {
        if (isSword_The_Alley_Flash) yield break;
        statBuff.allDamageMinus(weaponParameter.valueSums[0]);
        isSword_The_Alley_Flash = true;
        WeaponFade(true);
        while (true)
        {
            if (!GameManager.instance.IsPause) timeSword_The_Alley_Flash += 0.5f;
            if (timeSword_The_Alley_Flash > coolTime)
            {
                WeaponFade(false);
                statBuff.allDamageAdd(weaponParameter.valueSums[0]);
                isSword_The_Alley_Flash = false;
                break;
            }
            yield return new WaitForSecondsRealtime(0.5f);
        }
        yield return null;
    }
    bool isSword_The_Black = false;
    void Sword_The_Black() //일반공격 피해가 {0:P}증가함. 일반공격 사용 시 공격력의 {1:P}에 해당하는 HP를 회복함. 재사용 대기시간 10초
    {
        CharacterData.Name charname = GameDataManager.instance.saveData.userData.selectChars[0].charNum;
        CharacterData.ParameterWithKey characterParam = GameManager.instance.characterData.Get(charname);
        SkillData.ParameterWithKey baseAttack = skillData.skills[characterParam.skillBasic];
        baseAttack.parameter.damage += weaponParameter.valueSums[0];
        baseAttack.AddStartListener(() =>
        {
            if (isSword_The_Black) return;
            isSword_The_Black = true;
            WeaponFade();
            player.HealHealth(GameManager.instance.statCalcuator.Atk * weaponParameter.valueSums[1]);
            weapon.PatternDelay(10.0f).OnComplete(() =>
            {
                isSword_The_Black = false;
            });
        });
    }

    bool isSword_Aquila_Favonia = false;
    void Sword_Aquila_Favonia() //공격력이 {0:P}증가함. 적에게 체력이 감소된 경우 공격력의 {1:P}만큼 HP를 회복하고 그 두배만큼의 피해를 주변 적에게 줌. 재사용 대기시간 15초
    {
        float addAtk = player.stat.atk * weaponParameter.valueSums[0];
        statBuff.Atk += addAtk;

        SkillData.ParameterWithKey skillParameter = skillData.skills[SkillName.Weapon_Sword_Aquila_Favonia];
        skillParameter.parameter.damage = weaponParameter.valueSums[1] * 2;
        skillParameter.AddStartListener(() =>
        {
            WeaponFade(true);
        });
        skillParameter.AddEndListener(() =>
        {
            WeaponFade(false);
        });
        weapon.AddWeaponSkill(SkillName.Weapon_Sword_Aquila_Favonia);
        player.onDamaged.Add((damage) =>
        {
            if (isSword_Aquila_Favonia) return;
            isSword_Aquila_Favonia = true;
            player.HealHealth(weaponParameter.valueSums[1]);
            skillParameter.parameter.elementGauge = 10000;
            weapon.PatternDelay(15.0f).OnComplete(() =>
            {
                isSword_Aquila_Favonia = false;
            });
        });
    }
    bool isSword_Skyword_Blade = false;
    void Sword_Skyword_Blade() //행운이 {0} 증가함. 장착 캐릭터의 원소폭발 발동 시 이동속도와 10% 증가 일반공격 쿨타임 10% 감소. 일반공격 대미지 {1:P} 증가. 지속 12초
    {
        CharacterData.ParameterWithKey characterParam = GameManager.instance.characterData.Get(character.charNum);
        SkillData.ParameterWithKey burst = skillData.skills[skillData.skills[characterParam.skill].burst];
        statBuff.Luck += weaponParameter.valueSums[0];
        CharacterData.Name charname = GameDataManager.instance.saveData.userData.selectChars[0].charNum;
        CharacterData.ParameterWithKey characterMain = GameManager.instance.characterData.Get(charname);
        SkillData.ParameterWithKey baseAttack = skillData.skills[characterMain.skillBasic];
        burst.AddStartListener(() =>
        {
            if (isSword_Skyword_Blade) return;
            isSword_Skyword_Blade = true;
            WeaponFade(true);
            statBuff.BaseCooltime += 0.1f;
            statBuff.Speed += 0.1f;
            statBuff.BaseDamage += weaponParameter.valueSums[1];
            weapon.PatternDelay(12.0f).OnComplete(() =>
            {
                statBuff.BaseCooltime -= 0.1f;
                statBuff.Speed -= 0.1f;
                statBuff.BaseDamage -= weaponParameter.valueSums[1];
                isSword_Skyword_Blade = false;
                WeaponFade(true);
            });
        });
    }
    void Sword_Summit_Shaper() //보호막 체력이 {0:P}증가함. 공격력이 {1:P} 증가함. 보호막이 존재하면 공격력 증가 효과가 두배가 됨
    {
        statBuff.SheildPer += weaponParameter.valueSums[0];
        statBuff.KnwooDamagePer += weaponParameter.valueSums[1];
    }
    void Sword_Primordial_Jade_Cutter() //HP가 {0:P} 증가하고, HP최대치의 {1:P}에 해당하는 공격력 보너스를 획득함
    {
        statBuff.Hp += player.stat.hp * weaponParameter.valueSums[0];
        statBuff.HealthDamagePer += weaponParameter.valueSums[1];
    }
    void Sword_Freedom_Sworn() //가하는 피해 {0:P}증가함. 20초마다 일반공격피해 {1:P}증가, 공격력 {2:P} 증가함. 지속 12초
    {
        float time = 0.0f;
        statBuff.allDamageAdd(weaponParameter.valueSums[0]);
        weapon.StartCoroutine(WeaponCooltime(20.0f, time,
        () =>
        {
            statBuff.BaseDamage += weaponParameter.valueSums[1];
            float atk = player.stat.atk * weaponParameter.valueSums[2];
            statBuff.Atk += atk;
            WeaponFade(true);

            weapon.PatternDelay(12.0f).OnComplete(() =>
            {
                statBuff.BaseDamage -= weaponParameter.valueSums[1];
                statBuff.Atk -= atk;
                WeaponFade(false);
            });
        },
        () =>
        {
            time = 0.0f;
        }));
    }
    void Claymore_Favonius() //{0}초마다 {1:P}의 확률로 원소 에너지를 6pt 회복함
    {
        weapon.StartCoroutine(FavoniusBuff(weaponParameter.valueSums[0], weaponParameter.valueSums[1]));
    }

    bool isClaymore_The_Bell = false;
    void Claymore_The_Bell() //적에게 체력이 감소된 경우 HP최대치의 {0:P}의 체력을 가진 보호막이 생성됨. 보호막은 10초동안 유지됨 재사용 대기시간 45초. 보호막이 존재할 경우 피해가 {1:P}증가함
    {
        SkillData.ParameterWithKey skillParameter = skillData.skills[SkillName.Weapon_Claymore_The_Bell];
        skillParameter.parameter.sheildPer = weaponParameter.valueSums[0];

        skillParameter.AddStartListener(() =>
        {
            WeaponFade(true);
        });
        skillParameter.AddEndListener(() =>
        {
        });
        weapon.AddWeaponSkill(SkillName.Weapon_Claymore_The_Bell);

        player.onDamaged.Add((damage) =>
        {
            if (isClaymore_The_Bell) return;
            isClaymore_The_Bell = true;
            weapon.skillObj.SkillCastAbsolute();

            weapon.PatternDelay(45.0f).OnComplete(() =>
            {
                WeaponFade(false);
                isClaymore_The_Bell = false;
            });
        });
    }
    void Claymore_Sacrificial() //원소전투 스킬을 사용할 때 {0:P}의 확률로 해당 스킬의 재사용 대기시간이 초기화됨. 해당 효과는 {1}초마다 발동가능
    {
        Sacrificial();
    }
    void Claymore_Rainslasher() //물 원소 또는 번개원소가 부착된 적에게 피해 {0:P}증가함
    {
        statBuff.Claymore_Rainslasher = weaponParameter.valueSums[0];
        //StatCalculator.cs
    }
    void Claymore_Lithic_Blade() //파티의 리월 출신 캐릭터 1명당 {0:P}만큼 공격력이 증가하고 행운이 {1}증가함
    {
        float charCount = 0;
        foreach (Character character in GameDataManager.instance.saveData.userData.selectChars)
        {
            if (character == null) continue;
            CharacterData.ParameterWithKey characterParameter = GameManager.instance.characterData.Get(character.charNum);
            if (characterParameter.nationalType == CharacterData.NationalType.Liyue)
            {
                charCount++;
            }
        }

        statBuff.Atk += player.stat.atk * weaponParameter.valueSums[0] * charCount;
        statBuff.Luck += weaponParameter.valueSums[1] * charCount;
    }
    bool isClaymore_Serpent_SpineStack = false;
    int stackMax = 5;
    void Claymore_Serpent_Spine() //4초마다 피해 {0:P} 증가, 받는피해 {1:P} 증가함. 최대 중첩 5회. 피해를 받으면 중첩이 1줄어듬
    {
        float time = 0;
        statBuff.Claymore_Serpent_SpineDamage = weaponParameter.valueSums[0];
        statBuff.Claymore_Serpent_SpineReceiveDamage = weaponParameter.valueSums[1];
        WeaponCooltime(4.0f, time,
        () =>
        {
            WeaponFade();
            if (statBuff.Claymore_Serpent_SpineStack < stackMax)
            {
                statBuff.Claymore_Serpent_SpineStack++;
            }
        },
        () =>
        {
            time = 0;
        });
        player.onDamaged.Add((damage) =>
        {
            if (isClaymore_Serpent_SpineStack) return;
            if (statBuff.Claymore_Serpent_SpineStack > 0)
            {
                statBuff.Claymore_Serpent_SpineStack--;
            }
            isClaymore_Serpent_SpineStack = true;
            weapon.PatternDelay(1.0f).OnComplete(() =>
            {
                isClaymore_Serpent_SpineStack = false;
            });
        });
    }
    void Claymore_Skyward_Pride() //피해 {0:P}증가. 원소폭발 발동 시 가장 가까운 적에게 주기적으로 공격력의 {1:P}피해를 주는 진공의 칼날을 8회 발사함. 새로운 원소폭발을 발동할 경우 공격회수 초기화됨
    {
        statBuff.allDamageAdd(weaponParameter.valueSums[0]);
        SkillData.ParameterWithKey skillParameter = skillData.skills[SkillName.Weapon_Claymore_Skyward_Pride];
        skillParameter.parameter.damage = weaponParameter.valueSums[1];
        skillParameter.AddStartListener(() =>
        {
            if (statBuff.Claymore_Skyward_PrideStack > 0)
            {
                WeaponFade(true);
            }
        });
        skillParameter.AddEndListener(() =>
        {
            if (statBuff.Claymore_Skyward_PrideStack > 0)
            {
                statBuff.Claymore_Skyward_PrideStack--;
            }
            WeaponFade(false);
        });
        weapon.AddWeaponSkill(SkillName.Weapon_Claymore_Skyward_Pride);


        foreach (Character character in GameDataManager.instance.saveData.userData.selectChars)
        {
            if (character == null) continue;
            CharacterData.ParameterWithKey characterParam = GameManager.instance.characterData.Get(character.charNum);
            SkillData.ParameterWithKey burst = skillData.skills[skillData.skills[characterParam.skill].burst];
            burst.AddStartListener(() =>
            {
                statBuff.Claymore_Skyward_PrideStack = 9;
            });
        }
    }
    void Claymore_Wolfs_Gravestone() //공격력 {0:P}증가. 30초마다 공격력 {1:P} 증가함. 지속 12초. 재사용 대기시간 30초
    {
        float time = 0;
        statBuff.Atk += player.stat.atk * weaponParameter.valueSums[0];
        float atk = 0;
        weapon.StartCoroutine(WeaponCooltime(30.0f, time,
        () =>
        {
            atk = player.stat.atk * weaponParameter.valueSums[1];
            statBuff.Atk += atk;
            WeaponFade(true);

            weapon.PatternDelay(12.0f).OnComplete(() =>
            {
                statBuff.Atk -= atk;
                WeaponFade(false);
            });
        },
        () =>
        {
            time = 0.0f;
        }));
    }
    void Claymore_The_Unforged() //보호막 체력 {0:P}증가. 공격력이 {1:P} 증가함. 보호막이 존재할 경우 공격력 증가 효괴가 두배가 됨
    {
        statBuff.SheildPer += weaponParameter.valueSums[0];
        statBuff.KnwooDamagePer += weaponParameter.valueSums[1];
    }
    void Claymore_Song_Of_Broken_Pines() //공격력 {0:P}증가. 20초마다 일반공격 재사용 대기시간 {1:P}감소. 공격력 {2:P} 증가함. 지속 12초
    {
        float time = 0;
        statBuff.Atk += player.stat.atk * weaponParameter.valueSums[0];

        WeaponCooltime(20.0f, time,
        () =>
        {
            float atk = player.stat.atk * weaponParameter.valueSums[2];
            statBuff.BaseCooltime += weaponParameter.valueSums[1];
            statBuff.Atk += atk;
            WeaponFade(true);
            weapon.PatternDelay(12.0f).OnComplete(() =>
            {
                WeaponFade(false);
                statBuff.BaseCooltime -= weaponParameter.valueSums[1];
                statBuff.Atk -= atk;
            });
        },
        () =>
        {
            time = 0;
        });
    }
    void Spear_Dragons_Bane() //물 원소 또는 불원소가 부착된 적에게 가하는 피해가 {0:P}증가함
    {
        statBuff.Spear_Dragons_Bane = weaponParameter.valueSums[0];
        //StatCalculator.cs
    }
    void Spear_Favonius() //{0}초마다 {1:P}의 확률로 원소 에너지를 6pt 회복함
    {
        weapon.StartCoroutine(FavoniusBuff(weaponParameter.valueSums[0], weaponParameter.valueSums[1]));
    }
    void Spear_Lithic_Spear() //파티의 리월 출신 캐릭터 1명당 {0:P}만큼 공격력이 증가하고 행운이 {1}증가함
    {
        float charCount = 0;
        foreach (Character character in GameDataManager.instance.saveData.userData.selectChars)
        {
            if (character == null) continue;
            CharacterData.ParameterWithKey characterParameter = GameManager.instance.characterData.Get(character.charNum);
            if (characterParameter.nationalType == CharacterData.NationalType.Liyue)
            {
                charCount++;
            }
        }

        statBuff.Atk += player.stat.atk * weaponParameter.valueSums[0] * charCount;
        statBuff.Luck += weaponParameter.valueSums[1] * charCount;
    }
    void Spear_Deathmatch() //공격력 {0:P} 방어력 {1:P} 증가
    {
        statBuff.Atk += player.stat.atk * weaponParameter.valueSums[0];
        statBuff.Armor += player.stat.armor * weaponParameter.valueSums[1];
    }
    void Spear_Primordial_Jade_Spear() //공격력 {0:P} 피해 {1:P}증가
    {
        statBuff.Atk += player.stat.atk * weaponParameter.valueSums[0];
        statBuff.allDamageAdd(weaponParameter.valueSums[1]);
    }
    void Spear_Skyward_Spine() //행운 {0} 증가 일반공격 재사용 대기시간 12% 감소. 2초마다 가장 가까운 적 방향으로 진공 칼날을 날려 {1:P}의 피해를 줌
    {
        statBuff.Luck += weaponParameter.valueSums[0];
        statBuff.BaseCooltime += 0.12f;
        SkillData.ParameterWithKey skillParameter = skillData.skills[SkillName.Weapon_Spear_Skyward_Spine];
        skillParameter.parameter.damage = weaponParameter.valueSums[1];
        skillParameter.AddStartListener(() =>
        {
            WeaponFade();
        });
        skillParameter.AddEndListener(() =>
        {
        });
        weapon.AddWeaponSkill(SkillName.Weapon_Spear_Skyward_Spine);
    }
    void Spear_Vortex_Vanquisher() //보호막 체력 {0:P}증가. 공격력이 {1:P} 증가함. 보호막이 존재할 경우 공격력 증가 효괴가 두배가 됨
    {
        statBuff.SheildPer += weaponParameter.valueSums[0];
        statBuff.KnwooDamagePer += weaponParameter.valueSums[1];
    }
    void Spear_Staff_Of_Homa() //최대 체력 {0:P}증가. 공격력이 HP 최대치의 {1:P}만큼 증가함 HP가 50% 미만일 경우 공격력 증가효과가 두배가 됨
    {
        statBuff.Hp += player.stat.hp * weaponParameter.valueSums[0];
        statBuff.HealthDamagePer += weaponParameter.valueSums[1];
        weapon.StartCoroutine(Spear_Staff_Of_HomaBuff());
    }

    IEnumerator Spear_Staff_Of_HomaBuff()
    {
        bool isBuff = false;
        while (true)
        {
            float healthPer = player.health / GameManager.instance.statCalcuator.Health;
            if (healthPer < 0.5f && !isBuff)
            {
                statBuff.HealthDamagePer += weaponParameter.valueSums[1];
                isBuff = true;

            }
            else if (healthPer >= 0.5f && isBuff)
            {
                statBuff.HealthDamagePer -= weaponParameter.valueSums[1];
                isBuff = false;
            }

            yield return null;
        }
    }
    void Catalist_Favonius() //{0}초마다 {1:P}의 확률로 원소 에너지를 6pt 회복함
    {
        weapon.StartCoroutine(FavoniusBuff(weaponParameter.valueSums[0], weaponParameter.valueSums[1]));
    }
    void Catalist_Widsith() //30초마다 공격력 {0:P}, 피해 {1:P}, 원소마스터리 {2}중 하나가 랜덤으로 증가함. 지속 10초
    {
        float time = 0;
        float atk = 0;
        int randomNum = 0;
        weapon.StartCoroutine(WeaponCooltime(30.0f, time,
        () =>
        {
            randomNum = Random.Range(0, 3);
            switch (randomNum)
            {
                case 0:
                    atk = player.stat.atk * weaponParameter.valueSums[0];
                    statBuff.Atk += atk;
                    break;
                case 1:
                    statBuff.allDamageAdd(weaponParameter.valueSums[1]);
                    break;
                case 2:
                    statBuff.ElementMastery += weaponParameter.valueSums[2];
                    break;
            }
            WeaponFade(true);

            weapon.PatternDelay(12.0f).OnComplete(() =>
            {
                switch (randomNum)
                {
                    case 0:
                        statBuff.Atk -= atk;
                        break;
                    case 1:
                        statBuff.allDamageMinus(weaponParameter.valueSums[1]);
                        break;
                    case 2:
                        statBuff.ElementMastery -= weaponParameter.valueSums[2];
                        break;
                }
                WeaponFade(false);
            });
        },
        () =>
        {
            time = 0.0f;
        }));
    }
    void Catalist_Sacrificial() //원소전투 스킬을 사용할 때 {0:P}의 확률로 해당 스킬의 재사용 대기시간이 초기화됨. 해당 효과는 {1}초마다 발동가능
    {
        Sacrificial();
    }
    void Catalist_Eye_Of_Perception() //{0}초마다 적 사이를 4번 튕겨 {1:P}의 대미지를 주는 구체를 발사함
    {
        SkillData.ParameterWithKey skillParameter = skillData.skills[SkillName.Weapon_Catalist_Eye_Of_Perception];
        skillParameter.parameter.coolTime = weaponParameter.valueSums[0];
        skillParameter.parameter.damage = weaponParameter.valueSums[1];
        skillParameter.AddStartListener(() =>
        {
            WeaponFade();
        });
        skillParameter.AddEndListener(() =>
        {
        });
        weapon.AddWeaponSkill(SkillName.Weapon_Catalist_Eye_Of_Perception);
    }
    void Catalist_Wine_And_Song() //이동속도가 {0:P} 증가함. 이동 중 공격력이 {1:P} 증가함
    {
        statBuff.Speed += weaponParameter.valueSums[0];
        weapon.StartCoroutine(Catalist_Wine_And_SongBuff());
    }


    IEnumerator Catalist_Wine_And_SongBuff()
    {
        bool isBuff = false;
        float atk = 0;
        while (true)
        {
            Vector3 movePosition = player.inputVec;
            if (movePosition != Vector3.zero && !isBuff)
            {
                WeaponFade(true);
                atk = player.stat.atk * weaponParameter.valueSums[1];
                statBuff.Atk += atk;
                isBuff = true;
            }

            if (movePosition == Vector3.zero && isBuff)
            {
                WeaponFade(false);
                statBuff.Atk -= atk;
                isBuff = false;
            }
            yield return null;
        }
    }
    void Catalist_Solar_Pearl() //피해 {0:P}증가
    {
        statBuff.allDamageAdd(weaponParameter.valueSums[0]);
    }
    void Catalist_Skyward_Atlas() //원소 피해 보너스 {0:P} 증가. 30초마다 가장 가까운 적을 향해 발사해 {1:P}의 피해를 주는 투사체를 지속적으로 생성함. 지속 15초
    {
        statBuff.elementalDamageAdd(weaponParameter.valueSums[0]);
        SkillData.ParameterWithKey skillParameter = skillData.skills[SkillName.Weapon_Catalist_Skyward_Atlas];
        skillParameter.parameter.damage = weaponParameter.valueSums[1];
        skillParameter.AddStartListener(() =>
        {
            WeaponFade(true);
        });
        skillParameter.AddEndListener(() =>
        {
            WeaponFade(false);
        });
        weapon.AddWeaponSkill(SkillName.Weapon_Catalist_Skyward_Atlas);
    }
    void Catalist_Lost_Prayer_To_The_Sacered_Winds() //이동속도 10%, 피해 {1:P}증가
    {
        statBuff.Speed += 0.1f;
        statBuff.allDamageAdd(weaponParameter.valueSums[0]);
    }
    void Catalist_Memory_Of_Dust() //보호막 체력이 {0:P}증가함. 공격력이 {1:P} 증가함. 보호막이 존재하면 공격력 증가 효과가 두배가 됨
    {
        statBuff.SheildPer += weaponParameter.valueSums[0];
        statBuff.KnwooDamagePer += weaponParameter.valueSums[1];
    }
    void Bow_Favonius() //{0}초마다 {1:P}의 확률로 원소 에너지를 6pt 회복함
    {
        weapon.StartCoroutine(FavoniusBuff(weaponParameter.valueSums[0], weaponParameter.valueSums[1]));
    }
    void Bow_The_Stringless() //원소 전투스킬과 원소폭발 피해가 {0:P} 증가함
    {
        statBuff.SkillDamage += weaponParameter.valueSums[0];
        statBuff.BurstDamage += weaponParameter.valueSums[0];
    }
    void Bow_Sacrificial_Bow() //원소전투 스킬을 사용할 때 {0:P}의 확률로 해당 스킬의 재사용 대기시간이 초기화됨. 해당 효과는 {1}초마다 발동가능
    {
        Sacrificial();
    }
    void Bow_Rust() //일반 공격 피해 {0:P} 증가함
    {
        statBuff.BaseDamage += weaponParameter.valueSums[0];
    }
    void Bow_Alley_Hunter() //무기를 장착한 캐릭터가 메인캐릭터가 아닐경우 피해 {0:P} 증가함
    {
        if (GameDataManager.instance.saveData.userData.selectChars[0].charNum == character.charNum) return;
        statBuff.allDamageAdd(weaponParameter.valueSums[0]);
    }
    void Bow_Mitternachts_Waltz() //일반 공격 피해와 원소전투 스킬의 피해가 {0:P} 증가함
    {
        statBuff.BaseDamage += weaponParameter.valueSums[0];
        statBuff.SkillDamage += weaponParameter.valueSums[0];
    }
    void Bow_Viridescent_Hunt() //{0} 초마다 제일 가까운 적 방향에 적을 끌어당기고 공격력의 {1:P}의 지속피해를 주는 공격을함. 지속 4초
    {
        SkillData.ParameterWithKey skillParameter = skillData.skills[SkillName.Weapon_Bow_Viridescent_Hunt];
        skillParameter.parameter.coolTime = weaponParameter.valueSums[0];
        skillParameter.parameter.damage = weaponParameter.valueSums[1];
        skillParameter.AddStartListener(() =>
        {
            WeaponFade(true);
        });
        skillParameter.AddEndListener(() =>
        {
            WeaponFade(false);
        });
        weapon.AddWeaponSkill(SkillName.Weapon_Bow_Viridescent_Hunt);
    }
    void Bow_SkyWard_Harp() //행운 {0} 증가 {1}초마다 명중 시 {2:P}의 범위피해를 주는 화살을 발사함
    {
        statBuff.Luck += weaponParameter.valueSums[0];
        SkillData.ParameterWithKey skillParameter = skillData.skills[SkillName.Weapon_Bow_SkyWard_Harp];
        skillParameter.parameter.coolTime = weaponParameter.valueSums[1];
        skillParameter.parameter.damage = weaponParameter.valueSums[2];
        skillParameter.AddStartListener(() =>
        {
            WeaponFade();
        });
        skillParameter.AddEndListener(() =>
        {
        });
        weapon.AddWeaponSkill(SkillName.Weapon_Bow_SkyWard_Harp);
    }
    void Bow_Amos_Bow() //일반공격 피해 {0:P}증가 일반공격이 원거리 공격일 경우 해당 효과 4배 증가
    {
        float damage = weaponParameter.valueSums[0];

        CharacterData.Name charname = GameDataManager.instance.saveData.userData.selectChars[0].charNum;
        CharacterData.ParameterWithKey characterParam = GameManager.instance.characterData.Get(charname);
        SkillData.ParameterWithKey baseAttack = skillData.skills[characterParam.skillBasic];
        if (baseAttack.name == SkillName.Basic_Arrow ||
        baseAttack.name == SkillName.Basic_Klee ||
        baseAttack.name == SkillName.Basic_Ganyu ||
        baseAttack.name == SkillName.Basic_Mona ||
        baseAttack.name == SkillName.Basic_Catalist)
        {
            damage *= 4.0f;
        }
        statBuff.BaseDamage += damage;
    }
    void Bow_Elegy_For_The_End() //원소마스터리가 {0}증가함 20초마다 원소마스터리 {1} 증가, 공격력 {2:P}증가함 지속 12초
    {
        float time = 0;
        statBuff.ElementMastery += weaponParameter.valueSums[0];
        weapon.StartCoroutine(WeaponCooltime(20.0f, time,
        () =>
        {
            statBuff.ElementMastery += weaponParameter.valueSums[1];
            WeaponFade(true);

            weapon.PatternDelay(12.0f).OnComplete(() =>
            {
                statBuff.ElementMastery -= weaponParameter.valueSums[1];
                WeaponFade(false);
            });
        },
        () =>
        {
            time = 0.0f;
        }));
    }
    void Bow_Polar_Star() //원소전투 스킬과 원소폭발의 피해 {0:P}증가. 공격력 {1:P} 증가
    {
        statBuff.SkillDamage += weaponParameter.valueSums[0];
        statBuff.BurstDamage += weaponParameter.valueSums[0];
        statBuff.Atk += player.stat.atk * weaponParameter.valueSums[1];
    }

}