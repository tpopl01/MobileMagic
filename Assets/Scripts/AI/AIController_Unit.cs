using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIController_Unit : AIController
{
    Animator animator;
    [SerializeField] int damageAmount = 5;

    protected override void Init()
    {
        base.Init();
        animator = GetComponentInChildren<Animator>();
    }

    protected override void Stop()
    {
        base.Stop();
        animator.SetFloat("Vertical", Mathf.Lerp(animator.GetFloat("Vertical"), 0, Time.deltaTime * 30));
    }

    protected override void Attack()
    {
        if (h.CanMove)
        {
            base.Attack();
            animator.Play("attack_0" + Random.Range(1, aISettings.attackCount + 1));
            target.DamageHealth(damageAmount);
        }
    }

    protected override void MoveForward()
    {
        base.MoveForward();
        animator.SetFloat("Vertical", Mathf.Lerp(animator.GetFloat("Vertical"), aISettings.maxAnimSpeed, Time.deltaTime * 10));
    }
}
