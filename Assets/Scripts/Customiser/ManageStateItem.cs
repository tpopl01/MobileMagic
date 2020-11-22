using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "tpopl001/ManageState/Item")]
public class ManageStateItem : ManageState
{
    [SerializeField]bool purchasedOnly = false;
    const int MAX_WEAPON_DISPLAY = 3;
    int lastIndexMax = 0;
    int lastIndexMin = 0;

    protected override void SetElements(GameObject spawn, string prefab, int indexType, int dir)
    {
        JSONWeapon[] weapons = HandleJSON.GetAllWeapons();
        if (weapons != null)
        {
            if(dir == 0)
            {
                Last(weapons, indexType, prefab, spawn);
            }
            else if(dir == -1)
            {
                Prev(weapons, indexType, prefab, spawn);
            }
            else
            {
                Next(weapons, indexType, prefab, spawn);
            }

            //for (int i = 0; i < weapons.Length; i++)
            //{
            //    if (purchasedOnly && weapons[i].purchased == 0)
            //        continue;
            //    if (!purchasedOnly && weapons[i].purchased == 1)
            //        continue;
            //    if (indexType != -1 && weapons[i].slot != indexType)
            //        continue;

            //    UIWeapon w = Instantiate<UIWeapon>(Resources.Load<UIWeapon>("UI/" + prefab), spawn.transform);
            //    w.SetItem(weapons[i]);
            //}
        }
    }

    private void Next(JSONWeapon[] weapons, int indexType, string prefab, GameObject spawn)
    {
        int index = IncrementIndex(lastIndexMax, weapons.Length);
        int displaying = 0;
        while(displaying != MAX_WEAPON_DISPLAY)
        {
            if (purchasedOnly && weapons[index].purchased == 0)
            {
                if (index == lastIndexMax) break;
                index = IncrementIndex(index, weapons.Length);
                continue;
            }
            if (!purchasedOnly && weapons[index].purchased == 1)
            {
                if (index == lastIndexMax) break;
                index = IncrementIndex(index, weapons.Length);
                continue;
            }
            if (indexType != -1 && weapons[index].slot != indexType)
            {
                if (index == lastIndexMax) break;
                index = IncrementIndex(index, weapons.Length);
                continue;
            }

            displaying++;
            LoadItem(weapons[index], prefab, spawn);

            if(displaying == 1)
            {
                lastIndexMin = index;
            }
            else if(displaying == MAX_WEAPON_DISPLAY)
            {
                lastIndexMax = index;
                break;
            }

            if (index == lastIndexMax) break;
            index = IncrementIndex(index, weapons.Length);
        }
    }

    private void Last(JSONWeapon[] weapons, int indexType, string prefab, GameObject spawn)
    {
        int index = IncrementIndex(lastIndexMin, weapons.Length);
        int displaying = 0;
        while (index != lastIndexMin && displaying != MAX_WEAPON_DISPLAY)
        {
            if (purchasedOnly && weapons[index].purchased == 0)
            {
                index = IncrementIndex(index, weapons.Length);
                continue;
            }
            if (!purchasedOnly && weapons[index].purchased == 1)
            {
                index = IncrementIndex(index, weapons.Length);
                continue;
            }
            if (indexType != -1 && weapons[index].slot != indexType)
            {
                index = IncrementIndex(index, weapons.Length);
                continue;
            }

            displaying++;
            LoadItem(weapons[index], prefab, spawn);

            if (displaying == 1)
            {
                lastIndexMin = index;
            }
            else if (displaying == MAX_WEAPON_DISPLAY)
            {
                lastIndexMax = index;
                break;
            }

            index = IncrementIndex(index, weapons.Length);
        }
    }

    private void Prev(JSONWeapon[] weapons, int indexType, string prefab, GameObject spawn)
    {
        int index = DecrementIndex(lastIndexMin, weapons.Length);
        int displaying = 0;
        List<JSONWeapon> wps = new List<JSONWeapon>();
        while (displaying != MAX_WEAPON_DISPLAY)
        {
            if (purchasedOnly && weapons[index].purchased == 0)
            {
                if (index == lastIndexMin) break;
                index = DecrementIndex(index, weapons.Length);
                continue;
            }
            if (!purchasedOnly && weapons[index].purchased == 1)
            {
                if (index == lastIndexMin) break;
                index = DecrementIndex(index, weapons.Length);
                continue;
            }
            if (indexType != -1 && weapons[index].slot != indexType)
            {
                if (index == lastIndexMin) break;
                index = DecrementIndex(index, weapons.Length);
                continue;
            }

            displaying++;
            wps.Add(weapons[index]);
           // LoadItem(weapons[index], prefab, spawn);

            if (displaying == 1)
            {
                lastIndexMax = index;
            }
            else if (displaying == MAX_WEAPON_DISPLAY)
            {
                lastIndexMin = index;
                break;
            }
            if (index == lastIndexMin) break;
            index = DecrementIndex(index, weapons.Length);
        }

        for (int i = wps.Count-1; i > -1; i--)
        {
            LoadItem(wps[i], prefab, spawn);
        }
    }

    private int IncrementIndex(int index, int max)
    {
        index++;
        if (index >= max)
            index = 0;
        return index;
    }

    private int DecrementIndex(int index, int max)
    {
        index--;
        if (index < 0)
            index = max - 1;
        return index;
    }

    private void LoadItem(JSONWeapon weapon, string prefab, GameObject spawn)
    {
        UIItem w = Instantiate<UIItem>(Resources.Load<UIItem>("UI/" + prefab), spawn.transform);
        w.SetItem(weapon);
    }
}
