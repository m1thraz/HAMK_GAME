using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class activateBoss : MonoBehaviour
{
    [SerializeField] private GameObject Room1;
    [SerializeField] private GameObject Room2;
    [SerializeField] private GameObject Room3;
    [SerializeField] private GameObject Room4;
    [SerializeField] private GameObject Room5;
    private int col = 0;






    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.name == "Player")
        {
            col += 1;
            GetComponent<CircleCollider2D>().enabled = false;

        }
    }
    private void activateRoom(int room)
    {
      
    }
}