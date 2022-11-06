using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallSpawner : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField]
    private GameObject SpikyBall;
    float horMin, horMax, verMin, verMax;
    private int direction = 1; //1:left, 2:top, 3:right, 4:bottom;

    private float interval = 3f;
    void Start()
    {
        Camera camera = Camera.main;
        float halfHeight = camera.orthographicSize;
        float halfWidth = camera.aspect * halfHeight;

        horMin = -halfWidth;
        horMax = halfWidth;
        verMin = -halfHeight;
        verMax = halfHeight;
        StartCoroutine(spawnTimer());
        
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void FixedUpdate()
    {
        if(Input.GetKeyDown(KeyCode.Return))
        {
            spawnBall();
        }
    }
    private IEnumerator spawnTimer()
    {
        yield return new WaitForSeconds(interval);
        spawnBall();
        StartCoroutine(spawnTimer());
    }
    private void  spawnBall()
    {
        float x, y;
        direction = Random.Range(1, 5);
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
        //yield return new WaitForSeconds(interval);
       Instantiate(SpikyBall, new Vector2(x,y), Quaternion.identity);

    }
}
