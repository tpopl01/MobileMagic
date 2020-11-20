using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIController_Ranged_anim : AIController_Ranged
{
    Animator animator;

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
        spellSetting[Random.Range(0, spellSetting.Length)].TryUseSpell(spawnPos, animator, target, null);
        //animator.Play("attack_0" + Random.Range(1, aISettings.attackCount + 1));
    }
    protected override void MoveForward()
    {
        base.MoveForward();
        animator.SetFloat("Vertical", Mathf.Lerp(animator.GetFloat("Vertical"), aISettings.maxAnimSpeed, Time.deltaTime * 10));
    }
}
