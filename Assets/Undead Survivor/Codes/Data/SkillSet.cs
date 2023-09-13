using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "SkillData", menuName = "GenshinSurvivor/SkillSet", order = 0)]
public class SkillSet : ScriptableObject
{

    public enum SkillDamageStat
    {
        ATK,
        ARMOR,
        HP,
        ELEMENT_MASTERY
    }
    public List<SkillSequence> sequences;


    [System.Serializable]
    public class SkillSequence
    {
        [Tooltip("animation 스킬 애니메이션")]
        public AnimationClip animation;
        [Tooltip("objectType 스킬 타입")]
        public Skill.ObjectType objectType;
        [Tooltip("damage 스킬의 대미지")]
        public float damage = 1.0f;
        [Tooltip("damage 스킬의 대미지")]
        public SkillDamageStat damageStat;
        [Tooltip("elementType 스킬의 속성")]
        public Element.Type elementType;
        [Tooltip("duration 스킬의 지속시간, -1이면 무한지속")]
        public float duration;
        [Tooltip("scanRange 스킬의 스캔범위")]
        public float scanRange;
        [Tooltip("isProjectile 적에게 닿으면 사라지는 스킬 여부")]
        public bool isProjectile;
        [Tooltip("isSummonAttack 소환물 공격인지 여부")]
        public bool isSummonAttack;
        [Tooltip("isConditionChange 조건에 맞춘 스킬만 동작하도록 변경")]
        public bool isConditionChange;
        [Tooltip("isContinueDamage 지속공격인 대미지인 여부")]
        public bool isContinueDamage;
        [Tooltip("isTrigger 적에게 닿으면 isTriggerAttack가 true인 스킬을 생성할 지 결정")]
        public bool isTrigger;
        [Tooltip("isTriggerAttack 적에게 닿으면 생성되는 스킬 여부")]
        public bool isTriggerAttack;
        [Tooltip("isPlayerMove 돌진스킬 여부")]
        public bool isPlayerMove;
        [Tooltip("isSkillMove 이동형 스킬 여부")]
        public bool isSkillMove;
        [Tooltip("isImmune 소환물 무적 여부 Summon오브젝트만 적용")]
        public bool isImmune;
        [Tooltip("isMass 질량을 가지는 소환물인지 여부, Summon오브젝트만 적용")]
        public bool isMass;
        [Tooltip("isPositionFix 위치 고정 여부")]
        public bool isPositionFix;
        [Tooltip("isRotationFix 방향 고정 여부")]
        public bool isRotationFix;
        [Tooltip("isRotate 지속적으로 회전하는 스킬 여부")]
        public bool isRotate;
        [Tooltip("coolTime 0보다 크면 지속시간 동안 쿨타임이 되면 스킬을 새로 생성함")]
        public float coolTime;
        [Tooltip("skillCount 스킬 개수")]


        public int _skillCount = 1;
        public int skillCount
        {
            get { return _skillCount + (isSkillAdd ? (int)GameManager.instance.player.stat.amount : 0); }
        }
        [Tooltip("isSkillAdd true일 경우 스킬 개수가 스탯으로 추가된 개수만큼 추가로 증가")]
        public bool isSkillAdd;
        [Tooltip("delay 동작 사이 딜레이")]
        public float delay;
        [Tooltip("isContinue 애니메이션 시작과 동시에 다음 동작을 할건지 선택")]
        public bool isContinue;
        [Tooltip("aim 오브젝트가 생성될 방향 설정")]
        public Skill.Aim aim;
        [Tooltip("isTraking aim에 Target이 있을 경우 추적기능 추가")]
        public bool isTraking;
        [Tooltip("isSequence 이전 동작과 연속된 동작인지 체크")]
        public bool isMagnet;
        [Tooltip("isMagnet  적을 끌어당기는 스킬 여부")]
        public bool isSequence;
        [Tooltip("isTransform 속성을 변경가능한 스킬 여부")]
        public bool isTransform;
        [Tooltip("isChangedElement 원소전환 가능한 스킬 여부")]
        public bool isChangedElement;
        [Tooltip("isResetPosition defalutPosition부터 sequence 초기위치로 이동하는 지 여부")]
        public bool isResetPosition;
        [Tooltip("defalutPosition 초기위치")]
        public Vector2 defalutPosition;
        [Tooltip("defalutRotation 초기방향")]
        public Vector2 defalutRotation;
        [Tooltip("centerOffset 중앙 위치")]
        public Vector2 centerOffset;
        [Tooltip("skillSize 스킬 생성 시 크기")]
        public float skillSize = 1;
        [Tooltip("createRange 스킬 생성 시 거리")]
        public float createRange;
        [Tooltip("layerOrder 화면에 표시할 순서")]
        public int layerOrder;
        [Tooltip("moveRange 스킬 이동 거리 -1이면 해당 방향으로 무한정 이동함")]
        public float moveRange;
        [Tooltip("moveTime 스킬 이동 시간 0이면 moveRange만큼 이동하고 끝")]
        public float moveTime;
        [Tooltip("moveTimeInDuration 지속시간이 이동시간이 됨")]
        public bool moveTimeInDuration;
        [Tooltip("randomRange 0이상이면 스킬이 랜덤한 범위에서 생성됨")]
        public float randomRange;
        [Tooltip("animationSpeed 애니메이션 속도")]
        public float animationSpeed = 1;
        [Tooltip("isAnimationSpeedMatchDuration 지속시간에 애니메이션 속도를 맞추는 여부")]
        public bool isAnimationSpeedMatchDuration;
        [Tooltip("triggerSkillSequence 닿으면 생기는 스킬")]
        public List<SkillSequence> _triggerSkillSequence = new List<SkillSequence>();
        [Tooltip("subSkillSequence 소환물 서브스킬")]
        public List<SkillSequence> _subSkillSequence = new List<SkillSequence>();
        public SkillSequence triggerSkillSequence
        {
            get
            {
                if (_triggerSkillSequence.Count > 0)
                {
                    return _triggerSkillSequence[0];
                }
                return null;
            }
        }
        public SkillSequence subSkillSequence
        {
            get
            {
                if (_subSkillSequence.Count > 0)
                {
                    return _subSkillSequence[0];
                }
                return null;
            }
        }
    }

    private void OnValidate()
    {

    }

}
