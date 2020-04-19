using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CraftPotion : MonoBehaviour {
	public GameObject allPotionsCrafted;

	public void MakePotion() {
		PotionScriptableObject potionToCraft = ListOfPotions.instance.GetPotionToCraft();
		if (Inventory.instance == null) {
			Debug.LogError("No inventory was assigned in craftPotion component");
			return;
		}
		//if have enough ingredient, enable craft
		if (!Inventory.instance.CheckIngredients(potionToCraft)) {
			Debug.Log("I don't have enough ingredients");
			return;
		}
		//take away ingredients
		for (int i = 0; i < potionToCraft.ingredients.Count; i++) {
			IngredientScriptableObject ingredient = potionToCraft.ingredients[i];
			int amountRequired = potionToCraft.amountOfIngredients[i];
			for (int j = 0; j < amountRequired; j++) {
				Inventory.instance.RemoveIngredient(ingredient);
				Debug.Log("Removed ingredient");
			}
		}
		potionToCraft.amountOfPotion--;
		Debug.Log("I crafted a potion");

		foreach (PotionScriptableObject p in Inventory.instance.potionsToCraft) {
			if (p.amountOfPotion > 0) {
				return;
			}
		}
		allPotionsCrafted.SetActive(true);
	}
}
