using UnityEngine;
using System.Collections;

enum playerState
{
    STAND,
    CROUCH,
}


public class PlayerControllerScript : MonoBehaviour
{

    [SerializeField]
    Rigidbody2D rigid;

    [SerializeField]
    Transform playerTransform;

    [SerializeField]
    Animator animator;

    [SerializeField]
    SpriteRenderer sprite;

    [SerializeField]
    GameObject attackCollider;

    //[SerializeField]
    //Collider2D crouchPlayerCollider;

    //[SerializeField]
   // BoxCollider2D boxCrouchCollider;

    Vector2 standingSizePlayerBoxCollider;

    Collider2D playerCollider;
    BoxCollider2D playerBoxCollider;

    PlayerStatusScript playerStatus;

    private BoxCollider2D attackColliderBox;

    //Input LastKeyPressed;
    KeyCode LastKeyPressed;

    private Vector2 jumpForceV2;
    private Vector2 moveForceV2;
    private Vector2 dashForce;
    // Use this for initialization
    [SerializeField]
    float jumpForce;

    [SerializeField]
    float speed;
    [SerializeField]
    float gravity;

    public float velocityX = 0.0f;
    public float velocityY = 0.0f;
    public bool grounded;
    public bool isDashing;
    public bool isSprinting;
    public bool rdyToHit;
    public bool isCrouch;


    public int property;



    float horizontalValue;
    float verticalValue;
    float timePressed = 0.0f;
    float timeRelease = 0.0f;

    RaycastHit2D raycast;


    void Start()
    {

        playerCollider = GetComponent<BoxCollider2D>();
        playerBoxCollider = GetComponent<BoxCollider2D>();
        attackColliderBox = attackCollider.GetComponent<BoxCollider2D>();
        attackCollider.SetActive(false);
        rdyToHit = true;
        grounded = false;
        isSprinting = false;
        jumpForceV2 = new Vector2(0, 1) * jumpForce;
        dashForce = new Vector2(0, 0);
        playerStatus = GetComponent<PlayerStatusScript>();
        dashForce.x = -30000.0f;
        timePressed = 0.0f;
        property = 10;
        standingSizePlayerBoxCollider = playerCollider.bounds.size;
        

    }


    // Update is called once per frame
    void FixedUpdate()
    {
        raycast = new RaycastHit2D();
        raycast = Physics2D.Raycast(playerCollider.transform.position, new Vector2(playerCollider.transform.position.x + 10, 0));

        horizontalValue = Input.GetAxis("Horizontal");
        verticalValue = Input.GetAxis("Vertical");
        velocityX = 0;

        //Saut
        if (Input.GetKeyDown(KeyCode.Joystick1Button1) && grounded)
        {
            rigid.AddForce(jumpForceV2, ForceMode2D.Impulse);
            // rigid.AddForce(jumpForceV2, ForceMode2D.Force);
            timePressed = Time.time;
            Debug.Log("DOWN float :" + timePressed);
        }
        if (Input.GetKeyUp(KeyCode.Joystick1Button1))
        {

            timeRelease = Time.time;
            Debug.Log("UP float:" + timePressed);
            timePressed = timeRelease - timePressed;
            //rigid.velocity = new Vector2(rigid.velocity.x, rigid.velocity.y - timePressed*50);
            rigid.AddForce(new Vector2(0, -500), ForceMode2D.Impulse);
            Debug.Log(timePressed);

            timePressed = 0.0f;



        }

        #region ATTAQUE REG


        //Bigger Attaque
        else if (Input.GetKeyDown(KeyCode.Joystick1Button5))
        {
            Debug.Log("Big Attack");
        }

        else if ((Input.GetKey(KeyCode.Joystick1Button2) && Mathf.Abs(horizontalValue) > 0.30 && Mathf.Abs(rigid.velocity.x) < 0.5f) && rdyToHit)
        {

            Debug.Log("Jump Attack");
            StartCoroutine("stunAttack");
        }

        //Attaque au sol

        ///TODO A REVOIR
        else if (Input.GetKeyDown(KeyCode.Joystick1Button2) && rdyToHit && LastKeyPressed == KeyCode.Joystick1Button2)
        {
            Debug.Log("COMBO !");
            Debug.Log(rigid.velocity.x);
            LastKeyPressed = KeyCode.Joystick1Button2;

            StartCoroutine("attackReload");
        }

        else if (Input.GetKeyDown(KeyCode.Joystick1Button2) && rdyToHit)
        {
            Debug.Log("HIRU" + Mathf.Abs(horizontalValue));
            Debug.Log(rigid.velocity.x);
            LastKeyPressed = KeyCode.Joystick1Button2;

            StartCoroutine("attackReload");
        }

        #endregion

        //Dash arrière
        else if (Input.GetKeyDown(KeyCode.Joystick1Button4))
        {
            Debug.Log("Dash");
            StartCoroutine("dash");
            //if (sprite.flipX)
            //{
            //    dashForce = -dashForce;
            //}
            //rigid.AddForce(dashForce, ForceMode2D.Impulse);
        }
        //Utiliser objet équipé
        else if (Input.GetKeyDown(KeyCode.Joystick1Button3))
        {
            Debug.Log("ON HEAAAAAAAALLLLLLLL");

            playerStatus.consomableList[0].Use(this.gameObject);

        }

        else if (Input.GetKeyDown(KeyCode.Joystick1Button6))
        {
            //on affiche le menu 
            //On pause le jeu
            Time.timeScale = 0.0f;

        }

        //Touche de debug
        if (Input.GetKey(KeyCode.D))
        {
            Debug.Log(rigid.velocity.ToString());
            playerStatus.IncreaseHealthMax(50);
        }

        if (!isDashing)
        {

            //Déplacement droite - gauche
            if (horizontalValue > 0.2f)
            {
                velocityX = speed * horizontalValue;
                sprite.flipX = false;
                if (attackColliderBox.offset.x < 0)
                {
                    attackColliderBox.offset = new Vector2(-attackColliderBox.offset.x, attackColliderBox.offset.y);
                }

            }
            else if (horizontalValue < -0.2f)
            {
                velocityX = speed * horizontalValue;
                //TODO IF NOT TRUE, THEN TRUE
                sprite.flipX = true;
                if (attackColliderBox.offset.x > 0)
                {
                    attackColliderBox.offset = new Vector2(-attackColliderBox.offset.x, attackColliderBox.offset.y);
                }
            }
            else if (verticalValue > 0.2f)
            {
                //TODO IF NOT TRUE, SO TRUE

                //playerCollider.enabled = false;
                //crouchPlayerCollider.enabled = true;
                //playerBoxCollider.size = new Vector2(standingSizePlayerBoxCollider.x, standingSizePlayerBoxCollider.y/2);
               // playerCollider.
                isCrouch = true;


                Debug.Log("Crouuch");
            }
            else if (verticalValue < 0.2f)
            {
               // playerBoxCollider.size = standingSizePlayerBoxCollider;
                //crouchPlayerCollider.enabled = false;
                //playerCollider.enabled = true;
                isCrouch = false;
            }

        }
        if (isSprinting)
        {
            isSprinting = false;
        }

        //Sprint
        if (Input.GetKey(KeyCode.Joystick1Button0) && grounded && playerStatus.stamina > 0)
        {
            if (!isSprinting)
            {
                isSprinting = true;
            }
            playerStatus.stamina -= 0.3f;
            velocityX = velocityX * 1.5f;
        }



        rigid.velocity = new Vector2(velocityX, rigid.velocity.y);


        animator.SetFloat("VelocityY", rigid.velocity.y);
    }

