using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class ManageState : ScriptableObject
{
    [SerializeField] private Vector3 camPos;
    [SerializeField] private Vector3 camRot;
    [SerializeField] private int uiPanel;
    [SerializeField] private string prefab;

    public void SetState(Transform cam, Panels[] panels, int indexType, int dir=0)
    {
        cam.position = camPos;
        cam.rotation = Quaternion.Euler(camRot);
        panels[uiPanel].panel.SetActive(true);
        SetElements(panels[uiPanel].spawnPoint, prefab, indexType, dir);
    }

    public void RemoveState(Panels[] panels)
    {
        int l = panels[uiPanel].spawnPoint.transform.childCount;
        for (int i = 0; i < l; i++)
        {
            Destroy(panels[uiPanel].spawnPoint.transform.GetChild(i).gameObject);
        }

        panels[uiPanel].panel.SetActive(false);
    }

    protected abstract void SetElements(GameObject spawn, string prefab, int indexType, int dir);

}



