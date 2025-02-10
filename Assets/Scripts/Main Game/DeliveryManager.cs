using System;
using System.Collections.Generic;
using UnityEngine;

public class DeliveryManager : MonoBehaviour
{

    public static DeliveryManager Instance {get; private set;}
    public event EventHandler onRecipeSpawned;
    public event EventHandler onRecipeCompleted;
    public event EventHandler onRecipeSuccess;
    public event EventHandler onRecipeFail;
    
    

    [SerializeField] private RecipeListSO recipeListSO;

    private List<RecipeSO> waitingRecipeSOList;

    private float spawnRecipeTimer;
    private float spawnRecipeTimerMax = 4f;
    private int waitingRecipesMax = 4;
    private int successfulRecipes = 0;
    private int score = 0;

    private void Awake()
    {
        Instance = this;
        waitingRecipeSOList = new List<RecipeSO>();
    }

    private void Update()
    {
        spawnRecipeTimer -= Time.deltaTime;
        if(spawnRecipeTimer <= 0f)
        {
            spawnRecipeTimer = spawnRecipeTimerMax;
            
            if(waitingRecipeSOList.Count < waitingRecipesMax)
            {
                RecipeSO waitingRecipeSO = recipeListSO.recipeSOList[UnityEngine.Random.Range(0,recipeListSO.recipeSOList.Count)];
                waitingRecipeSOList.Add(waitingRecipeSO);
                // Debug.Log(waitingRecipeSO.recipeName);

                onRecipeSpawned?.Invoke(this, EventArgs.Empty);
            }
        }
    }

    public void DeliverRecipe(FabObjectSO fabObjectSO)
    {
        for(int i = 0; i<waitingRecipeSOList.Count; i++)
        {
            RecipeSO waitingRecipeSO = waitingRecipeSOList[i];
            bool ingredientFound = false;
            foreach(FabObjectSO waitingFabObjectSO in waitingRecipeSO.fabObjectSOList)
            {
                // add another for each if each delieverable is more than 1 fabObjectSO
                if(fabObjectSO == waitingFabObjectSO) 
                {
                    ingredientFound = true;
                    break;
                }
            }
            if(ingredientFound)
            {
                successfulRecipes++;
                score += waitingRecipeSO.score;
                // Debug.Log("delievered correct");
                waitingRecipeSOList.RemoveAt(i);
                
                onRecipeCompleted?.Invoke(this,EventArgs.Empty);
                onRecipeSuccess?.Invoke(this,EventArgs.Empty);
                return;
            }
        }

        // no matches
        // Debug.Log("no correct delivery");
        onRecipeFail?.Invoke(this,EventArgs.Empty);
    }

    public List<RecipeSO> GetWaitingRecipeSOList()
    {
        return waitingRecipeSOList;
    }

    public int GetSuccessfulRecipes()
    {
        return successfulRecipes;
    }

    public int GetScore()
    {
        return score;
    }
}
