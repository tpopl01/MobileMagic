using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelCompleteUI : MonoBehaviour
{
    int coins = 0;
    [SerializeField] Text coinText;
    const string COIN = "Coins: ";

    [SerializeField] GameObject victory;
    [SerializeField] GameObject defeat;
    [SerializeField] Text points_victory;
    [SerializeField] Text points_defeat;
    [SerializeField] Text trophy;

    private void Start()
    {
        EventHandler.OnLevelComplete += Victory;
        EventHandler.OnPlayerDeath += Defeat;
        coinText.text = COIN + coins.ToString();
        EventHandler.OnCoins += AddCoins;
    }

    private void OnDestroy()
    {
        EventHandler.OnLevelComplete -= Victory;
        EventHandler.OnPlayerDeath -= Defeat;
        EventHandler.OnCoins -= AddCoins;
    }

    private void Victory()
    {
        victory.SetActive(true);
        JSONPlayer player = HandleJSON.GetPlayer();
        player.coins += coins;
        points_victory.text = "Points: " + coins;
        HandleJSON.WriteJsonPlayer(player);

        JSONLevel l = HandleJSON.GetLevels()[0];
        if(coins >= l.gold)
        {
            trophy.text = "Gold";
        }
        else if (coins >= l.silver)
        {
            trophy.text = "Silver";
        }
        else if (coins >= l.bronze)
        {
            trophy.text = "Bronze";
        }
        else
        {
            trophy.text = "";
        }
    }

    private void Defeat()
    {
        defeat.SetActive(true);
        points_defeat.text = "Points: " + coins;
    }

    private void AddCoins(int amount)
    {
        coins += amount;
        coinText.text = COIN + coins.ToString();
    }
}
