using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class EventHandler
{
    public delegate void SingleEvent();
    public static event SingleEvent OnDeath;
    public static event SingleEvent OnPlayerDeath;
    public static event SingleEvent OnLevelComplete;
    public static event SingleEvent OnLevelBegin;

    public delegate void CoinEvent(int amount);
    public static event CoinEvent OnCoins;

    public static void Death()
    {
        OnDeath?.Invoke();
    }

    public static void PlayerDeath()
    {
        OnPlayerDeath?.Invoke();
    }

    public static void BeginLevel()
    {
        OnLevelBegin?.Invoke();
    }

    public static void LevelComplete()
    {
        OnLevelComplete?.Invoke();
    }

    public static void Coins(int amount)
    {
        OnCoins?.Invoke(amount);
    }
}
