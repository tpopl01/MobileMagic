using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public static class HandleJSON
{
    const string JSON_PATH = "/items.json";
    const string JSON_PLAYER_PATH = "/player.json";
    const string JSON_LEVEL_PATH = "/level.json";

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

    public static JSONWeapon[] GetAllEquippedWeapons()
    {
        JSONWeapon[] weapons = GetAllWeapons();
        List<JSONWeapon> w = new List<JSONWeapon>();
        for (int i = 0; i < weapons.Length; i++)
        {
            if(weapons[i].is_equipped == 1)
            {
                w.Add(weapons[i]);
            }
        }
        return w.ToArray();
    }

    public static JSONPlayer GetPlayer()
    {
        string json = File.ReadAllText(Application.streamingAssetsPath + JSON_PLAYER_PATH);

        JSONPlayer p = JsonUtility.FromJson<JSONPlayer>(json);

        return p;
    }

    public static JSONLevel[] GetLevels()
    {
        string json = File.ReadAllText(Application.streamingAssetsPath + JSON_LEVEL_PATH);
        if (string.IsNullOrEmpty(json))
        {
            Debug.Log("Failed");
            return null;
        }
        JsonLevelWrapper jW = JsonUtility.FromJson<JsonLevelWrapper>(json);
        JSONLevel[] j = jW.levels;
        return j;
    }

    public static void WriteWeapon(JSONWeapon w)
    {
        JSONWeapon[] weaps = GetAllWeapons();
        for (int i = 0; i < weaps.Length; i++)
        {
            if(weaps[i].item_name.Equals(w.item_name))
            {
                weaps[i] = w;
                string j = JsonUtility.ToJson(weaps, true);
                File.WriteAllText(Application.streamingAssetsPath + JSON_PATH, j);
                break;
            }
        }
    }
    public static void WriteWeaponPurchased(string item_name, int purchased)
    {
        JSONWeapon[] weaps = GetAllWeapons();
        for (int i = 0; i < weaps.Length; i++)
        {
            if (weaps[i].item_name.Equals(item_name))
            {
                weaps[i].purchased = purchased;
                JsonWrapper jW1 = new JsonWrapper();
                jW1.items = weaps;
                string j = JsonUtility.ToJson(jW1, true);
                File.WriteAllText(Application.streamingAssetsPath + JSON_PATH, j);
                break;
            }
        }
    }
    public static void WriteWeaponEquipped(string item_name, int equipped)
    {
        JSONWeapon[] weaps = GetAllWeapons();
        for (int i = 0; i < weaps.Length; i++)
        {
            if (weaps[i].item_name.Equals(item_name))
            {
                weaps[i].is_equipped = equipped;
                JsonWrapper jW1 = new JsonWrapper();
                jW1.items = weaps;
                string j = JsonUtility.ToJson(jW1, true);
                File.WriteAllText(Application.streamingAssetsPath + JSON_PATH, j);
                break;
            }
        }
    }

    public static void WriteWeaponUnequipped(int slot)
    {
        JSONWeapon[] weaps = GetAllWeapons();
        for (int i = 0; i < weaps.Length; i++)
        {
            if (weaps[i].slot == slot && weaps[i].is_equipped != 0)
            {
                weaps[i].is_equipped = 0;
                JsonWrapper jW1 = new JsonWrapper();
                jW1.items = weaps;
                string j = JsonUtility.ToJson(jW1, true);
                File.WriteAllText(Application.streamingAssetsPath + JSON_PATH, j);
                break;
            }
        }
    }

    public static void WriteJsonPlayer(JSONPlayer p)
    {
        string j = JsonUtility.ToJson(p, true);
        File.WriteAllText(Application.streamingAssetsPath + JSON_PLAYER_PATH, j);
    }

    public static void WriteJsonLevel(JSONLevel level)
    {
        JSONLevel[] levels = GetLevels();
        for (int i = 0; i < levels.Length; i++)
        {
            if(levels[i].level_name.Equals(level.level_name))
            {
                levels[i] = level;
                JsonLevelWrapper jW = new JsonLevelWrapper();
                jW.levels = levels;
                string j = JsonUtility.ToJson(jW, true);
                File.WriteAllText(Application.streamingAssetsPath + JSON_LEVEL_PATH, j);
                break;
            }
        }
    }

}

[System.Serializable] public struct JsonWrapper { public JSONWeapon[] items; }

[System.Serializable]
public class JSONWeapon
{
    public string item_name;
    public string img;
    public int req_level;
    public int damage;
    public int speed;
    public int price;
    public int purchased;
    public int slot;
    public int is_equipped;

    public JSONWeapon(string item_name, string img, int req_level, int damage, int speed, int price, int purchased, int slot, int is_equipped)
    {
        this.item_name = item_name;
        this.img = img;
        this.req_level = req_level;
        this.damage = damage;
        this.speed = speed;
        this.price = price;
        this.purchased = purchased;
        this.slot = slot;
        this.is_equipped = is_equipped;
    }
}

[System.Serializable]
public class JSONPlayer
{
    public string name;
    public int level;
    public int coins;
    public int endless_level;
    public int endless_highscore;
    public int current_level;
    public int is_endless;
    //public JSONLevel[] levels;

    public JSONPlayer(string name, int level, int coins, int endless_level, int endless_highscore, int current_level, int is_endless)
    {
        this.name = name;
        this.level = level;
        this.coins = coins;
        this.endless_level = endless_level;
        this.endless_highscore = endless_highscore;
        this.current_level = current_level;
        this.is_endless = is_endless;
        //  this.levels = levels;
    }
}

[System.Serializable] public struct JsonLevelWrapper { public JSONLevel[] levels; }
[System.Serializable]
public class JSONLevel
{
    public string level_name;
    public int player_score;
    public int gold;
    public int silver;
    public int bronze;
    public JSONWave[] waves;

    public string GetTrophy()
    {
        if (player_score > gold) return "Gold";
        else if (player_score > silver) return "Silver";
        else if (player_score > bronze) return "Bronze";
        return "";
    }
}

[System.Serializable]
public class JSONWave
{
    public int[] pattern;
    public int timer;
    public int pickable_count;
}
