
[System.Serializable]
public class Upgrade //Cnt는 업그레이드 갯수, Unit은 한번 강화당 올라가는 단위, Max는 최대 업그레이드 개수, 는 최종 증가량
{
    public UpgradeComponent[] upgradeComponents;
    public Upgrade()
    {
        upgradeComponents = new UpgradeComponent[18];
        upgradeComponents[(int)UpgradeType.ATK] = new UpgradeComponent(_unit:5, _max:5, _price:500, key:"ATK", _percent:true);
        upgradeComponents[(int)UpgradeType.ARMOR] = new UpgradeComponent(_unit:1, _max:5, _price:500, key:"ARMOR");
        upgradeComponents[(int)UpgradeType.HP] = new UpgradeComponent(_unit:10, _max:5, _price:500, key:"HP", _percent:true);
        upgradeComponents[(int)UpgradeType.HEAL] = new UpgradeComponent(_unit:10, _max:5, _price:500, key:"HEAL", _percent:true);
        upgradeComponents[(int)UpgradeType.COOL] = new UpgradeComponent(_unit:3, _max:5, _price:1000, key:"COOLTIME", _percent:true);
        upgradeComponents[(int)UpgradeType.AREA] = new UpgradeComponent(_unit:10, _max:5, _price:500, key:"AREA", _percent:true);
        upgradeComponents[(int)UpgradeType.SPEED] = new UpgradeComponent(_unit:10, _max:5, _price:500, key:"ASPEED", _percent:true);
        upgradeComponents[(int)UpgradeType.DURATION] = new UpgradeComponent(_unit:15, _max:5, _price:500, key:"DURATION", _percent:true);
        upgradeComponents[(int)UpgradeType.AMOUNT] = new UpgradeComponent(_unit:1, _max:2, _price:5000, key:"AMOUNT");
        upgradeComponents[(int)UpgradeType.MSPEED] = new UpgradeComponent(_unit:5, _max:5, _price:500, key:"MSPEED", _percent:true);
        upgradeComponents[(int)UpgradeType.MAGNET] = new UpgradeComponent(_unit:25, _max:5, _price:1000, key:"MAGNET", _percent:true);
        upgradeComponents[(int)UpgradeType.LUCK] = new UpgradeComponent(_unit:10, _max:5, _price:777, key:"LUCK", _percent:true);
        upgradeComponents[(int)UpgradeType.GROWTH] = new UpgradeComponent(_unit:10, _max:5, _price:1000, key:"GROWTH", _percent:true);
        upgradeComponents[(int)UpgradeType.GREED] = new UpgradeComponent(_unit:5, _max:5, _price:1000, key:"GREED", _percent:true);
        upgradeComponents[(int)UpgradeType.CURSE] = new UpgradeComponent(_unit:10, _max:5, _price:1000, key:"CURSE", _percent:true);
        upgradeComponents[(int)UpgradeType.REBIRTH] = new UpgradeComponent(_unit:1, _max:1, _price:5000, key:"REBIRTH");
        upgradeComponents[(int)UpgradeType.REROLL] = new UpgradeComponent(_unit:1, _max:2, _price:1000, key:"REROLL");
        upgradeComponents[(int)UpgradeType.SKIP] = new UpgradeComponent(_unit:1, _max:2, _price:1000, key:"SKIP");
    }
}  

public enum UpgradeType{
    ATK,
    ARMOR,
    HP,
    HEAL,
    COOL,
    AREA,
    SPEED,
    DURATION,
    AMOUNT,
    MSPEED,
    MAGNET,
    LUCK,
    GROWTH,
    GREED,
    CURSE,
    REBIRTH,
    REROLL,
    SKIP
}






// 피해량(Atk): 랭크당 모든 피해량 5% 증가
// 방어력(Armor): 랭크당 피격 대미지 1 감소
// 최대 체력(Health): 랭크당 최대 체력 10% 증가
// 회복(Heal): 랭크당 회복효율 10% 증가
// 쿨타임(Cooldown): 랭크당 쿨타임 2.5% 감소
// 공격 범위(Area): 랭크당 범위 5% 증가
// 속도(Speed): 모든 스킬의 시전 속도 10% 증가
// 지속시간(Duration): 랭크당 지속력 15% 증가
// 투사체 수(Amount): 랭크당 투사체 개수 +1
// 이동 속도(Move Speed): 랭크당 이동속도 5% 증가
// 자석(Magnet): 랭크당 획득 반경 25% 증가
// 행운(Luck): 랭크당 행운 10% 증가(행운수치가 치명타 드롭률 등 영향 있음)
// 충전효율(Gain): 랭크당 경험치 획득량 3% 원소충전 효율 10% 증가
// 탐욕(Greed): 랭크당 골드, 원석 획득량 5% 증가
// 저주(Curse): 랭크당 적의 속도, 체력, 개체 수, 스폰율 10% 증가
// 부활(Revival): 랭크당 부활 횟수 +1
// 새로고침(Reroll): 랭크당 1회, 레벨 업 선택지 새로고침
// 건너뛰기(Skip): 랭크당 1회, 레벨 업 선택지 대신 경험치 획득