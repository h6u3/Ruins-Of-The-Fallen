using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.UI;

/*
 * Edited to change the input values so that when the TAB key
 * is pressed the movement is disabled. Add canvas toggle later.
 * 
 */

namespace StarterAssets
{
    public class StarterAssetsInputs : MonoBehaviour
    {
        [Header("Character Input Values")]
        public Vector2 move;
        public Vector2 look;
        public bool jump;
        public bool sprint;

        private bool inputsEnabled = true; // Track whether inputs are enabled

        [Header("Movement Settings")]
        public bool analogMovement;

        [Header("Mouse Cursor Settings")]
        public bool cursorLocked = true;
        public bool cursorInputForLook = true;

        private InputAction toggleInputsAction;

        public RectTransform ContentObject;
        public CanvasRenderer InventoryToToggle;

        private bool isInventoryOpen = false;

#if ENABLE_INPUT_SYSTEM
        private void OnEnable()
        {
            // Initialize the InputActions
            toggleInputsAction = new InputAction("toggleInputs", binding: "<Keyboard>/tab");
            toggleInputsAction.performed += ToggleInputsPerformed;
            toggleInputsAction.Enable();
        }

        private void OnDisable()
        {
            // Disable the InputAction and unsubscribe from its event
            toggleInputsAction.Disable();
            toggleInputsAction.performed -= ToggleInputsPerformed;
        }

        private void ToggleInputsPerformed(InputAction.CallbackContext context)
        {
            inputsEnabled = !inputsEnabled;

            if (!inputsEnabled)
            {
                // Reset movement input to zero when inputs are disabled
                MoveInput(Vector2.zero);
                Cursor.lockState = CursorLockMode.None;
            }
            else
            {
                SetCursorState(cursorLocked);
            }
        }

        public void OnMove(InputValue value)
        {
            if (inputsEnabled)
            {
                MoveInput(value.Get<Vector2>());
            }
        }

        public void OnLook(InputValue value)
        {
            if (inputsEnabled && cursorInputForLook)
            {
                LookInput(value.Get<Vector2>());
            }
        }

        public void OnJump(InputValue value)
        {
            if (inputsEnabled)
            {
                JumpInput(value.isPressed);
            }
        }

        public void OnSprint(InputValue value)
        {
            if (inputsEnabled)
            {
                SprintInput(value.isPressed);
            }
        }
#endif

        public void MoveInput(Vector2 newMoveDirection)
        {
            if (inputsEnabled)
            {
                move = newMoveDirection;
            }
            else
            {
                // Reset movement input to zero when inputs are disabled
                move = Vector2.zero;
            }
        }

        public void LookInput(Vector2 newLookDirection)
        {
            if (inputsEnabled)
            {
                look = newLookDirection;
            }
        }

        public void JumpInput(bool newJumpState)
        {
            if (inputsEnabled)
            {
                jump = newJumpState;
            }
        }

        public void SprintInput(bool newSprintState)
        {
            if (inputsEnabled)
            {
                sprint = newSprintState;
            }
        }

        private void OnApplicationFocus(bool hasFocus)
        {
            if (inputsEnabled)
            {
                SetCursorState(cursorLocked);
            }
        }

        private void SetCursorState(bool newState)
        {
            Cursor.lockState = newState ? CursorLockMode.Locked : CursorLockMode.None;
        }

        private void Update()
        {
            // Check if the Tab key is pressed
            if (Input.GetKeyDown(KeyCode.Tab))
            {
                // Set Y position of Content Object to ZERO using a method
                SetPosition(ContentObject);

                // Toggle the canvas visibility
                InventoryToToggle.gameObject.SetActive(!InventoryToToggle.gameObject.activeSelf);

                // Update the inventory open state
                isInventoryOpen = InventoryToToggle.gameObject.activeSelf;

                if (isInventoryOpen)
                {
                    InventoryManager.Instance.ListItems();
                }

            }
        }
        private void SetPosition(RectTransform go)
        {
            go.localPosition = new Vector3(go.GetComponent<RectTransform>().localPosition.x, 0, go.GetComponent<RectTransform>().localPosition.y);
        }
    }
}
