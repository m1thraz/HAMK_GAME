using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using TMPro;
using Unity.VisualScripting;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField]
    private GameObject spikyBall, goblin, spider, zombie, drake, bossPrefab;
    List<GameObject> enemies = new();
    [SerializeField] bool inStory = false;
    
    


    private float horMin, horMax, verMin, verMax;

    [SerializeField]
    int waveCount = 1;
    [SerializeField]
    float startingEnemies= 5, enemyGrowth = 1.5f;
    [SerializeField]
    int enemiesInWave;
    
    [SerializeField]
    private float timeBetweenEnemies = 2f;
    private float enemyTimer = 0;
    [SerializeField]
    private bool activeWave = true;
    [SerializeField]
    private bool stopSpawn = false;

    [SerializeField]
    private TextMeshProUGUI waveCountText;

    [SerializeField]
    private float spawnedEnemyCount;
    [SerializeField]
    private float currentEnemies = 0;
    [SerializeField]
    public float killedEnemies = 0;
    private bool spawnBoss =true;
    [SerializeField]
    private bool allEnemiesSpawned = false;
    void Start()
    {

        Debug.Log("spawner started");
        Camera camera = Camera.main;
        float halfHeight = camera.orthographicSize;
        float halfWidth = camera.aspect * halfHeight;

        horMin = -halfWidth;
        horMax = halfWidth;
        verMin = -halfHeight;
        verMax = halfHeight;
        enemiesInWave = Mathf.CeilToInt(startingEnemies * enemyGrowth);
        enemies.Add(spikyBall);
    }
    private void Update()
    {
        if (!inStory) { 
        enemyTimer += Time.deltaTime;
        if (enemyTimer >= timeBetweenEnemies && spawnedEnemyCount < enemiesInWave)
        {
            waveSpawner();
        }
        if (allEnemiesSpawned && killedEnemies == spawnedEnemyCount)
        {
            switch (waveCount)
            {
                case 1:
                    enemies.Add(goblin);
                    break;
                case 2:
                    enemies.Add(spider);
                    break;
                case 3:
                    enemies.Add(zombie);
                    break;
                case 4:
                    enemies.Add(drake);
                    break;
            }
            allEnemiesSpawned = false;
            killedEnemies = 0;
            waveCount++;
            if(waveCount == 10)
            {
                timeBetweenEnemies = 2f;
            } else
            {
                timeBetweenEnemies *= 0.9f;

            }
            enemiesInWave = Mathf.CeilToInt(startingEnemies * Mathf.Pow(enemyGrowth, waveCount));
            spawnedEnemyCount = 0;
            enemyTimer = 0;
            currentEnemies = 0;
            }
        }
        
    }
    private ArrayList getSpawnLocation()
    {
        float x, y;
        int direction = Random.Range(1, 5);
        switch (direction)
        {
            case 1:
                x = horMin - 0.3f;
                y = Random.Range(verMin, verMax);
                break;
            case 2:
                x = Random.Range(horMin, horMax);
                y = verMax + 0.3f;
                break;
            case 3:
                x = horMax + 0.3f;
                y = Random.Range(verMin, verMax);
                break;
            case 4:
                x = Random.Range(horMin, horMax);
                y = verMin - 0.3f;
                break;
            default:
                x = horMin;
                y = verMax;
                break;

        }
        ArrayList returnVal = new();
        returnVal.Add((float)x);
        returnVal.Add((float)y);
        return returnVal;

    }
    public void spawnEnemy(GameObject enemy)
    {
        ArrayList pos = getSpawnLocation();

        Vector2 playerLocation = GameObject.Find("Player").transform.position;

        GameObject newEnemy = Instantiate(enemy, new Vector2((float)pos[0] + playerLocation.x, (float)pos[1] + playerLocation.y), Quaternion.identity);
        if (waveCount > 5)
        {
            float difficultyMultiplier = (waveCount + 1) * 0.2f;
            if (newEnemy.GetComponent<shootingEnemy>())
            {
                newEnemy.GetComponent<shootingEnemy>().init(difficultyMultiplier);
            } 
            if (newEnemy.GetComponent<ChasingEnemy>())
            {
                newEnemy.GetComponent<ChasingEnemy>().init(difficultyMultiplier);
            }
            if (newEnemy.GetComponent<smartChasing>())
            {
                newEnemy.GetComponent<smartChasing>().init(difficultyMultiplier);
            }
        }
    }
    private void updateWaveText(int waveCount)
    {
        waveCountText.text = "Wave: " + waveCount.ToString();

    }
    public void enemyDied()
    {
        currentEnemies--;
        killedEnemies++;
    }
    private void waveSpawner()
    {
        int enemyIndex = Random.Range(0, enemies.Count);
        spawnEnemy(enemies[enemyIndex]);
        spawnedEnemyCount++;
        currentEnemies++;
        enemyTimer = 0;
        if (waveCount == 10)
        {
            spawnedEnemyCount = 0;
            if (spawnBoss)
            {
                spawnEnemy(bossPrefab);
                spawnBoss = false;
            }                
        }
        if(spawnedEnemyCount == enemiesInWave && spawnBoss == true)
        {
            allEnemiesSpawned = true;
        }
    }  
}