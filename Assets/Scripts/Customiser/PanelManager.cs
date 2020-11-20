using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PanelManager : MonoBehaviour
{
    private int index = 0;
    [SerializeField] private ManageState[] manageStates;
    [SerializeField] Panels[] uiPanels;

    private void Start()
    {
        if(manageStates.Length > 0)
        {
            index = 0;
            SetState(index);
        }
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
        SetState(index);
    }

    private void Back()
    {
        RemoveState(index);
        index--;
        if (index < 0)
        {
            index = manageStates.Length-1;
        }
        SetState(index);
    }

    private void RemoveState(int i)
    {
        manageStates[i].RemoveState(uiPanels);
    }

    private void SetState(int i)
    {
        manageStates[i].SetState(Camera.main.transform, uiPanels);
    }

}

[System.Serializable]
public class Panels
{
    public GameObject panel;
    public GameObject spawnPoint;
}

