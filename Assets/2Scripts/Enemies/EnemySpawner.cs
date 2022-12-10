using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using TMPro;
using Unity.VisualScripting;

public class EnemySpawner : MonoBehaviour
{
    //Initialize prefabs here
    [SerializeField]
    private GameObject spikyBall, goblin, spider, zombie, drake, bossPrefab;
    [SerializeField] bool waveMode = true;




    private float horMin, horMax, verMin, verMax;

    // initialize stuff for waves
    [SerializeField]
    int waveCount = 1;
    public float maxSpawningIterations = 5;
    public float currentSpawningIterations = 0;
    private float SpawnModifier = 1.4f;
    private float spawnRate = 1.0f;
    [SerializeField]
    private float timeBetweenWaves = 10.0f;
    private float timeBetweenEnemies = 2f;
    private float enemyTimer = 0;
    [SerializeField]
    private bool activeWave = true;
    [SerializeField]
    private bool stopSpawn = false;

    [SerializeField]
    private TextMeshProUGUI waveCountText;

    public float spawnedEnemyCount;
    public float currentEnemies = 1;
    public float killedEnemies = 0;
    private bool spawnBoss =true;
    public bool allEnemiesSpawned = false;
    // Start is called before the first frame update
    void Start()
    {
        if (waveMode)
        {
            Debug.Log("spawner started");
            Camera camera = Camera.main;
            float halfHeight = camera.orthographicSize;
            float halfWidth = camera.aspect * halfHeight;

            horMin = -halfWidth;
            horMax = halfWidth;
            verMin = -halfHeight;
            verMax = halfHeight;
            //StartCoroutine(waveSpawner());
            //StartCoroutine(spawnEnemy(enemyInterval1, enemyPrefab1));
            //StartCoroutine(spawnEnemy(enemyInterval2, enemyPrefab2));
            //StartCoroutine(spawnEnemy(enemyPrefab11, goblinPrefab));
            //goblinPrefab.transform.parent = cloneContainer.transform;

        }
    }
    private void Update()
    {
        if (waveMode)
        {
            enemyTimer += Time.deltaTime;
            if (enemyTimer >= timeBetweenEnemies && currentSpawningIterations < maxSpawningIterations)
            {
                Debug.Log("spawning enemeis");
                waveSpawner();
            }
            if (killedEnemies == spawnedEnemyCount && allEnemiesSpawned)
            {
                allEnemiesSpawned = false;
                killedEnemies = 0;
                waveCount++;
                maxSpawningIterations = Mathf.Floor(maxSpawningIterations * 1.2f);
                currentSpawningIterations = 0;
                spawnedEnemyCount = 0;
                enemyTimer = 0;
                currentEnemies = 0;

            }
            if (waveCount == 11)
            {
                Debug.Log("you won");
            }
            //waveCountText.text = "Wave: " + waveCount.ToString();
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
        if (waveCount == 1)
        {
            spawnEnemy(spikyBall);
            spawnedEnemyCount++;
            currentEnemies++;
            enemyTimer = 0;
            currentSpawningIterations++;
        }
        else if (waveCount == 2)
        {
            spawnEnemy(spikyBall);
            spawnEnemy(spikyBall);
            spawnedEnemyCount+=2;
            currentEnemies+=2;
            enemyTimer = 0;
            currentSpawningIterations++;
        }
        else if (waveCount == 3)
        {
            spawnEnemy(spikyBall);
            spawnedEnemyCount++;
            currentEnemies++;
            if (spawnedEnemyCount % 2 == 1)
            {
                spawnEnemy(goblin);
                spawnedEnemyCount++;
                currentEnemies++;
            }
            enemyTimer = 0;
            currentSpawningIterations++;
        }
        else if (waveCount == 4)
        {
            spawnEnemy(goblin); 
            spawnedEnemyCount++;
            currentEnemies++;
            if (spawnedEnemyCount % 3 == 1)
            {
                spawnEnemy(spider);
                spawnEnemy(spikyBall);
                spawnedEnemyCount += 2 ;
                currentEnemies+=2;
            }
            enemyTimer = 0;
            currentSpawningIterations++;
        }
        else if (waveCount == 5)
        {
            spawnEnemy(goblin);
            spawnedEnemyCount++;
            currentEnemies++;
            if (spawnedEnemyCount % 2 == 1)
            {
                spawnEnemy(spider);
                spawnedEnemyCount++;
                currentEnemies++;
            }
            enemyTimer = 0;
            currentSpawningIterations++;
        }
        else if (waveCount == 6)
        {
            spawnEnemy(spider);
            spawnedEnemyCount++;
            currentEnemies++;
            if (spawnedEnemyCount % 3 == 1)
            {
                spawnEnemy(zombie);
                spawnEnemy(goblin);
                spawnedEnemyCount+=2;
                currentEnemies+=2;
            }
            enemyTimer = 0;
            currentSpawningIterations++;
        }
        else if (waveCount == 7)
        {
            spawnEnemy(spider);
            spawnedEnemyCount++;
            currentEnemies++;
            if (spawnedEnemyCount % 2 == 1)
            {
                spawnEnemy(zombie);
                spawnedEnemyCount++;
                currentEnemies++;
            }
            enemyTimer = 0;
            currentSpawningIterations++;
        }
        else if (waveCount == 8)
        {

            spawnEnemy(zombie);
            spawnedEnemyCount++;
            currentEnemies++;
            if (spawnedEnemyCount % 3 == 1)
            {
                spawnEnemy(spider);
                spawnEnemy(drake);
                spawnedEnemyCount+=2;
                currentEnemies+=2;
            }
            enemyTimer = 0;
            currentSpawningIterations++;

        }
        else if (waveCount == 9)
        {
            spawnEnemy(drake);
            spawnedEnemyCount++;
            currentEnemies++;

            if (spawnedEnemyCount % 3 == 1)
            {
                spawnEnemy(spider);
                spawnEnemy(zombie);
                spawnedEnemyCount+=2;
                currentEnemies+=2;

            }
            enemyTimer = 0;
            currentSpawningIterations++;

        }
        else if (waveCount == 10)
        {
            if (spawnBoss)
            {
                spawnEnemy(bossPrefab);
                spawnBoss = false;
            }
            //boss battle

                spawnEnemy(spider);
                spawnedEnemyCount++;
                if (spawnedEnemyCount % 3 == 1)
                {
                    spawnEnemy(zombie);
                    spawnedEnemyCount++;
                }
                enemyTimer = 0;
                
        }
        if(currentSpawningIterations == maxSpawningIterations)
        {
            allEnemiesSpawned = true;
        } else
        {
            allEnemiesSpawned = false;
        }
    }  
}