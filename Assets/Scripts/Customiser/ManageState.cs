using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class ManageState : ScriptableObject
{
    [SerializeField] private Vector3 camPos;
    [SerializeField] private Quaternion camRot;
    [SerializeField] private int uiPanel;
    [SerializeField] private string prefab;

    public void SetState(Transform cam, Panels[] panels)
    {
        cam.position = camPos;
        cam.rotation = camRot;
        panels[uiPanel].panel.SetActive(true);
        SetElements(panels[uiPanel].spawnPoint, prefab);
    }

    public void RemoveState(Panels[] panels)
    {
        int l = panels[uiPanel].spawnPoint.transform.childCount;
        for (int i = 0; i < l; i++)
        {
            Destroy(panels[uiPanel].spawnPoint.transform.GetChild(i));
        }

        panels[uiPanel].panel.SetActive(false);
    }

    protected abstract void SetElements(GameObject spawn, string prefab);

}



