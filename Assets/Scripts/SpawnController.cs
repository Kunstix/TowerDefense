using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnController : MonoBehaviour
{
    public GameObject[] pathPoints;
    public GameObject winMessagePanel;
    public Wave[] waves;
    public int timeBetweenWaves = 5;
    private int enemiesCount;
    private float lastSpawnTime;

    void Start()
    {
        lastSpawnTime = Time.time;
    }

    void Update()
    {
        int currentWaveIndex = GameManager.GM.GetWave();

        Debug.Log("Waves count: " + waves.Length);
        Debug.Log("Current wave: " + currentWaveIndex);

        if (currentWaveIndex < waves.Length)
        {
            Wave currentWave = waves[currentWaveIndex];
            SpawnEnemy(currentWave);
            BeatWave(currentWave);
        }
        else
        {
            Debug.Log("Game ends.");
            Time.timeScale = 0;
            GameManager.GM.GameOver();
            winMessagePanel.SetActive(true);
        }
    }

    private void SpawnEnemy(Wave currentWave)
    {
        if (ShouldSpawnEnemy(currentWave))
        {
            Debug.Log("Spawn enemy!");
            lastSpawnTime = Time.time;
            GameObject newEnemy = Instantiate(currentWave.enemyPrefab, pathPoints[0].transform.position, Quaternion.Euler(0, 0, 90));
            newEnemy.GetComponent<EnemyController>().pathPoints = pathPoints;
            enemiesCount++;
        }
    }

    private bool ShouldSpawnEnemy(Wave currentWave)
    {
        float timePassed = Time.time - lastSpawnTime;

        bool timeToSpawnEnemy = timePassed > currentWave.spawnInterval;
        bool timeBetweenWavesPassed = timePassed > timeBetweenWaves;
        bool noEnemiesPresent = enemiesCount == 0;
        bool notEnoughEnemies = enemiesCount < currentWave.maxEnemiesCount;

        bool shouldSpawnEnemy = ((noEnemiesPresent && timeBetweenWavesPassed) || timeToSpawnEnemy) && notEnoughEnemies;
        Debug.Log("Should spawn enemy: " + shouldSpawnEnemy);

        return shouldSpawnEnemy;
    }

    private void BeatWave(Wave currentWave)
    {
        bool maxEnemiesReached = enemiesCount == currentWave.maxEnemiesCount;
        bool allEnemiesDead = GameObject.FindGameObjectWithTag("Enemy") == null;

        Debug.Log("All enemies dead: " + allEnemiesDead);
        Debug.Log("Max enemies reached: " + maxEnemiesReached);

        if (maxEnemiesReached && allEnemiesDead)
        {
            StartNewWave();
            IncreaseCoins();
        }
    }

    private void IncreaseCoins()
    {
        Debug.Log("Increase coins!");
        GameManager.GM.SetCoins(Mathf.RoundToInt(GameManager.GM.GetCoins() * 1.1F));
    }

    private void StartNewWave()
    {
        Debug.Log("Increase wave to: " + (GameManager.GM.GetWave() + 1));
        GameManager.GM.SetWave(GameManager.GM.GetWave() + 1);
        enemiesCount = 0;
        lastSpawnTime = Time.time;
    }
}

[System.Serializable]
public class Wave
{
    public GameObject enemyPrefab;
    public float spawnInterval;
    public int maxEnemiesCount;
}
