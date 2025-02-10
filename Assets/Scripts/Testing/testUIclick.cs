using UnityEngine;
using UnityEngine.EventSystems;

public class testUIclick : MonoBehaviour, IPointerClickHandler
{
    public void OnPointerClick(PointerEventData eventData)
    {
        Debug.Log("Image Clicked: " + gameObject.name);
    }
}
