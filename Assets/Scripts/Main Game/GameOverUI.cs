using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GameOverUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI recipesDeliveredText;
    [SerializeField] private Button mainMenuButton;

    private void Awake()
    {
        GameManager.Instance.OnStateChanged += GameManager_OnStateChanged;
        mainMenuButton.onClick.AddListener(() => {
            Time.timeScale = 1f;
            Loader.Load(Loader.Scene.MainMenu);
        });
        Hide();
    }



    private void GameManager_OnStateChanged(object sender, System.EventArgs e)
    {
        if(GameManager.Instance.IsGameOver())
        {
            recipesDeliveredText.text = DeliveryManager.Instance.GetSuccessfulRecipes().ToString();
            Show();
        }
        else
        {
            Hide();
        }
    }


    private void Show()
    {
        gameObject.SetActive(true);
    }

    private void Hide()
    {
        gameObject.SetActive(false);
    }
}
