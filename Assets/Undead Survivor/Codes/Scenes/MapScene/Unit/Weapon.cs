using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;
using System;
using DG.Tweening;

public class Weapon : MonoBehaviour
{
    public int weaponNum;
    Player player;
    public Sprite[] weaponSprite;
    SpriteRenderer spriteRenderer;
    WeaponType currentWeaponType;
    WeaponStarter weaponStarter;
    Material material;
    public Func<bool> weaponAction;
    float currentAngle;
    float distance = 0.8f;
    public bool weaponCheck = true;
    public SkillObject skillObj;
    private void Awake()
    {
        player = GetComponentInParent<Player>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        material = spriteRenderer.material;
        weaponStarter = new WeaponStarter();
    }
    private void Update()
    {
        RotationFix();
        ChangeCurrentWeaponType();
    }

    void RotationFix()
    {
        transform.rotation = Quaternion.identity;
    }

    public void OutlineFade(bool fade)
    {
        material.SetFloat("_OuterOutlineFade", fade ? 1 : 0);
    }


    void ChangeCurrentWeaponType()
    {
        spriteRenderer.color = new Color(1, 1, 1, 1);
        switch (currentWeaponType)
        {
            case WeaponType.None:
                transform.localScale = new Vector3(1.0f, 1.0f, 1.0f);
                spriteRenderer.color = new Color(1, 1, 1, 0);
                break;
            case WeaponType.Sword:
                transform.localScale = new Vector3(1.0f, 1.0f, 1.0f);
                break;
            case WeaponType.Claymore:
                transform.localScale = new Vector3(1.2f, 1.2f, 1.0f);
                break;
            case WeaponType.Spear:
                transform.localScale = new Vector3(1.0f, 1.0f, 1.0f);
                break;
            case WeaponType.Catalist:
                transform.localScale = new Vector3(0.8f, 0.8f, 1.0f);
                break;
            case WeaponType.Bow:
                transform.localScale = new Vector3(1.0f, 1.0f, 1.0f);
                break;
        }
        if (currentWeaponType == WeaponType.None)
        {
            spriteRenderer.sprite = null;
        }
        else
        {
            spriteRenderer.sprite = weaponSprite[(int)currentWeaponType - 1];
        }
    }

    public void InitWeapon(WeaponType currentWeaponType)
    {
        this.currentWeaponType = currentWeaponType;
        currentAngle = weaponNum * 90;
        InitWeaponEquip();
    }
    void InitWeaponEquip()
    {
        Character character = GameDataManager.instance.saveData.userData.selectChars[weaponNum];
        if (character == null) return;
        weaponStarter.InitWeapon(character, this);
    }

    public void AddWeaponSkill(SkillName skillName)
    {
        SkillObject skillObject = GameManager.instance.poolManager.GetObject<SkillObject>();
        SkillData.ParameterWithKey param = GameManager.instance.skillData.skills[skillName];
        skillObj = skillObject.GetComponent<SkillObject>();
        skillObj.Init(param);
        skillObject.transform.parent = transform;
        skillObject.transform.localPosition = Vector3.zero;
        param.level = 1;
    }

}

public enum WeaponType
{
    None,
    Sword,
    Claymore,
    Spear,
    Catalist,
    Bow
}

public enum WeaponName
{
    None_Weapoen,//없음
    Sword_Favonius,//페보니우스 검
    Sword_The_Flute,//피리검
    Sword_Sacrificial,//제례검
    Sword_Lions_Loar,//용의 포효
    Sword_The_Alley_Flash,//뒷골목의 섬광
    Sword_The_Black,//칠흑검
    Sword_Aquila_Favonia, //매의 검
    Sword_Skyword_Blade, //천공의 검
    Sword_Summit_Shaper, //참봉의 칼날
    Sword_Primordial_Jade_Cutter, //반암결록
    Sword_Freedom_Sworn, //오래된 자유의 서약
    Claymore_Favonius,//페보니우스 대검
    Claymore_The_Bell,//시간의 검
    Claymore_Sacrificial,//제례 대검
    Claymore_Rainslasher, //빗물 베기
    Claymore_Lithic_Blade, //천암고검
    Claymore_Serpent_Spine, //이무기 검
    Claymore_Skyward_Pride,//천공의 긍지
    Claymore_Wolfs_Gravestone, //늑대의 말로
    Claymore_The_Unforged,//무공의 검
    Claymore_Song_Of_Broken_Pines, //송뢰가 울릴 무렵
    Spear_Dragons_Bane, //용학살창
    Spear_Favonius, //페보니우스 장창
    Spear_Lithic_Spear, //천암장창
    Spear_Deathmatch, //결투의 창
    Spear_Primordial_Jade_Spear, //화박연
    Spear_Skyward_Spine, //천공의 마루
    Spear_Vortex_Vanquisher, //관홍의 창
    Spear_Staff_Of_Homa, //호마의 창
    Catalist_Favonius, //페보니우스 비전
    Catalist_Widsith, //음유시인의 악장
    Catalist_Sacrificial,//제례의 악장
    Catalist_Eye_Of_Perception, //소심
    Catalist_Wine_And_Song, //뒷골목의 술과 시
    Catalist_Solar_Pearl, //일월의 정수
    Catalist_Skyward_Atlas,//천공의 두루마리
    Catalist_Lost_Prayer_To_The_Sacered_Winds, //사풍원서
    Catalist_Memory_Of_Dust,//속세의 자물쇠
    Bow_Favonius,//페보니우스 활
    Bow_The_Stringless,//절현
    Bow_Sacrificial_Bow,//제례활
    Bow_Rust,//녹슨활
    Bow_Alley_Hunter,//뒷골목의 사냥꾼
    Bow_Mitternachts_Waltz,//유야의 왈츠
    Bow_Viridescent_Hunt, //청록의 사냥활
    Bow_SkyWard_Harp,//천공의 날개
    Bow_Amos_Bow,//아모스의 활
    Bow_Elegy_For_The_End, //종말 탄식의 노래
    Bow_Polar_Star, //극지의 별

    Sword_Mistsplitter_Reforged, //안개를 가르는 회광
    Bow_Thundering_Pulse, //비뢰의 고동
    Spear_Engulfing_Lightning, //예초의 번개
    Catalist_Everlating_Moonglow, //불멸의 달빛
    Claymore_Akuomaru, //아쿠오마루
    Spear_Wavebreakers_Fin, //파도베는 지느러미
    Bow_Mouuns_Moon, //모운의 달
    Claymore_Redhorn_Stonethresher, //쇄석의 붉은 뿔
    Spear_Calamity_Queller, //식재
    Catalist_Kaguras_Verirty, //카구라의 진의
    Sword_Haran_Geppaku_Futsu, //하란 월백의 후츠
    Bow_Aqua_Simulacra, //약수

}