using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelSelect : MonoBehaviour
{
    [SerializeField] GameObject levelSelectPanel;
    [SerializeField] Transform prefabSpawn;
    [SerializeField] LevelSelectUIItem prefab;
    [SerializeField] GameObject menu;

    public void OpenLevelSelect()
    {
        levelSelectPanel.SetActive(true);
        menu.SetActive(false);
        if(prefabSpawn.childCount == 0)
        {
            JSONLevel[] levels = HandleJSON.GetLevels();
            for (int i = 0; i < levels.Length; i++)
            {
                if(levels[i].player_score < levels[i].bronze)
                {
                    break;
                }
                LevelSelectUIItem p = Instantiate<LevelSelectUIItem>(prefab, prefabSpawn);
                p.SetItem(levels[i].level_name, levels[i].GetTrophy(), levels[i].player_score);
            }
        }
    }

    public void CloseLevelSelect()
    {
        levelSelectPanel.SetActive(false);
        menu.SetActive(true);
    }

    public void Continue()
    {
        JSONPlayer p = HandleJSON.GetPlayer();
        p.current_level = p.level;
        p.is_endless = 0;
        HandleJSON.WriteJsonPlayer(p);
        SceneManager.LoadScene("Scene_Main");
    }

    public void Endless()
    {
        JSONPlayer p = HandleJSON.GetPlayer();
        p.current_level = 0;
        p.is_endless = 1;
        HandleJSON.WriteJsonPlayer(p);
        SceneManager.LoadScene("Scene_Main");
    }

}
