using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.Video;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;
using DG.Tweening;
public class WishManager : MonoBehaviour
{
    [Header("# Rewiered")]
    Rewired.Player rewiredPlayer;
    public enum WishType
    {
        Noraml,
        Rare
    }

    enum Wish
    {
        Character,
        Weapon
    }

    Wish wish;
    public Sprite[] wishImage;

    public TextMeshProUGUI textPrimoGem;
    public TextMeshProUGUI textWishTitle;
    public TextMeshProUGUI textWishName;
    public Button buttonWish1;
    public Button buttonWish10;
    public Button buttonWishClose;
    public Button buttonWishVideo;
    public Button buttonWishChange;

    public GameObject wishResultPanel;
    public Button buttonWishResultClose;
    public SwipePanel swipePanel;
    public Image imgWishRarity;
    public Image imgWishMain;
    public Image imgWishElement;
    public Image imgWishBackground;
    public Image[] imgWishSub;

    public GameObject wishTypePannel;
    public GameObject wishTitlePannelPrefab;

    [Header("Wish Result")]
    public GameObject wishCharacterSlot;
    public GameObject wishCharacterSlotPrefab;
    List<GameObject> wishSlotPools;

    public WishType wishType;
    public Material[] materialOutlines;
    public Sprite[] iconElements;
    public Sprite[] iconRarity;
    public Sprite[] bgElements;
    public PickUpTarget[] pickUpTargets;
    public PickUpTargetWeapon[] pickUpWeapons;
    Toggle[] wishToggles;

    public CharacterData characterData;
    List<CharacterData.ParameterWithKey> pickableCharacterList;
    List<CharacterData.ParameterWithKey> pickUpResult;

    public WeaponData weaponData;
    List<WeaponData.Parameter> pickableWeaponList;
    List<WeaponData.Parameter> pickUpWeaponResult;

    EventSystem eventSystem;



    int selectPickUp = 0;
    List<Toggle> togglePickUpPannels;

    [Serializable]
    public class PickUpTarget
    {
        public CharacterData.Name[] names;
    }

    [Serializable]
    public class PickUpTargetWeapon
    {
        public WeaponName[] names;
    }

    public VideoPlayer wishVideoPlayer;
    public VideoClip[] wishVideoClips;
    int WISH_1 = 160;
    int WISH_10 = 1600;
    int RARE_PER = 10;
    void Awake()
    {
        eventSystem = EventSystem.current;
        SetPrimogem();
        rewiredPlayer = Rewired.ReInput.players.GetPlayer(0);
        wishSlotPools = new List<GameObject>();
        wishVideoPlayer.loopPointReached += OnVideoEnd;
        buttonWish1.onClick.AddListener(() => ClickWish(WISH_1));
        buttonWish10.onClick.AddListener(() => ClickWish(WISH_10));
        buttonWishClose.onClick.AddListener(() => ClickWishClose());
        buttonWishResultClose.onClick.AddListener(() => ClickWishResultClose());
        buttonWishVideo.onClick.AddListener(ClickSkip);
        swipePanel.AddSwipeListener((Swipe swipe) =>
        {
            int selectPickUpIndex = selectPickUp;
            int maxPickUpCount = togglePickUpPannels.Count;
            switch (swipe)
            {
                case Swipe.LEFT:
                    selectPickUpIndex--;
                    if (selectPickUpIndex < 0)
                    {
                        selectPickUpIndex = maxPickUpCount - 1;
                    }
                    break;
                case Swipe.RIGHT:
                    selectPickUpIndex++;
                    if (selectPickUpIndex >= maxPickUpCount)
                    {
                        selectPickUpIndex = 0;
                    }
                    break;
            }
            togglePickUpPannels[selectPickUpIndex].isOn = true;
        });

        AudioManager.instance.PlayBGM(AudioManager.BGM.Wish, true);
        wishToggles = wishTypePannel.GetComponentsInChildren<Toggle>(true);

        InitPickUpCharacter();
        InitChangeWish();
    }

