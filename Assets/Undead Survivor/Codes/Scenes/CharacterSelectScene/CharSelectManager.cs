using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Linq;
using TMPro;

public class CharSelectManager : MonoBehaviour
{
    [Header("# Sprite")]
    public Sprite[] textIcons;
    public Sprite[] elementIcons;
    private Sprite slotSpriteDefault;

    [Header("# Button")]
    public Button btnBack;
    public Button btnNext;

    [Header("# Slot")]
    public GameObject[] charSlot;

    private int selectSlot = 0;
    private int selectCharNum = 0;

    [Header("# List")]
    public GameObject panel;
    private ImageText[] imageTexts;
    private List<GameObject> characterObjects;
    private List<List<GameObject>> charArray;

    public CharacterData characterData;

    [Header("# Prefab")]
    public GameObject charPrefab;
    public GameObject imageTextPrefab;

    [Header("# Stella")]
    public Toggle[] stellaToggles;

    [Header("# discription")]
    public Image imgDiscription;
    public TextMeshProUGUI nameDiscription;
    public Transform contentDiscription;

    [Header("# weapon")]
    public Image imageWeapon;
    public Button btnWeapon;
    Character selectedCharacterData;
    int selectedCharacterIndex = 0;
    public WeaponData weaponData;


    [Header("# element")]
    public DropdownElement dropdownElement;

    Rewired.Player rewiredPlayer;
    private void Awake()
    {
        rewiredPlayer = Rewired.ReInput.players.GetPlayer(0);
        Init();
        slotSpriteDefault = charSlot[0].GetComponentsInChildren<Image>()[1].sprite;
        SetupCharSlotButtons();
        btnBack.onClick.AddListener(OnClickBack);
        btnNext.onClick.AddListener(OnClickNext);
        btnNext.interactable = false;
        InitWeaponButton();
    }

    void Update()
    {
        if (rewiredPlayer.GetButtonDown("Square"))
        {
            if (selectSlot != -1)
            {
                charSlot[selectSlot].GetComponentsInChildren<Button>(true)[1].onClick.Invoke();
            }
        }
    }
    private void Start()
    {

        dropdownElement.dropdown.onValueChanged.AddListener((index) =>
        {
            if (selectedCharacterData.charNum != CharacterData.Name.Travler_FEMALE
             && selectedCharacterData.charNum != CharacterData.Name.Travler_MALE) return;
            Element.Type elementType = dropdownElement.availableElements[index];
            CharacterData.ParameterWithKey characterParameter = characterData.Get(selectedCharacterData.charNum);
            characterParameter.elementType = elementType;
            selectedCharacterData.toolTipKey = "Travler.".AddString(elementType.ToString());
            selectedCharacterData.elementType = elementType;
            switch (elementType)
            {
                case Element.Type.Anemo:
                    characterParameter.skill = SkillName.E_Travler_Anemo;
                    break;
                case Element.Type.Geo:
                    characterParameter.skill = SkillName.E_Travler_Geo;
                    break;
            }
            UpdateTooltip(selectedCharacterData, selectedCharacterIndex);
        });
    }

    private void SetupCharSlotButtons()
    {
        for (int i = 0; i < charSlot.Length; i++)
        {
            int idx = i;
            charSlot[idx].GetComponentsInChildren<Button>()[0].onClick.AddListener(() =>
            {
                AudioManager.instance.PlaySFX(AudioManager.SFX.Click);
                selectSlot = idx;
                Character character = GameDataManager.instance.saveData.userData.selectChars[idx];
                HandleSelectedCharacter(character, (int)character.charNum);
            });
            charSlot[idx].GetComponentsInChildren<Button>(true)[1].onClick.AddListener(() =>
            {
                AudioManager.instance.PlaySFX(AudioManager.SFX.Click);
                ResetCharSlot(idx);
            });
        }
    }

