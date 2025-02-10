using UnityEngine;

public class ClearCounter : BaseCounter
{

    [SerializeField] private FabObjectSO fabObjectSO;

    
    public override void Interact(Player player)
    {
        // Debug.Log("interacted");
        if(!HasFabObject())
        {
            // Debug.Log("empty");
            if(player.HasFabObject())
            {
                // Debug.Log("try take player obj");
                player.GetFabObject().SetFabObjectParent(this);
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

    


}
