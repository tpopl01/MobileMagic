using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health_AI : Health
{
    [SerializeField] int coins = 1;


    public override void InitHealth(int maxHealth)
    {
        base.InitHealth(maxHealth);
        UnitManager.instance.AddHealthAI(this);
    }

    protected override void Kill()
    {
        base.Kill();
        UnitManager.instance.RemoveHealthAI(this);
        SetupGame.instance.AddCoins(coins);
    }
}
