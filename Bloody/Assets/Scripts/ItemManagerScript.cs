using UnityEngine;
using System.Collections;
using System.Collections.Generic;


public class ItemManagerScript : MonoBehaviour {

    List<Item> allObjectsList;
    


	// Use this for initialization
	void Start ()
    {
        allObjectsList = new List<Item>();

        //Listing de tous les items
        allObjectsList.Add(new Potion());

	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
