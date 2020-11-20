using System.Collections;
using System.Collections.Generic;
using tpopl001.Utils;
using UnityEngine;

public class ObjectPool_Projectile : ObjectPool<Projectile>
{
    [SerializeField] float despawnTime = 5;
    Timer timeTillDespawn = new Timer(5);
    List<Projectile> active = new List<Projectile>();
    Timer spawnTime = new Timer(0);
    Transform spawnSpot;
    bool spawnOverTime = false;

    private void Start()
    {
        timeTillDespawn.Duration = despawnTime;
    }

    public void Spawn(Transform position, Vector3 targetPos, float timer)
    {
        var ammo = Get();
        if (ammo == null || spawnOverTime)
        {
            return;
        }

        if (timeTillDespawn.GetComplete() || active.Count == 0)
            timeTillDespawn.StartTimer();

        spawnTime.Duration = timer;
        spawnTime.StartTimer();
        spawnOverTime = true;

        //ammo.transform.position = position;
        //  ammo.gameObject.SetActive(true);
        spawnSpot = position;
        active.Add(ammo);
        ammo.LookPos = targetPos;
    }

    public void Return(Projectile a)
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
            if (spawnTime.GetComplete() && spawnOverTime)
            {
                spawnOverTime = false;
                for (int i = active.Count-1; i >-1; i--)
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
