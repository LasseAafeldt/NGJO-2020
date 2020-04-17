using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 Jônatas Dourado Porto
     */
public class ItemInteractable : MonoBehaviour
{
    public Item item;
    void Start()
    {
        item = GetComponent<Item>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        //if the player hit the item
        if (collision.gameObject.CompareTag("Player"))
        {
            Inventory.instance.add(item);
            this.gameObject.GetComponent<SpriteRenderer>().sprite = null;
            Destroy(this);
        }
    }
}
