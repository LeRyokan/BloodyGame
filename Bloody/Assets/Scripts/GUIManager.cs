using UnityEngine;
using System.Collections;

public class GUIManager : MonoBehaviour {

    [SerializeField]
    PlayerStatusScript playerStatus;

    [SerializeField]
    RectTransform healthBar;
    [SerializeField]
    RectTransform staminaBar;
    [SerializeField]
    RectTransform bufferBar;
    [SerializeField]
    RectTransform healthBorderBar;
    [SerializeField]
    RectTransform staminaBorderBar;

    int count = 0;
    // Use this for initialization
    void Start () {

        playerStatus = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerStatusScript>();
       /*
        ///Health
        healthBar.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, playerStatus.healthMax);
        bufferBar.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, playerStatus.healthMax);
        healthBorderBar.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, playerStatus.healthMax);

        //Stamina
        staminaBorderBar.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, playerStatus.staminaMax);
        staminaBar.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, playerStatus.staminaMax);

    */

    }
	
	// Update is called once per frame
	void LateUpdate () {
        if(count==0)
        {
            ///Health
            healthBar.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, playerStatus.healthMax);
            bufferBar.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, playerStatus.healthMax);
            healthBorderBar.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, playerStatus.healthMax);

            //Stamina
            staminaBorderBar.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, playerStatus.staminaMax);
            staminaBar.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, playerStatus.staminaMax);
            count++;
        }

        
            healthBar.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, playerStatus.health);
            bufferBar.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, playerStatus.healthBuffer);
        staminaBar.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, playerStatus.stamina);




    }
}
