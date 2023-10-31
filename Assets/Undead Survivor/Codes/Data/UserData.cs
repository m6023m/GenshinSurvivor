using System.Collections;
using System.Collections.Generic;
[System.Serializable]
public class UserData
{
    public int defaultChar = -1; //-1: none, 0: aether, 1:lumine 
    public string name = "";
    public static int AETHER = 0;
    public static int LUMINE = 1;
    public Upgrade upgrade;

    public long mora = 0;
    public long primoGem = 0;
    public bool isPity = false;
    public bool isPityWeapon = false;
    public Character[] selectChars;
    public List<CharacterData.Name> getCharacters;
    public int stageLevel = 0;
    public int currentMapNumber = 0;
    public float stageHP
    {
        get
        {
            return 0.4f + (stageLevel * 0.3f);
        }
    }

    public float stageATK
    {
        get
        {
            return 0.4f + (stageLevel * 0.3f);
        }
    }

    public UserData(int _defaultChar, Upgrade _upgrade, long _mora, long _primoGem, Character[] _selectChars, List<CharacterData.Name> _getCharacters)
    {
        defaultChar = _defaultChar;
        upgrade = _upgrade;
        mora = _mora;
        primoGem = _primoGem;
        selectChars = _selectChars;
        getCharacters = _getCharacters;
    }

    public UserData()
    {
        upgrade = new Upgrade();
        mora = 0;
        primoGem = 0;
        selectChars = new Character[4];
        getCharacters = new List<CharacterData.Name>();
    }
}
