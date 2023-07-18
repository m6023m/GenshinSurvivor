using System.Collections;
using System.Collections.Generic;
[System.Serializable]
public class WeaponStat
{
    public WeaponType weaponType;
    public WeaponName weaponName;
    public int level;
    public int rank;
    public WeaponStat()
    {

    }
    public WeaponStat(WeaponType weaponType, WeaponName weaponName)
    {
        this.weaponType = weaponType;
        this.weaponName = weaponName;
    }
}