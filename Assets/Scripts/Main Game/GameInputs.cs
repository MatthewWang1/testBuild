using System;
using System.Diagnostics.Tracing;
using UnityEngine;
using UnityEngine.InputSystem;

public class GameInputs : MonoBehaviour
{

    public static GameInputs Instance {get; private set;}
    
    private PlayerInputActions playerInputActions;
    public event EventHandler OnInteractAction;
    public event EventHandler OnInteractAlternateAction;
    public event EventHandler OnPauseAction;

    private bool playerActionEnabled;

    private void Awake()
    {
        Instance = this;
        playerActionEnabled = true;
        playerInputActions = new PlayerInputActions();
        playerInputActions.Player.Enable();

        playerInputActions.Player.Interact.performed += Interact_performed;
        playerInputActions.Player.InteractAlternate.performed += InteractAlternate_performed;
        playerInputActions.Player.Pause.performed += Pause_Performed;
    }

    private void OnDestroy()
    {
        playerInputActions.Player.Interact.performed -= Interact_performed;
        playerInputActions.Player.InteractAlternate.performed -= InteractAlternate_performed;
        playerInputActions.Player.Pause.performed -= Pause_Performed;

        playerInputActions.Dispose();
    }

    private void Pause_Performed(InputAction.CallbackContext context)
    {
        OnPauseAction?.Invoke(this, EventArgs.Empty);
    }

    private void InteractAlternate_performed(InputAction.CallbackContext context)
    {
        if(playerActionEnabled) OnInteractAlternateAction?.Invoke(this, EventArgs.Empty);
    }

    private void Interact_performed(UnityEngine.InputSystem.InputAction.CallbackContext obj)
    {
        if(playerActionEnabled) OnInteractAction?.Invoke(this, EventArgs.Empty);
    }

    public Vector2 GetMovementVectorNormalized()
    {
        Vector2 inputVector = playerInputActions.Player.Move.ReadValue<Vector2>();
        
        if(playerActionEnabled)  inputVector = inputVector.normalized;
        else                     inputVector = Vector2.zero;

        return inputVector;
    }

    public void DisablePlayerActions()
    {
        playerActionEnabled = false;
    }

    public void EnablePlayerActions()
    {
        playerActionEnabled = true;
    }

}
