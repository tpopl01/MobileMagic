using System.Collections;
using System.Collections.Generic;
using tpopl001.Utils;
using UnityEngine;

public class ObjectPool_Heal : ObjectPool<Healer_Player>
{
    [SerializeField] float despawnTime = 5;
    Timer timeTillDespawn = new Timer(5);
    List<Healer_Player> active = new List<Healer_Player>();
    Timer spawnTime = new Timer(0);
    Transform spawnSpot;

    private void Start()
    {
        timeTillDespawn.Duration = despawnTime;
    }

    public void Spawn(Transform position, Vector3 targetPos, float timer)
    {
        var ammo = Get();
        if (ammo == null || spawnTime.GetComplete() == false)
        {
            return;
        }

        if (timeTillDespawn.GetComplete())
            timeTillDespawn.StartTimer();

        spawnTime.Duration = timer;
        spawnTime.StartTimer();

        spawnSpot = position;
      //  ammo.LookPos = targetPos;
        active.Add(ammo);
    }

    public void Return(Healer_Player a)
    {
        if (active.Contains(a))
        {
            ReturnObject(a);
            active.Remove(a);
        }
    }

    private void Update()
    {
        if (active.Count > 0)
        {
            if (spawnTime.GetComplete())
            {
                for (int i = 0; i < active.Count; i++)
                {
                    if (active[i].isActiveAndEnabled == false)
                    {
                        active[i].transform.position = spawnSpot.position;
                        active[i].gameObject.SetActive(true);
                        break;
                    }
                }
            }
            if (timeTillDespawn.GetComplete())
            {
                ReturnObject(active[0]);
                active.RemoveAt(0);
                timeTillDespawn.StartTimer();
            }
        }
    }
}
