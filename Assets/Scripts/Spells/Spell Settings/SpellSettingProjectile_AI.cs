using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "tpopl001/Spells/Settings/Projectile AI")]
public class SpellSettingProjectile_AI : SpellSettingBase
{
    ObjectPool_Projectile_AI p;

    public override void Initialise(GameObject objectPooler)
    {
        p = objectPooler.GetComponentInChildren<ObjectPool_Projectile_AI>();
    }

    protected override void UseSpell(Transform spawnPos, float timer, Vector3 targetPos)
    {
        p.Spawn(spawnPos, UnitManager.instance.GetHealthPlayer(targetPos), timer);
    }
    protected override void UseSpell(Transform spawnPos, float timer, Health target)
    {
        p.Spawn(spawnPos, target, timer);
    }
}
