using System.Collections;
using System.Collections.Generic;
using tpopl001.Utils;
using UnityEngine;

public class Health_AI_Unit : Health_AI
{
    Animator animator;
    Timer hitTimer = new Timer(1);
    public override void Init()
    {
        animator = GetComponentInChildren<Animator>();
        base.Init();
    }

    public override void DamageHealth(int amount)
    {
        base.DamageHealth(amount);
        if(!IsDead)
        {
            animator.Play("getHit");
            hitTimer.StartTimer();
            CanMove = false;
        }
    }

    void Update()
    {
        if(!CanMove && hitTimer.GetComplete())
        {
            CanMove = true;
        }
    }

    protected override void Kill()
    {
        base.Kill();
        animator.Play("die");
        EventHandler.Death();
    }

    public override void InitHealth(int maxHealth)
    {
        base.InitHealth(maxHealth);
        animator.Play("taunt");
    }
}
