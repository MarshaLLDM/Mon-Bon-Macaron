using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu()]
public class BlenderRecipeSO : ScriptableObject
{

    public KitchenObject input;
    public KitchenObject input_two;
    public KitchenObject output;
    public float _blenderTimerMax;
}
