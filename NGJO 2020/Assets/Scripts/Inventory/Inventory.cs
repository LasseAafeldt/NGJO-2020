using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
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

	public int maxPotions = 3;
	public List<PotionScriptableObject> potionsToCraft;
	public List<Item> itemList;
	public Dictionary<string, int> itemsToCollect = new Dictionary<string, int>();
	[Space]
	public GameObject allCollectedCanvas;

	void Start() {
		itemList = new List<Item>();

		foreach (PotionScriptableObject p in potionsToCraft) {
			p.amountOfPotion = 0;
		}
		while (maxPotions > 0) {
			foreach (PotionScriptableObject p in potionsToCraft) {
				if (p.amountOfPotion == 0) {
					p.amountOfPotion = Random.Range(0, 2);
					if (p.amountOfPotion > 0) {
						foreach (IngredientScriptableObject i in p.ingredients) {
							if (!itemsToCollect.ContainsKey(i._name)) {
								itemsToCollect.Add(i._name, 1);
							} else {
								itemsToCollect[i._name]++;
							}
						}
						maxPotions -= p.amountOfPotion;
						if (maxPotions <= 0) {
							break;
						}
					}
				}
			}
		}

		refreshUI();
	}

	bool allItemsCollected;
	public void refreshUI() {
		allItemsCollected = true;
		GameObject[] g = GameObject.FindGameObjectsWithTag("ReagentUI");
		if (g.Length == 0) {
			return;
		}
		foreach (GameObject i in g) {
			bool done = false;
			foreach (Item item in itemList) {
				if (i.name == item.ingredient._name) {
					WriteAmount(i, item, item.amount);
					done = true;
					break;
				}
			}
			if (!done) {
				foreach (GameObject go in FindObjectOfType<SpawnItemsInAvailiblePosition>().itemsToSpawn) {
					Item item = go.GetComponent<Item>();
					if (i.name == item.ingredient._name) {
						WriteAmount(i, item, 0);
					}
				}
			}
		}
		if (allItemsCollected) {
			allCollectedCanvas.SetActive(true);
			StartCoroutine(LoadCraft());
		}
	}
	void WriteAmount(GameObject i, Item item, int amount) {
		if (!itemsToCollect.ContainsKey(item.ingredient._name)) {
			itemsToCollect.Add(item.ingredient._name, 0);
		}
		if (itemsToCollect[item.ingredient._name] > 0) {
			if (amount >= itemsToCollect[item.ingredient._name]) {
				i.GetComponent<TextMeshProUGUI>().text = "<color=\"green\">";
			} else {
				i.GetComponent<TextMeshProUGUI>().text = "<color=\"red\">";
				allItemsCollected = false;
			}
		} else {
			i.GetComponent<TextMeshProUGUI>().text = "";
		}
		i.GetComponent<TextMeshProUGUI>().text += amount + "/" + itemsToCollect[item.ingredient._name];
	}
	IEnumerator LoadCraft() {
		yield return new WaitForSeconds(2);
		SceneManager.LoadScene(1);
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
			if (ingredient == i.ingredient) {
				if (i.amount == 1) {
					itemList.Remove(i);
					//Destroy(i.gameObject);
				} else {
					i.amount--;
				}
				Debug.Log(i.ingredient._name + "was removed from inventory!");
			}
		}
	}
	public bool CheckIngredients(PotionScriptableObject potion) {
		List<IngredientScriptableObject> requiredIngredients = new List<IngredientScriptableObject>(potion.ingredients);
		List<int> ingredientAmount = new List<int>(potion.amountOfIngredients);
		//List<int> ingredientAmount = new List<int>();

		for (int i = 0; i < requiredIngredients.Count; i++) {
			//int value;
			//ingredientAmount.Add(itemsToCollect.(requiredIngredients[i]._name));
			//Debug.Log("amount ingredient required: " + ingredientAmount[i] + " of " + requiredIngredients[i].name);

			foreach (Item it in itemList) {
				if (requiredIngredients[i] == it.ingredient) {
					if (it.amount >= ingredientAmount[i]) {
						continue;
					} else {
						Debug.Log("No enough ingredients for " + potion._name);
						return false;
					}
				}
			}
			Debug.Log("Missing ingredient for " + potion._name);
			return false;
		}
		//Debug.Log("If inventory got to here somthing is probably wrong in the code!");
		return true;

	}




	public string GetAmount(string ingredient, int requiredAmount) {
		string s;
		Item i = get(ingredient);
		if (i) {
			s = i.amount.ToString();
		} else {
			s = "0";
		}
		s += "/";
		if (!itemsToCollect.ContainsKey(ingredient)) {
			itemsToCollect.Add(ingredient, 0);
		}
		s += requiredAmount.ToString();
		return s;
	}
}
