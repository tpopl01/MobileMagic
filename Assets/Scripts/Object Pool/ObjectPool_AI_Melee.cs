using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPool_AI_Melee : ObjectPool<AIController_Unit>
{
    List<AIController_Unit> active = new List<AIController_Unit>();


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

    public void Return(AIController_Unit a)
    {
        if (active.Contains(a))
        {
            ReturnObject(a);
            active.Remove(a);
        }
    }
}
