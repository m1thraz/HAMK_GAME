using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class activateBoss : MonoBehaviour
{
    [SerializeField] private GameObject Boss;


    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.name == "Player")
            Boss.SetActive(true);
        GetComponent<CircleCollider2D>().enabled = false;
    }

}
