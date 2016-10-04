using UnityEngine;
using System.Collections;

public class Potion : Item {

    int healthAmounGiven;


    public Potion()
    {
        Debug.Log("ON CREER LA POTE");
        name = "Potion";
        ID = 0;
        isStackable = true;
        type = ItemType.CONSOMABLE;
        healthAmounGiven = 10;
        stackMax = 3;
        stack = stackMax;
    }
	
    
	public override void Use(GameObject user)
    {  
        if(stack>0)
        {
            Debug.Log("One pot use ! " + stack + " remaining.");
            user.GetComponent<PlayerStatusScript>().regenInstant(healthAmounGiven);
            if(stack-1<0)
            {
                stack = 0;
            }
            else
            {
                stack--;
            }
        }
        else
        {
            Debug.Log("No potion available");
        }
    }
}
