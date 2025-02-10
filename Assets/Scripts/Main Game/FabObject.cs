using UnityEngine;

public class FabObject : MonoBehaviour
{
    [SerializeField] private FabObjectSO fabObjectSO;
    
    private IFabObjectParent fabObjectParent;
    
    public FabObjectSO GetFabObjectSO()
    {
        return fabObjectSO;
    }
    
    public void SetFabObjectParent(IFabObjectParent fabObjectParent)
    {
        if(this.fabObjectParent != null)
        {
            this.fabObjectParent.ClearFabObject();
        }

        this.fabObjectParent = fabObjectParent;
        if(fabObjectParent.HasFabObject()) {Debug.Log("ERROR object already has object");}
        fabObjectParent.SetFabObject(this);

        transform.parent = fabObjectParent.GetFabObjectFollowTransform();
        transform.localPosition = Vector3.zero;
    }
    
    public IFabObjectParent GetFabObjectParent()
    {
        return fabObjectParent;
    }

    public void DestroySelf()
    {
        fabObjectParent.ClearFabObject();
        Destroy(gameObject);
    }

    public static FabObject SpawnFabObject(FabObjectSO fabObjectSO, IFabObjectParent fabObjectParent)
    {
        Transform fabObjectTransform = Instantiate(fabObjectSO.prefab);
        FabObject fabObject = fabObjectTransform.GetComponent<FabObject>();
        
        fabObject.SetFabObjectParent(fabObjectParent);

        return fabObject;
    }

}
