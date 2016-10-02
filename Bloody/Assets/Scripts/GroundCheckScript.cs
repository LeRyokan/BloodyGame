using UnityEngine;
using System.Collections;

public class GroundCheckScript : MonoBehaviour {

    [SerializeField]
    BoxCollider2D groundBoxCollider;

    [SerializeField]
    BoxCollider2D playerBoxCollider;


    [SerializeField]
    PlayerControllerScript player;
    // Use this for initialization
    void Start()
    {
        Physics2D.IgnoreCollision(playerBoxCollider, groundBoxCollider, true);
    }

    // Update is called once per frame

    void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log("CONTACT");
        player.grounded = true;
    }

    void OnTriggerStay2D(Collider2D other)
    {
        player.grounded = true;
    }

    void OnTriggerExit2D(Collider2D other)
    {
        Debug.Log("JE M'ENVOLE");
        player.grounded = false;
    }
}
