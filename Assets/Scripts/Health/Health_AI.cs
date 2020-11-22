using System.Collections;
using System.Collections.Generic;
using tpopl001.Utils;
using UnityEngine;

public class Health_AI : Health
{
    [SerializeField] int coins = 1;
    public AIAudioHandler audioHandler;

    public override void InitHealth(int maxHealth)
    {
        base.InitHealth(maxHealth);
        UnitManager.instance.AddHealthAI(this);
    }

    public override void DamageHealth(int amount)
    {
        base.DamageHealth(amount);
        audioHandler.PlayDamagedAudio(SoundManager.instance.GetAudioSource());
    }

    protected override void Kill()
    {
        base.Kill();
        audioHandler.PlayDeathAudio(SoundManager.instance.GetAudioSource());
        UnitManager.instance.RemoveHealthAI(this);
        EventHandler.Coins(coins);
    }

}
