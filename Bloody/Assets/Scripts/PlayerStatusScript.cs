using UnityEngine;
using System.Collections;

public class PlayerStatusScript : MonoBehaviour {

    [SerializeField]
    GameObject playerGameObject;

    public int health;
    public int healthbuffer;
    public int life;
    public int damage;
    public bool isDead = false;
    void Start()
    {
        health = 100;
        life = 2;
        damage = 40;
    }

	
	
	// Update is called once per frame
    void Update()
    {
        if(health<=0)
        {
            Debug.Log("JE MEURT AAAAAAAAH !");
            Destroy(playerGameObject);

            //On arrete le jeu
            isDead = true;
        }
    }


    public void GetHit(int damage)
    {
        Debug.Log("J'ai pris un coup je crois");
        health -= damage;
    }



    // BLOODBORN MECHANICS SECTION //

    public void lifeBuffer(int actualhealth,int damage)
    {
        healthbuffer = actualhealth;
        health -= damage;

        if (hit)
        {
            if(health+5>healthbuffer)
            {
                health += 5;
            }
            
        }

    }





}
