using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class PlayerStatusScript : MonoBehaviour
{

    // [SerializeField]
    // public GameObject playerGameObject;

    //[SerializeField]
    // Image healthBar;

    CameraControllerScript mainCamera;
   

    public List<Item> consomableList;


    public float staminaMax;
    public float stamina;
    public int health;
    public int healthBuffer;
    public int healthMax;
    public int life;
    public int damage;
    public bool isDead = false;
    public bool isHit = false;
    public int valueToAdd;

    void Start()
    {
        //A MEDITER POUR GARDER LES INFOS ALIVE ! 

        DontDestroyOnLoad(this.gameObject);
        mainCamera =  GameObject.FindGameObjectWithTag("MainCamera").GetComponent<CameraControllerScript>();
        staminaMax = 70.0f;
        stamina = staminaMax;
        health = 100;
        healthMax = health;
        healthBuffer = health;
        life = 2;
        damage = 10;
        valueToAdd = 10;
        //AUTANT ETRE SALEEEEEEE
        consomableList = new List<Item>();
        
        Item myItem = new Potion();
        consomableList.Add(myItem);
    }



    // Update is called once per frame
    void Update()
    {
        if (health <= 0)
        {
            Debug.Log("JE MEURT AAAAAAAAH !");
            Destroy(this.gameObject);

            //On arrete le jeu
            isDead = true;
        }
    }


    public void GetHit(int damage)
    {
        isHit = true;
        mainCamera.ShakeCamera(0.5f, 0.3f);
        StopCoroutine("degenLifeBuffer");
        Debug.Log("J'ai pris un coup je crois");
        healthBuffer = health;
        health -= damage;
        //healthMask.PerformClipping();
        StartCoroutine("degenLifeBuffer");
    }



    // BLOODBORN MECHANICS SECTION //
    public void hasLandedHit()
    {

        if (health + valueToAdd > healthBuffer)
        {
            health = healthBuffer;
        }
        else
        {
            health += 5;
        }

    }

    /// <summary>
    /// SECTION DU HEAL
    /// </summary>
    /// <returns></returns>
    /// 
    #region HEAL

    public void regenInstant(int healthRegen)
    {
        if(health + healthRegen > healthMax)
        {
            health = healthMax;
        }
        else
        {
            health += healthRegen;
        }
       
    }

   /* void regenDuringTime(int healthRegen, int time)
    {
        StartCoroutine("regenDuringTimeCo");
    
    }

    IEnumerator regenDuringTime(int healthRegen, int time)
    {
        int hpPerSec = 0;
        int actualtime = 0;
        hpPerSec = healthRegen / time;

        while (actualtime != time)
        {
            yield return new WaitforSeconds(1);
            health += hpPerSec;
            actualtime++;
        }
    }
    */

    public void IncreaseHealthMax(int healAmount)
    {
        healthMax += healAmount;
        health = healthMax;
        healthBuffer = healthMax;
    }

    public void IncreaseStaminaMax(int staminaAmount)
    {
        staminaMax += staminaAmount;
        stamina = staminaMax;
    }
    #endregion



    IEnumerator degenLifeBuffer()
    {

        yield return new WaitForSeconds(3.0f);
        healthBuffer = health;
        isHit = false; 
    }


    void OnDestroy()
    {
        
    }
}
