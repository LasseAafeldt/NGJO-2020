using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
/*
 Jônatas Dourado Porto
     */
public class Inventory : MonoBehaviour
{

    #region Singleton
    public static Inventory instance;

    void Awake()
    {
        if(instance != null)
        {
            Debug.LogWarning("Dude you mess up! There is more than one inventory instance.");
        }
        instance = this;
    }
    #endregion

    private int maxSize;
    public int row = 2;
    public int column = 3;
    public GameObject SlotPrefab;
    public List<Item> itemList;
    

    void Start()
    {
        itemList = new List<Item>();
        maxSize = row * column;
    }
    public string add(Item item)
    {
        //if the list is full, return
        if(itemList.Count == maxSize)
        {
            return "Inventory is full!";
        }
        // if is not full, add in the list and return
        //check if alredy exist tha item
        Item it = get(item.name);
        if(it == null)
        {
            GameObject g = Instantiate(SlotPrefab);
            g.GetComponent<Image>().sprite = item.sprite;
            Item i = g.AddComponent<Item>();
            i.constructor(item);
            itemList.Add(i);
            Destroy(item.gameObject);
            g.transform.parent = transform;
        }
        else
        {
            it.amount += item.amount;
        }
        
        return itemList[itemList.Count - 1].name + " was added to the inventory!";

    }
    private void remove(string itemName)
    {
        foreach (Item i in itemList)
        {
            if (i.name == itemName)
            {  
                Debug.Log(i.name + "was removed from inventory!");
                if(i.amount == 1)
                {
                    itemList.Remove(i);
                    Destroy(i.gameObject);
                }
                else
                {
                    i.amount--;
                }
                
            }
        }
        
    }
    public Item get(string itemName)
    {
        foreach (Item i in itemList)
        {
            if (i.name == itemName)
            {
                return i;
            }
        }
        return null;
    }
    public bool hasCompletedPotion(string[] names)
    {
        foreach (Item i in itemList)
        {
            bool find = false;
            for (int j = 0; j < names.Length; j++)
            {
                if (i.name == names[j])
                {
                    find = true;
                    break;
                }
            }
            if (!find) return false;
        }
        for (int j = 0; j < names.Length; j++)
        {
            remove(names[j]);
        }
        return true;
    }
}
