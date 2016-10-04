using UnityEngine;
using System.Collections;


public enum ItemType
{
    CONSOMABLE,
    ARME,
    ARMUR,
    PARCHEMINS
}

public class Item{

    public string name;
    public int ID;
    public ItemType type;
    protected bool isStackable;
    protected int stack;
    protected int stackMax;

	public Item()
    {

    }

    public virtual void Use(GameObject user)
    {
        
    }

	
}
