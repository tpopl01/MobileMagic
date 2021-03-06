﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIWeapon : UIItem
{
    string item_name;
    int slot;

    public override void SetItem(JSONWeapon w)
    {
        base.SetItem(w);
        item_name = w.item_name;
        slot = w.slot;
    }

    public override void Use()
    {
        PanelManager.instance.Equip(item_name, slot);
    }

}
