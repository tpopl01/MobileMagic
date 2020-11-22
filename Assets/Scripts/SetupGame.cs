using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SetupGame : MonoBehaviour
{
   // [SerializeField] PlayerSpell[] spells;
    [SerializeField] bool endless;
    int progress = 1;

    public static SetupGame instance;
    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        endless = HandleJSON.GetPlayer().is_endless == 1;
        Setup();
    }


    void Setup()
    {
        if (!endless)
        {
            progress = HandleJSON.GetPlayer().current_level;
            JSONLevel[] levels = HandleJSON.GetLevels();
            if (progress > levels.Length) progress = levels.Length - 1;
            JSONWave[] jws = levels[progress].waves;
            Wave[] fromJSON = new Wave[jws.Length];
            for (int i = 0; i < jws.Length; i++)
            {
                fromJSON[i] = new Wave(jws[i]);
            }

            WaveManager wM = GetComponentInChildren<WaveManager>();
            wM.SetWaves(fromJSON);
        }
        else
        {
            Wave[] w = new Wave[progress];
            for (int i = 0; i < w.Length; i++)
            {
                int[] units = new int[progress];
                for (int x = 0; x < units.Length; x++)
                {
                    units[x] = Random.Range(0, 3);
                }
                w[i] = new Wave(units, Random.Range(2, 8), (progress % 10 == 0) ? 1 : 0);
            }
            WaveManager wM = GetComponentInChildren<WaveManager>();
            wM.SetWaves(w);
        }
    }

    public void ReplayLevel()
    {
        Setup();
    }

    public void NextLevel()
    {
        if(endless)
        {
            progress++;
            Setup();
        }
        else
        {
            int length = HandleJSON.GetLevels().Length;
            if(progress < length-1)
            {
                JSONPlayer p = HandleJSON.GetPlayer();
                p.current_level = progress;
                HandleJSON.WriteJsonPlayer(p);
                progress++;
                Setup();
            }
            else
            {
                SceneManager.LoadScene("Scene_Main_Menu");
            }
        }
    }

}
