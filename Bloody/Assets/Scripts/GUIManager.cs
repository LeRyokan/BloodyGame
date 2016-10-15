using UnityEngine;
using System.Collections;

public class GUIManager : MonoBehaviour
{

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
    [SerializeField]
    RectTransform whiteLine;

    int playerMaxHealth;
    float playerStaminaMax;

    int count = 0;
    // Use this for initialization
    void Start()
    {
       
        playerStatus = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerStatusScript>();
        playerMaxHealth = playerStatus.healthMax;
        playerStaminaMax = playerStatus.staminaMax;
        whiteLine.SetInsetAndSizeFromParentEdge(RectTransform.Edge.Right, 0.5f, 2.0f);

    }

    void Update()
    {
        if (count == 0)
        {
            ///Health
            healthBar.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, playerStatus.healthMax);
            bufferBar.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, playerStatus.healthMax);
            healthBorderBar.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, playerStatus.healthMax+5);

            //Stamina
            staminaBorderBar.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, playerStatus.staminaMax);
            staminaBar.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, playerStatus.staminaMax);
            count++;
        }

        if (playerStaminaMax < playerStatus.staminaMax)
        {
            ExtendStaminaBar();
        }

        if (playerMaxHealth < playerStatus.healthMax)
        {
            ExtendHealthBar();
        }

        healthBar.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, playerStatus.health);
        bufferBar.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, playerStatus.healthBuffer);
        staminaBar.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, playerStatus.stamina);
    }

    // Update is called once per frame
    void LateUpdate()
    {








    }



    void ExtendHealthBar()
    {
        healthBar.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, playerStatus.healthMax);
        bufferBar.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, playerStatus.healthMax);
        healthBorderBar.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, playerStatus.healthMax);
    }

    void ExtendStaminaBar()
    {
        staminaBorderBar.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, playerStatus.staminaMax);
        staminaBar.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, playerStatus.staminaMax);
    }
}
