﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CraftPotion : MonoBehaviour
{
    public Inventory inventory;

    public void MakePotion(PotionScriptableObject potionToCraft)
    {
        if(inventory == null)
        {
            Debug.LogError("No inventory was assigned in craftPotion component");
            return;
        }
        //if have enough ingredient, enable craft
        if (!inventory.CheckIngredients(potionToCraft))
            return;
        //take away ingredients
        for (int i = 0; i < potionToCraft.ingredients.Count; i++)
        {
            IngredientScriptableObject ingredient = potionToCraft.ingredients[i];
            int amountRequired = potionToCraft.amountOfIngredients[i];
            for (int j = 0; j < amountRequired; j++)
            {
                inventory.RemoveIngredient(ingredient);
            }
        }
        potionToCraft.amountOfPotion++;
    }
}