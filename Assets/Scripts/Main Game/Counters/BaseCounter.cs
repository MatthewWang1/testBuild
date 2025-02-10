using System;
using UnityEngine;

public class BaseCounter : MonoBehaviour, IFabObjectParent
{

    [SerializeField] private Transform counterTopPoint;
    
    private FabObject fabObject;

    public static event EventHandler OnAnyObjectPlacedHere;

    public static void ResetStaticData()
    {
        OnAnyObjectPlacedHere = null;
    }

    public virtual void Interact(Player player)
    {
        // Debug.Log("BaseCounter.Interact should not be triggered");
    }

    public virtual void InteractAlternate(Player player)
    {
        // Debug.Log("BaseCounter.Interact should not be triggered");
    }

    
    public Transform GetFabObjectFollowTransform()
    {
        return counterTopPoint;
    }

    public void SetFabObject(FabObject fabObject)
    {
        this.fabObject = fabObject;
        
        if(fabObject != null)
        {
            OnAnyObjectPlacedHere?.Invoke(this, EventArgs.Empty);
        }
    }

    public FabObject GetFabObject()
    {
        return fabObject;
    }

    public void ClearFabObject()
    {
        fabObject = null;
    }

    public bool HasFabObject()
    {
        return fabObject != null;
    }
}
