using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPool_AI_Pickables : ObjectPool<Health_AI_Pickable>
{
    List<Health_AI_Pickable> active = new List<Health_AI_Pickable>();

    public void Spawn(Vector3 targetPos)
    {
        var ai = Get();
        if (ai == null)
        {
            Debug.Log("No Pickable Available");
            return;
        }

        ai.gameObject.SetActive(true);
        ai.SetPool(this);
        ai.transform.position = targetPos;
        active.Add(ai);
    }

    public bool Return(Health_AI_Pickable a)
    {
        Debug.Log("Return");
        if (active.Contains(a))
        {
            ReturnObject(a);
            active.Remove(a);
            return true;
        }
        return false;
    }
}
