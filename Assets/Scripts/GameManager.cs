using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager GM;

    [Header("Game Managing")]
    public bool gameOver = false;
    public GameObject gameOverPanel;
    public GameObject playButtonPanel;
    public GameObject pausePanel;
    public GameObject resumePanel;

    public int gamePlayHighscore;
    public int gameOverScore;
    public int highScore;
    public int score;

    public Text gamePlayHighScoreField;
    public Text gameOverScoreField;
    public Text gameOverHighScoreField;
    public Text resumeScoreField;
    public Text resumeHighScoreField;
    public Text winScoreField;
    public Text winHighScoreField;



    [Header("Coins")]
    public Text coinsLabel;
    public int coins;

    [Header("Waves")]
    public Text waveLabel;
    private int wave;

    [Header("Lives")]
    public GameObject[] liveItems;
    public Text livesLabel;
    private int lives;

    private void Awake()
    {
        Time.timeScale = 0.0F;

        if(GM == null)
        {
            GM = this;
        }
        else
        {
            Destroy(gameObject);
            DontDestroyOnLoad(gameObject);
        }
    }

    private void Start()
    {
        SetWave(0);
        SetLives(liveItems.Length);
        highScore = PlayerPrefs.GetInt("HIGHSCORE", 0);
        gamePlayHighScoreField.text = highScore.ToString();
        gameOverHighScoreField.text = highScore.ToString();
        resumeHighScoreField.text = highScore.ToString();
        winHighScoreField.text = highScore.ToString();
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

    public int GetScore()
    {
        return score;
    }

    public void SetScore(int scoreValue)
    {
        score = scoreValue;
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
            gameOverPanel.SetActive(true);
            GameOver();
            Debug.Log("Play gameover");
            AudioManager.AM.gameOver.Play();
        }

        for (int i = 0; i < liveItems.Length; i++)
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

    public void OnPlayButton()
    {
        playButtonPanel.SetActive(false);
        Time.timeScale = 1.0f;
    }

    public void OnResumeButton()
    {
        resumePanel.SetActive(false);
        pausePanel.SetActive(true);
        Time.timeScale = 1.0f;
    }

    public void OnPauseButton()
    {
        Time.timeScale = 0.0f;
        resumePanel.SetActive(true);
        pausePanel.SetActive(false);
        resumeScoreField.text = score.ToString();
        resumeHighScoreField.text = highScore.ToString();
    }

    public void OnRestartButton()
    {
        Time.timeScale = 1.0f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
     }

    public void GameOver()
    {
        gameOver = true;
        Time.timeScale = 0;
        pausePanel.SetActive(false);

        highScore = PlayerPrefs.GetInt("HIGHSCORE", 0);
        if (score > highScore)
        {
            highScore = score;
            PlayerPrefs.SetInt("HIGHSCORE", highScore);
        }

        gameOverScoreField.text = score.ToString();
        gameOverHighScoreField.text = highScore.ToString();
        gamePlayHighScoreField.text = highScore.ToString();
        winScoreField.text = score.ToString();
        winHighScoreField.text = highScore.ToString();
    }

}
