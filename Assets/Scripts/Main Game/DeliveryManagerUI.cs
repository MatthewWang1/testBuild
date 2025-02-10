using System;
using UnityEngine;

public class DeliveryManagerUI : MonoBehaviour
{
    [SerializeField] private Transform container;
    [SerializeField] private Transform recipeTemplate;
    
    private void Awake()
    {
        recipeTemplate.gameObject.SetActive(false);
    }

    private void Start()
    {
        DeliveryManager.Instance.onRecipeSpawned += Instance_onRecipeSpawned;
        DeliveryManager.Instance.onRecipeCompleted += Instance_onRecipeCompleted;
        UpdateVisual();
    }

    private void Instance_onRecipeSpawned(object sender, EventArgs e)
    {
        UpdateVisual();
    }

    private void Instance_onRecipeCompleted(object sender, EventArgs e)
    {
        UpdateVisual();
    }

    private void UpdateVisual()
    {
        foreach (Transform child in container)
        {
            if(child == recipeTemplate) continue;
            Destroy(child.gameObject);
        }

        foreach(RecipeSO recipeSO in DeliveryManager.Instance.GetWaitingRecipeSOList())
        {
            Transform recipeTransform = Instantiate(recipeTemplate, container);
            recipeTransform.gameObject.SetActive(true);
            recipeTransform.GetComponent<DeliverManagerSingleUI>().SetRecipeSO(recipeSO);

        }
    }

}
