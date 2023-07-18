
using System;
[System.Serializable]

public class UpgradeComponent
{

    //공격력
    public int cnt = 0;
    public int unit = 5;
    public int unitAdd = 0;
    public int max = 5;
    public int price = 500;
    public string key = "defaultKey";
    public bool percent;

    public UpgradeComponent(int _cnt = 0, int _unit = 5, int _unitAdd = 0, int _max = 5, int _price = 500, string key = "defaultKey", bool _percent = false)
    {
        cnt = _cnt;
        unit = _unit;
        unitAdd = _unitAdd;
        max = _max;
        price = _price;
        this.key = key;
        percent = _percent;
    }
    public float GetValue()
    {
        return (float)(percent ? cnt * unit / 100.0f : cnt * unit);
    }


    public string GetTooltip(float val, float upValue)
    {
        float up = upValue - val;
        string tooltipKey = "Upgrade.Name.".AddString(key);
        string tooltip = string.Format("{0}: {1}(+{2})\n", tooltipKey.Localize(), val.FormatDecimalPlaces(), up.FormatDecimalPlaces());
        return tooltip;
    }

    public string GetTooltipInfo()
    {
        string tooltipKey = "Upgrade.Name.Discription.".AddString(key).Localize();
        return tooltipKey;
    }

    public string GetDiscription()
    {
        string tooltipKey = string.Format("{0}{1}", "Upgrade.Discription.", key);
        return tooltipKey.Localize();
    }

}