    private void ResetCharSlot(int idx)
    {
        charSlot[idx].GetComponentsInChildren<Image>()[1].sprite = slotSpriteDefault;
        charSlot[idx].GetComponentsInChildren<Button>(true)[1].gameObject.SetActive(false);
        GameDataManager.instance.saveData.userData.selectChars[idx] = null;
        SetCharacterSlot();
        NextButtonInteraction();
    }

    void NextButtonInteraction()
    {
        bool result = false;
        foreach (Character character in GameDataManager.instance.saveData.userData.selectChars)
        {
            if (character != null) result = true;
        }

        btnNext.interactable = result;
    }

    void InitWeaponButton()
    {
        btnWeapon.onClick.AddListener(() =>
        {
            if (selectedCharacterData == null) return;
            WeaponManager.instance.InitUIWithPlayer(characterData.Get(selectedCharacterData.charNum).weaponType);
            WeaponManager.instance.onSelect = (weapon) =>
            {
                if (weapon == null)
                {
                    imageWeapon.sprite = null;
                    selectedCharacterData.weaponName = WeaponName.None_Weapoen;

                    UpdateTooltip(selectedCharacterData, selectedCharacterIndex);
                    return;
                }
                imageWeapon.sprite = weapon.icon;
                selectedCharacterData.weaponName = weapon.weaponName;

                UpdateTooltip(selectedCharacterData, selectedCharacterIndex);
            };
        });
    }

    void OnClickBack()
    {
        AudioManager.instance.PlaySFX(AudioManager.SFX.Click);
        GameDataManager.instance.SaveInstance();
        MoveMain();
    }

    void OnClickNext()
    {
        AudioManager.instance.PlaySFX(AudioManager.SFX.Click);
        SortCharacters();
        GameDataManager.instance.SaveInstance();
        SceneManager.LoadScene("MapSelectScene");
    }

    void SortCharacters()
    {
        Character[] characters = { null, null, null, null };
        int index = 0;
        foreach (Character character in GameDataManager.instance.saveData.userData.selectChars)
        {
            if (character == null) continue;
            characters[index] = character;
            index++;
        }
        GameDataManager.instance.saveData.userData.selectChars = characters;
    }

    void Init()
    {
        InitUI();
    }

    void InitUI()
    {
        InitializeVariables();
        SetupCharacterObjects(CharacterData.Type.Player);
        SetupCharacterObjects(CharacterData.Type.Rare);
        SetupCharacterObjects(CharacterData.Type.Normal);
        SetupCharacterObjectsOnNotMine(CharacterData.Type.Rare);
        SetupCharacterObjectsOnNotMine(CharacterData.Type.Normal);
        SetupStellaToggles();
    }

    private void InitializeVariables()
    {
        characterObjects = new List<GameObject>();
        for (int i = 0; i < 4; i++)
        {
            GameDataManager.instance.saveData.userData.selectChars[i] = null;
        }
        imageTexts = contentDiscription.GetComponentsInChildren<ImageText>(true);
    }

    private void SetupCharacterObjects(CharacterData.Type rarityType)
    {
        for (int i = 0; i < GameDataManager.instance.saveData.charactors.Count; i++)
        {
            int idx = i;
            if (characterData.characters[idx].type != rarityType) continue;
            GameObject charObject = Instantiate(charPrefab, new Vector3(0, 0, 0), Quaternion.identity);
            Character character = GameDataManager.instance.saveData.charactors[idx];
            Sprite charSprite = characterData.characters[idx].icon;

            if (characterData.characters[idx].stat.isMine)
            {
                SetupCharButton(charObject, charSprite, character, idx);
                charObject.transform.parent = panel.transform;
                characterObjects.Add(charObject);
            }
        }
    }
    private void SetupCharacterObjectsOnNotMine(CharacterData.Type rarityType)
    {
        for (int i = 0; i < GameDataManager.instance.saveData.charactors.Count; i++)
        {
            int idx = i;
            if (idx == UserData.AETHER || idx == UserData.LUMINE) continue;
            if (characterData.characters[idx].type != rarityType) continue;

            Character character = GameDataManager.instance.saveData.charactors[idx];
            GameObject charObject = Instantiate(charPrefab, new Vector3(0, 0, 0), Quaternion.identity);

            Sprite charSprite = characterData.characters[idx].icon;

            if (!characterData.characters[idx].stat.isMine)
            {
                SetupCharButton(charObject, charSprite, character, idx);
                charObject.transform.parent = panel.transform;
                characterObjects.Add(charObject);
            }
        }
    }

