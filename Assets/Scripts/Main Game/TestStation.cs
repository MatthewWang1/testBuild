using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class TestStation : MonoBehaviour, Interactable_Interface
{

    [SerializeField] private string _prompt;
    [SerializeField] private Transform station;

    [SerializeField] private GameObject _minigame;
    [SerializeField] private Transform _minigameParent;

    [SerializeField] private GameObject _minigame_with_script_to_activate;

    public string InteractionPrompt => _prompt;

    private bool _interacted = false;

    private fill_up_bar_script etching_script;

    [SerializeField] private GameObject GameStateManagerObject;
    private GameStateManager GameStateManagerScript;

    void Start()
    {
        GameStateManagerScript = GameStateManagerObject.GetComponent<GameStateManager>();
    }


    public bool Interact(Interactor interactor)
    {
        Debug.Log("Interacted with test station!");
        // station.position += Vector3.left;

        if(!_interacted)
        {
            // minigame = Instantiate(_minigame, _minigameParent); // put pos, rot, between mini game and parent
            _minigame.SetActive(true);
            _interacted = true;
            _minigame_with_script_to_activate = GameObject.FindGameObjectWithTag("Etching");
            activate_script();
            GameStateManagerScript.Set_Game_State(4);
        }
        else
        {
            _minigame_with_script_to_activate = GameObject.FindGameObjectWithTag("Etching");
            reset_deactivate_script();
            _interacted = false;
            _minigame.SetActive(false);
            GameStateManagerScript.Set_Game_State(3);
        }
        return true;
    }


    private void activate_script()
    {
        if (_minigame_with_script_to_activate != null)
        {
            etching_script = _minigame_with_script_to_activate.GetComponent<fill_up_bar_script>();

            if (etching_script != null)
            {
                etching_script.begin();
            }
            else
            {
                Debug.LogError("Fill_up_bar_script not found on the GameObject with tag 'Etching'.");
            }
        }
        else
        {
            Debug.LogError("No GameObject found with tag 'Etching'.");
        }
    }

    private void reset_deactivate_script()
    {
        // if (_minigame_with_script_to_activate != null)
        // {
        //     etching_script = _minigame_with_script_to_activate.GetComponent<fill_up_bar_script>();

        //     if (etching_script != null)
        //     {
        //         etching_script.reset();
        //     }
        //     else
        //     {
        //         Debug.LogError("Fill_up_bar_script not found on the GameObject with tag 'Etching'.");
        //     }
        // }
        // else
        // {
        //     Debug.LogError("No GameObject found with tag 'Etching'.");
        // }
    }
}
