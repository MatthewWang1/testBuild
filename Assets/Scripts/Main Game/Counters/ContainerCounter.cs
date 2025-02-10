using System;
using UnityEngine;

public class ContainerCounter : BaseCounter
{

    [SerializeField] private FabObjectSO fabObjectSO;
    
    public event EventHandler OnPlayerGrabbedObject;

    public override void Interact(Player player)
    {
        if(!player.HasFabObject())
        {
            FabObject.SpawnFabObject(fabObjectSO, player);
            
            OnPlayerGrabbedObject?.Invoke(this, EventArgs.Empty);
        }
    }
 

}
