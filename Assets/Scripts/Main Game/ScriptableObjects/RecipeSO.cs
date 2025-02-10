using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu()]
public class RecipeSO : ScriptableObject
{
    public List<FabObjectSO> fabObjectSOList;
    public string recipeName;
    public int score;
}
