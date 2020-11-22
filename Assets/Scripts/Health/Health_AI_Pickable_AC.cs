using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health_AI_Pickable_AC : Health_AI_Pickable
{
    Animator animator;
    const string pick_anim_name = "Pick";
    const string reset_anim_name = "Reset";

    public override void Init()
    {
        base.Init();
        animator = GetComponentInChildren<Animator>();
    }

    public override void InitHealth(int maxHealth)
    {
        base.InitHealth(maxHealth);
        animator.Play(reset_anim_name);
    }

    protected override void Kill()
    {
        base.Kill();
        animator.Play(pick_anim_name);
    }
}
