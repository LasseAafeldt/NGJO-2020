using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CraftPotion : MonoBehaviour {
	public GameObject allPotionsCrafted;
	public AudioClip allDone;

	public void MakePotion() {
		PotionScriptableObject potionToCraft = ListOfPotions.instance.GetPotionToCraft();
		if (!potionToCraft || potionToCraft.amountOfPotion <= 0) {
			Debug.Log("Already crafted");
			return;
		}
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
		ListOfPotions.instance.SetPotionToCraft(potionToCraft);
		GetComponent<AudioSource>().Play();

		foreach (PotionScriptableObject p in Inventory.instance.potionsToCraft) {
			if (p.amountOfPotion > 0) {
				return;
			}
		}
		StartCoroutine(AllDone());
	}

	IEnumerator AllDone() {
		AudioSource audioSource = GetComponent<AudioSource>();
		yield return new WaitForSeconds(audioSource.clip.length);
		audioSource.clip = allDone;
		audioSource.Play();
		allPotionsCrafted.SetActive(true);
	}
}
