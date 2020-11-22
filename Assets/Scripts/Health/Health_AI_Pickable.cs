using System.Collections;
using System.Collections.Generic;
using tpopl001.Utils;
using UnityEngine;

public class Health_AI_Pickable : Health_AI
{
    [SerializeField]private ObjectPool_AI_Pickables pool;
    Timer timerTillDespawn = new Timer(1);
    [SerializeField]float despawnTime = 0;
    bool respawning = false;

    public override void Init()
    {
        base.Init();
        timerTillDespawn.Duration = despawnTime;
    }

    public void SetPool(ObjectPool_AI_Pickables p)
    {
        pool = p;
        InitHealth(10);
    }

    public override void InitHealth(int maxHealth)
    {
        if (!effectDeath) Init();
        base.InitHealth(maxHealth);
        respawning = false;
    }

    private void Update()
    {
        if(IsDead)
        {
            if (respawning)
            {
                if (timerTillDespawn.GetComplete())
                {
                    if(!pool.Return(this))
                    {
                        gameObject.SetActive(false);
                    }
                }
            }
            else
            {
                respawning = true;
                timerTillDespawn.StartTimer();
            }
        }
    }
}
