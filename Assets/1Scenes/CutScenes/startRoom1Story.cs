using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class startRoom1Story : MonoBehaviour
{
    [SerializeField]
    GameObject room1, endDialog;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void activateRoom()
    {
        room1.SetActive(true);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {

        activateEndDialog();

    }

        public void activateEndDialog()
    {
        endDialog.SetActive(true);
    }
}
