using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class Player : MonoBehaviour, IKitchenObjectParent
{
    public static Player Instance { get; private set; }
    public event EventHandler<OnSelectedCounterChanvedEventArgs> OnSeleCtedCounterChanged;
    public class OnSelectedCounterChanvedEventArgs : EventArgs
    {
        public BaseCounter selectedCounter;
    }
    [SerializeField] float moveSpeed = 7f;
    [SerializeField] private GameInput gameInput;
    [SerializeField] private LayerMask counterLayerMask;
    [SerializeField] private Transform kitchenObjectHoldPoint;
    private bool isWalking;
    private float rotateSpeed = 8f;
    private float playerRadius = .7f;
    private float playerHeight = 2f;
    private bool canMove;
    private Vector3 lastInteractDir;
    private BaseCounter selectedCounter;
    private KitchinObject kitchenObject;


    private void Awake()
    {
        if (Instance != null)
        {
            Debug.LogError("There is more than one player instance.");
        }
        Instance = this;
    }
    
    private void Start()
    {
        gameInput.OnInteractionAction += GameInput_OnInteractionAction;
        gameInput.OnInteractAlternateAction += GameInput_OnInteractAlternateAction;
    }

    private void GameInput_OnInteractAlternateAction(object sender, EventArgs e)
    {
        if (selectedCounter != null)
        {
            selectedCounter.InteractAlternate(this);
        }
    }

    void GameInput_OnInteractionAction(object sender, System.EventArgs e)
    {
        if (selectedCounter != null)
        {
            selectedCounter.Interact(this);
        }
    }
    
    private void Update()
    {
       HundleMovement();
       HundleInteraction();
    }
    
    public bool IsWalking()
    {
        return isWalking;
    }
    
    private void HundleInteraction()
    {
        Vector2 inputVector = gameInput.GetMovementVectorNormalized();
        Vector3 moveDir = new Vector3(inputVector.x, 0, inputVector.y);
        if (moveDir != Vector3.zero)
        {
            lastInteractDir = moveDir;
        }
        float InteractDistance = 2f;
        if (Physics.Raycast(transform.position, lastInteractDir, out RaycastHit raycastHit ,InteractDistance, counterLayerMask))
        {
            /*Debug.Log("RayHit");---Clear---*/
            if (raycastHit.transform.TryGetComponent(out BaseCounter baseCounter))
            {
                /*Debug.Log("GetClearCounter");---Clear---*/
                if (baseCounter != selectedCounter)
                {
                    SetSelectedCounter(baseCounter);
                }
            }
            else
            {
                /*Debug.Log("DontGetClearCounter");---Clear---*/

                SetSelectedCounter(null);

            }
        }
        else
        {
            SetSelectedCounter(null);
        }
    }
    
    private void HundleMovement()
    {
        Vector2 inputVector = gameInput.GetMovementVectorNormalized();
        Vector3 moveDir = new Vector3(inputVector.x, 0, inputVector.y);
        float moveDistance = moveSpeed * Time.deltaTime;
        canMove =  !Physics.CapsuleCast(transform.position, transform.position + Vector3.up * playerHeight, playerRadius, moveDir, moveDistance);
        if (!canMove)
        {
            Vector3 moveDirX = new Vector3(moveDir.x, 0, 0).normalized;
            canMove = moveDir.x != 0 && !Physics.CapsuleCast(transform.position, transform.position + Vector3.up * playerHeight, playerRadius, moveDirX, moveDistance);
            if (canMove)
            {
                moveDir = moveDirX;
            }
            else
            {
                Vector3 moveDirZ = new Vector3(0, 0, moveDir.z).normalized;
                canMove = moveDir.z != 0 && !Physics.CapsuleCast(transform.position, transform.position + Vector3.up * playerHeight, playerRadius, moveDirZ, moveDistance);
                if (canMove)
                {
                    moveDir = moveDirZ;
                }
                else
                {
                    //Can not move any direction
                }
            }
        }
        if (canMove)
        {
            transform.position += moveDir * (moveSpeed * Time.deltaTime);
        }
        isWalking = (moveDir != Vector3.zero);
        
        if (moveDir != Vector3.zero)
        {
            transform.forward = Vector3.Slerp(transform.forward, moveDir, Time.deltaTime * rotateSpeed);
        }
    }

    private void SetSelectedCounter(BaseCounter selectedCounter)
    {
        this.selectedCounter = selectedCounter;
        OnSeleCtedCounterChanged?.Invoke(this, new OnSelectedCounterChanvedEventArgs { selectedCounter = selectedCounter });

    }

    public Transform GetKitchenObjectFollowTransform()
    {
        return kitchenObjectHoldPoint;
    }

    public void SetKitchenObject(KitchinObject kitchinObject)
    {
        this.kitchenObject = kitchinObject;
    }

    public KitchinObject GetKitchenObject()
    {
        return kitchenObject;
    }

    public void ClearKitchenObject()
    {
        kitchenObject = null;
    }

    public bool HasKitchenObject()
    {
        return kitchenObject != null;
    }
}
