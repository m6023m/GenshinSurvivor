using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "EnemyData", menuName = "GenshinSurvivor/EnemyData", order = 0)]
public class EnemyData : ScriptableObject
{
    public List<EnemyStat> enemtStats;
    [System.Serializable]
    public class EnemyStat
    {
        public EnemyNormal.Name name;
        public RuntimeAnimatorController anim;
        public EnemyNormal.Pattern pattern;
        public EnemyAttackData enemyAttackData;
        public float patternCoolTime;
        public float patternRange;
        public Element.Type elementType;
        public float health;
        public float damage;
        public float armor;
        public float speed;
        public float size;
        public float exp;
        public float PhysicsRes;
        public float PyroRes;
        public float HydroRes;
        public float AnemoRes;
        public float DendroRes;
        public float ElectroRes;
        public float CyroRes;
        public float GeoRes;
    }

    public RuntimeAnimatorController GetAnim(EnemyNormal.Name name)
    {
        foreach (EnemyStat enemyStat in enemtStats)
        {
            if (enemyStat.name.Equals(name))
            {
                return enemyStat.anim;
            }
        }
        return null;
    }
    public EnemyStat Get(EnemyNormal.Name name)
    {
        foreach (EnemyStat enemyStat in enemtStats)
        {
            if (enemyStat.name.Equals(name))
            {
                return enemyStat;
            }
        }
        return null;
    }
}
