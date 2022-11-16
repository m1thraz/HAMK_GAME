
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    //Initialize prefabs here
    [SerializeField]
    private GameObject spikyBallPrefab;
    [SerializeField]
    private GameObject spikyBallBossPrefab;
    [SerializeField]
    private GameObject goblinPrefab;

    [SerializeField]
    private float spikyBallInterval;
    [SerializeField]
    private float spikyBallBossInterval;
    [SerializeField]
    private float goblinInterval;

    private float horMin,horMax, verMin, verMax;

    // initialize stuff for waves
    private int waveCount;
    private int enemyCount;
    private float waveTextTimer = 1.0f;
    private float spawnRate = 1.0f;
    [SerializeField]
    private float timeBetweenWaves = 10.0f;
    [SerializeField]
    private bool activeWave = true;
    [SerializeField]
    private bool stopSpawn = false;

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
        StartCoroutine(spawnEnemy(spikyBallInterval, spikyBallPrefab));
        StartCoroutine(spawnEnemy(spikyBallBossInterval, spikyBallBossPrefab));
        StartCoroutine(spawnEnemy(goblinInterval, goblinPrefab));
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
        //GameObject newEnemy = Instantiate(enemy, new Vector3(Random.Range(-5f, 5), Random.Range(-6f, 6f), 0), Quaternion.identity);
        StartCoroutine(spawnEnemy(interval, enemy));
        
    }
    private IEnumerator waveSpawner()
    {
        while (activeWave = true && stopSpawn == false)
        {
            spawnEnemy(goblinInterval, goblinPrefab);
        }
        yield return new WaitForSeconds(10);
    }
}
