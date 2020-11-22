using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PanelManager : MonoBehaviour
{
    private int index = 0;

    [SerializeField] private ManageState[] manageStates;
    [SerializeField] Panels[] uiPanels;
    [SerializeField] string[] weaponType = new string[] { "Weapons", "Spells" };
    private int indexType = -1;
    [SerializeField] Text typeText;
    [SerializeField] Transform weaponHolder;
    [SerializeField] Text coinsText;
    JSONPlayer jSONPlayer;

    public static PanelManager instance;
    void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        if(manageStates.Length > 0)
        {
            index = 0;
            SetState(index,-1);
        }

        jSONPlayer = HandleJSON.GetPlayer();
        coinsText.text = "Coins: " + jSONPlayer.coins;
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.E))
        {
            Next();
        }
        else if(Input.GetKeyDown(KeyCode.Q))
        {
            Back();
        }
    }

    private void Next()
    {
        RemoveState(index);
        index++;
        if (index >= manageStates.Length)
        {
            index = 0;
        }
        SetState(index, -1);
    }

    private void Back()
    {
        RemoveState(index);
        index--;
        if (index < 0)
        {
            index = manageStates.Length-1;
        }
        SetState(index, -1);
    }

    public void NextType()
    {
        indexType++;
        if(indexType >= weaponType.Length)
        {
            indexType = -1;
        }
        RemoveState(index);
        SetState(index, indexType);
    }

    public void PrevType()
    {
        indexType--;
        if (indexType < -1)
        {
            indexType = weaponType.Length-1;
        }
        RemoveState(index);
        SetState(index, indexType);
    }

    public void NextSet()
    {
        RemoveState(index);
        SetState(index, indexType, 1);
    }

    public void PrevSet()
    {
        RemoveState(index);
        SetState(index, indexType, -1);
    }

    private void RemoveState(int i)
    {
        manageStates[i].RemoveState(uiPanels);
    }

    private void SetState(int i, int indexType, int dir = 0)
    {
        if(indexType != -1)
        {
            typeText.text = weaponType[indexType];
        }
        else
        {
            typeText.text = "All";
        }
        manageStates[i].SetState(Camera.main.transform, uiPanels, indexType, dir);
    }

    public void Purchase(string item_name, int price)
    {
        if(jSONPlayer.coins >= price)
        {
            jSONPlayer.coins -= price;
            coinsText.text = jSONPlayer.coins.ToString();

            //write item as purchased
            HandleJSON.WriteWeaponPurchased(item_name, 1);

            //write json player
            HandleJSON.WriteJsonPlayer(jSONPlayer);

            RemoveState(index);

            //reload
            SetState(index, indexType);
        }
    }

    public void Equip(string item_name, int slot)
    {
        Debug.Log(slot);
        if (slot == 0)
        {
            // visually unequip old item
            int childCount = weaponHolder.childCount;
            if (childCount > 0)
            {
                Destroy(weaponHolder.GetChild(0).gameObject);
            }

            // visually equip new item
            GameObject w = Instantiate(Resources.Load<GameObject>("Weapons/" + item_name), weaponHolder);
            w.transform.localPosition = Vector3.zero;
            w.transform.rotation = Quaternion.Euler(Vector3.zero);
        }

        // write item as unequipped
        HandleJSON.WriteWeaponUnequipped(slot);

        // write item as equipped
        HandleJSON.WriteWeaponEquipped(item_name, 1);

        RemoveState(index);

        //reload
        SetState(index, indexType);
    }

}

[System.Serializable]
public class Panels
{
    public GameObject panel;
    public GameObject spawnPoint;
}

