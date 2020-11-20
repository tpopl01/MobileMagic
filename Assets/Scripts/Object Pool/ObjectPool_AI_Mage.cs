using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPool_AI_Mage : ObjectPool<AIController_Ranged_anim>
{
    List<AIController_Ranged_anim> active = new List<AIController_Ranged_anim>();

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

    public void Return(AIController_Ranged_anim a)
    {
        if (active.Contains(a))
        {
            ReturnObject(a);
            active.Remove(a);
        }
    }
}
