using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.Linq;
using Unity.VisualScripting.Antlr3.Runtime;
using UnityEngine.Assertions.Must;

public class Clickable : MonoBehaviour, IPointerClickHandler
{
    public static bool isInputEnabled = true;
    private readonly int max = 3;
    private int current = 0;
    public GameObject[] circles;
    private Color redColor = Color.red;
    private Color greenColor = Color.green;

    [SerializeField] private GameObject topLevel;
    [SerializeField] private GameObject oxidationGameManager;
    
    void Start()
    {
        isInputEnabled = true;
        foreach (GameObject circle in circles)
        {
            if (circle.transform.TryGetComponent<Image>(out var spriteRenderer))
            {
                spriteRenderer.color = redColor;
            }
        }
    }

    void Update()
    {
        if (!isInputEnabled)
        {
            // Debug.Log("hi");
            float max_height = 172;
            float min_height = 118;
            bool success = true;
            foreach (GameObject circle in circles)
            {
                var growth = circle.transform.GetComponent<Growth>();
                double h = growth.Height();
                if (h > max_height || h < min_height)
                {
                    // Debug.Log(circle.name + " "+ h+" fail");
                    success = false;
                }
            }

            if(success)
            {
                // Debug.Log("win");
                GameInputs.Instance.EnablePlayerActions();
                Destroy(topLevel);
            }
            else
            {
                // Debug.Log("lose");
                var gm = oxidationGameManager.GetComponent<OxidationGameManager>();
                gm.Reset();
                Reset();
            }

            // foreach (GameObject circle in circles)
            // {
            //     var spriteRenderer = circle.GetComponent<Image>();
            //     Debug.Log(circle.transform.name + spriteRenderer);
            //     if (finished && max_height - min_height < 1.5)
            //     {
            //         spriteRenderer.color = greenColor;
            //     }
            //     else
            //     {
            //         spriteRenderer.color = redColor;
            //         // SceneManager.LoadScene("Intro");
            //     }
            // }

        }
    }

    public void Reset()
    {
        isInputEnabled = true;
        current = 0;
        foreach (GameObject circle in circles)
        {
            if (circle.transform.TryGetComponent<Image>(out var spriteRenderer))
            {
                spriteRenderer.color = redColor;
            }
            var growth = circle.transform.GetComponent<Growth>();
            growth.Reset();
        }
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        // Get the exact GameObject that was clicked
        GameObject clickedObject = eventData.pointerCurrentRaycast.gameObject;
        // Debug.Log("clicked");
        if (clickedObject != null && circles.Contains(clickedObject))
        {
            ToggleColor(clickedObject.transform);
        }
    }

    void ToggleColor(Transform circle)
    {
        // Debug.Log("toggle"+circle.name);
        // Get the SpriteRenderer of the clicked circle
        var spriteRenderer = circle.GetComponent<Image>();
        Growth growth = circle.GetComponent<Growth>();
        if (spriteRenderer != null)
        {
            // Debug.Log("afsdguhsdjgjsagd");
            // Toggle between red and green
            if (spriteRenderer.color == redColor && current < max)
            {
                growth.isGrowing = true;
                spriteRenderer.color = greenColor;
                current++;
            }
            else if (spriteRenderer.color == greenColor)
            {
                growth.isGrowing = false;
                spriteRenderer.color = redColor;
                current--;
            }
        }
    }
}
