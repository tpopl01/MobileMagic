using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LevelSelectUIItem : MonoBehaviour
{
    int level;
    [SerializeField] Text title;
    [SerializeField] Text trophy;

    public void SetItem(string title, string trophy, int level)
    {
        this.level = level;
        this.title.text = title;
        this.trophy.text = trophy;
    }

    public void LoadLevel()
    {
        JSONPlayer p = HandleJSON.GetPlayer();
        p.current_level = level;
        p.is_endless = 0;
        HandleJSON.WriteJsonPlayer(p);
        SceneManager.LoadScene("Scene_Main");
    }

}
