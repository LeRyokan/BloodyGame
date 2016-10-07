using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;

public class GameManagerScript : MonoBehaviour
{

    [SerializeField]
    GameObject player;

    [SerializeField]
    LevelManagerScript levelManager;

    PlayerControllerScript playerControllerScript;

    PlayerStatusScript playerStatusScript;
  
    bool endGame = false;
    // Use this for initialization
    void Start()
    {
        
        playerControllerScript = player.GetComponent<PlayerControllerScript>();
        playerStatusScript = player.GetComponent<PlayerStatusScript>();
    }

    // Update is called once per frame
    void Update()
    {
       
        if(playerStatusScript.isDead)
        {
            endGame = true;
            Time.timeScale = 0;
        }
      
        //if (Input.GetKeyDown(KeyCode.Joystick1Button4))
        //{
        //    SceneManager.LoadScene(0);
        //    Time.timeScale = 1;
        //}

    }

   

}