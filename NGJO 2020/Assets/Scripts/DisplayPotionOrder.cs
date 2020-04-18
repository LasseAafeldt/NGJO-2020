using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DisplayPotionOrder : MonoBehaviour
{
    public string potionOrder = "Default Potion"; //change when we know how potions are done

    [SerializeField] private string[] order;
    [SerializeField] private string[] reason;

    [SerializeField] private TextMeshProUGUI text;

    private void Start()
    {
        DisplayOrder();
    }

    void DisplayOrder()
    {
        potionOrder = "Default Potion"; //do some kind of get potion thing when we know how potions are done

        string _order = SelectOrderPhrasing();
        string _reason = SelectReasonPhrasing();

        text.text = _order + " " + potionOrder + " " + _reason;
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
