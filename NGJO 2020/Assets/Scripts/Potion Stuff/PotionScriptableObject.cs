using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName ="New Potion", menuName = "Create new Potion")]
public class PotionScriptableObject : ScriptableObject
{
    public string _name = "New Potion";
    [TextArea]
    public string description;
    public Sprite sprite;

    public List<IngredientScriptableObject> ingredients;
    public List<int> amountOfIngredients;

    public int amountOfPotion;

}
