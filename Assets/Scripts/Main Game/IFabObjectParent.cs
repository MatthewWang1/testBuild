using UnityEngine;

public interface IFabObjectParent
{
    public Transform GetFabObjectFollowTransform();
    
    public void SetFabObject(FabObject fabObject);
    
    public FabObject GetFabObject();
    
    public void ClearFabObject();
    
    public bool HasFabObject();


}
