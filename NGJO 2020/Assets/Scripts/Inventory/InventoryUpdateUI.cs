using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryUpdateUI : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        Inventory.instance.refreshUI();
    }

    // Update is called once per frame
}
