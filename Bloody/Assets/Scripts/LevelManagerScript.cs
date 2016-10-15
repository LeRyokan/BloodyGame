using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;

public class LevelManagerScript : MonoBehaviour {

    [SerializeField]
    PlayerControllerScript playerManager;

    [SerializeField]
    EnemySpawnerScript enemySpawner;

    [SerializeField]
    CollisionBorderLevelScript triggerIn;

    [SerializeField]
    CollisionBorderLevelScript triggerOut;

    [SerializeField]
    List<int> sceneList;

   
    public int actualSceneId;
    //SceneManager sceneManager;

    /* [SerializeField]
     ItemManager playerManager;*/

    // Use this for initialization
    void Start () {

        sceneList = new List<int>();
        
    }
	
	// Update is called once per frame
	void Update () {

        //Changement de scene a l'arrache
        if (triggerIn != null)
        {
            if (triggerIn.playerIsOut)
            {
                SceneManager.LoadScene(0);

            }
        }
       
      

        if (triggerOut.playerIsOut)
        {
            //  ChangeScene(sceneList[actualSceneId + 1]);
            SceneManager.LoadScene(1);
        }
    }


    void ChangeScene(Scene nextScene)
    {
        SceneManager.LoadScene(nextScene.buildIndex);
    }

    void DestroyObjectInScene(List<GameObject> objectToDestroy)
    {
        for(int i = 0; i < objectToDestroy.Count;++i)
        {
            Destroy(objectToDestroy[i]);
        }
       
    }

    void LoadObjectNeededInScene(List<GameObject> objectToCreate)
    {
        //Instance maybe
        
    }
}
