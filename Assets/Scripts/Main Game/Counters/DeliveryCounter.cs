using UnityEngine;

public class DeliveryCounter : BaseCounter
{

    public static DeliveryCounter Instance {get; private set;}

    private void Awake()
    {
        Instance = this;
    }

    public override void Interact(Player player)
    {
        // add more checks for correct thing (not checking for have plate)
        if(player.HasFabObject())
        {
            FabObjectSO deliveredFabObjectSO = player.GetFabObject().GetFabObjectSO();
            if(deliveredFabObjectSO != null)
            {
                DeliveryManager.Instance.DeliverRecipe(deliveredFabObjectSO);
                player.GetFabObject().DestroySelf();
            }
        }
    }


}