    void Update()
    {

        jumpForceV2 = new Vector2(0, 1) * jumpForce;
        // Debug.Log(rigid.velocity.ToString());
        animator.SetFloat("VelocityX", Mathf.Abs(Input.GetAxis("Horizontal")));
        animator.SetBool("Grounded", grounded);
        animator.SetBool("Sprint", isSprinting);
        if (!isSprinting && playerStatus.stamina != playerStatus.staminaMax)
        {
            Debug.Log("STAMINA REGEN !");
            if (playerStatus.stamina + 1 > playerStatus.staminaMax)
            {
                playerStatus.stamina = playerStatus.staminaMax;
            }
            else
            {
                playerStatus.stamina += 1;
            }

        }
    }


    IEnumerator attackReload()
    {

        Debug.Log("ATTACK");
        animator.SetTrigger("Attack");
        attackCollider.SetActive(true);
        rdyToHit = false;
        yield return new WaitForEndOfFrame();
        attackCollider.SetActive(false);
        yield return new WaitForSeconds(0.1f * Time.deltaTime);
        rdyToHit = true;
    }


    IEnumerator dash()
    {
        //TO DO A FINIOLER
        Debug.Log("Dash Coroutine");
        isDashing = true;
        animator.SetTrigger("Dash");
        if (sprite.flipX)
        {
            dashForce = -dashForce;
        }
        rigid.AddForce(dashForce, ForceMode2D.Impulse);
        yield return new WaitForFixedUpdate();
        rigid.velocity = new Vector2(0, 0);
        yield return new WaitForSeconds(0.7f);
        isDashing = false;
    }

    IEnumerator stunAttack()
    {


        yield return new WaitForSeconds(0.2f);
    }

    /* void OnDrawGizmos()
     {
         //Gizmos.DrawRay(new Vector3(raycast.centroid.x, raycast.centroid.y, 0), new Vector3(raycast.point.x, raycast.point.y, 0));
         Gizmos.DrawRay(new Vector2(raycast.transform.position.x+ playerCollider.bounds.extents.x, raycast.transform.position.y+playerCollider.bounds.extents.y), Vector2.right);
     }*/

    ///GETTER SETTER
    /// 

    public bool GetGrounded()
    {
        return grounded;
    }

    public void SetGrounded(bool value)
    {
        grounded = value;
    }

    public bool GetIsSprinting()
    {
        return isSprinting;
    }

    public void SetIsSprinting(bool value)
    {
        isSprinting = value;
    }

    public bool GetIsDashing()
    {
        return isDashing;
    }
    public void SetDashing(bool value)
    {
        isDashing = value;
    }

    public bool GetRdyToHit()
    {
        return rdyToHit;
    }

    public void SetRdyToHit(bool value)
    {
        rdyToHit = value;
    }
}

