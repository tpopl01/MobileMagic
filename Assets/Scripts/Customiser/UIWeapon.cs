using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIWeapon : MonoBehaviour
{
    [SerializeField] Text titleText;
    [SerializeField] Text descText;
    [SerializeField] Image image;

    public void SetItem(JSONWeapon w)
    {
        titleText.text = w.item_name;
        descText.text = "Damage: " + w.value + "\n"
            + "Level: " + w.req_level + "\n"
            + "Damage: " + w.value + "\n"
            + "Price: " + w.price;
        image.sprite = Resources.Load<Sprite>("IMG/" + w.img);
    }

}
