using UnityEngine;

public class WaferController : MonoBehaviour
{
    public float rotationSpeed = 50;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(0, 0,  rotationSpeed * Time.deltaTime);
    }
}
