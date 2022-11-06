using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using TMPro;


public class GameManager : MonoBehaviour
{
    public TextMeshProUGUI score;
    private GameObject BulletLogic;
    private int enemyKillCount;
    // Start is called before the first frame update
    void Start()
    {
       
    }

    // Update is called once per frame
    void Update()
    {

        score.text = enemyKillCount.ToString();
    }


    public void newEnemyKilled()
    {
        enemyKillCount++;
    }
}
