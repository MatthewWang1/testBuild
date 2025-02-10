using JetBrains.Annotations;
using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class PointerMovement : MonoBehaviour
{
    public float speed = 500;
    public Transform leftBoundary;
    public Transform rightBoundary;
    public WaferRadialAnimation waferRadialAnimation;
    public SafeZonePosition safeZone;
    private bool movingRight;
    private bool canClick = true;
    private bool inSafeZone;
    public Text completeText;
    private bool isGamePaused = false;
    public Image flashImage;
    public float flashDuration = 0.5f;
    [SerializeField] private GameObject topLevel;
    // private bool isFlashing = false;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        transform.position = new Vector3(0, transform.position.y, transform.position.z);
        movingRight = true;
        completeText.text = "";
        if(flashImage != null)
        {
            flashImage.color = new Color(1, 0, 0, 0);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (isGamePaused)
        {
            return;
        }

        if (movingRight && canClick)
        {
            transform.position = transform.position + (Vector3.right * speed) * Time.deltaTime;
        }
        else if (!movingRight && canClick)
        {
            transform.position = transform.position + (Vector3.left * speed) * Time.deltaTime;
        }

        if(!inSafeZone && Input.GetMouseButtonDown(0) && canClick)
        {
            canClick = false;
            StartCoroutine(FlashandMove());
        }

        if(inSafeZone && Input.GetMouseButtonDown(0))
        {
            if(completeText != null)
            {
                StartCoroutine(waferRadialAnimation.FadeOut());
                completeText.text = "Process Complete!";
                GameInputs.Instance.EnablePlayerActions();
                Destroy(topLevel, 0.8f);
            }

            isGamePaused = true;
        }
    }
    void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Slider"))
        {
            movingRight = !movingRight;
        }
        if(other.CompareTag("SafeZone"))
        {
            inSafeZone = false;
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("SafeZone"))
        {
            inSafeZone = true;
        }
    }

    IEnumerator FlashandMove()
    {
        if(!canClick)
        {
        //    isFlashing = true;
           flashImage.color = new Color(1, 0, 0, 0.5f);
           yield return new WaitForSeconds(0.5f);
           flashImage.color = new Color(1, 0, 0, 0);
           safeZone.MoveSafeZone();
        //    isFlashing = false;
            canClick = true;
        }
    }
}