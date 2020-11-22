using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    [SerializeField] AudioClip buttonClick;
    [SerializeField] AudioClip levelVictory;
    [SerializeField] AudioClip levelDefeat;
    [SerializeField] AudioClip levelStart;
    [SerializeField] AudioClip coinPickup;
    [SerializeField] AudioSource audioSource;

    public static SoundManager instance;

    private void Awake()
    {
        instance = this;
    }

    public AudioSource GetAudioSource()
    {
        return audioSource;
    }

    private void Start()
    {
        EventHandler.OnLevelBegin += Begin;
        EventHandler.OnLevelComplete += Victory;
        EventHandler.OnPlayerDeath += Defeat;
        EventHandler.OnCoins += Coin;
    }

    private void OnDestroy()
    {
        EventHandler.OnLevelBegin -= Begin;
        EventHandler.OnLevelComplete -= Victory;
        EventHandler.OnPlayerDeath -= Defeat;
        EventHandler.OnCoins -= Coin;
    }

    public void PlayButtonClick()
    {
        audioSource.PlayOneShot(buttonClick);
    }

    private void Begin()
    {
        audioSource.PlayOneShot(levelStart);
    }

    private void Victory()
    {
        audioSource.PlayOneShot(levelVictory);
    }

    private void Defeat()
    {
        audioSource.PlayOneShot(levelDefeat);
    }

    private void Coin(int amount)
    {
        if(amount > 0)
        {
            audioSource.PlayOneShot(coinPickup);
        }
    }

}
