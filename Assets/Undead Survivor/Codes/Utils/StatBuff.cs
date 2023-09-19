using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class StatBuff
{
    public float atk = 0.0f;
    public float armor = 0.0f;
    public float hp = 0.0f;
    public float heal = 0.0f;
    public float cooltime = 0.0f;
    public float area = 0.0f;
    public float aspeed = 0.0f;
    public float duration = 0.0f;
    public float amount = 0.0f;
    public float speed = 0.0f;
    public float magnet = 0.0f;
    public float luck = 0.0f;
    public float regen = 0.0f;
    public float exp = 0.0f;
    public float greed = 0.0f;
    public float curse = 0.0f;
    public float resurration = 0.0f;
    public float reroll = 0.0f;
    public float skip = 0.0f;
    public float physicsDmg = 0.0f;
    public float pyroDmg = 0.0f;
    public float hydroDmg = 0.0f;
    public float anemoDmg = 0.0f;
    public float dendroDmg = 0.0f;
    public float electroDmg = 0.0f;
    public float cyroDmg = 0.0f;
    public float geoDmg = 0.0f;
    public float baseCooltime = 0.0f;
    public float baseDamage = 0.0f;
    public float skillDamage = 0.0f;
    public float burstDamage = 0.0f;
    public float sheildPer = 0.0f;
    public float knwooDamagePer = 0.0f;
    public float healthDamagePer = 0.0f;
    public void allDamageAdd(float value)
    {
        physicsDmg += value;
        pyroDmg += value;
        hydroDmg += value;
        anemoDmg += value;
        dendroDmg += value;
        electroDmg += value;
        cyroDmg += value;
        geoDmg += value;
    }
    public void allDamageMinus(float value)
    {
        physicsDmg -= value;
        pyroDmg -= value;
        hydroDmg -= value;
        anemoDmg -= value;
        dendroDmg -= value;
        electroDmg -= value;
        cyroDmg -= value;
        geoDmg -= value;
    }
    public void elementalDamageAdd(float value)
    {
        pyroDmg += value;
        hydroDmg += value;
        anemoDmg += value;
        dendroDmg += value;
        electroDmg += value;
        cyroDmg += value;
        geoDmg += value;
    }
    public void elementalDamageMinus(float value)
    {
        pyroDmg -= value;
        hydroDmg -= value;
        anemoDmg -= value;
        dendroDmg -= value;
        electroDmg -= value;
        cyroDmg -= value;
        geoDmg -= value;
    }
    public float elementMastery = 0.0f;
    public bool isResonance = false;
    public bool isThundering_FuryCooltime = false;
    public float eulaSkillStack = 0;
    public float eulaStack = 0;
    public bool hutaoConstell5 = false;
    public bool allCritical = false;

    public float Sword_Lions_Loar = 0;
    public float Claymore_Rainslasher = 0;
    public float Spear_Dragons_Bane = 0;
    public float Claymore_Serpent_SpineStack = 0;
    public float Claymore_Serpent_SpineDamage = 0;
    public float Claymore_Serpent_SpineReceiveDamage = 0;
    public float Claymore_Skyward_PrideStack = 0;

    public int E_Travler_Electro = 1;
}
