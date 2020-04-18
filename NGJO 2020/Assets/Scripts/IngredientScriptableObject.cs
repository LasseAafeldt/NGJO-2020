using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New ingredient", menuName = "Create New Ingredient")]
public class IngredientScriptableObject : ScriptableObject
{
    public string _name = "New Ingredient";
    [TextArea]
    public string description;
    public Sprite sprite;
}
