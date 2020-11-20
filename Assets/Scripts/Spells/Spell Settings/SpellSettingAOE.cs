using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "tpopl001/Spells/Settings/AOE")]
public class SpellSettingAOE : SpellSettingBase
{
    ObjectPool_Heal op;

    public override void Initialise(GameObject objectPooler)
    {
        op = objectPooler.GetComponentInChildren<ObjectPool_Heal>();
    }

    protected override void UseSpell(Transform spawnPos, float timer, Vector3 targetPos)
    {
        op.Spawn(spawnPos, targetPos, timer);

    }

    protected override void UseSpell(Transform spawnPos, float timer, Health target)
    {
        op.Spawn(spawnPos, target.transform.position + Vector3.up, timer);
    }
}
