using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerMovement : MonoBehaviour
{
    public float speed;
    private Vector2 direction;
    private Vector2 directionHistory = Vector2.right;
    private Animator animator;

    public float shootSpeed, shootTimer;
    public Transform shootPosCentral;

    Vector2 dirUp = Vector2.up;
    Vector2 dirDown = Vector2.down;
    Vector2 dirRight = Vector2.right;
    Vector2 dirLeft = Vector2.left;

    Vector2 dirUpRight = new Vector2(1,1);
    Vector2 dirUpLeft = new Vector2(1, -1);
    Vector2 dirDownRight = new Vector2(-1, 1);
    Vector2 dirDownLeft = new Vector2(-1, -1);

    public int newShootControls;

    public GameObject bullet;


    [SerializeField]
    private bool inCutScene = false;

    private bool isShooting, isShootingBig;


    public GameObject gameManager;
    bool isPowerMenuOpen;
    PowerUpMenu powerMenu;
    public GameObject pauseMenu;
    bool gamePaused = false;
    public GameObject settingsMenu;

    public GameObject bigBullet;
    public GameObject bigBulletFreeze;

    public bool isBiggerSpell = false;
    public int biggerSpellCount = 0;

    public bool isBiggerSpellFreeze = false;
    public int biggerSpellFreezeCount = 0;


    public float dashSpeed;
    private float dashCount;
    public float startDashCount;
    public float dashTimer;
    public bool isPlayerDashing;
    public Rigidbody2D rb;




    private float slowTimer = 0;
    private bool slow;
    private float slowDuration = 2;
    private float normalSpeed;




    private void Start()
    {
        isShooting = false;
        animator = GetComponent<Animator>();
        powerMenu = gameManager.GetComponent(typeof(PowerUpMenu)) as PowerUpMenu;
        isPowerMenuOpen = false;
        rb = GetComponent<Rigidbody2D>() as Rigidbody2D;
        dashCount = startDashCount;
        if (!PlayerPrefs.HasKey("playershoot"))
        {
            PlayerPrefs.SetInt("playershoot", 0); 
        } 
       

    }
    // Update is called once per frame
    void Update()
    {
        TakeInput();
       
        if(slow)
        {
            slowTimer += Time.deltaTime;
            if(slowTimer >= slowDuration)
            {
                slowTimer = 0;
                slow = false;
                speed = normalSpeed;
            }
        }

    }
    void FixedUpdate()
    {
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
    public void slowMovement()
    {
        if (slow)
        {
            slowTimer = 0;
        } else
        {
            normalSpeed = speed;
            float slowedSpeed = 0.5f * speed;

            slow = true;
            speed = slowedSpeed;
        }

    }
    private void TakeInput()
    {
        direction = Vector2.zero;
        if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow))
        {
            direction += Vector2.up;
            
        }
        if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow))
        {
            direction += Vector2.left;
           
        }
        if (Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.DownArrow))
        {
            direction += Vector2.down;
        }
        if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow))
        {
            direction += Vector2.right;
            
        }

        
        if (!inCutScene) {

            // newControls ON
            if (PlayerPrefs.GetInt("shootcontrol") == 1)
            {
                // Shooting
                if (!isShooting && Input.GetKey(KeyCode.Mouse0) || Input.GetKeyDown(KeyCode.Return) )
                {
                    if (!gamePaused)
                    {

                        StartCoroutine(playerShootDirection());
                    }
                }


                if (!isShootingBig && (Input.GetKeyDown(KeyCode.Mouse1) || Input.GetKeyDown(KeyCode.LeftShift) || Input.GetKeyDown(KeyCode.RightShift)) && biggerSpellFreezeCount > 0)
                {
                    if (!gamePaused)
                    {

                        StartCoroutine(playerShootDirection(1));
                        biggerSpellFreezeCount--;
                    }


                }

                else if (!isShootingBig && (Input.GetKeyDown(KeyCode.Mouse1) || Input.GetKeyDown(KeyCode.LeftShift) || Input.GetKeyDown(KeyCode.RightShift)) && biggerSpellCount > 0)
                {
                    if (!gamePaused)
                    {

                        StartCoroutine(playerShootDirection(0));
                        biggerSpellCount--;
                    }


                }





            }
            else // OLD Controls ON
            {
            
                if (!isShooting && Input.GetKey(KeyCode.Mouse0))
                {
                    if (!gamePaused)
                    {

                        StartCoroutine(MousePointerShot());
                    }
                }


                if (!isShootingBig && Input.GetKeyDown(KeyCode.Mouse1) && biggerSpellFreezeCount > 0)
                {
                    if (!gamePaused)
                    {

                        StartCoroutine(MouseDirectionShotSpecial(1));
                        biggerSpellFreezeCount--;
                    }


                }

                else if (!isShootingBig && Input.GetKeyDown(KeyCode.Mouse1) && biggerSpellCount > 0)
                {
                    if (!gamePaused)
                    {

                        StartCoroutine(MouseDirectionShotSpecial(0));
                        biggerSpellCount--;
                    }


                }
            }


            


    }



        if (Input.GetKeyDown(KeyCode.Escape))
        {

            Pause();
        }

        if (direction != Vector2.zero)
        {
            directionHistory = direction;
            //Debug.Log(directionHistory);
        }
        

      
        // For Debugging shooting
        if (Input.GetKeyDown(KeyCode.O))
        {
            StartCoroutine(playerShootDirection());

        }


        if (Input.GetKeyDown(KeyCode.Space) && !isPlayerDashing)
        {
            isPlayerDashing = true;
            StartCoroutine(playerDash());
            
        }



    }

    IEnumerator playerDash()
    {

        // Dashing

        gameObject.layer = LayerMask.NameToLayer("Dash");

        if (directionHistory == dirUpLeft)
        {
            rb.velocity = new Vector2(1.0f * dashSpeed, -1.0f * dashSpeed);
            yield return new WaitForSeconds(0.25f);
            rb.velocity = Vector2.zero;
            gameObject.layer = LayerMask.NameToLayer("Player");
        }
        if (directionHistory == dirUpRight)
        {
            rb.velocity = new Vector2(1.0f * dashSpeed, 1.0f * dashSpeed);
            yield return new WaitForSeconds(0.25f);
            rb.velocity = Vector2.zero;
            gameObject.layer = LayerMask.NameToLayer("Player");
        }
        if (directionHistory == dirDownLeft)
        {
            rb.velocity = new Vector2(-1.0f * dashSpeed, -1.0f * dashSpeed);
            yield return new WaitForSeconds(0.25f);
            rb.velocity = Vector2.zero;
            gameObject.layer = LayerMask.NameToLayer("Player");
        }
        if (directionHistory == dirDownRight)
        {
            rb.velocity = new Vector2(-1.0f * dashSpeed, 1.0f * dashSpeed);
            yield return new WaitForSeconds(0.25f);
            rb.velocity = Vector2.zero;
            gameObject.layer = LayerMask.NameToLayer("Player");
        }

        if (directionHistory == Vector2.up)
            {
                rb.velocity = new Vector2(0f, 1.0f * dashSpeed);
                yield return new WaitForSeconds(0.25f);
                rb.velocity = Vector2.zero;
                gameObject.layer = LayerMask.NameToLayer("Player");
        }

        if (directionHistory == Vector2.right)
            {
                rb.velocity = new Vector2(1.0f * dashSpeed, 0f);
                yield return new WaitForSeconds(0.25f);
                rb.velocity = Vector2.zero;
                gameObject.layer = LayerMask.NameToLayer("Player");
        }

        if (directionHistory == Vector2.left)
        {
            rb.velocity = new Vector2(-1.0f * dashSpeed, 0f);
            yield return new WaitForSeconds(0.25f);
            rb.velocity = Vector2.zero;
            gameObject.layer = LayerMask.NameToLayer("Player");

        }
        if (directionHistory == Vector2.down)
        {
        rb.velocity = new Vector2(0f, -1.0f * dashSpeed);
        yield return new WaitForSeconds(0.25f);
        rb.velocity = Vector2.zero;
        gameObject.layer = LayerMask.NameToLayer("Player");
        }

        yield return new WaitForSeconds(dashTimer);
        isPlayerDashing = false;
    }


    private void SetAnimatorMovement(Vector2 direction)
    {
        animator.SetLayerWeight(1, 1);
        animator.SetFloat("xDir", directionHistory.x);
        animator.SetFloat("yDir", directionHistory.y);
        
    }

    // ALternative control
    // Big Bullet 0 / freeze Bullet 1 // alternative shooting in player moving dir for Special spells
    IEnumerator playerShootDirection(int mode)
    {
        isShootingBig = true;
        GameObject newBullet;

        /*Vector2 dirUp = Vector2.up;
        Vector2 dirDown = Vector2.down;
        Vector2 dirRight = Vector2.right;
        Vector2 dirLeft = Vector2.left;

        Vector2 dirUpRight = new Vector2(1,1);
        Vector2 dirUpLeft = new Vector2(1, -1);
        Vector2 dirDownRight = new Vector2(-1, 1);
        Vector2 dirDownLeft = new Vector2(-1, -1);*/


        if (mode == 0)
        {
            if (directionHistory == Vector2.up)
            {
                newBullet = Instantiate(bigBullet, shootPosCentral.position, Quaternion.identity);
            }
            else if (directionHistory == Vector2.right)
            {
                newBullet = Instantiate(bigBullet, shootPosCentral.position, Quaternion.identity);
            }
            else if (directionHistory == Vector2.left)
            {
                newBullet = Instantiate(bigBullet, shootPosCentral.position, Quaternion.identity);
            }
            else if ((directionHistory == Vector2.left))
            {
                newBullet = Instantiate(bigBullet, shootPosCentral.position, Quaternion.identity);
            }
            // diagonal
            else if (directionHistory == dirUpRight)
            { // North East

                newBullet = Instantiate(bigBullet, shootPosCentral.position, Quaternion.identity);
            }
            else if (directionHistory == dirUpLeft) // North West
            {
                newBullet = Instantiate(bigBullet, shootPosCentral.position, Quaternion.identity);
            }
            else if (directionHistory == dirDownRight) // South East
            {
                newBullet = Instantiate(bigBullet, shootPosCentral.position, Quaternion.identity);
            }
            else  // South West
            {
                newBullet = Instantiate(bigBullet, shootPosCentral.position, Quaternion.identity);
            }

        }

        //Freeze

        else
        {
            if (directionHistory == Vector2.up)
            {
                newBullet = Instantiate(bigBulletFreeze, shootPosCentral.position, Quaternion.identity);
            }
            else if (directionHistory == Vector2.right)
            {
                newBullet = Instantiate(bigBulletFreeze, shootPosCentral.position, Quaternion.identity);
            }
            else if (directionHistory == Vector2.left)
            {
                newBullet = Instantiate(bigBulletFreeze, shootPosCentral.position, Quaternion.identity);
            }
            else if ((directionHistory == Vector2.left))
            {
                newBullet = Instantiate(bigBulletFreeze, shootPosCentral.position, Quaternion.identity);
            }
            // diagonal
            else if (directionHistory == dirUpRight)
            { // North East

                newBullet = Instantiate(bigBulletFreeze, shootPosCentral.position, Quaternion.identity);
            }
            else if (directionHistory == dirUpLeft) // North West
            {
                newBullet = Instantiate(bigBulletFreeze, shootPosCentral.position, Quaternion.identity);
            }
            else if (directionHistory == dirDownRight) // South East
            {
                newBullet = Instantiate(bigBulletFreeze, shootPosCentral.position, Quaternion.identity);
            }
            else  // South West
            {
                newBullet = Instantiate(bigBulletFreeze, shootPosCentral.position, Quaternion.identity);
            }
        }




            if (directionHistory == dirUpRight)
            {
                newBullet.GetComponent<Rigidbody2D>().velocity = new Vector2(shootSpeed * Time.fixedDeltaTime, shootSpeed * Time.fixedDeltaTime);
            }
            else if (directionHistory == dirUpLeft)
            {
                newBullet.GetComponent<Rigidbody2D>().velocity = new Vector2(shootSpeed * Time.fixedDeltaTime, -shootSpeed * Time.fixedDeltaTime);
            }
            else if (directionHistory == dirDownRight)
            {
                newBullet.GetComponent<Rigidbody2D>().velocity = new Vector2(-shootSpeed * Time.fixedDeltaTime, shootSpeed * Time.fixedDeltaTime);
            }
            else if (directionHistory == dirDownLeft)
            {
                newBullet.GetComponent<Rigidbody2D>().velocity = new Vector2(-shootSpeed * Time.fixedDeltaTime, -shootSpeed * Time.fixedDeltaTime);
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
            isShootingBig = false;
        }


    // ALternative control normal shooting
    IEnumerator playerShootDirection()
    {
        isShooting = true;
        GameObject newBullet;

       
      
        if (directionHistory == Vector2.up)
        {
            newBullet = Instantiate(bullet, shootPosCentral.position, Quaternion.identity);
        }
        else if (directionHistory == Vector2.right)
        {
            newBullet = Instantiate(bullet, shootPosCentral.position, Quaternion.identity);
        }
        else if (directionHistory == Vector2.left)
        {
            newBullet = Instantiate(bullet, shootPosCentral.position, Quaternion.identity);
        }
        else if ((directionHistory == Vector2.left))
        {
            newBullet = Instantiate(bullet, shootPosCentral.position, Quaternion.identity);
        }
        // diagonal
        else if (directionHistory == dirUpRight)
        { // North East

            newBullet = Instantiate(bullet, shootPosCentral.position, Quaternion.identity);
        }
        else if (directionHistory == dirUpLeft) // North West
        {
            newBullet = Instantiate(bullet, shootPosCentral.position, Quaternion.identity);
        }
        else if (directionHistory == dirDownRight) // South East
        {
            newBullet = Instantiate(bullet, shootPosCentral.position, Quaternion.identity);
        }
        else  // South West
        {
            newBullet = Instantiate(bullet, shootPosCentral.position, Quaternion.identity);
        }

        if (directionHistory == dirUpRight)
        {
            newBullet.GetComponent<Rigidbody2D>().velocity = new Vector2(shootSpeed * Time.fixedDeltaTime, shootSpeed * Time.fixedDeltaTime);
        }
        else if (directionHistory == dirUpLeft)
        {
            newBullet.GetComponent<Rigidbody2D>().velocity = new Vector2(shootSpeed * Time.fixedDeltaTime, -shootSpeed * Time.fixedDeltaTime);
        }
        else if (directionHistory == dirDownRight)
        {
            newBullet.GetComponent<Rigidbody2D>().velocity = new Vector2(-shootSpeed * Time.fixedDeltaTime, shootSpeed * Time.fixedDeltaTime);
        }
        else if (directionHistory == dirDownLeft)
        {
            newBullet.GetComponent<Rigidbody2D>().velocity = new Vector2(-shootSpeed * Time.fixedDeltaTime, -shootSpeed * Time.fixedDeltaTime);
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

    public Vector2 getDirection()
    {
        return direction.normalized;
    }


    public void increaseSpeed()
    {
        speed *= 1.1f;
    }

    public void increaseShootSpeed()
    {
        shootTimer *= 0.9f;
    }


    public void Pause()
    {
        if (!gamePaused)
        {
            Time.timeScale = 0f;
            pauseMenu.SetActive(true);
            gamePaused = true;
        }
        else
        {
            Time.timeScale = 1f;
            pauseMenu.SetActive(false);
            gamePaused = false;
            settingsMenu.SetActive(false);
        }
    }
    IEnumerator MousePointerShot()
    {
        isShooting = true;
        // GameObject newBullet = Instantiate(bullet, transform.position, Quaternion.identity);
        // GameObject spell = Instantiate(bullet, shootPosCentral.position, Quaternion.identity);
        Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);

       
            Vector2 myPos = transform.position;
            Vector2 direct = (mousePos - myPos).normalized;

            GameObject newBullet = Instantiate(bullet, transform.position, Quaternion.identity);

           // Debug.Log(direct);
            newBullet.GetComponent<Rigidbody2D>().velocity = direct * 5;

            yield return new WaitForSeconds(shootTimer);
            isShooting = false;
    }

    IEnumerator MouseDirectionShotSpecial(int mode)
    {
        GameObject newBullet;
        isShooting = true;
        Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector2 myPos = transform.position;
        Vector2 direct = (mousePos - myPos).normalized;


        if (mode == 0) // big bigBullet
        {

            newBullet = Instantiate(bigBullet, transform.position, Quaternion.identity);

        }
        else // freeze bigBulletFreeze
        {
            newBullet = Instantiate(bigBulletFreeze, transform.position, Quaternion.identity);
        }

        // Debug.Log(direct);
        newBullet.GetComponent<Rigidbody2D>().velocity = direct * 5;

        yield return new WaitForSeconds(shootTimer);
        isShooting = false;

        // GameObject newBullet = Instantiate(bullet, transform.position, Quaternion.identity);
        // GameObject spell = Instantiate(bullet, shootPosCentral.position, Quaternion.identity);

    }

    public void activateBigSpell()
    {
         biggerSpellCount += 10;
        

    }

    public void activateBigSpellFreeze()
    {
        biggerSpellFreezeCount += 5;
    }

    public void lowerDashTimer()
    {
        dashTimer *= 0.9f;
    }

   
}
