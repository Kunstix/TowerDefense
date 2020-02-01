using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager GM;

    public Text coinsLabel;
    public int coins;
    public Text waveLabel;
    private int wave;

    public bool gameOver = false;
    public GameObject gameOverPanel;

    public GameObject[] liveItems;
    public Text livesLabel;
    private int lives;

    private void Awake()
    {
        if(GM == null)
        {
            GM = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        SetWave(0);
        SetLives(liveItems.Length);
    }

    private void Update()
    {
        DisplayCoins();
        DisplayWave();
        DisplayLives();
    }

    public int GetCoins()
    {
        return coins;
    }

    public void SetCoins(int coinsValue)
    {
        coins = coinsValue;
    }

    public int GetWave()
    {
        return wave;
    }

    public void SetWave(int currentWave)
    {
        wave = currentWave;
    }

    public int GetLives()
    {
        return lives;
    }

    public void SetLives(int currentLives)
    {
        lives = currentLives;
        if (lives <= 0 && !gameOver)
        {
            gameOver = true;
            Time.timeScale = 0;
            gameOverPanel.SetActive(true);
        }

        for(int i = 0; i < liveItems.Length; i++)
        {
            if(i < lives)
            {
                Debug.Log("Active live: " + liveItems[i]);
                liveItems[i].SetActive(true);
            } else
            {
                Debug.Log("Deactive live: " + liveItems[i]);
                liveItems[i].SetActive(false);
            }
        }
    }

    public void DisplayCoins()
    {
        coinsLabel.text = "Coins: " + coins;
    }

    private void DisplayWave()
    {
        waveLabel.text = "Wave: " + wave;
    }

    private void DisplayLives()
    {
        livesLabel.text = "Lives: " + lives;
    }

}
