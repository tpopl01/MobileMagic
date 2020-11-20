using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPool_AI : ObjectPool<AIController>
{
    List<AIController> active = new List<AIController>();

    public bool Spawn(Vector3 targetPos)
    {
        var ai = Get();
        if (ai == null)
        {
            return false;
        }

        ai.transform.position = targetPos;
        ai.gameObject.SetActive(true);
        ai.SetPool(this);
        ai.Respawn();
        active.Add(ai);
        return true;
    }

    public void Return(AIController a)
    {
        if (active.Contains(a))
        {
            ReturnObject(a);
            active.Remove(a);
        }
    }
}
