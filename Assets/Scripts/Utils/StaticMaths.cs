using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public static class StaticMaths
{
    public static float GetAngle(Vector3 targetPos, Vector3 currentPos, Vector3 currentForward)
    {
        Vector3 dir = GetDirection(targetPos, currentPos);
        dir.y = 0;
        return Vector3.Angle(currentForward, dir);
    }

    public static Vector3 GetDirection(Vector3 targetPos, Vector3 currentPos)
    {
        Vector3 directionToLookTo = targetPos - currentPos;
        directionToLookTo.y = 0;

        return directionToLookTo;
    }


    public static Quaternion GetLookRotation(Vector3 targetPos, Vector3 currentPos, Vector3 currentForward, out bool shouldRotate, float threshold = 0.1f)
    {
        shouldRotate = false;
        Vector3 dir = GetDirection(targetPos, currentPos);
        float angle = Vector3.Angle(currentForward, dir);
        if (angle > threshold)
        {
            shouldRotate = true;
            if (dir == Vector3.zero) dir = currentForward;
            return Quaternion.LookRotation(dir);
        }

        return Quaternion.identity;
    }



}
