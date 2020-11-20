using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "tpopl001/Spells/Settings/Projectile")]
public class SpellSettingProjectile : SpellSettingBase
{
    ObjectPool_Projectile p;

    public override void Initialise(GameObject objectPooler)
    {
        p = objectPooler.GetComponentInChildren<ObjectPool_Projectile>();
    }

    protected override void UseSpell(Transform spawnPos, float timer, Vector3 targetPos)
    {
        p.Spawn(spawnPos, targetPos, timer);
    }
    protected override void UseSpell(Transform spawnPos, float timer, Health target)
    {
        p.Spawn(spawnPos, target.transform.position + Vector3.up, timer);
    }
}
