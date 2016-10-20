using UnityEngine;
using System.Collections;

public class MonsterAIScript : MonoBehaviour {

    [SerializeField]
    Transform enemytransform;

    [SerializeField]
    GameObject enemy;
   
    [SerializeField]
    Rigidbody2D enemyRigid;

    PlayerStatusScript playerStatus;

    Transform playerTransform;
    public int health;
    public int damage;
    public bool isDead;
    public int stageringThreshold;
    public int stagerBuffer;
    public bool isHit;
    float initialStagerTime;

    // Use this for initialization
    void Start () {

        isDead = false;
        health = 40;
        damage = 10;
        stagerBuffer = 0;
        initialStagerTime = 0.0f;
        stageringThreshold = 20;
        isHit = false;
        playerStatus = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerStatusScript>();
        playerTransform = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        
        enemytransform.position = new Vector2(Random.Range(0, 20), Random.Range(5, 15));
	}
	
	// Update is called once per frame
	void Update () {

      
        if(health<=0)
        {
            isDead = true;
            Destroy(enemy);
            
        }
	}

    void FixedUpdate()
    {
        IA();
    }

    public void GetHit(int damage)
    {
        Debug.Log("ARK");

        isHit = true;
        health -= damage;
        playerStatus.hasLandedHit();

        
        stagerBuffer += damage;
        if (stagerBuffer >= stageringThreshold)
        {
            Debug.Log("ENEMY STUN !");
            StopCoroutine("Stagerring");
            stagerBuffer = 0;
        }
        else
        {
            StartCoroutine("Staggering" , damage);
        }
       
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        
        if (other.CompareTag("Player"))
        {
            Debug.Log("BIM BIM");
            //On fais des dégats au joueur
            playerStatus.GetHit(damage);
        }
        if(other.CompareTag("PlayerAttack"))
        {
            GetHit(playerStatus.damage);
        }
    }


    IEnumerator Staggering(int damage)
    {
  
        yield return new WaitForSeconds(3.0f);
        stagerBuffer -= damage;
    }

    void IA()
    {
        Vector3 distanceBetween;
        distanceBetween = playerTransform.position - enemytransform.position;
        //Debug.Log(distanceBetween.magnitude);

        if(distanceBetween.magnitude>5)
        {
            //Debug.Log("Enemy moving");
            enemyRigid.AddForce(distanceBetween.normalized, ForceMode2D.Force);
        }
    }

}
