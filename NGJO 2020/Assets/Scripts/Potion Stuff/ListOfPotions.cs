using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ListOfPotions : MonoBehaviour
{
    static ListOfPotions _instance;
    public static ListOfPotions instance
    {
        get { return _instance; }
    }


    public PotionScriptableObject charismaPotion;
    //some other potions

    private PotionScriptableObject potionToCraft;

    private void Awake()
    {
        if(instance == null)
        {
            _instance = this;
            DontDestroyOnLoad(this);
        }
        else
        {
            Destroy(this.gameObject);
        }
    }

    public PotionScriptableObject matchPotionWithSelected(PotionScriptableObject selectedPotion)
    {
        if (selectedPotion.Equals(charismaPotion))
        {
            return charismaPotion;
        }

        return null;
    }

    public void SetPotionToCraft(PotionScriptableObject potion)
    {
        potionToCraft = potion;
    }
}
