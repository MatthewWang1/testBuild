using UnityEngine;

public class SafeZonePosition : MonoBehaviour
{
    public Transform leftBoundary;
    public Transform rightBoundary;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        MoveSafeZone();
    }

    public void MoveSafeZone()
    {
        transform.position = new Vector3(Random.Range(leftBoundary.position.x, rightBoundary.position.x), transform.position.y, transform.position.z);

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
