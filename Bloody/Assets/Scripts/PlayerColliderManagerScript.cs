using UnityEngine;
using System.Collections;
using System.Collections.Generic;



public class PlayerColliderManagerScript : MonoBehaviour {

    [SerializeField]
    BoxCollider2D playerHitbox;

    List<Ray2D> playerRaycast;
    RaycastHit2D ray;
    Ray2D aray;
    RaycastOrigins raycastOrigins;

    float skinWidth = .015f;

    public int horizontalRayCount = 4;
    public int verticalRayCount = 4;

    float horizontalRaySpacing;
    float verticalRaySpacing;

    // Use this for initialization
    void Start () {
       /* Debug.Log(playerHitbox.size.x);
        Debug.Log(playerHitbox.size.y);
        aray = new Ray2D(new Vector2(playerHitbox.bounds.max.x,playerHitbox.bounds.max.y),  Vector2.right);
        ray =  Physics2D.Raycast(playerHitbox.size, Vector2.right);*/
    }
	
	// Update is called once per frame
	void Update () {
        UpdateRaycastOrigins();
        CalculateRaySpacing();

        for (int i=0; i< verticalRayCount; i++)
        {
            Debug.DrawRay(raycastOrigins.bottomLeft + Vector2.right * verticalRaySpacing * i , Vector2.up *-2, Color.red); 
        }
    }

    void UpdateRaycastOrigins()
    {
        Bounds playerHitBoxbounds = playerHitbox.bounds;
        playerHitBoxbounds.Expand(skinWidth * -2);

        raycastOrigins.bottomLeft = new Vector2(playerHitBoxbounds.min.x, playerHitBoxbounds.min.y);
        raycastOrigins.bottomRight = new Vector2(playerHitBoxbounds.max.x, playerHitBoxbounds.min.y);
        raycastOrigins.topLeft = new Vector2(playerHitBoxbounds.min.x, playerHitBoxbounds.max.y);
        raycastOrigins.topRight = new Vector2(playerHitBoxbounds.max.x, playerHitBoxbounds.max.y);

    }

    void CalculateRaySpacing()
    {
        Bounds playerHitBoxbounds = playerHitbox.bounds;
        playerHitBoxbounds.Expand(skinWidth * -2);

        horizontalRayCount = Mathf.Clamp(horizontalRayCount, 2, int.MaxValue);
        verticalRayCount = Mathf.Clamp(verticalRayCount, 2, int.MaxValue);

        horizontalRaySpacing = playerHitBoxbounds.size.y / (horizontalRayCount - 1);
        verticalRaySpacing = playerHitBoxbounds.size.x / (verticalRayCount - 1);
    }

    struct RaycastOrigins{
        public Vector2 topLeft, topRight;
        public Vector2 bottomLeft, bottomRight;
    }


    void OnDrawGizmos()
    {
        float sphereRadius = 0.2f;
        Gizmos.DrawWireSphere(raycastOrigins.bottomLeft, sphereRadius);
        Gizmos.DrawWireSphere(raycastOrigins.bottomRight, sphereRadius);
        Gizmos.DrawWireSphere(raycastOrigins.topLeft, sphereRadius);
        Gizmos.DrawWireSphere(raycastOrigins.topRight, sphereRadius);


        //Gizmos.DrawRay()
    }
}
