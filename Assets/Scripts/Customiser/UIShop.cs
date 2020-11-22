using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIShop : UIItem
{
    int price = 0;
    string item_name;

    public override void SetItem(JSONWeapon w)
    {
        base.SetItem(w);
        price = w.price;
        item_name = w.item_name;
    }

    public override void Use()
    {
        PanelManager.instance.Purchase(item_name, price);
    }
}
