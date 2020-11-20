using System.Collections;
using System.Collections.Generic;
using tpopl001.Utils;
using UnityEngine;

public class Healer_Player : MonoBehaviour
{
    Timer timer = new Timer(1);
    Health_Player health;
    [SerializeField] int heal = 3;


    void Start()
    {
        health = GameObject.FindObjectOfType<Health_Player>();
    }

    void Update()
    {
        if(timer.GetComplete())
        {
            timer.StartTimer();
            health.Heal(heal);
        }
    }
}
