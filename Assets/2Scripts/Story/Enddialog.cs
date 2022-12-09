using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enddialog : MonoBehaviour
{
    [SerializeField] GameObject son, boss, activator;


    public void BossFight()
    {
        son.SetActive(false);
        boss.SetActive(true);
        activator.GetComponent<BoxCollider2D>().enabled = false;
    }
}
