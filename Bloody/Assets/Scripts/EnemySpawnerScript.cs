using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class EnemySpawnerScript : MonoBehaviour {

    [SerializeField]
    List<GameObject> listOfMonsterType;

    List<GameObject> listOfEnemy;

    int enemyCount;

    // Use this for initialization
    void Start () {
        enemyCount = 0;
        listOfEnemy = new List<GameObject>();
       
        StartCoroutine("spawnMonster", listOfMonsterType[0]);
    }
	
	// Update is called once per frame
	void Update () {

        if (listOfEnemy.Count >= 10)
        {
            StopCoroutine("spawnMonster");
        }

    }


    IEnumerator spawnMonster(GameObject monster)
    {
        while (enemyCount<10)
        {
            yield return new WaitForSeconds(5);
            Instantiate(monster);
            listOfEnemy.Add(monster);
            enemyCount++;
        }
    }
}
