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



    private float horMin, horMax, verMin, verMax;

    // initialize stuff for waves
    private int waveCount = 1;
    private float enemyCount = 5;
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

    private float spawnedEnemyCount;
    public float currentEnemies;
    private bool spawnBoss =true;

    // Start is called before the first frame update
    void Start()
    {
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
    private void Update()
    {
        enemyTimer += Time.deltaTime;
        if (enemyTimer >= timeBetweenEnemies && spawnedEnemyCount <= enemyCount)
        {
            waveSpawner();
        }
            if (currentEnemies <= 0)
        {
            waveCount++;
            enemyCount = enemyCount * 1.5f;
            spawnedEnemyCount = 0;
            enemyTimer = 0;
            currentEnemies = enemyCount;

        }
        waveCountText.text = "Wave: " + waveCount.ToString();
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
    }
    private void waveSpawner()
    {
        while (activeWave == true)
        {

            if (waveCount == 1)
            {
                spawnEnemy(spikyBall);
                spawnedEnemyCount++;
                enemyTimer = 0;
            }
            else if (waveCount == 2)
            {
                spawnEnemy(spikyBall);
                spawnedEnemyCount++;
                enemyTimer = 0;
            }
            else if (waveCount == 3)
            {

                    spawnEnemy(spikyBall);
                    spawnedEnemyCount++;

                if (spawnedEnemyCount % 2 == 1)
                    {
                        spawnEnemy(goblin);
                        spawnedEnemyCount++;

                }
                enemyTimer = 0;
                
            }
            else if (waveCount == 4)
            {

                    spawnEnemy(goblin); 
                spawnedEnemyCount++;

                if (spawnedEnemyCount % 3 == 1)
                    {
                        spawnEnemy(spider);
                        spawnEnemy(spikyBall);
                    spawnedEnemyCount += 2 ;

                }
                enemyTimer = 0;
                
            }
            else if (waveCount == 5)
            {
                    spawnEnemy(goblin);
                spawnedEnemyCount++;

                if (spawnedEnemyCount % 2 == 1)
                    {
                        spawnEnemy(spider);
                    spawnedEnemyCount++;

                }
                enemyTimer = 0;
            }
            else if (waveCount == 6)
            {
                    spawnEnemy(spider);
                spawnedEnemyCount++;

                if (spawnedEnemyCount % 3 == 1)
                    {
                        spawnEnemy(zombie);
                        spawnEnemy(goblin);
                    spawnedEnemyCount+=2;

                }
                enemyTimer = 0;
            }
            else if (waveCount == 7)
            {

                    spawnEnemy(spider);
                    spawnedEnemyCount++;

                    if (spawnedEnemyCount % 2 == 1)
                    {
                        spawnEnemy(zombie);
                        spawnedEnemyCount++;

                    }
                    enemyTimer = 0;
                
            }
            else if (waveCount == 8)
            {

                    spawnEnemy(zombie);
                    spawnedEnemyCount++;

                    if (spawnedEnemyCount % 3 == 1)
                    {
                        spawnEnemy(spider);
                        spawnEnemy(drake);
                        spawnedEnemyCount+=2;

                    }
                    enemyTimer = 0;
                
            }
            else if (waveCount == 9)
            {
                //drakes
                    spawnEnemy(drake);
                    spawnedEnemyCount++;

                    if (spawnedEnemyCount % 3 == 1)
                    {
                        spawnEnemy(spider);

                        spawnEnemy(zombie);
                        spawnedEnemyCount+=2;

                    }
                    enemyTimer = 0;
                
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
                    if (spawnedEnemyCount % 3 == 1)
                    {
                        spawnEnemy(zombie);
                    }
                    enemyTimer = 0;
                
            }
            else if (waveCount == 11)
            {
                activeWave = false;
            }
        }


    }
}