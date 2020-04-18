using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item:MonoBehaviour
{
    public IngredientScriptableObject ingredient;
    public int amount = 1;

    void Start()
    {
        GetComponent<SpriteRenderer>().sprite = ingredient.sprite;
    }
}
