using System.Collections;
using System.Collections.Generic;
using CameraBehavior;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Serialization;


namespace Player
{
    [RequireComponent(typeof(CharacterController))]
    public class PlayerController : MonoBehaviour
    {
        public Camera playerCamera;
        public PlayerState currentState;
        [Header("Player controls")]
        [Header("Movement")]
        public bool canMove = true;
        [SerializeField]internal  bool isWalking;
        [SerializeField]internal  bool isRunning;
        [SerializeField]internal float walkSpeed = 6f;
        [SerializeField]internal float runSpeed = 12f;
        [SerializeField]private float jumpPower = 7f;
        [SerializeField]private float gravity = 10f;

        private Vector2 lookPos;
        [SerializeField]public float lookSpeed = 2f;
        [SerializeField]private float lookXLimit = 45f;

        [HideInInspector]public Vector3 moveDirection = Vector3.zero;
        internal float rotationX = 0;

        [Header("Interaction")] 
        public Transform holdingPoint;

        [Header("Crouch")]
        public bool isCrouching;
        [SerializeField] private float crouchWalkSpeed = 3f;
        private float baseWalkSpeed;
        
        private Vector3 mouseWorldPosition;
        private Vector2 mousePosition;
        
        internal  CharacterController characterController;
        public static PlayerController Instance;

        private void Awake()
        {
            Instance = this;
        }
        
        void Start()
        {
            playerCamera = Camera.main;
            characterController = GetComponent<CharacterController>();
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
            
            isCrouching = false;
            baseWalkSpeed = walkSpeed;
        }
        
        void Update()
        {
            mousePosition = Input.mousePosition;
            Movement();
            mouseWorldPosition = Vector3.zero;
        }
        public void OnCrouch(InputAction.CallbackContext ctx)
        {
            isCrouching = !isCrouching;
        }
        
        public void OnRun(InputAction.CallbackContext ctx)
        {
            if (ctx.started)
            {
                isRunning = true;
                isCrouching = true;
            }
            
            if (ctx.canceled)
                isRunning = false;
        }

        public void OnLook(InputAction.CallbackContext ctx)
        {
            lookPos = ctx.ReadValue<Vector2>();
        }
        
        public void ChangeState(PlayerState state)
        {
            this.currentState = state;
        }
        
        #region Movement 

        private void Movement()
        {
            Vector3 forward = transform.TransformDirection(Vector3.forward);
            Vector3 right = transform.TransformDirection(Vector3.right);
            
            float curSpeedX = canMove ? (isRunning ? runSpeed : walkSpeed) * Input.GetAxis("Vertical") : 0;
            float curSpeedY = canMove ? (isRunning ? runSpeed : walkSpeed) * Input.GetAxis("Horizontal") : 0;
            float movementDirectionY = moveDirection.y;
            moveDirection = ((forward.normalized * curSpeedX) + (right.normalized * curSpeedY));
            
            isWalking = moveDirection != Vector3.zero; // check if walking

            if (isRunning)
            {
                isCrouching = false;
                isWalking = false;
            }
            //walkSpeed = isCrouching ? crouchWalkSpeed : baseWalkSpeed;
            
            CameraManager.instance.ChangeState(isCrouching
                ? CameraManager.CameraState.Crouch
                : CameraManager.CameraState.Idle);
            
            CameraManager.instance.ChangeState(isRunning ? CameraManager.CameraState.Running : CameraManager.CameraState.Idle); //Player is running

            if (!isWalking)
            {
                CameraManager.instance.ChangeState(isRunning
                    ? CameraManager.CameraState.Running
                    : CameraManager.CameraState.Idle);
            }
            else
            {
                CameraManager.instance.ChangeState(CameraManager.CameraState.Walking); //Player is running
            }

            HandlesJumping(movementDirectionY);
            HandlesRotation();
        }

        private void HandlesJumping(float movementDirectionY)
        {
            if (Input.GetButton("Jump") && canMove && characterController.isGrounded)
            {
                moveDirection.y = jumpPower;
            }
            else
            {
                moveDirection.y = movementDirectionY;
            }

            if (!characterController.isGrounded)
            {
                moveDirection.y -= gravity * Time.deltaTime;
            }
        }

        private float posY;
        private float posX;
        private void HandlesRotation()
        {
            characterController.Move(moveDirection * Time.deltaTime);

            if (!canMove) return;
            rotationX += -lookPos.y * lookSpeed;
            rotationX = Mathf.Clamp(rotationX, -lookXLimit, lookXLimit);
            playerCamera.transform.localRotation = Quaternion.Euler(rotationX, 0, 0);
            transform.rotation *= Quaternion.Euler(0, lookPos.x * lookSpeed, 0);
        }

        #endregion

        

    }
}

public enum PlayerState
{
    Idle,
    Walking,
    Running,
    Crouch,
    Jumping
}