    private void SetupCharButton(GameObject charObject, Sprite charSprite, Character character, int idx)
    {
        charObject.GetComponent<CharButton>().Init(charSprite, character.Name(), character, idx, (selectedCharacter, selectedIndex) =>
        {
            HandleSelectedCharacter(selectedCharacter, selectedIndex);
        });
    }

    private void SetupStellaToggles()
    {
        for (int i = 0; i < stellaToggles.Length; i++)
        {
            int idx = i;
            stellaToggles[idx].onValueChanged.AddListener((bool isStella) =>
            {
                ToggleStella(idx, isStella);
            });
        }
    }
    private void ToggleStella(int idx, bool isStella)
    {
        GameDataManager.instance.saveData.charactors[selectCharNum].constellation[idx] = isStella;
        UpdateCharacterUI(GameDataManager.instance.saveData.charactors[selectCharNum], selectCharNum);
    }


    void SetTooltip(List<string> tooltips, int characterIndex)
    {
        for (int i = 0; i < tooltips.Count; i++)
        {
            SetTooltipContent(i, tooltips[i], characterIndex);
        }
    }

    void SetTooltipStatInfo()
    {
        UpgradeComponent[] upComps = GameDataManager.instance.saveData.userData.upgrade.upgradeComponents;
        int imageTextIndex = 1; //0은 속성
        foreach (UpgradeComponent upComp in upComps)
        {
            string targetText = "Upgrade.Name.".AddString(upComp.key).Localize();
            string targetTextInfo = upComp.GetTooltipInfo();
            ImageText imageText = imageTexts[imageTextIndex];
            imageText.mouseOverText = targetTextInfo;
            imageTextIndex++;
        }
    }


    private void SetTooltipContent(int index, string tooltip, int characterIndex)
    {
        if (!imageTexts[index].gameObject.activeSelf)
        {
            imageTexts[index].gameObject.SetActive(true);
        }
        imageTexts[index].textMesh.text = tooltip;
        if (index < textIcons.Length)
        {
            imageTexts[index].image.sprite = textIcons[index];
        }
        else
        {
            imageTexts[index].image.color = new Color(1, 1, 1, 0);
        }
        int elementSprite = (int)characterData.characters[characterIndex].elementType;
        imageTexts[0].image.sprite = elementIcons[elementSprite];
    }

    void MoveMain()
    {
        SceneManager.LoadScene("MainScene");
    }
    private void HandleSelectedCharacter(Character selectedCharacter, int characterIndex)
    {
        Sprite selectedCharacterIcon = characterData.characters[characterIndex].icon;
        selectedCharacterData = selectedCharacter;
        selectedCharacterIndex = characterIndex;
        selectCharNum = characterIndex;
        UpdateCharacterUI(selectedCharacter, characterIndex);
        if (!selectedCharacter.isMine) return;
        // 이미 선택한 캐릭터인 경우 return
        if (GameDataManager.instance.saveData.userData.selectChars
        .Any(c => c?.Name() == selectedCharacter.Name()))
        {
            return;
        }

        for (int i = 0; i < 4; i++)
        {
            Character selectChar = GameDataManager.instance.saveData.userData.selectChars[i];
            if (selectChar != null && selectChar.Name() == selectedCharacter.Name())
            {
                ResetCharSlot(i);
            }
        }


        UpdateCharSlot(selectSlot, selectedCharacterIcon, selectedCharacter);
        SetCharacterSlot();
    }
    void SetCharacterSlot()
    {
        selectSlot = 0;
        foreach (Character character in GameDataManager.instance.saveData.userData.selectChars)
        {
            if (character == null) return;
            selectSlot++;
        }

        selectSlot = -1;
    }

