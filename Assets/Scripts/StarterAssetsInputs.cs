using UnityEngine;
using UnityEngine.InputSystem;

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
        public CanvasRenderer reticle;

        public Canvas PauseMenu;

        private bool isInventoryOpen = false;

        public float maxDistance = 5f;

        public AudioClip[] PickUpAudioClips;

#if ENABLE_INPUT_SYSTEM
        private void OnEnable()
        {
            // Initialize the InputActions
            toggleInputsAction = new InputAction("toggleInputs", binding: "<Keyboard>/tab");
            toggleInputsAction.AddBinding("<Keyboard>/escape"); //test binding rn
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
                look = Vector2.zero;
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

        public void SetCursorState(bool newState)
        {
            Cursor.lockState = newState ? CursorLockMode.Locked : CursorLockMode.None;
        }

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                if (isInventoryOpen)
                {
                    // Set Y position of Content Object to ZERO using a method
                    SetPosition(ContentObject);

                    // Toggle the canvas visibility
                    InventoryToToggle.gameObject.SetActive(!InventoryToToggle.gameObject.activeSelf);
                    reticle.gameObject.SetActive(!reticle.gameObject.activeSelf);

                    // Update the inventory open state
                    isInventoryOpen = InventoryToToggle.gameObject.activeSelf;
                }
                else
                {
                    // Toggle the canvas visibility
                    PauseMenu.gameObject.SetActive(!PauseMenu.gameObject.activeSelf);
                }
            }

            // Check if the Tab key is pressed
            if (Input.GetKeyDown(KeyCode.Tab))
            {
                // Set Y position of Content Object to ZERO using a method
                SetPosition(ContentObject);

                // Toggle the canvas visibility
                InventoryToToggle.gameObject.SetActive(!InventoryToToggle.gameObject.activeSelf);
                reticle.gameObject.SetActive(!reticle.gameObject.activeSelf);

                // Update the inventory open state
                isInventoryOpen = InventoryToToggle.gameObject.activeSelf;

                if (isInventoryOpen)
                {
                    InventoryManager.Instance.UpdateUI();
                }

            }

            if (Input.GetKeyDown(KeyCode.E))
            {
                TryPickup();
            }
        }
        private void SetPosition(RectTransform go)
        {
            go.localPosition = new Vector3(go.GetComponent<RectTransform>().localPosition.x, 0, go.GetComponent<RectTransform>().localPosition.y);
        }

        private void TryPickup()
        {
            RaycastHit hit;
            if (Physics.Raycast(Camera.main.transform.position, Camera.main.transform.forward, out hit, maxDistance))
            {
                if (hit.collider.CompareTag("Pickupable"))
                {
                    ItemPickup itemPickup = hit.collider.GetComponent<ItemPickup>();
                    if (itemPickup != null)
                    {
                        itemPickup.Pickup();

                        // Play a random PickUp audio clip
                        if (PickUpAudioClips.Length > 0)
                        {
                            int randomIndex = Random.Range(0, PickUpAudioClips.Length);
                            AudioClip randomClip = PickUpAudioClips[randomIndex];

                            // Play the audio clip directly
                            AudioSource.PlayClipAtPoint(randomClip, hit.point);
                        }
                    }
                }
            }
        }
    }
}