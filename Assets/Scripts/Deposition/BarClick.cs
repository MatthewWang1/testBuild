using UnityEngine;
using UnityEngine.EventSystems;

public class BarClick : MonoBehaviour, IPointerClickHandler
{

    [SerializeField] private GameObject target;
    [SerializeField] private GameObject moving;

    private bool complete;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        complete = false;
    }

    // Update is called once per frame
    void Update()
    {
        // Debug.Log(Vector3.Distance(target.transform.position, moving.transform.position));
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if(Vector3.Distance(target.transform.position, moving.transform.position) < 30f)
        {
            moving.GetComponent<MovingCircle>().SetComplete(true);
            complete = true;
        }
    }

    public bool IsComplete()
    {
        return complete;
    }
}
