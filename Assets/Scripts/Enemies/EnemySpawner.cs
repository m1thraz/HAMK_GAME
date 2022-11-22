
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using TMPro;

public class EnemySpawner : MonoBehaviour
{
    //Initialize prefabs here
    [SerializeField]
    private GameObject enemyPrefab1, enemyPrefab2, enemyPrefab3, enemyPrefab4, enemyPrefab5, bossPrefab;

    [SerializeField]
    private float enemyInterval1, enemyInterval2, enemyInterval3, enemyInterval4, enemyInterval5;



    private float horMin,horMax, verMin, verMax;

    // initialize stuff for waves
    private int waveCount = 1;
    private int enemyCount = 3;
    private float waveTextTimer = 1.0f;
    private float spawnRate = 1.0f;
    [SerializeField]
    private float timeBetweenWaves = 10.0f;
    [SerializeField]
    private bool activeWave = true;
    [SerializeField]
    private bool stopSpawn = false;

    [SerializeField]
    private TextMeshProUGUI waveCountText;

    private GameObject cloneContainer;

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
        StartCoroutine(waveSpawner());
        //StartCoroutine(spawnEnemy(enemyInterval1, enemyPrefab1));
        //StartCoroutine(spawnEnemy(enemyInterval2, enemyPrefab2));
        //StartCoroutine(spawnEnemy(enemyPrefab11, goblinPrefab));
        //goblinPrefab.transform.parent = cloneContainer.transform;


    }
    private ArrayList getSpawnLocation()
    {
        float x, y;
        int direction = Random.Range(1, 5);
        switch (direction)
        {
            case 1:
                x = horMin - 0.1f;
                y = Random.Range(verMin, verMax);
                break;
            case 2:
                x = Random.Range(horMin, horMax);
                y = verMax + 0.1f;
                break;
            case 3:
                x = horMax + 0.1f;
                y = Random.Range(verMin, verMax);
                break;
            case 4:
                x = Random.Range(horMin, horMax);
                y = verMin - 0.1f;
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
    private IEnumerator spawnEnemy(float interval, GameObject enemy) 
    {
        yield return new WaitForSeconds(interval);
        ArrayList pos = getSpawnLocation();
        GameObject newEnemy = Instantiate(enemy, new Vector2((float)pos[0],(float)pos[1]), Quaternion.identity);
        
    }
    private IEnumerator waveSpawner()
    {
        while (activeWave == true)
        {
            if (waveCount == 1)
            {
                for (int i = 0; i < enemyCount; i++)
                {
                    StartCoroutine(spawnEnemy(enemyInterval1, enemyPrefab1));
                }
                Debug.Log("wave spawning 1 completed");
                waveCount++;
                yield return new WaitForSeconds(10);
                Debug.Log("waveCount is" + waveCount);
                Debug.Log("spawning next wave");
            }
            else if (waveCount == 2)
            {
                for (int i = 0; i < enemyCount; i++)
                {
                    StartCoroutine(spawnEnemy(enemyInterval2, enemyPrefab2));
                }
                Debug.Log("wave spawning 2 completed");
                waveCount++;
                yield return new WaitForSeconds(10);
                Debug.Log("spawning next wave");
            }
            else if (waveCount == 3)
            {
                for (int i = 0; i < enemyCount; i++)
                {
                    StartCoroutine(spawnEnemy(enemyInterval2, enemyPrefab2));
                }
                Debug.Log("wave spawning 2 completed");
                waveCount++;
                yield return new WaitForSeconds(10);
                Debug.Log("spawning next wave");
            }
            else if (waveCount == 4)
            {
                for (int i = 0; i < enemyCount; i++)
                {
                    StartCoroutine(spawnEnemy(enemyInterval2, enemyPrefab2));
                }
                Debug.Log("wave spawning 2 completed");
                waveCount++;
                yield return new WaitForSeconds(10);
                Debug.Log("spawning next wave");
            }
            else if (waveCount == 5)
            {
                for (int i = 0; i < enemyCount; i++)
                {
                    StartCoroutine(spawnEnemy(enemyInterval2, enemyPrefab2));
                }
                Debug.Log("wave spawning 2 completed");
                waveCount++;
                yield return new WaitForSeconds(10);
                Debug.Log("spawning next wave");
            }
            else if (waveCount == 6)
            {
                for (int i = 0; i < enemyCount; i++)
                {
                    StartCoroutine(spawnEnemy(enemyInterval2, enemyPrefab2));
                }
                Debug.Log("wave spawning 2 completed");
                waveCount++;
                yield return new WaitForSeconds(10);
                Debug.Log("spawning next wave");
            }
            else if (waveCount == 7)
            {
                for (int i = 0; i < enemyCount; i++)
                {
                    StartCoroutine(spawnEnemy(enemyInterval2, enemyPrefab2));
                }
                Debug.Log("wave spawning 2 completed");
                waveCount++;
                yield return new WaitForSeconds(10);
                Debug.Log("spawning next wave");
            }
            else if (waveCount == 8)
            {
                for (int i = 0; i < enemyCount; i++)
                {
                    StartCoroutine(spawnEnemy(enemyInterval2, enemyPrefab2));
                }
                Debug.Log("wave spawning 2 completed");
                waveCount++;
                yield return new WaitForSeconds(10);
                Debug.Log("spawning next wave");
            }
            else if (waveCount == 9)
            {
                for (int i = 0; i < enemyCount; i++)
                {
                    StartCoroutine(spawnEnemy(enemyInterval2, enemyPrefab2));
                }
                Debug.Log("wave spawning 2 completed");
                waveCount++;
                yield return new WaitForSeconds(10);
                Debug.Log("spawning next wave");
            }
            else if (waveCount == 10)
            {
                for (int i = 0; i < enemyCount; i++)
                {
                    StartCoroutine(spawnEnemy(enemyInterval2, enemyPrefab2));
                }
                Debug.Log("wave spawning 2 completed");
                waveCount++;
                yield return new WaitForSeconds(10);
                Debug.Log("spawning next wave");
            }
        }


        //als activewave ==true en stopspawn == false 
        //spawnWave
        //als wave == 1
        //spawn spikyball x 10
        //dmg++
    }
    private void activateWaveText()
    {
       
        
    }

}
