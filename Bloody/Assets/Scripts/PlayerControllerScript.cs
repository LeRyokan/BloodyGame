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

    void Start ()
    {
        rdyToHit = true;
        grounded = false;
        jumpForceV2 = new Vector2(0, 1) * jumpForce;
        dashForce = new Vector2(0, 0);
	}
   

    // Update is called once per frame
    void FixedUpdate() {
   
        horizontalValue = Input.GetAxis("Horizontal");
        velocityX = 0;
        dashForce.x =  -6000.0f;
        if(!isDashing)
        {
            //Déplacement droite - gauche
            if (horizontalValue > 0.2f)
            {
                velocityX = speed * horizontalValue;
                sprite.flipX = false;
            }
            else if (horizontalValue < -0.2f)
            {
                velocityX = speed * horizontalValue;
                sprite.flipX = true;
            }
        }
        

        rigid.velocity = new Vector2(velocityX, rigid.velocity.y);

                 //Saut
        if ((Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.Joystick1Button1)) && grounded)
        { 
            rigid.AddForce(jumpForceV2 , ForceMode2D.Impulse);
        }
                //Attaque au sol
        else if(Input.GetKeyDown(KeyCode.Joystick1Button2) && rdyToHit)
        {
            StartCoroutine("attackReload");
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
        yield return new WaitForSeconds(0.2f * Time.deltaTime);
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
