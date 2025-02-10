using UnityEngine;

public class MetalizationGameManager : MonoBehaviour
{

    [SerializeField] private GameObject etchedWafer;
    [SerializeField] private EraseEffect eraseManager;
    

    public void MetalizeEtchedWafer()
    {
        etchedWafer.SetActive(false);
        eraseManager.LiftoffEnable();
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        etchedWafer.SetActive(true);
    }


    private void Reset()
    {
        etchedWafer.SetActive(true);
    }

    // Update is called once per frame
    void Update()
    {
        if(eraseManager.IsComplete())
        {
            GameInputs.Instance.EnablePlayerActions();
            Destroy(gameObject, 0.1f);
        }
    }
}
