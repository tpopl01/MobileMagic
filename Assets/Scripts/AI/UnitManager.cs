using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitManager : MonoBehaviour
{
    List<Health> aiHealths = new List<Health>();
    List<Health> playerHealths = new List<Health>();

    public static UnitManager instance;
    private void Awake()
    {
        instance = this;
    }

    public void AddHealthAI(Health h)
    {
        if(!aiHealths.Contains(h))
        {
            aiHealths.Add(h);
        }
    }
    public void AddHealthPlayer(Health h)
    {
        if (!playerHealths.Contains(h))
        {
            playerHealths.Add(h);
        }
    }

    public void RemoveHealthAI(Health h)
    {
        aiHealths.Remove(h);
    }
    public void RemoveHealthPlayer(Health h)
    {
        playerHealths.Remove(h);
    }

    public Health GetClosestHealthAI(Vector3 pos, float goodDist = 1)
    {
        float dist = Mathf.Infinity;
        Health target = null;
        for (int i = 0; i < aiHealths.Count; i++)
        {
            float nDist = Vector3.Distance(pos, aiHealths[i].transform.position);
            if (nDist < dist)
            {
                dist = nDist;
                target = aiHealths[i];
                if(dist < goodDist)
                {
                    return target;
                }
            }
        }

        return target;
    }
    public Health GetHealthPlayer()
    {
        return playerHealths[Random.Range(0, playerHealths.Count)];
    }
    public Health GetHealthPlayer(Vector3 pos, float goodDist = 1)
    {
        float dist = Mathf.Infinity;
        Health target = null;
        for (int i = 0; i < playerHealths.Count; i++)
        {
            float nDist = Vector3.Distance(pos, playerHealths[i].transform.position);
            if (nDist < dist)
            {
                dist = nDist;
                target = playerHealths[i];
                if (dist < goodDist)
                {
                    return target;
                }
            }
        }

        return target;
    }

}
