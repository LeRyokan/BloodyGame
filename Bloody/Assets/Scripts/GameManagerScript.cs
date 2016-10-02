using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;

public class GameManagerScript : MonoBehaviour
{
    [SerializeField]
    GameObject enemySpawner;

    [SerializeField]
    GameObject player;

    [SerializeField]
    List<GameObject> listOfMonsterType;

    List<GameObject> listOfEnemy;

    PlayerControllerScript playerControllerScript;

    PlayerStatusScript playerStatusScript;
    int enemyCount;
    bool endGame = false;
    // Use this for initialization
    void Start()
    {
        listOfEnemy = new List<GameObject>(); 
        Instantiate(enemySpawner);
        StartCoroutine("spawnMonster", listOfMonsterType[0]);
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
       if(listOfEnemy.Count >= 10)
        {
            StopCoroutine("spawnMonster");
        }
        //if (Input.GetKeyDown(KeyCode.Joystick1Button4))
        //{
        //    SceneManager.LoadScene(0);
        //    Time.timeScale = 1;
        //}

    }

    IEnumerator spawnMonster(GameObject monster)
    {
        while (!endGame)
        { 
            yield return new WaitForSeconds(5);
            Instantiate(monster);
            listOfEnemy.Add(monster);
            //enemyCount++;
        }
    }   

}