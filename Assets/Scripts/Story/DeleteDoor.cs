using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeleteDoor : MonoBehaviour
{
    [SerializeField] GameObject Door1;
    [SerializeField] GameObject Door2;
    [SerializeField] GameObject Door3;
    [SerializeField] GameObject Door4;
    [SerializeField] GameObject Door5;
 
    private float score;

    // Start is called before the first frame update
    void Start()
    {
        //playerLogic = GameObject.FindGameObjectWithTag("Player").GetComponent(typeof(PlayerLogic)) as PlayerLogic;
    }


    // Update is called once per frame
    void Update()
    {

        score = GetComponent<PlayerLogic>().getScore();
        RemoveDoor();
    }

    void RemoveDoor()
    {
       
        Debug.Log("score:" + score);
        if (score == 100)
        {
            Destroy(Door1);
            Debug.Log("A door opened");
        }
        if (score == 200)
        {
            Destroy(Door2);
            Debug.Log("Door2 opened");
        }
        if (score == 300)
        {
            Destroy(Door3);
            Debug.Log("Door3 opened");
        }
        if (score == 400)
        {
            Destroy(Door4);
            Debug.Log("Door4 opened");
        }
        if (score == 500)
        {
            Destroy(Door5);
            Debug.Log("Door5 opened");
        }
    }
}
