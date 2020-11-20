using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public static class HandleJSON
{
    const string JSON_PATH = "/items.json";

    public static JSONWeapon GetWeapon(string weap_name)
    {
        JSONWeapon[] json = GetAllWeapons();
        if (json == null) json = new JSONWeapon[0];
        for (int i = 0; i < json.Length; i++)
        {
            if (json[i].item_name.Equals(weap_name))
            {
                return json[i];
            }
        }
        return null;
    }

    public static JSONWeapon[] GetAllWeapons()
    {
        string json = File.ReadAllText(Application.streamingAssetsPath + JSON_PATH);
        if (string.IsNullOrEmpty(json))
        {
            Debug.Log("Failed");
            return null;
        }
        JsonWrapper jW = JsonUtility.FromJson<JsonWrapper>(json);
        JSONWeapon[] j = jW.items;
        return j;
    }


}

[System.Serializable] public struct JsonWrapper { public JSONWeapon[] items; }

[System.Serializable]
public class JSONWeapon
{
    public string item_name;
    public string img;
    public int req_level;
    public int value;
    public int price;

    public JSONWeapon(string item_name, string img, int req_level, int value, int price)
    {
        this.item_name = item_name;
        this.img = img;
        this.req_level = req_level;
        this.value = value;
        this.price = price;
    }
}
