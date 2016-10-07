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
    Camera mainCamera;

    float shakeTimer;

	// Use this for initialization
	void Start (){
       
        mainCamera = GetComponent<Camera>();
        minCameraPos = new Vector3(10, 0, 0);
        maxCameraPos = new Vector3(100, 0, 0);
        originalCameraPosition = mainCamera.transform.position;
        position = new Vector3(playerTransform.position.x, cameraTransform.position.y, cameraTransform.position.z);
	}
	
	// Update is called once per frame
	void Update (){
        
        //TODO AJOUTER UNE ANIMATION DE COUP POUR RENDRE LE TRUC REALISTE
        if(shakeTimer >= 0)
        {
            Vector2 shakePos = Random.insideUnitCircle * shakeAmt;
            mainCamera.transform.position = new Vector3(originalCameraPosition.x + shakePos.x, originalCameraPosition.y + shakePos.y, transform.position.z);
            shakeTimer -= Time.deltaTime;
        }
        else
        {
            position.x = playerTransform.position.x;
            cameraTransform.position = new Vector3(Mathf.Clamp(position.x, minCameraPos.x, maxCameraPos.x), originalCameraPosition.y, mainCamera.transform.position.z);
        }
    }

    /// <summary>
    /// /CAMERA SHAKE
    /// </summary>

    Vector3 originalCameraPosition;
    [SerializeField]
    float shakeAmt;

    public void ShakeCamera(float shakePwr, float shakeDur)
    {
        originalCameraPosition= mainCamera.transform.position;
        shakeAmt = shakePwr;
        shakeTimer = shakeDur;
    }

   

}
