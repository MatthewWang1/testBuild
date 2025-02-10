using UnityEngine;

public class DepositionCounter : BaseCounter
{

    [SerializeField] private CleaningRecipeSO[] cleaningRecipeSOArray ;
    [SerializeField] private GameObject depositionMiniGame;

    public override void Interact(Player player)
    {
        if(!HasFabObject())
        {
            if(player.HasFabObject())
            {
                if(HasRecipeWithInput(player.GetFabObject().GetFabObjectSO()))
                {
                    player.GetFabObject().SetFabObjectParent(this); // take it
                }
            }
            else
            {
                
            }
        }
        else
        {
            if(player.HasFabObject())
            {
                
            }
            else
            {
                GetFabObject().SetFabObjectParent(player);
            }
        }
    }

    private bool HasRecipeWithInput(FabObjectSO inputCleaningObjectSO)
    {
        CleaningRecipeSO cleaningRecipeSO = GetCleaningRecipeSOWithInput(inputCleaningObjectSO);
        return cleaningRecipeSO != null;
    } 

    public override void InteractAlternate(Player player)
    {
        if(HasFabObject() && HasRecipeWithInput(GetFabObject().GetFabObjectSO()))
        {
            // do etching minigame
            // if complete minigame do the next 3 lines to output next wafer
            GameInputs.Instance.DisablePlayerActions();
            GameObject minigame = Instantiate(depositionMiniGame, Vector3.zero, Quaternion.identity);
            minigame.SetActive(true);

            FabObjectSO outputFabObjectSO = GetOutputForInput(GetFabObject().GetFabObjectSO());
            GetFabObject().DestroySelf();
            FabObject.SpawnFabObject(outputFabObjectSO, this);

        }
    }

    private FabObjectSO GetOutputForInput(FabObjectSO inputCleaningObjectSO)
    {

        foreach (CleaningRecipeSO cleaningRecipeSO in cleaningRecipeSOArray)
        {
            if(cleaningRecipeSO.input == inputCleaningObjectSO )
            {
                return cleaningRecipeSO.output;
            }
        }
        return null;
    }

    private CleaningRecipeSO GetCleaningRecipeSOWithInput(FabObjectSO inputFabObjectSO)
    {
        foreach (CleaningRecipeSO cleaningRecipeSO in cleaningRecipeSOArray)
        {
            if(cleaningRecipeSO.input == inputFabObjectSO )
            {
                return cleaningRecipeSO;
            }
        }
        return null;
    }

}