    private void Update()
    {
        if (rewiredPlayer.GetButtonDown("special"))
        {
            buttonWishChange.onClick.Invoke();
        }
    }

    void InitChangeWish()
    {
        Image imageFirst = buttonWishChange.GetComponentsInChildren<Image>()[1];
        Image imageSecond = buttonWishChange.GetComponentsInChildren<Image>()[3];
        buttonWishChange.onClick.AddListener(() =>
        {
            if (wish == Wish.Character)
            {
                wish = Wish.Weapon;
                imageFirst.sprite = wishImage[(int)Wish.Weapon];
                imageSecond.sprite = wishImage[(int)Wish.Character];
                InitPickUpWeapon();
            }
            else
            {
                wish = Wish.Character;
                imageFirst.sprite = wishImage[(int)Wish.Character];
                imageSecond.sprite = wishImage[(int)Wish.Weapon];
                InitPickUpCharacter();
            }
            togglePickUpPannels[0].isOn = true;
            imageFirst.DOColor(new Color(1, 1, 1, 1), 0.05f).OnComplete(() =>
            {
                togglePickUpPannels[togglePickUpPannels.Count - 1].isOn = true;
            });
        });
    }

    void DisableWishToggles()
    {
        foreach (Toggle wishToggle in wishToggles)
        {
            wishToggle.gameObject.SetActive(false);
        }
    }
    void InitPickUpCharacter()
    {
        DisableWishToggles();

        int idx = 0;
        togglePickUpPannels = new List<Toggle>();
        foreach (PickUpTarget target in pickUpTargets)
        {
            CharacterData.ParameterWithKey mainChar = characterData.Get(target.names[0]);
            Toggle pickUpPannel = wishToggles[idx];
            pickUpPannel.gameObject.SetActive(true);
            Image pickupPannelImage = pickUpPannel.GetComponentsInChildren<Image>()[2];
            pickupPannelImage.sprite = mainChar.wishPannel;
            InitPickUpCharacterPannel(pickUpPannel, target.names, idx);
            idx++;
        }
        togglePickUpPannels[idx - 1].isOn = true;
        InitPickUpCharacters();
    }
    void InitPickUpWeapon()
    {
        DisableWishToggles();
        int idx = 0;
        togglePickUpPannels = new List<Toggle>();
        foreach (PickUpTargetWeapon target in pickUpWeapons)
        {
            WeaponData.Parameter mainChar = weaponData.Get(target.names[0]);
            Toggle pickUpPannel = wishToggles[idx];
            pickUpPannel.gameObject.SetActive(true);
            Image pickupPannelImage = pickUpPannel.GetComponentsInChildren<Image>()[2];
            pickupPannelImage.sprite = mainChar.wishPannel;
            InitPickUpWeaponPannel(pickUpPannel, target.names, idx);
            idx++;
        }
        togglePickUpPannels[idx - 1].isOn = true;
        InitPickUpWeapons();
    }

    void InitPickUpCharacterPannel(Toggle pickUpPannel, CharacterData.Name[] names, int idx)
    {
        int index = idx;
        pickUpPannel.onValueChanged.RemoveAllListeners();
        pickUpPannel.onValueChanged.AddListener((bool value) =>
        {
            if (!value) return;
            selectPickUp = index;
            SelectPickUpPannel(index, names);
        });
        togglePickUpPannels.Add(pickUpPannel);
    }

    void InitPickUpWeaponPannel(Toggle pickUpPannel, WeaponName[] names, int idx)
    {
        int index = idx;
        pickUpPannel.onValueChanged.RemoveAllListeners();
        pickUpPannel.onValueChanged.AddListener((bool value) =>
        {
            if (!value) return;
            selectPickUp = index;
            SelectPickUpWeaponPannel(index, names);
        });
        togglePickUpPannels.Add(pickUpPannel);
    }

