using System;
using System.Collections;
using System.Collections.Generic;
[System.Serializable]
public class SaveData
{
    public int currentVersion = 1;
    public UserData userData;
    public Record record;
    public Option option;
    public AchieveData achieveData;
    public List<Character> charactors;
    public Dictionary<WeaponName, WeaponStat> weaponStats;
    public GameInfoData gameInfoData;
    public SaveData(UserData _userData,
     Option _option,
      AchieveData _archiveData,
       List<Character> _charactors,
        Record _record,
         GameInfoData _gameInfoData,
         Dictionary<WeaponName, WeaponStat> _weaponStats)
    {
        userData = _userData;
        option = _option;
        charactors = _charactors;
        achieveData = _archiveData;
        record = _record;
        gameInfoData = _gameInfoData;
        weaponStats = _weaponStats;
    }
    public SaveData()
    {
        userData = new UserData();
        option = new Option();
        achieveData = new AchieveData();
        record = new Record();
        charactors = new List<Character>();
        gameInfoData = new GameInfoData();
        weaponStats = new Dictionary<WeaponName, WeaponStat>();
    }

    public void InitCharacter()
    {

        charactors = new List<Character>();
        //여행자
        charactors.Add(InitChar.Default0());
        charactors.Add(InitChar.Default1());
        //여행자 이외
        charactors.Add(InitChar.Amber());
        charactors.Add(InitChar.Xiangling());
        charactors.Add(InitChar.Bennet());
        charactors.Add(InitChar.Diluc());
        charactors.Add(InitChar.Klee());
        charactors.Add(InitChar.Barbara());
        charactors.Add(InitChar.Xingqiu());
        charactors.Add(InitChar.Mona());
        charactors.Add(InitChar.Sucrose());
        charactors.Add(InitChar.Jean());
        charactors.Add(InitChar.Venti());
        charactors.Add(InitChar.Lisa());
        charactors.Add(InitChar.Razor());
        charactors.Add(InitChar.Beidou());
        charactors.Add(InitChar.Fischl());
        charactors.Add(InitChar.Keqing());
        charactors.Add(InitChar.Kaeya());
        charactors.Add(InitChar.Chongyun());
        charactors.Add(InitChar.Qiqi());
        charactors.Add(InitChar.Ningguang());
        charactors.Add(InitChar.Noelle());
    }

    public void InitWeapons()
    {
        if (weaponStats == null)
        {
            weaponStats = new Dictionary<WeaponName, WeaponStat>();
        }
        foreach (WeaponName weapon in Enum.GetValues(typeof(WeaponName)))
        {
            string type = weapon.ToString().Split('_')[0];
            WeaponType weaponType = (WeaponType)Enum.Parse(typeof(WeaponType), type);
            WeaponStat newWeaponStat = new WeaponStat(weaponType, weapon);
            weaponStats.AddOrNotthing(weapon, newWeaponStat);
        }
    }

    public Character GetCharacter(string name)
    {
        foreach (Character character in charactors)
        {
            if (character.toolTipKey == name)
            {
                return character;
            }
        }
        return InitChar.Default0();
    }

    public void AddNewCharacterOnVersionChange(int version, int currentVersion)
    {
        if (currentVersion < version && currentVersion < 2)
        {
            charactors.Add(InitChar.Tartaglia());
            charactors.Add(InitChar.Diona());
            charactors.Add(InitChar.Zhongli());
            charactors.Add(InitChar.Xinyan());
            charactors.Add(InitChar.Albedo());
            charactors.Add(InitChar.Ganyu());
            GameDataManager.instance.saveData.record.formPlayCount = 0;
        }
        if (currentVersion < version && currentVersion < 3)
        {
            charactors.Add(InitChar.Xiao());
            charactors.Add(InitChar.Hutao());
            charactors.Add(InitChar.Rosaria());
            charactors.Add(InitChar.Yanfei());
            charactors.Add(InitChar.Eula());
            charactors.Add(InitChar.Kazuha());
            GameDataManager.instance.saveData.record.formPlayCount = 0;
        }
        if (currentVersion < version && currentVersion < 4)
        {
            GameDataManager.instance.saveData.record.formPlayCount = 0;
        }
        if (currentVersion < version && currentVersion < 5)
        {
            charactors.Add(InitChar.Ayaka());
            charactors.Add(InitChar.Yoimiya());
            charactors.Add(InitChar.Sayu());
            charactors.Add(InitChar.Raiden());
            charactors.Add(InitChar.Sara());
            charactors.Add(InitChar.Kokomi());
            charactors.Add(InitChar.Ito());
            charactors.Add(InitChar.Goro());
            charactors.Add(InitChar.Thoma());
            charactors.Add(InitChar.Shenhe());
            charactors.Add(InitChar.Yunjin());
            charactors.Add(InitChar.Miko());
            charactors.Add(InitChar.Ayato());
            charactors.Add(InitChar.Yelan());
            charactors.Add(InitChar.Heizo());
            charactors.Add(InitChar.Shinobu());
            GameDataManager.instance.saveData.record.formPlayCount = 0;
        }

        this.currentVersion = version;
    }
}
