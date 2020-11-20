using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPool_AI_Balista : ObjectPool<AIController_Ranged>
{
    List<AIController_Ranged> active = new List<AIController_Ranged>();

    public void Spawn(Vector3 targetPos)
    {
        var ai = Get();
        if (ai == null)
        {
            return;
        }

        ai.transform.position = targetPos;
        active.Add(ai);
    }

    public void Return(AIController_Ranged a)
    {
        if (active.Contains(a))
        {
            ReturnObject(a);
            active.Remove(a);
        }
    }
}
