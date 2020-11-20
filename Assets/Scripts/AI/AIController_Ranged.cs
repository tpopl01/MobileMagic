using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIController_Ranged : AIController
{
   // [SerializeField] ObjectPool_Projectile_AI projectile;
    [SerializeField] protected Transform spawnPos;
    [SerializeField] protected SpellSettingBase[] spellSetting;

    protected override void Init()
    {
        base.Init();
        for (int i = 0; i < spellSetting.Length; i++)
        {
            spellSetting[i].Initialise(gameObject);
        }
        //projectile = GetComponentInChildren<ObjectPool_Projectile_AI>();
    }

    protected override void Attack()
    {
        base.Attack();
        spellSetting[Random.Range(0, spellSetting.Length)].TryUseSpell(spawnPos, target, null);
       // projectile.Spawn(spawnPos, target, spawnTime);
        //spellSetting.TryUseSpell(spawnPos, null, target.position, null);
    }


}
