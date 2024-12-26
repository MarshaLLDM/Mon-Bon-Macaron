using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu()]
public class RecipeSO : ScriptableObject
{
    //Список ингнидиентов

    public List<KitchenObject> _kitchenObjects;
    public string recipeName;
}
