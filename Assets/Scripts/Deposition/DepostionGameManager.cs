using UnityEngine;

public class DepostionGameManager : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created

    [SerializeField] GameObject[] bars;
    private bool complete;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        complete = true;

        foreach(GameObject bar in bars)
        {
            if(!bar.GetComponent<BarClick>().IsComplete())
            {
                complete = false;
            }
        }

        if(complete)
        {
            // Debug.Log("WWWOOO");
            GameInputs.Instance.EnablePlayerActions();
            Destroy(gameObject, 0.1f);
        }
    }
}