    void SelectPickUpPannel(int index, CharacterData.Name[] names)
    {
        AudioManager.instance.PlaySFX(AudioManager.SFX.Click_Wish);
        for (int i = 0; i < togglePickUpPannels.Count; i++)
        {
            Toggle pannel = togglePickUpPannels[i];
            if (i != index)
            {
                pannel.isOn = false;
            }
        }

        int wishIndex = 0;
        foreach (CharacterData.Name name in names)
        {
            CharacterData.ParameterWithKey pickUpChar = characterData.Get(name);
            if (wishIndex == 0)
            {
                imgWishMain.sprite = pickUpChar.wishImage;
                imgWishMain.GetComponent<UITweening>().SetDefaultPosition();
                imgWishElement.sprite = iconElements[(int)pickUpChar.elementType];
                imgWishBackground.sprite = bgElements[(int)pickUpChar.elementType];
                imgWishBackground.GetComponent<UITweening>().SetDefaultPosition();
                textWishName.text = "Character.".AddString(name.ToString()).Localize();
                textWishTitle.text = "Wish.".AddString(name.ToString()).Localize();
            }
            else
            {
                imgWishSub[wishIndex - 1].sprite = pickUpChar.wishImage;
                imgWishSub[wishIndex - 1].GetComponent<UITweening>().SetDefaultPosition();
            }
            wishIndex++;
        }
    }

    void SelectPickUpWeaponPannel(int index, WeaponName[] names)
    {
        AudioManager.instance.PlaySFX(AudioManager.SFX.Click_Wish);
        for (int i = 0; i < togglePickUpPannels.Count; i++)
        {
            Toggle pannel = togglePickUpPannels[i];
            if (i != index)
            {
                pannel.isOn = false;
            }
        }

        int wishIndex = 0;
        foreach (WeaponName name in names)
        {
            WeaponData.Parameter pickUp = weaponData.Get(name);
            if (wishIndex == 0)
            {
                imgWishMain.sprite = pickUp.wishImage;
                imgWishMain.GetComponent<UITweening>().SetDefaultPosition();
                imgWishElement.sprite = iconElements[0];
                imgWishBackground.sprite = bgElements[0];
                imgWishBackground.GetComponent<UITweening>().SetDefaultPosition();
                textWishName.text = "Weapon.".AddString(name.ToString()).Localize();
                textWishTitle.text = ("Wish.Weapon").Localize();
            }
            else
            {
                if (wishIndex > imgWishSub.Length) continue;
                imgWishSub[wishIndex - 1].sprite = pickUp.wishIcon;
                imgWishSub[wishIndex - 1].GetComponent<UITweening>().SetDefaultPositionWithSize();
            }
            wishIndex++;
        }
    }

    void InitPickUpCharacters()
    {
        pickableCharacterList = new List<CharacterData.ParameterWithKey>();
        foreach (CharacterData.ParameterWithKey character in characterData.characters)
        {
            if (CheckPickableCharacter(character))
            {
                pickableCharacterList.Add(character);
            }
        }
    }

    void InitPickUpWeapons()
    {
        pickableWeaponList = new List<WeaponData.Parameter>();
        foreach (WeaponData.Parameter weapon in weaponData.weapons)
        {
            if (CheckPickableWeapon(weapon))
            {
                pickableWeaponList.Add(weapon);
            }
        }
    }



    bool CheckPickableCharacter(CharacterData.ParameterWithKey character)
    {
        if (character.type == CharacterData.Type.Player) return false;
        if (character.type == CharacterData.Type.None) return false;
        if (character.pickUpType == CharacterData.PickUpType.PickUp) return false;
        return true;
    }
    bool CheckPickableWeapon(WeaponData.Parameter weapon)
    {
        if (weapon.type == WeaponData.Type.Player) return false;
        if (weapon.type == WeaponData.Type.None) return false;
        if (weapon.pickUpType == WeaponData.PickUpType.PickUp) return false;
        return true;
    }

    void SetPrimogem()
    {
        textPrimoGem.text = "<sprite name=primogem>".AddString(GameDataManager.instance.saveData.userData.primoGem.ToString());
    }

