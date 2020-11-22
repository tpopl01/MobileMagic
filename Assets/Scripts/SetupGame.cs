﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SetupGame : MonoBehaviour
{
    [SerializeField] PlayerSpell[] spells;
    int coins = 0;
    [SerializeField]Text coinText;
    const string COIN = "Coins: ";

    public static SetupGame instance;
    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        Setup();
        coinText.text = COIN + coins.ToString();
    }

    void Setup()
    {
        //Wave[] ws = {
        //    new Wave(new int[] { 0, 0 }, 3, 1),
        //    new Wave(new int[]{ 0,0,0,1,1,2 }, 10, 0),
        //    new Wave(new int[]{ 0,0,0,0 }, 1, 1)
        //};

        JSONWave[] jws = HandleJSON.GetLevels()[0].waves;
        Wave[] fromJSON = new Wave[jws.Length];
        for (int i = 0; i < jws.Length; i++)
        {
            fromJSON[i] = new Wave(jws[i]);
        }

        WaveManager wM = GetComponentInChildren<WaveManager>();
        wM.w = fromJSON;

    }

    public void AddCoins(int amount)
    {
        coins+= amount;
        coinText.text = COIN + coins.ToString();
    }

}
