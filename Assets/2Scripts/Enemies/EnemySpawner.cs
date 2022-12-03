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
    private GameObject enemyPrefab1, enemyPrefab2, enemyPrefab3, enemyPrefab4, enemyPrefab5, enemyPrefab6, bossPrefab;



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
        //StartCoroutine(spawnEnemy(enemyInterval1, enemyPrefab1));
        //StartCoroutine(spawnEnemy(enemyInterval2, enemyPrefab2));
        //StartCoroutine(spawnEnemy(enemyPrefab11, goblinPrefab));
        //goblinPrefab.transform.parent = cloneContainer.transform;


    }
    private void Update()
    {
        waveCountText.text = "Wave: " + waveCount.ToString();
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
    private IEnumerator waveSpawner()
    {
        while (activeWave == true)
        {

            if (waveCount == 1)
            {

                //spikyBall
                for (int i = 0; i < enemyCount; i++)
                {
                    spawnEnemy(enemyPrefab1);
                }
                Debug.Log("spawned" + enemyCount + "Enemies. In wave " + waveCount);
                enemyCount += 2;
                waveCount++;
                yield return new WaitForSeconds(timeBetweenWaves);

            }
            else if (waveCount == 2)
            {
                //more spikyballs
                for (int i = 0; i < enemyCount; i++)
                {
                    spawnEnemy(enemyPrefab1);

                }
                Debug.Log("spawned" + enemyCount + "Enemies. In wave " + waveCount);
                enemyCount += 2;
                waveCount++;
                yield return new WaitForSeconds(timeBetweenWaves);
            }
            else if (waveCount == 3)
            {
                //spikyball + big spikyBalls
                for (int i = 0; i < enemyCount; i++)
                {
                    spawnEnemy(enemyPrefab1);
                    spawnEnemy(enemyPrefab2);
                }
                Debug.Log("spawned" + enemyCount + "Enemies. In wave " + waveCount);
                enemyCount += 2;
                waveCount++;
                yield return new WaitForSeconds(timeBetweenWaves);
            }
            else if (waveCount == 4)
            {
                //goblins
                for (int i = 0; i < enemyCount; i++)
                {
                    spawnEnemy(enemyPrefab3);

                }
                Debug.Log("spawned" + enemyCount + "Enemies. In wave " + waveCount);
                enemyCount /= enemyCount;
                waveCount++;
                yield return new WaitForSeconds(timeBetweenWaves);
            }
            else if (waveCount == 5)
            {
                //goblins + zombies
                for (int i = 0; i < enemyCount; i++)
                {
                    spawnEnemy(enemyPrefab3);
                    spawnEnemy(enemyPrefab4);
                    Debug.Log("spawned" + enemyCount + "Enemies. In wave " + waveCount);
                }
                enemyCount += 1;
                waveCount++;
                yield return new WaitForSeconds(timeBetweenWaves);
            }
            else if (waveCount == 6)
            {
                // goblins + zombies + spikyballs
                for (int i = 0; i < enemyCount; i++)
                {
                    spawnEnemy(enemyPrefab1);
                    spawnEnemy(enemyPrefab3);
                    spawnEnemy(enemyPrefab4);
                    Debug.Log("spawned" + enemyCount + "Enemies. In wave " + waveCount);
                }
                enemyCount += 6;
                waveCount++;
                yield return new WaitForSeconds(timeBetweenWaves);
                Debug.Log("spawning next wave");
            }
            else if (waveCount == 7)
            {
                //spiders
                for (int i = 0; i < enemyCount; i++)
                {
                    spawnEnemy(enemyPrefab5);
                    Debug.Log("spawned" + enemyCount + "Enemies. In wave " + waveCount);
                }
                enemyCount += 2;
                waveCount++;
                yield return new WaitForSeconds(timeBetweenWaves);
                Debug.Log("spawning next wave");
            }
            else if (waveCount == 8)
            {
                //spiders + zombies
                for (int i = 0; i < enemyCount; i++)
                {
                    spawnEnemy(enemyPrefab4);
                    spawnEnemy(enemyPrefab5);
                    Debug.Log("spawned" + enemyCount + "Enemies. In wave " + waveCount);
                }
                enemyCount /= 2;
                waveCount++;
                yield return new WaitForSeconds(timeBetweenWaves);
                Debug.Log("spawning next wave");
            }
            else if (waveCount == 9)
            {
                //drakes
                for (int i = 0; i < enemyCount; i++)
                {
                    spawnEnemy(enemyPrefab6);
                    Debug.Log("spawned" + enemyCount + "Enemies. In wave " + waveCount);
                }
                waveCount++;
                yield return new WaitForSeconds(timeBetweenWaves);
                Debug.Log("spawning next wave");
            }
            else if (waveCount == 10)
            {
                //boss battle
                for (int i = 0; i < enemyCount; i++)
                {
                    spawnEnemy(bossPrefab);
                    Debug.Log("spawned" + enemyCount + "Enemies. In wave " + waveCount);
                }
                waveCount++;
                yield return new WaitForSeconds(timeBetweenWaves);
                Debug.Log("spawning next wave");
            }
            else if (waveCount == 11)
            {
                Debug.Log("you won");
            }
        }


    }
}