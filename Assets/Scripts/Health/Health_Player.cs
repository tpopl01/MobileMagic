using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health_Player : Health
{
    Animator animator;
    UIBar_Health healthBar;

    public override void Init()
    {
        animator = GetComponentInChildren<Animator>();
        base.Init();
        healthBar = GameObject.FindObjectOfType<UIBar_Health>();
        healthBar.Init(maxHP);
    }

    public override void DamageHealth(int amount)
    {
        base.DamageHealth(amount);
        healthBar.UpdateUI(health);
    }

    public override void Heal(int amount)
    {
        base.Heal(amount);
        healthBar.UpdateUI(health);
    }

    protected override void Kill()
    {
        base.Kill();
        animator.Play("die");
        EventHandler.PlayerDeath();
    }
}
