using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EnemyMovement : MonoBehaviour
{
    public float speed;
    private Vector2 direction;
    private Animator animator;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        WalkPath();
    }
    private void WalkPath() {
        float xPos = 0;
        float yPos = 0;
        if (xPos == 0 && yPos ==0) {
            direction += Vector2.left;
        }
    }

}
