using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeleteDoor : MonoBehaviour
{
    [SerializeField] GameObject Door1, Door2, Door3, Door4, Door5;


    [SerializeField] GameObject Room1, Room2, Room3, Room4, Room5, Room6;




    // Start is called before the first frame update



    // Update is called once per frame
    void FixedUpdate()
    {

     
        RemoveDoor();

    }

    void RemoveDoor()
    {
       
        
        if (Room1.transform.childCount == 0)
        {
            Destroy(Door1);
            Debug.Log("Door1 opened");
        }
        if (Room2.transform.childCount == 0)
        {
            Destroy(Door2);
            Debug.Log("Door2 opened");
        }
        if (Room3.transform.childCount == 0)
        {
            Destroy(Door3);
            Debug.Log("Door3 opened");
        }
        if (Room4.transform.childCount == 0)
        {
            Destroy(Door4);
            Debug.Log("Door4 opened");
        }
        if (Room5.transform.childCount == 0)
        {
            Destroy(Door5);
            Debug.Log("Door5 opened");
        }
    }
}
