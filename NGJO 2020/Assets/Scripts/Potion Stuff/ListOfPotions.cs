using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ListOfPotions : MonoBehaviour {
	static ListOfPotions _instance;
	public static ListOfPotions instance {
		get { return _instance; }
	}


	public PotionScriptableObject charismaPotion;
	public PotionScriptableObject dexterityPotion;
	public PotionScriptableObject staminaPotion;
	public PotionScriptableObject strengthPotion;

	private PotionScriptableObject potionToCraft;

	public List<Image> ingredientsSprite;
	public List<TMP_Text> ingredientsText;

	private void Awake() {
		if (instance == null) {
			_instance = this;
			//DontDestroyOnLoad(this);
		} else {
			Destroy(this.gameObject);
		}
	}

	public PotionScriptableObject matchPotionWithSelected(PotionScriptableObject selectedPotion) {
		if (selectedPotion.Equals(charismaPotion)) {
			return charismaPotion;
		}

		return null;
	}

	public void SetPotionToCraft(PotionScriptableObject potion) {
		potionToCraft = potion;
		for (int i = 0; i < potion.ingredients.Count; i++) {
			ingredientsSprite[i].gameObject.SetActive(true);
			ingredientsSprite[i].sprite = potion.ingredients[i].sprite;
			ingredientsText[i].text = Inventory.instance.GetAmount(potion.ingredients[i]._name, potion.amountOfIngredients[i]);
		}
	}

	public PotionScriptableObject GetPotionToCraft() {
		return potionToCraft;
	}
}
