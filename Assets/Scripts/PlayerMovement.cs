using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerMovement : MonoBehaviour
{
    public float speed;
    private Vector2 direction;
    private Vector2 directionHistory;
    private Animator animator;

    public float shootSpeed, shootTimer;
    public Transform shootPos;
    public Transform shootPos2;
    public GameObject bullet;
    private bool isShooting;


    private void Start()
    {
        isShooting = false;
        animator = GetComponent<Animator>();
    }
    // Update is called once per frame
    void Update()
    {
        TakeInput();
        Move();
       
    }

    private void Move()
    {
        transform.Translate(direction * speed * Time.deltaTime);

        if(direction.x != 0 || direction.y != 0) { 
      
        SetAnimatorMovement(direction);

        } else
        {
            animator.SetLayerWeight( 1, 0);
        }
    }
    private void TakeInput()
    {
        direction = Vector2.zero;
        if (Input.GetKey(KeyCode.W))
        {
            direction += Vector2.up;
            
        }
        if (Input.GetKey(KeyCode.A))
        {
            direction += Vector2.left;
           
        }
        if (Input.GetKey(KeyCode.S))
        {
            direction += Vector2.down;
        }
        if (Input.GetKey(KeyCode.D))
        {
            direction += Vector2.right;
            
        }
        if (Input.GetKey(KeyCode.Space) && !isShooting)
        {
            StartCoroutine(playerShoot());
        }
        if (Input.GetKey(KeyCode.R))
        {
            SceneManager.LoadScene(1);
        }

        if (Input.GetKey(KeyCode.Escape))
        {
            Application.Quit();
        }

        if (direction != Vector2.zero)
        {
            directionHistory = direction;
            //Debug.Log(directionHistory);
        }

    }
    private void SetAnimatorMovement(Vector2 direction)
    {
        animator.SetLayerWeight(1, 1);
        animator.SetFloat("xDir", directionHistory.x);
        animator.SetFloat("yDir", directionHistory.y);
        
    }

    IEnumerator playerShoot()
    {
        isShooting = true;
        GameObject newBullet;

        //directions
        Vector2 rightUp = new Vector2(1.0f, 1.0f);
        Vector2 rightDown = new Vector2(1.0f, -1.0f);
        Vector2 leftUp = new Vector2(-1.0f, 1.0f);
        Vector2 leftDown = new Vector2(-1.0f, -1.0f);



        if (directionHistory == Vector2.up || directionHistory == Vector2.right)
        {
             newBullet = Instantiate(bullet, shootPos.position, Quaternion.identity);
        }
        else
        {
             newBullet = Instantiate(bullet, shootPos2.position, Quaternion.identity);
        }

        if (directionHistory == rightUp)
        {
            newBullet.GetComponent<Rigidbody2D>().velocity = new Vector2(0f, shootSpeed * Time.fixedDeltaTime);
        }
        else if (directionHistory == rightDown)
        {
            newBullet.GetComponent<Rigidbody2D>().velocity = new Vector2(0f, -shootSpeed * Time.fixedDeltaTime);
        }
        else if (directionHistory == leftUp)
        {
            newBullet.GetComponent<Rigidbody2D>().velocity = new Vector2(0f, shootSpeed * Time.fixedDeltaTime);
        }
        else if (directionHistory == leftDown)
        {
            newBullet.GetComponent<Rigidbody2D>().velocity = new Vector2(0f, -shootSpeed * Time.fixedDeltaTime);
        }

        else if (directionHistory == Vector2.up)
        {
            newBullet.GetComponent<Rigidbody2D>().velocity = new Vector2(0f, shootSpeed * Time.fixedDeltaTime);
        }

        else if (directionHistory == Vector2.down)
        {
            newBullet.GetComponent<Rigidbody2D>().velocity = new Vector2(0f, -shootSpeed * Time.fixedDeltaTime);
        }

        else if (directionHistory == Vector2.right)
        {
            newBullet.GetComponent<Rigidbody2D>().velocity = new Vector2(shootSpeed * Time.fixedDeltaTime, 0f);
        }

        else if (directionHistory == Vector2.left)
        {
            newBullet.GetComponent<Rigidbody2D>().velocity = new Vector2(-shootSpeed * Time.fixedDeltaTime, 0f);
        }


        yield return new WaitForSeconds(shootTimer);
        isShooting = false;
    }

}
