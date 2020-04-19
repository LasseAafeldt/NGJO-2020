using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DisplayPotionOrder : MonoBehaviour
{
    public PotionScriptableObject potionOrder;

    [SerializeField] private string[] order;
    [SerializeField] private string[] reason;

    [SerializeField] private TextMeshProUGUI text;


    private void Start()
    {
        DisplayOrder();
    }

    void DisplayOrder()
    {
        int index = Random.Range(1, Inventory.instance.potionsOrdered.Count) - 1;
        if (index >= 0)
        {
            potionOrder = Inventory.instance.potionsOrdered[index];
            Inventory.instance.potionsOrdered.Remove(Inventory.instance.potionsOrdered[index]);
        }
        else { return; }

        string _order = SelectOrderPhrasing();
        string _reason = SelectReasonPhrasing();

        text.text = _order + " " + potionOrder._name + " " + "<sprite name="+potionOrder._name+"> " + " " + _reason;
    }

    string SelectOrderPhrasing()
    {
        int index = Random.Range(0, order.Length - 1);
        return order[index];
    }

    string SelectReasonPhrasing()
    {
        int index = Random.Range(0, reason.Length - 1);
        return reason[index];
    }
}
