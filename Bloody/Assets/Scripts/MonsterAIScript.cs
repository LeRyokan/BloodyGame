using UnityEngine;
using System.Collections;

public class MonsterAIScript : MonoBehaviour {

    [SerializeField]
    Transform enemytransform;

    [SerializeField]
    GameObject enemy;
   

    PlayerStatusScript playerStatus;


    public int health;
    public int damage;
    public bool isDead;

    // Use this for initialization
    void Start () {
        isDead = false;
        health = 40;
        damage = 10;
        playerStatus = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerStatusScript>();
        enemytransform.position = new Vector2(Random.Range(0, 20), Random.Range(0, 20));
	}
	
	// Update is called once per frame
	void Update () {

      
        if(health<=0)
        {
            isDead = true;
            Destroy(enemy);
            
        }
	}

    public void GetHit(int damage)
    {
        Debug.Log("ARK");
        health -= damage;
        //TO DO A AMELIORER
        playerStatus.hasLandedHit();
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
}
