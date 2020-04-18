using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item:MonoBehaviour
{
    public IngredientScriptableObject ingredient;
    public Sprite sprite;
    public string name;
    public string description;
    public int amount = 1;
    public void constructor(Item item)
    {
        this.sprite = ingredient.sprite;
        this.name = ingredient.name;
        this.description = ingredient.description;
    }
}
