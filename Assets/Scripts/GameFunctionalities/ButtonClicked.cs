using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

 
public class ButtonClicked : MonoBehaviour
{

    PowerUpMenu powerupMenu;

    Button btn;
    public int buttonNr;

    // Start is called before the first frame update
    void Start()
    {
        powerupMenu = GameObject.FindGameObjectWithTag("GameManager").GetComponent(typeof(PowerUpMenu)) as PowerUpMenu;
         btn = gameObject.GetComponent<Button>();
        btn.onClick.AddListener(clicked);

    }

    // Update is called once per frame
    void Update()
    {
        
    }


    void clicked()
    {
        powerupMenu.Buttonclicked(buttonNr);
    }
}

