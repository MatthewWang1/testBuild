using UnityEngine;
using TMPro;

public class ButtonRenaming : MonoBehaviour
{
    public TMP_Text uiText;

    private static bool first = true;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        if (first)
        {
            first = false;
            uiText.text = "Begin";
        }
        else
        {
            uiText.text = "Retry";
        }
    }

    // Update is called once per frame
    void Update()
    {

    }
}
