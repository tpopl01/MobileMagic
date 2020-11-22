using System.Collections;
using System.Collections.Generic;
using tpopl001.Utils;
using UnityEngine;

public class WaveManager : MonoBehaviour
{
    Wave[] w;
    int index = 0;
    ObjectPool_AI[] objectPools;
    ObjectPool_AI_Pickables pickablePool;
    Timer spawnTimer = new Timer(0);
    [SerializeField]Transform[] spawnSpots;
    [SerializeField] Transform[] pickableSpots;
    int unitsRemaining;

    public void SetWaves(Wave[] ws)
    {
        w = ws;
        for (int i = 0; i < w.Length; i++)
        {
            unitsRemaining += w[i].spawnPattern.Length;
        }
    }

    //public static WaveManager instance;
    //private void Awake()
    //{
    //    instance = this;
    //}

    void Start()
    {
        objectPools = GetComponentsInChildren<ObjectPool_AI>();
        pickablePool = GetComponentInChildren<ObjectPool_AI_Pickables>();
        EventHandler.OnDeath += OnDeath;
    }
    
    void Update()
    {
        if (w.Length <= index) return;
        if(spawnTimer.GetComplete())
        {
            if (w[index].Spawned(objectPools, spawnSpots, pickablePool, pickableSpots))
            {
                spawnTimer.Duration = w[index].coolDownTime;
                index++;
                spawnTimer.StartTimer();
            }
        }
    }

    public void OnDeath()
    {
        unitsRemaining--;
        if(unitsRemaining <= 0)
        {
            //End Level
            Debug.Log("Level Complete");

            //start level ended event
            EventHandler.LevelComplete();
        }
    }

    private void OnDestroy()
    {
        EventHandler.OnDeath -= OnDeath;
    }

}

[System.Serializable]
public class Wave
{
    public int[] spawnPattern;
    int counter;
    public float coolDownTime = 3;
    int pickables = 0;

    public Wave(int[] spawnPattern, float coolDownTimer, int pickableCount = 0)
    {
        this.spawnPattern = spawnPattern;
        this.coolDownTime = coolDownTimer;
        this.pickables = pickableCount;
    }

    public Wave(JSONWave wave)
    {
        this.spawnPattern = wave.pattern;
        this.coolDownTime = wave.timer;
        this.pickables = wave.pickable_count;
    }

    public bool Spawned(ObjectPool_AI[] objectPools, Transform[] spots, ObjectPool_AI_Pickables p, Transform[] pickableSpots)
    {
        if (counter == spawnPattern.Length) return true;

        while(counter < spawnPattern.Length)
        {
            Vector3 pos = spots[Random.Range(0, spots.Length)].position;
            if(!objectPools[spawnPattern[counter]].Spawn(new Vector3(pos.x+ Random.Range(-5,5), pos.y, pos.z+ Random.Range(-5,5))))
            {
                return false;
            }
            counter++;
        }

        int c = 0;
        while(c< pickables)
        {
            p.Spawn(pickableSpots[Random.Range(0, pickableSpots.Length)].position);
            Debug.Log("Spawn Pickable");
            c++;
        }

        return false;
    }

}
