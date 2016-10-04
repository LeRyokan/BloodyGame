using UnityEngine;
using System.Collections;

public class PlayerControllerScript : MonoBehaviour {

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

    
    PlayerStatusScript playerStatus;

    private BoxCollider2D attackColliderBox;

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

    public bool rdyToHit;




    float horizontalValue;
    float verticalValue;
    float timePressed = 0.0f;
    float timeRelease = 0.0f;

    void Start ()
    {
        attackColliderBox = attackCollider.GetComponent<BoxCollider2D>();
        attackCollider.SetActive(false);
        rdyToHit = true;
        grounded = false;
        jumpForceV2 = new Vector2(0, 1) * jumpForce;
        dashForce = new Vector2(0, 0);
        playerStatus = GetComponent<PlayerStatusScript>();
        dashForce.x = -30000.0f;
        
        timePressed = 0.0f;
    }
   

    // Update is called once per frame
    void FixedUpdate() {
   
        horizontalValue = Input.GetAxis("Horizontal");
        verticalValue = Input.GetAxis("Vertical");
        velocityX = 0;

        if (!isDashing)
        {
            
            //Déplacement droite - gauche
            if (horizontalValue > 0.2f)
            {
                velocityX = speed * horizontalValue;
                sprite.flipX = false;
                if(attackColliderBox.offset.x < 0)
                {
                    attackColliderBox.offset = new Vector2(-attackColliderBox.offset.x, attackColliderBox.offset.y);
                }
                    
               
                    
                

            }
            else if (horizontalValue < -0.2f)
            {
                velocityX = speed * horizontalValue;
                sprite.flipX = true;
                if(attackColliderBox.offset.x>0)
                {
                    attackColliderBox.offset = new Vector2(-attackColliderBox.offset.x, attackColliderBox.offset.y);
                }
            }
            else if(verticalValue>0.2f)
                {
                Debug.Log("Crouuch");
            }
        }

        rigid.velocity = new Vector2(velocityX, rigid.velocity.y);



        //Saut
        if ( Input.GetKeyDown(KeyCode.Joystick1Button1) && grounded)
        {
            rigid.AddForce(jumpForceV2, ForceMode2D.Impulse);
           // rigid.AddForce(jumpForceV2, ForceMode2D.Force);
            timePressed = Time.time ;
            Debug.Log("DOWN float :" + timePressed);
        }
        if(Input.GetKeyUp(KeyCode.Joystick1Button1))
        {
           
            timeRelease = Time.time ;
            Debug.Log("UP float:" + timePressed);
            timePressed = timeRelease - timePressed;
            //rigid.velocity = new Vector2(rigid.velocity.x, rigid.velocity.y - timePressed*50);
            rigid.AddForce(new Vector2(0, -500), ForceMode2D.Impulse);
            Debug.Log(timePressed);

            timePressed = 0.0f;

            
            
        }
            //Attaque au sol
        else if (Input.GetKeyDown(KeyCode.Joystick1Button2) && rdyToHit)
        {
            StartCoroutine("attackReload");
        }
            //Bigger Attaque

        else if (Input.GetKeyDown(KeyCode.Joystick1Button5))
        {
            Debug.Log("Big Attack");
        }
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

        }

        //Touche de debug
        if (Input.GetKey(KeyCode.D))
        {
            Debug.Log(rigid.velocity.ToString());

        }




        animator.SetFloat("VelocityY", rigid.velocity.y);
    }

    void Update()
    {
        
        jumpForceV2 = new Vector2(0, 1) * jumpForce;
        // Debug.Log(rigid.velocity.ToString());
        animator.SetFloat("VelocityX", Mathf.Abs(Input.GetAxis("Horizontal")));
        animator.SetBool("Grounded", grounded);

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



}
