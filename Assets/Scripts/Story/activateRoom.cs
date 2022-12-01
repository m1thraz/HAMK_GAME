using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class activateRoom : MonoBehaviour
{
    [SerializeField] private GameObject Room1;

    private int col = 0;





    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.name == "Player")
        {
            col += 1;
            //  GetComponent<CircleCollider2D>().enabled = false;
            Destroy(this);
            Room1.SetActive(true);

        }
    }

}
