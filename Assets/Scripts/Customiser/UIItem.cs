using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public abstract class UIItem : MonoBehaviour
{
    [SerializeField] Text titleText;
    [SerializeField] Text descText;
    [SerializeField] Image image;

    public virtual void SetItem(JSONWeapon w)
    {
        titleText.text = w.item_name;
        descText.text = "Damage: " + w.damage + "\n"
            + "Speed: " + w.speed + "\n"
            + "Price: " + w.price + "\n"
            + "Equipped: " + w.is_equipped + "\n"
            + "Level: " + w.req_level;
        image.sprite = Resources.Load<Sprite>("IMG/" + w.img);
    }

    public abstract void Use();
}