    private void UpdateCharacterUI(Character selectedCharacter, int characterIndex)
    {
        bool[] constellation = selectedCharacter.constellation;
        int constellaCount = selectedCharacter.constellaCount;
        string name = selectedCharacter.Name();
        Sprite selectedCharacterIcon = characterData.characters[characterIndex].icon;

        if (selectedCharacterData.weaponName != WeaponName.None_Weapoen)
        {
            imageWeapon.sprite = weaponData.Get(selectedCharacterData.weaponName).icon;
        }
        else
        {
            imageWeapon.sprite = slotSpriteDefault;
        }
        imgDiscription.sprite = selectedCharacterIcon;

        UpdateStellaToggles(constellation, constellaCount);
        UpdateNameDiscription(name, characterIndex);
        UpdateTooltip(selectedCharacter, characterIndex);
        UpdateDropdownElement(selectedCharacter, characterIndex);
    }

    private void ResetSameCharacter(Character selectedCharacter)
    {
        for (int i = 0; i < 4; i++)
        {
            Character selectChar = GameDataManager.instance.saveData.userData.selectChars[i];
            if (selectChar != null && selectChar.Name() == selectedCharacter.Name())
            {
                ResetCharSlot(i);
            }
        }
    }

    private void UpdateCharSlot(int selectSlot, Sprite selectedCharacterIcon, Character selectedCharacter)
    {
        if (selectSlot == -1) return;
        charSlot[selectSlot].GetComponentsInChildren<Image>()[1].sprite = selectedCharacterIcon;
        Button button = charSlot[selectSlot].GetComponentsInChildren<Button>(true)[1];
        button.gameObject.SetActive(true);
        GameDataManager.instance.saveData.userData.selectChars[selectSlot] = selectedCharacter;

        NextButtonInteraction();
    }

    private void UpdateStellaToggles(bool[] constellation, int constellaCount)
    {
        for (int i = 0; i < constellation.Length; i++)
        {
            stellaToggles[i].interactable = (i < constellaCount);
            stellaToggles[i].isOn = constellation[i];
        }
    }

    private void UpdateNameDiscription(string name, int characterIndex)
    {
        nameDiscription.text = name;
        if (characterIndex == UserData.AETHER || characterIndex == UserData.LUMINE)
        {
            nameDiscription.text = GameDataManager.instance.saveData.userData.name;
        }
    }

    private void UpdateTooltip(Character selectedCharacter, int characterIndex)
    {
        StatCalculator statCalcuator = new StatCalculator(selectedCharacter).WeaponData(weaponData.Get(selectedCharacter.weaponName));
        SetTooltip(statCalcuator.ToolTip(), characterIndex);
        SetTooltipStatInfo();
    }

    private void UpdateDropdownElement(Character selectedCharacter, int characterIndex)
    {
        List<Element.Type> elemetalTypes = new List<Element.Type>();
        if (selectedCharacter.charNum == CharacterData.Name.Travler_FEMALE
         || selectedCharacter.charNum == CharacterData.Name.Travler_MALE)
        {
            elemetalTypes.Add(Element.Type.Anemo);
            elemetalTypes.Add(Element.Type.Geo);
            dropdownElement.ResetDropdown(elemetalTypes);
            switch (selectedCharacter.elementType)
            {
                case Element.Type.Anemo:
                    dropdownElement.dropdown.value = 0;
                    break;
                case Element.Type.Geo:
                    dropdownElement.dropdown.value = 1;
                    break;
            }
        }
        else
        {
            CharacterData.ParameterWithKey characterParameter = characterData.Get(selectedCharacter.charNum);
            elemetalTypes.Add(characterParameter.elementType);
            dropdownElement.ResetDropdown(elemetalTypes);
        }
    }
}
