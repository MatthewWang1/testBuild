using UnityEngine;

public class Growth : MonoBehaviour
{
    private Vector3 origHeight;
    private Vector3 origPositionY;
    public bool isGrowing = false;
    private float growthSpeed = 0.8f;

    private float maxHeight = 8.0f;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        origPositionY = transform.position;
        origHeight    = transform.localScale;
        Reset();
    }

    // Update is called once per frame
    void Update()
    {
        isGrowing = isGrowing && Clickable.isInputEnabled;
        Vector3 currentScale = transform.localScale;
        // Debug.Log(gameObject.name + currentScale);
        if (isGrowing && currentScale.y < maxHeight)
        {
            // Debug.Log("growing");
            float heightIncrease = growthSpeed * Time.deltaTime;
            transform.localScale += Vector3.up*heightIncrease;
            transform.position   += Vector3.up*heightIncrease*50;
        }
    }

    public void Reset()
    {
        isGrowing = false;
        transform.localScale   = origHeight;
        transform.position     = origPositionY;
    }

    public float Height()
    {
        return GetComponent<RectTransform>().anchoredPosition.y;
    }

    public double Top()
    {
        return transform.position.y + transform.localScale.y / 2.0;
    }
}