    void ClickWish(int amount)
    {
        AudioManager.instance.PlaySFX(AudioManager.SFX.Click_Wish);
        int wishCount = amount / WISH_1;

        if (GameDataManager.instance.saveData.userData.primoGem > amount)
        {
            GameDataManager.instance.saveData.userData.primoGem -= amount;
            SetPrimogem();

            if (wish == Wish.Character)
            {
                WishType wishType = PickUpCharacter(wishCount);
                buttonWishVideo.gameObject.SetActive(true);
                wishVideoPlayer.clip = wishVideoClips[(int)wishType];
                AudioManager.instance.EffectBgm(true);
                AudioManager.instance.PlaySFX(AudioManager.SFX.WishEffect);
            }
            else
            {
                WishType wishType = PickUpWeapon(wishCount);
                buttonWishVideo.gameObject.SetActive(true);
                wishVideoPlayer.clip = wishVideoClips[(int)wishType];
                AudioManager.instance.EffectBgm(true);
                AudioManager.instance.PlaySFX(AudioManager.SFX.WishEffect);
            }
            eventSystem.SetSelectedGameObject(buttonWishVideo.gameObject);
        }
    }

    void ClickSkip()
    {
        VisibleWishResult();
    }

    void OnVideoEnd(VideoPlayer player)
    {
        VisibleWishResult();
    }

    void ClickWishClose()
    {
        AudioManager.instance.PlaySFX(AudioManager.SFX.Click_Wish);
        SceneManager.LoadScene("MainScene");
    }

    void ClickWishResultClose()
    {
        wishResultPanel.gameObject.SetActive(false);
        AudioManager.instance.EffectBgm(false);
        AudioManager.instance.PlaySFX(AudioManager.SFX.Click_Wish);

        foreach (GameObject item in wishSlotPools)
        {
            item.SetActive(false);
        }
        GameDataManager.instance.SaveInstance();
        eventSystem.SetSelectedGameObject(buttonWish10.gameObject);
    }
    void VisibleWishResult()
    {
        wishResultPanel.gameObject.SetActive(true);
        buttonWishVideo.gameObject.SetActive(false);
        if (wish == Wish.Character)
        {
            DisplayWishCharacters();
        }
        else
        {
            DisplayWishWeapons();
        }

        eventSystem.SetSelectedGameObject(buttonWishResultClose.gameObject);
        GameDataManager.instance.SaveInstance();
    }

    WishType PickUpCharacter(int count)
    {
        List<CharacterData.ParameterWithKey> nomalPool = GetCharacterPool(CharacterData.Type.Normal);
        List<CharacterData.ParameterWithKey> rarePool = GetCharacterPool(CharacterData.Type.Rare);
        pickUpResult = new List<CharacterData.ParameterWithKey>();
        WishType result = WishType.Noraml;
        for (int i = 0; i < count; i++)
        {
            int pickRandom = UnityEngine.Random.Range(0, 100);
            if (pickRandom < RARE_PER)
            {
                pickUpResult.Add(GetCharacter(rarePool));
                result = WishType.Rare;
            }
            else
            {
                pickUpResult.Add(GetCharacter(nomalPool));
            }
        }
        GameDataManager.instance.SaveInstance();
        return result;
    }


    List<CharacterData.ParameterWithKey> GetCharacterPool(CharacterData.Type type)
    {
        List<CharacterData.ParameterWithKey> characters = new List<CharacterData.ParameterWithKey>();
        foreach (CharacterData.ParameterWithKey character in pickableCharacterList)
        {
            if (character.type == type) characters.Add(character);
        }

        return characters;
    }


    WishType PickUpWeapon(int count)
    {
        List<WeaponData.Parameter> nomalPool = GetWeaponpool(WeaponData.Type.Normal);
        List<WeaponData.Parameter> rarePool = GetWeaponpool(WeaponData.Type.Rare);
        pickUpWeaponResult = new List<WeaponData.Parameter>();
        WishType result = WishType.Noraml;
        for (int i = 0; i < count; i++)
        {
            int pickRandom = UnityEngine.Random.Range(0, 100);
            if (pickRandom < RARE_PER)
            {
                pickUpWeaponResult.Add(GetWeapon(rarePool));
                result = WishType.Rare;
            }
            else
            {
                pickUpWeaponResult.Add(GetWeapon(nomalPool));
            }
        }
        GameDataManager.instance.SaveInstance();
        return result;
    }

