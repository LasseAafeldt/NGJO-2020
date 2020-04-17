using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item:MonoBehaviour
{
    public Sprite sprite;
    public string name;
    public string description;
    public int amount = 1;
    public void constructor(Item item)
    {
        this.sprite = item.sprite;
        this.name = item.name;
        this.description = item.description;
    }
}
