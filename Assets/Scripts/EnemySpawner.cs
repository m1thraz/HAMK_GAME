
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    //Initialize prefabs here
    [SerializeField]
    private GameObject spikyBallPrefab;
    [SerializeField]
    private GameObject spikyBallBossPrefab;

    [SerializeField]
    private float spikyBallInterval;
    [SerializeField]
    private float spikyBallBossInterval;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(spawnEnemy(spikyBallInterval, spikyBallPrefab));
        StartCoroutine(spawnEnemy(spikyBallBossInterval, spikyBallBossPrefab));
    }

    private IEnumerator spawnEnemy(float interval, GameObject enemy) 
    {
        yield return new WaitForSeconds(interval);
        GameObject newEnemy = Instantiate(enemy, new Vector3(Random.Range(-5f, 5), Random.Range(-6f, 6f),0), Quaternion.identity);
        StartCoroutine(spawnEnemy(interval, enemy));
    }
}
