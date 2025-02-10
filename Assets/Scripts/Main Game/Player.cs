using UnityEngine;
using System.Collections;   
using UnityEngine.Video;
using System;

public class Player : MonoBehaviour, IFabObjectParent
{
    public static Player Instance {get; private set;}

    public event EventHandler<OnSelectedCounterChangedEventArgs> OnSelectedCounterChanged;

    public event EventHandler OnPickedSomething; 

    public class OnSelectedCounterChangedEventArgs : EventArgs 
    {
        public BaseCounter selectedCounter;  
    }

    [SerializeField] private float moveSpeed = 7f;
    [SerializeField] private GameInputs gameInputs;
    [SerializeField] private LayerMask countersLayerMask;
    [SerializeField] private Transform fabObjectHoldPoint;
    
    private bool isWalking;
    private Vector3 lastInteractDir;
    private BaseCounter selectedCounter;
    private FabObject fabObject;

    private void Start()
    {
        gameInputs.OnInteractAction += GameInput_OnInteractAction;
        gameInputs.OnInteractAlternateAction += GameInput_OnInteractAlternateAction;
    }

    private void Awake()
    {
        if(Instance != null)
        {
            Debug.Log("More than 1 player instance, something wrong");
        }
        Instance = this;
    }

    private void GameInput_OnInteractAction(object sender, System.EventArgs e)
    {
        if(!GameManager.Instance.IsGamePlaying()) return;
        if(selectedCounter != null)
        {
            selectedCounter.Interact(this);
        }
    }

    private void GameInput_OnInteractAlternateAction(object sender, System.EventArgs e)
    {
        if(!GameManager.Instance.IsGamePlaying()) return;
        if(selectedCounter != null)
        {
            selectedCounter.InteractAlternate(this);
        }
    }

    private void Update()
    {
        HandleMovement();
        HandleInteractions();
    }

    private void HandleInteractions()
    {
        Vector2 inputVector = gameInputs.GetMovementVectorNormalized();
        Vector3 moveDir = new Vector3(inputVector.x, 0f, inputVector.y);

        if(moveDir != Vector3.zero)
        {
            lastInteractDir = moveDir;
        }

        float interactionDistance = 2f;
        if(Physics.Raycast(transform.position, lastInteractDir, out RaycastHit raycastHit, interactionDistance, countersLayerMask))
        {
            // Debug.Log("in range");
            if(raycastHit.transform.TryGetComponent(out BaseCounter baseCounter))
            {
                if(baseCounter != selectedCounter)
                {
                    SetSelectedCounter(baseCounter);                    
                }
            }
            else
            {
                SetSelectedCounter(null);
            }
        }
        else
        {
            SetSelectedCounter(null);
        }        
    }

    public bool IsWalking()
    {
        return isWalking;
    }

    private void HandleMovement()
    {
        Vector2 inputVector = gameInputs.GetMovementVectorNormalized();
        Vector3 moveDir = new Vector3(inputVector.x, 0f, inputVector.y);

        float moveDistance = moveSpeed*Time.deltaTime;
        float playerRadius = 0.7f;
        float playerHeight = 2f;
        bool canMove = !Physics.CapsuleCast(transform.position, transform.position + Vector3.up*playerHeight, playerRadius, moveDir, moveDistance);

        if (!canMove) {
            // Cannot move towards moveDir

            // Attempt only X movement
            Vector3 moveDirX = new Vector3(moveDir.x, 0, 0).normalized;
            canMove = (moveDir.x < -.5f || moveDir.x > +.5f) && !Physics.CapsuleCast(transform.position, transform.position + Vector3.up * playerHeight, playerRadius, moveDirX, moveDistance);

            if (canMove) {
                // Can move only on the X
                moveDir = moveDirX;
            } else {
                // Cannot move only on the X

                // Attempt only Z movement
                Vector3 moveDirZ = new Vector3(0, 0, moveDir.z).normalized;
                canMove = (moveDir.z < -.5f || moveDir.z > +.5f) && !Physics.CapsuleCast(transform.position, transform.position + Vector3.up * playerHeight, playerRadius, moveDirZ, moveDistance);

                if (canMove) {
                    // Can move only on the Z
                    moveDir = moveDirZ;
                } else {
                    // Cannot move in any direction
                }
            }
        }

        if(canMove)
        {
            transform.position += moveDir*moveDistance;
        }
        
        isWalking = moveDir != Vector3.zero;

        float rotateSpeed = 10f;
        if (isWalking) {
            transform.forward = Vector3.Slerp(transform.forward, moveDir, Time.deltaTime*rotateSpeed);
        }   

    }

    private void SetSelectedCounter(BaseCounter selectedCounter)
    {
        this.selectedCounter = selectedCounter;

        OnSelectedCounterChanged?.Invoke(this, new OnSelectedCounterChangedEventArgs {
            selectedCounter = selectedCounter
        }); 
    }

    public Transform GetFabObjectFollowTransform()
    {
        return fabObjectHoldPoint;
    }

    public void SetFabObject(FabObject fabObject)
    {
        this.fabObject = fabObject;
        if(fabObject != null)
        {
            OnPickedSomething?.Invoke(this, EventArgs.Empty);
        }
    }

    public FabObject GetFabObject()
    {
        return fabObject;
    }

    public void ClearFabObject()
    {
        fabObject = null;
    }

    public bool HasFabObject()
    {
        return fabObject != null;
    }
}
