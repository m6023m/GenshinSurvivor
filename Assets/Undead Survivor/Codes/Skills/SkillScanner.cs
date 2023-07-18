using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillScanner : MonoBehaviour
{
    float scanRange;
    public LayerMask targetLayer;
    public Collider2D[] targets = new Collider2D[200];
    public int targetNum = 0;

#if UNITY_EDITOR
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, scanRange);
    }

#endif
    public void Init(float scanRange)
    {
        this.scanRange = scanRange;
    }

    public void GetTargets()
    {
        targetNum = Physics2D.OverlapCircleNonAlloc(transform.position, scanRange, targets, targetLayer);
    }


    public Transform GetNearest()
    {
        GetTargets();
        Transform result = null;
        float diff = scanRange;
        for (int index = 0; index < targetNum; index++)
        {
            Collider2D target = targets[index];
            Vector3 myPos = transform.position;
            Vector3 targetPos = target.transform.position;
            float curDiff = Vector3.Distance(myPos, targetPos);
            if (curDiff < diff)
            {
                diff = curDiff;
                result = target.transform;
            }
        }

        return result;
    }


    public Transform GetNearestSecond(Collider2D[] targets)
    {
        Transform closest = null;
        Transform secondClosest = null;
        float closestDiff = scanRange;
        float secondClosestDiff = scanRange;

        for (int index = 0; index < targets.Length; index++)
        {
            Collider2D target = targets[index];
            Vector3 myPos = transform.position;
            Vector3 targetPos = target.transform.position;
            float curDiff = Vector3.Distance(myPos, targetPos);

            if (curDiff < closestDiff)
            {
                secondClosest = closest;
                secondClosestDiff = closestDiff;

                closestDiff = curDiff;
                closest = target.transform;
            }
            else if (curDiff < secondClosestDiff)
            {
                secondClosestDiff = curDiff;
                secondClosest = target.transform;
            }
        }
        return secondClosest;
    }
}
