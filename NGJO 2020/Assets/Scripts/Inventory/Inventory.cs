using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
/*
 Jônatas Dourado Porto
     */
public class Inventory : MonoBehaviour {

	#region Singleton
	public static Inventory instance;

	void Awake() {
		if (instance != null) {
			Debug.LogWarning("Dude you mess up! There is more than one inventory instance.");
		}
		if (this != Inventory.instance && Inventory.instance != null) {
			Destroy(this.gameObject);
			return;
		}
		instance = this;
		DontDestroyOnLoad(this.gameObject);
	}
	#endregion

	//public int maxSize;
	public List<Item> itemList;

	public Dictionary<Item, int> itemsToCollect = new Dictionary<Item, int>();

	void Start() {
		itemList = new List<Item>();
		refreshUI();
	}

	public void refreshUI() {
		GameObject[] g = GameObject.FindGameObjectsWithTag("ReagentUI");
		foreach (GameObject i in g) {
			bool done = false;
			foreach (Item item in itemList) {
				if (i.name == item.ingredient._name) {
					if (!itemsToCollect.ContainsKey(item)) {
						itemsToCollect.Add(item, 0);
					}
					i.GetComponent<TextMeshProUGUI>().text = item.amount + "/" + itemsToCollect[item];
					done = true;
					break;
				}
			}
			if (!done) {
				foreach (GameObject go in FindObjectOfType<SpawnItemsInAvailiblePosition>().itemsToSpawn) {
					Item item = go.GetComponent<Item>();
					if (i.name == item.ingredient._name) {
						if (!itemsToCollect.ContainsKey(item)) {
							itemsToCollect.Add(item, 0);
						}
						i.GetComponent<TextMeshProUGUI>().text = "0/" + itemsToCollect[item];
					}
				}
			}
		}
	}
	public string add(Item item) {
		//if the list is full, return
		/*
        if(itemList.Count == maxSize)
        {
            return "Inventory is full!";
        }*/
		// if is not full, add in the list and return
		//check if alredy exist tha item
		Item it = get(item.ingredient._name);
		GameObject[] g = GameObject.FindGameObjectsWithTag("ReagentUI");
		if (it == null) {
			//GameObject g = Instantiate(SlotPrefab);
			//g.GetComponent<Image>().sprite = item.sprite;
			//Item i = g.AddComponent<Item>();
			//i.constructor(item);
			itemList.Add(item);
			item.gameObject.GetComponent<SpriteRenderer>().sprite = null;
			item.gameObject.transform.parent = gameObject.transform;


			refreshUI();

			//g.transform.parent = transform;
		} else {
			it.amount += item.amount;
			Destroy(item.gameObject);
			foreach (GameObject i in g) {
				Debug.Log(i.name + "  " + it.ingredient._name);
				if (i.name == it.ingredient._name) {
					i.GetComponent<TextMeshProUGUI>().text = it.amount + "/1";
				}
			}
		}

		return itemList[itemList.Count - 1].ingredient._name + " was added to the inventory!";

	}
	private void remove(string itemName) {
		foreach (Item i in itemList) {
			if (i.ingredient._name == itemName) {
				Debug.Log(i.ingredient._name + "was removed from inventory!");
				if (i.amount == 1) {
					itemList.Remove(i);
					Destroy(i.gameObject);
				} else {
					i.amount--;
				}

			}
		}

	}
	public Item get(string itemName) {
		foreach (Item i in itemList) {
			if (i.ingredient._name == itemName) {
				return i;
			}
		}
		return null;
	}
	public bool hasCompletedPotion(string[] names) {
		foreach (Item i in itemList) {
			bool find = false;
			for (int j = 0; j < names.Length; j++) {
				if (i.ingredient._name == names[j]) {
					find = true;
					break;
				}
			}
			if (!find) return false;
		}
		for (int j = 0; j < names.Length; j++) {
			remove(names[j]);
		}
		return true;
	}

	public void RemoveIngredient(IngredientScriptableObject ingredient) {
		foreach (Item i in itemList) {
			if (ingredient == i) {
				Debug.Log(i.ingredient._name + "was removed from inventory!");
				if (i.amount == 1) {
					itemList.Remove(i);
					Destroy(i.gameObject);
				} else {
					i.amount--;
				}
			}
		}
	}
	public bool CheckIngredients(PotionScriptableObject potion) {
		List<IngredientScriptableObject> requiredIngredients = new List<IngredientScriptableObject>(potion.ingredients);
		List<int> ingredientAmount = new List<int>(potion.amountOfIngredients);

		for (int i = 0; i < requiredIngredients.Count; i++) {
			foreach (Item it in itemList) {
				if (requiredIngredients[i] == it.ingredient) {
					if (it.amount > ingredientAmount[i]) {
						continue;
					} else {
						return false;
					}
				}
			}
			return true;
		}
		Debug.Log("If inventory got to here somthing is probably wrong in the code!");
		return false;
	}
}
