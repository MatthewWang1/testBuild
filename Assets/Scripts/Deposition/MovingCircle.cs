using UnityEngine;

public class MovingCircle : MonoBehaviour
{
    public RectTransform barBackground; // The background bar reference
    [SerializeField] private float speed;
    private float minX, maxX;
    private bool movingRight = true;
    private bool compelete;

    void Start()
    {
        compelete = false;
        // Get the min and max X positions within the bar
        float barWidth = barBackground.rect.width;
        minX = -barWidth / 2f + GetComponent<RectTransform>().rect.width/2f;
        maxX = barWidth / 2f  - GetComponent<RectTransform>().rect.width/2f;
        RectTransform rt = GetComponent<RectTransform>();
        rt.anchoredPosition = new Vector2(Random.Range(minX, maxX), rt.anchoredPosition.y);
    }

    void Update()
    {
        if(!compelete)
        {
            // Move left and right
            float moveAmount = speed * Time.deltaTime * (movingRight ? 1 : -1);
            transform.localPosition += new Vector3(moveAmount, 0, 0);

            // Reverse direction when hitting edges
            if (transform.localPosition.x >= maxX)
                movingRight = false;
            else if (transform.localPosition.x <= minX)
                movingRight = true;
        }
    }

    public void SetComplete(bool status)
    {
        compelete = status;
    }
}
