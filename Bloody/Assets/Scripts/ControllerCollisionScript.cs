using UnityEngine;
using System.Collections;

public class ControllerCollisionScript : MonoBehaviour {

    // Use this for initialization

    [SerializeField]
    BoxCollider2D platformTriggerCollider;

    [SerializeField]
    BoxCollider2D platformCollider;

    BoxCollider2D playerCollider;

	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        playerCollider = GameObject.Find("Player").GetComponent<BoxCollider2D>();
        Physics2D.IgnoreCollision(platformCollider, platformTriggerCollider,true);
	}

    void OnTriggerEnter2D(Collider2D other)
    {
        if(other.gameObject.name == "Player")
        {
            Physics2D.IgnoreCollision(playerCollider, platformCollider, true);
        }
    }
    void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.name == "Player")
        {
            Physics2D.IgnoreCollision(playerCollider, platformCollider, false);
        }
    }
}
