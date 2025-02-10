using UnityEngine;

public class RemoveColliders : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Collider[] colliders = this.GetComponentsInChildren<Collider>();
        Debug.Log("Number of colliders: " + colliders.Length);
        
        foreach(var c in colliders)
        {
            c.enabled = false;
            Renderer renderer = c.GetComponent<Renderer>(); // Get the Renderer component

            if (renderer != null)
            {
                renderer.shadowCastingMode = UnityEngine.Rendering.ShadowCastingMode.Off; // Disable casting shadows
                // Debug.Log("Disabled shadows for: " + renderer.gameObject.name);
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
