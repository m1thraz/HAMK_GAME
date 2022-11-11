using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemy : MonoBehaviour
{
    [SerializeField]
    private float speed = 5;
    [SerializeField]
    private float damage = 1.5f;

    [SerializeField]
    private EnemyData data;

    private GameObject player;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("player");   
    }

    // Update is called once per frame
    void Update()
    {
        Swarm();
    }
    private void Swarm()
    {
        transform.position = Vector2.MoveTowards(transform.position, player.transform.position, speed * Time.deltaTime);
    }

    //WIP script for enemy health
    //private void OnTriggerEnter2D(Collider2D collider)
    //{
    //    if (collider.CompareTag("Player"))
    //    {
    //        if (collider.GetComponent<Health>() != null)
    //        {
    //            collider.GetComponent<Health>().Damage(10000);
    //        }
    //    }
    //}
}
