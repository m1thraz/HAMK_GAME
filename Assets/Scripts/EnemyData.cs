using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Data", menuName = "ScriptableObjects/Enemy", order = 1)]

public class EnemyData : ScriptableObject
{
    public int hp;
    public float damage;
    public float speed;
}
