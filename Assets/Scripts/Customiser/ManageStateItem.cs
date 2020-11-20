using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "tpopl001/ManageState/Item")]
public class ManageStateItem : ManageState
{
    protected override void SetElements(GameObject spawn, string prefab)
    {
        JSONWeapon[] weapons = HandleJSON.GetAllWeapons();
        if (weapons != null)
        {
            for (int i = 0; i < weapons.Length; i++)
            {
                UIWeapon w = Instantiate<UIWeapon>(Resources.Load<UIWeapon>("UI/" + prefab), spawn.transform);
                w.SetItem(weapons[i]);
            }
        }
    }
}
