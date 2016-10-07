using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class LevelManagerScript : MonoBehaviour {

    [SerializeField]
    PlayerControllerScript playerManager;

    [SerializeField]
    EnemySpawnerScript enemySpawner;

    //SceneManager sceneManager;

   /* [SerializeField]
    ItemManager playerManager;*/

    // Use this for initialization
    void Start () {
        //sceneManager = new SceneManager();

    }
	
	// Update is called once per frame
	void Update () {
	
	}


    void ChangeScene(Scene nextScene)
    {
        SceneManager.LoadScene(nextScene.buildIndex);
    }
}
