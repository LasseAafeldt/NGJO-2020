﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PotionScript : MonoBehaviour {
	[SerializeField] private PotionScriptableObject potion;

	Image potionSprite;
	Text amountText;

	int amountOfPotion;

	private void Awake() {
		potionSprite = GetComponent<Image>();
		amountText = GetComponentInChildren<Text>();
	}

	private void Start() {
		potionSprite.sprite = potion.sprite;

		updateDisplayPotionAmount();
	}

	private void Update() {
		updateDisplayPotionAmount();
	}

	void updateDisplayPotionAmount() {
		amountOfPotion = potion.amountOfPotion;
		//updating text
		string amount = "x" + amountOfPotion;
		amountText.text = amount;
	}

	public void SelectPotionToCraft() {
		PotionScriptableObject potionToCraft;
		potionToCraft = ListOfPotions.instance.matchPotionWithSelected(potion);
		ListOfPotions.instance.SetPotionToCraft(potion);
	}
}
