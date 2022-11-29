
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
    private GameObject enemyPrefab1, enemyPrefab2, enemyPrefab3, enemyPrefab4, enemyPrefab5, bossPrefab;

    [SerializeField]
    private float enemyInterval1, enemyInterval2, enemyInterval3, enemyInterval4, enemyInterval5;


    private float horMin, horMax, verMin, verMax;

    // initialize stuff for waves
    private int waveCount = 1;
    private float enemyCount = 3;
    private float SpawnModifier = 1.4f;
    private float spawnRate = 1.0f;
    [SerializeField]
    private float timeBetweenWaves = 10.0f;
    [SerializeField]
    private bool activeWave = true;
    [SerializeField]
    private bool stopSpawn = false;

    [SerializeField]
    private TextMeshProUGUI waveCountText;
    

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
        waveCountText.text = "Wave: " + waveCount.ToString();
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

        Vector2 playerLocation = GameObject.Find("Player").transform.position;

        GameObject newEnemy = Instantiate(enemy, new Vector2((float)pos[0]+playerLocation.x, (float)pos[1]+playerLocation.y), Quaternion.identity);
        //GameObject newEnemy = Instantiate(enemy, new Vector3(Random.Range(-5f, 5), Random.Range(-6f, 6f), 0), Quaternion.identity);
        StartCoroutine(spawnEnemy(interval, enemy));

    }
    private IEnumerator waveSpawner()
    {
        while (activeWave == true)
        {
            
            if (waveCount == 1)
            {
                
                //spikyBall
                for (int i = 0; i < enemyCount; i++)
                {
                    StartCoroutine(spawnEnemy(enemyInterval2, enemyPrefab2));                   
                }
                Debug.Log("spawned" + enemyCount + "Enemies. In wave " + waveCount);
                enemyCount += 2;
                waveCount++;
                yield return new WaitForSeconds(10);

            }
            else if (waveCount == 2)
            {
                //more spikyballs
                for (int i = 0; i < enemyCount; i++)
                {
                    StartCoroutine(spawnEnemy(enemyInterval2, enemyPrefab2));
                    
                }
                Debug.Log("spawned" + enemyCount + "Enemies. In wave " + waveCount);
                enemyCount += 2;
                waveCount++;
                yield return new WaitForSeconds(10);
            }
            else if (waveCount == 3)
            {
                //spikyball + big spikyBalls
                for (int i = 0; i < enemyCount; i++)
                {
                    StartCoroutine(spawnEnemy(enemyInterval2, enemyPrefab2));
                    StartCoroutine(spawnEnemy(enemyInterval3, enemyPrefab3));
                }
                Debug.Log("spawned" + enemyCount + "Enemies. In wave " + waveCount);
                enemyCount += 2;
                waveCount++;
                yield return new WaitForSeconds(10);
            }
            else if (waveCount == 4)
            {
                //goblins
                for (int i = 0; i < enemyCount; i++)
                {
                    StartCoroutine(spawnEnemy(enemyInterval1, enemyPrefab1));
                    
                }
                Debug.Log("spawned" + enemyCount + "Enemies. In wave " + waveCount);
                enemyCount += 2;
                waveCount++;
                yield return new WaitForSeconds(10);
            }
            else if (waveCount == 5)
            {
                //goblins + spikyballs
                for (int i = 0; i < enemyCount; i++)
                {
                    StartCoroutine(spawnEnemy(enemyInterval2, enemyPrefab2));
                    Debug.Log("spawned" + enemyCount + "Enemies. In wave "+ waveCount);
                }
                enemyCount += 2;
                waveCount++;
                yield return new WaitForSeconds(10);
            }
            else if (waveCount == 6)
            {
                // goblins + spikyballs + big spikyballs
                for (int i = 0; i < enemyCount; i++)
                {
                    StartCoroutine(spawnEnemy(enemyInterval2, enemyPrefab2));
                    Debug.Log("spawned" + enemyCount + "Enemies. In wave " + waveCount);
                }
                waveCount++;
                yield return new WaitForSeconds(10);
                Debug.Log("spawning next wave");
            }
            else if (waveCount == 7)
            {
                //repeat untill clarified
                for (int i = 0; i < enemyCount; i++)
                {
                    StartCoroutine(spawnEnemy(enemyInterval2, enemyPrefab2));
                    Debug.Log("spawned" + enemyCount + "Enemies. In wave " + waveCount);
                }
                waveCount++;
                yield return new WaitForSeconds(10);
                Debug.Log("spawning next wave");
            }
            else if (waveCount == 8)
            {
                //repeat
                for (int i = 0; i < enemyCount; i++)
                {
                    StartCoroutine(spawnEnemy(enemyInterval2, enemyPrefab2));
                    Debug.Log("spawned" + enemyCount + "Enemies. In wave " + waveCount);
                }
                waveCount++;
                yield return new WaitForSeconds(10);
                Debug.Log("spawning next wave");
            }
            else if (waveCount == 9)
            {
                //repeat
                for (int i = 0; i < enemyCount; i++)
                {
                    StartCoroutine(spawnEnemy(enemyInterval2, enemyPrefab2));
                    Debug.Log("spawned" + enemyCount + "Enemies. In wave " + waveCount);
                }
                waveCount++;
                yield return new WaitForSeconds(10);
                Debug.Log("spawning next wave");
            }
            else if (waveCount == 10)
            {
                //boss battle
                for (int i = 0; i < enemyCount; i++)
                {
                    StartCoroutine(spawnEnemy(enemyInterval2, enemyPrefab2));
                    Debug.Log("spawned" + enemyCount + "Enemies. In wave " + waveCount);
                }
                waveCount++;
                yield return new WaitForSeconds(10);
                Debug.Log("spawning next wave");
            }
            else if(waveCount == 11)
            {
                Debug.Log("you won");
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

