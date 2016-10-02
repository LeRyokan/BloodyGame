using UnityEngine;
using System.Collections;

public class CameraControllerScript : MonoBehaviour {
    [SerializeField]
    Transform playerTransform;

    [SerializeField]
    Transform cameraTransform;

    //Pourrais etre remplacer par des int
    Vector3 minCameraPos;
    Vector3 maxCameraPos;

    Vector3 position;
	// Use this for initialization
	void Start () {
        minCameraPos = new Vector3(10, 0, 0);
        maxCameraPos = new Vector3(100, 0, 0);

        position = new Vector3(playerTransform.position.x, cameraTransform.position.y, cameraTransform.position.z);
	}
	
	// Update is called once per frame
	void Update () {
        position.x = playerTransform.position.x;

        cameraTransform.position = new Vector3(Mathf.Clamp(position.x, minCameraPos.x,maxCameraPos.x), cameraTransform.position.y, cameraTransform.position.z);
	    
	}
}
