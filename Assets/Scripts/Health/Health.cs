using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    protected int health = 100;
    protected int maxHP = 0;
    public bool CanMove { get; protected set; } = true;

    public bool IsDead { get; private set; } = false;
    [SerializeField] protected DeathEffects deathEffect;

    GameObject effectDeath;

    private void Start()
    {
        Init();
        if (maxHP == 0)
            InitHealth(10);
    }

    public virtual void Init()
    {
        effectDeath = Instantiate<GameObject>(deathEffect.deathEffect, transform);
        effectDeath.transform.localPosition = Vector3.zero;
        effectDeath.SetActive(false);
    }

    public virtual void InitHealth(int maxHealth)
    {
        IsDead = false;
        maxHP = maxHealth;
        health = maxHP;
        effectDeath.SetActive(false);
    }

    public virtual void DamageHealth(int amount)
    {
        if(!IsDead)
        {
            health -= amount;
            health = Mathf.Clamp(health, 0, maxHP);
            if (health == 0)
            {
                Kill();
            }
        }
    }

    public virtual void Heal(int amount)
    {
        if (!IsDead)
        {
            health += amount;
            health = Mathf.Clamp(health, 0, maxHP);
        }
    }

    protected virtual void Kill()
    {
        IsDead = true;
        health = 0;
        effectDeath.SetActive(true);

    }

}
