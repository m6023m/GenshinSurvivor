using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//자주 사용하는 Trasfrom 움직임 코드를 저장한 확장 클래스
public static class TransformExtention
{
    public static Vector3 MoveTargetDirectionLinear(this Transform from, Vector3 fromPosition, Vector3 targetDir, float speed)
    {
        Vector3 fromPositionWorld = fromPosition;
        Vector3 position = fromPositionWorld + (targetDir.normalized * speed * Time.deltaTime);
        from.position = position;

        return position;
    }

    public static Vector3 CalcTarget(this Transform from, Transform to)
    {
        Vector3 vectorFrom = new Vector3(from.position.x, from.position.y, 0);
        Vector3 vectorTo = new Vector3(to.position.x, to.position.y, 0);
        Vector3 dir = vectorTo - vectorFrom;
        return dir.normalized;
    }

    public static Vector3 CalcTarget(this Transform from, Vector3 to)
    {
        Vector3 vectorFrom = new Vector3(from.position.x, from.position.y, 0);
        Vector3 vectorTo = new Vector3(to.x, to.y, 0);
        Vector3 dir = vectorTo - vectorFrom;
        return dir;
    }

    public static float CalcAngle(this Transform from, Transform to)
    {
        Vector3 targetDir = to.position - from.position;
        return Mathf.Atan2(targetDir.y, targetDir.x) * Mathf.Rad2Deg;
    }
    public static float CalcAngle(this Transform from, Vector3 to)
    {
        Vector3 targetDir = to - from.position;
        return Mathf.Atan2(targetDir.y, targetDir.x) * Mathf.Rad2Deg;
    }
    public static float CalcAngle(this Vector3 from, Vector3 to)
    {
        Vector3 targetDir = to - from;
        return Mathf.Atan2(targetDir.y, targetDir.x) * Mathf.Rad2Deg;
    }

    public static void RotationFix(this Transform target, Vector3 defaultRotation)
    {
        target.rotation = Quaternion.identity;
        target.rotation = Quaternion.FromToRotation(Vector3.up, defaultRotation);
    }

    public static void ScaleFront(this Transform target, Transform parent, Vector3 scale)
    {
        target.localScale = scale;
        Vector3 anchoredPosition = new Vector3(0, parent.localScale.y / 2 + target.localScale.y / 2, 0);
        target.localPosition = anchoredPosition;
    }

    public static TransformValue CopyTransformValue(this Transform target)
    {
        TransformValue result = new TransformValue(target);
        return result;
    } 
}