    List<WeaponData.Parameter> GetWeaponpool(WeaponData.Type type)
    {
        List<WeaponData.Parameter> weapons = new List<WeaponData.Parameter>();
        foreach (WeaponData.Parameter weapon in pickableWeaponList)
        {
            if (weapon.type == type) weapons.Add(weapon);
        }

        return weapons;
    }

    public CharacterData.Type Rarity(CharacterData.Name name, CharacterData characterData)
    {
        CharacterData.ParameterWithKey character = characterData.Get(name);
        return character.type;
    }
    CharacterData.ParameterWithKey GetCharacter(List<CharacterData.ParameterWithKey> characterPool)
    {
        CharacterData.ParameterWithKey result = new CharacterData.ParameterWithKey();
        int pickRandom = UnityEngine.Random.Range(0, 2);

        int wishRandom = UnityEngine.Random.Range(0, characterPool.Count);

        result = characterPool[wishRandom];


        if (pickRandom == 0 && selectPickUp != 0) //PickUp
        {
            if (characterPool[0].type == CharacterData.Type.Rare)
            {
                result = characterData.Get(pickUpTargets[selectPickUp].names[0]);
            }
            else
            {
                int randomNormalPick = UnityEngine.Random.Range(1, pickUpTargets[selectPickUp].names.Length);
                result = characterData.Get(pickUpTargets[selectPickUp].names[randomNormalPick]);
            }
        }

        if (GameDataManager.instance.saveData.userData.isPity && result.type == CharacterData.Type.Rare) //그 전에 픽뚫일 경우 픽업으로 바꿔줌
        {
            GameDataManager.instance.saveData.userData.isPity = false;
            result = characterData.Get(pickUpTargets[selectPickUp].names[0]);
        }
        else if (result.type == CharacterData.Type.Rare && result.pickUpType == CharacterData.PickUpType.Normal) //레어로 뽑은게 픽뚫이면 픽업 스택으로 바꿔줌
        {
            GameDataManager.instance.saveData.userData.isPity = true;
        }

        SetCharacterInUserData(result);

        return result;
    }

    WeaponData.Parameter GetWeapon(List<WeaponData.Parameter> weaponPool)
    {
        WeaponData.Parameter result = new WeaponData.Parameter();
        int pickRandom = UnityEngine.Random.Range(0, 2);

        int wishRandom = UnityEngine.Random.Range(0, weaponPool.Count);

        result = weaponPool[wishRandom];


        if (pickRandom == 0 && selectPickUp != 0) //PickUp
        {
            if (weaponPool[0].type == WeaponData.Type.Rare)
            {
                result = weaponData.Get(pickUpWeapons[selectPickUp].names[0]);
            }
            else
            {
                int randomNormalPick = UnityEngine.Random.Range(1, pickUpWeapons[selectPickUp].names.Length);
                result = weaponData.Get(pickUpWeapons[selectPickUp].names[randomNormalPick]);
            }
        }

        if (GameDataManager.instance.saveData.userData.isPityWeapon && result.type == WeaponData.Type.Rare) //그 전에 픽뚫일 경우 픽업으로 바꿔줌
        {
            GameDataManager.instance.saveData.userData.isPityWeapon = false;
            result = weaponData.Get(pickUpWeapons[selectPickUp].names[0]);
        }
        else if (result.type == WeaponData.Type.Rare && result.pickUpType == WeaponData.PickUpType.Normal) //레어로 뽑은게 픽뚫이면 픽업 스택으로 바꿔줌
        {
            GameDataManager.instance.saveData.userData.isPityWeapon = true;
        }

        SetWeaponInUserData(result);

        return result;
    }

