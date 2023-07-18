using System;
using System.Collections;
using System.Collections.Generic;
public enum Achieve
{
    Lead_Wind_6,
    Dead_1557_1,
    Game_Start_1,
    Tree_Chicken_1
}
public static class AchieveExtensions
{
    public static String GetLocalizationExpanation(this Achieve achieve)
    {
        return "Achieve.Expanation.".AddString(achieve.ToString()).Localize();
    }

    public static String GetLocalizationName(this Achieve achieve)
    {
        return "Achieve.Name.".AddString(achieve.ToString()).Localize();
    }

    public static int GetMaxAchieveCount(this Achieve achieve)
    {
        string textAchieve = achieve.ToString();
        string lastChar = textAchieve[textAchieve.Length - 1].ToString();
        return int.Parse(lastChar);
    }
}
[Serializable]
public class AchieveData
{
    public Dictionary<Achieve, int> achieve;
    Dictionary<Achieve, int> _allAchieves;
    Dictionary<Achieve, int> checkAchieveList;
    public Dictionary<Achieve, int> allAchieves
    {
        get
        {
            if (_allAchieves == null)
            {
                _allAchieves = new Dictionary<Achieve, int>();
                foreach (Achieve achieve in (Achieve[])Enum.GetValues(typeof(Achieve)))
                {
                    _allAchieves.Add(achieve, 0);
                }
            }
            foreach (KeyValuePair<Achieve, int> achieve in achieve)
            {
                _allAchieves[achieve.Key] = achieve.Value;
            }
            return _allAchieves;
        }
    }


    public AchieveData()
    {
        achieve = new Dictionary<Achieve, int>();
    }
    public AchieveData(Dictionary<Achieve, int> _achieves)
    {
        achieve = _achieves;
    }

    public void AttainmentAchieve(Achieve achieve, Dictionary<Achieve, int> checkAchieves)
    {
        if (!this.achieve.ContainsKey(achieve))
        {
            this.achieve.Add(achieve, 0);
        }
        if (this.achieve[achieve] < achieve.GetMaxAchieveCount())
        {
            checkAchieves[achieve]++;
            this.achieve[achieve] = checkAchieves[achieve];
        }
    }

    public bool AvailabeCheckAchieve(Achieve achieve)
    {
        if (this.achieve.ContainsKey(achieve))
        {
            if (this.achieve[achieve] >= achieve.GetMaxAchieveCount()) return false;
        }
        return true;
    }
    public Dictionary<Achieve, int> GetCheckAchieveList()
    {
        checkAchieveList = new Dictionary<Achieve, int>();
        Dictionary<Achieve, int> attainmentedAchieve = GameDataManager.instance.saveData.achieveData.achieve;
        foreach (KeyValuePair<Achieve, int> achieve in allAchieves)
        {
            if (achieve.Value < achieve.Key.GetMaxAchieveCount())
            {
                checkAchieveList.Add(achieve.Key, achieve.Value);
            }
        }
        return checkAchieveList;
    }

}