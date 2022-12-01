using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class activateRoom : MonoBehaviour
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
            activateR(col);

        }
    }
    private void activateR(int room)
    {
        switch (room)
        {
            case 1:
                Room1.SetActive(true);
                break;
            case 2:
                Room2.SetActive(true);
                break;
            case 3:
                Room3.SetActive(true);
                break;
            case 4:
                Room4.SetActive(true);
                break;
            case 5:
                Room5.SetActive(true);
                break;

        }
    }
}