    void DisplayWishCharacters()
    {
        int index = 0;
        foreach (CharacterData.ParameterWithKey character in pickUpResult)
        {
            GameObject wishSlot = GetWishSlot();
            WishSlotAvatarIcon wishSlotAvatar = wishSlot.GetComponentInChildren<WishSlotAvatarIcon>();
            WishSlotOutline wishSlotOutline = wishSlot.GetComponentInChildren<WishSlotOutline>();
            WishSlotElement wishSlotElement = wishSlot.GetComponentInChildren<WishSlotElement>();
            WishSlotRarity wishSlotRarity = wishSlot.GetComponentInChildren<WishSlotRarity>();
            wishSlotAvatar.Init(character.wishIcon);
            wishSlotOutline.Init(materialOutlines[(int)character.type]);
            wishSlotElement.Init(iconElements[(int)character.elementType]);
            wishSlotRarity.Init(iconRarity[(int)character.type]);
            wishSlot.GetComponent<UITweeningWishSlot>().Tweening(index, pickUpResult.Count);
            index++;
        }
    }

    void DisplayWishWeapons()
    {

        int index = 0;
        foreach (WeaponData.Parameter weapon in pickUpWeaponResult)
        {
            GameObject wishSlot = GetWishSlot();
            WishSlotAvatarIcon wishSlotAvatar = wishSlot.GetComponentInChildren<WishSlotAvatarIcon>();
            WishSlotOutline wishSlotOutline = wishSlot.GetComponentInChildren<WishSlotOutline>();
            WishSlotElement wishSlotElement = wishSlot.GetComponentInChildren<WishSlotElement>();
            WishSlotRarity wishSlotRarity = wishSlot.GetComponentInChildren<WishSlotRarity>();
            wishSlotAvatar.Init(weapon.wishIcon);
            wishSlotOutline.Init(materialOutlines[(int)weapon.type]);
            wishSlotElement.Init(iconElements[0]);
            wishSlotRarity.Init(iconRarity[(int)weapon.type]);
            wishSlot.GetComponent<UITweeningWishSlot>().Tweening(index, pickUpWeaponResult.Count);
            index++;
        }
    }
    public GameObject GetWishSlot()
    {
        GameObject select = null;

        foreach (GameObject item in wishSlotPools)
        {
            if (item != null)
            {
                if (!item.activeSelf)
                {
                    select = item;
                    select.SetActive(true);
                    break;
                }
            }
        }
        if (!select)
        {
            select = Instantiate(wishCharacterSlotPrefab, wishCharacterSlot.transform);
            wishSlotPools.Add(select);
        }

        return select;
    }


    void SetCharacterInUserData(CharacterData.ParameterWithKey character)
    {

        GameDataManager.instance.saveData.record.wishCount++;
        Character getCharacter = GameDataManager.instance.saveData.GetCharacter(character.name.ToString());
        if (!getCharacter.isMine)
        {
            getCharacter.isMine = true;
            GameDataManager.instance.saveData.record.getCharacterCount++;
        }
        else if (getCharacter.constellaCount < 6)
        {
            getCharacter.constellaCount++;
        }
        else
        {
            if (character.type == CharacterData.Type.Normal)
            {
                GameDataManager.instance.saveData.record.getTotalMora += 500;
                GameDataManager.instance.saveData.userData.mora += 500;
            }
            else
            {
                GameDataManager.instance.saveData.record.getTotalMora += 5000;
                GameDataManager.instance.saveData.userData.mora += 5000;
            }
        }
        characterData.Init();
    }
    void SetWeaponInUserData(WeaponData.Parameter weapon)
    {

        GameDataManager.instance.saveData.record.wishCount++;
        WeaponStat getWeapon = GameDataManager.instance.saveData.weaponStats[weapon.weaponName];
        if (getWeapon.rank == 0)
        {
            getWeapon.level = 1;
        }
        if (getWeapon.rank < 5)
        {
            getWeapon.rank++;
        }
        else
        {
            if (weapon.type == WeaponData.Type.Normal)
            {
                GameDataManager.instance.saveData.record.getTotalMora += 500;
                GameDataManager.instance.saveData.userData.mora += 500;
            }
            else
            {
                GameDataManager.instance.saveData.record.getTotalMora += 5000;
                GameDataManager.instance.saveData.userData.mora += 5000;
            }
        }
        weaponData.Init();
    }

}
