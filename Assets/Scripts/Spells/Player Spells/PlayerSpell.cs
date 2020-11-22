using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "tpopl001/Spells/Player/Spell")]
public class PlayerSpell : ScriptableObject
{
    public SpellSettingBase spell;
    public DetectMotion motion;
    public Sprite icon;
}
