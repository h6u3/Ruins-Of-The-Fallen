using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThirdPersonMovement : MonoBehaviour
{
    public CharacterController controller;
    public Transform cam;

    public float speed = 4f;
    public float turnSmoothTime = 0.1f;
    public float jumpHeight = 1.0f;
    public float gravity = -9.81f;

    public float turnSmoothVelocity;
    Vector3 velocity;

    bool isJumping = false;

    public RectTransform ContentObject;

    public CanvasRenderer InventoryToToggle;
    public CinemachineFreeLook cameraPan;

    private bool isInventoryOpen = false;

    private void Start()
    {
        // Hide the canvas when the game starts
        InventoryToToggle.gameObject.SetActive(false);
        Cursor.visible = false;
    }

    // Update is called once per frame
    void Update()
    {
        // Check if the Inventory canvas is active
        bool isCanvasActive = InventoryToToggle.gameObject.activeSelf;

        if (!isCanvasActive)
        {
            // Handle player movement and camera rotation
            float horizontal = Input.GetAxisRaw("Horizontal");
            float vertical = Input.GetAxisRaw("Vertical");
            Vector3 direction = new Vector3(horizontal, 0f, vertical).normalized;

            if (controller.isGrounded)
            {
                velocity.y = -2f;

                if (Input.GetButtonDown("Jump") && !isJumping)
                {
                    isJumping = true;
                    velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
                }
            }

            if (controller.isGrounded)
            {
                isJumping = false;
            }

            velocity.y += gravity * Time.deltaTime;

            if (direction.magnitude > 0.1f)
            {
                float targetAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg + cam.eulerAngles.y;
                float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref turnSmoothVelocity, turnSmoothTime);
                transform.rotation = Quaternion.Euler(0f, angle, 0f);

                Vector3 moveDir = Quaternion.Euler(0f, targetAngle, 0f) * Vector3.forward;
                controller.Move(moveDir.normalized * speed * Time.deltaTime);
            }

            controller.Move(velocity * Time.deltaTime);
        }
        else
        {
            // Apply gravity even when the canvas is active
            if (!controller.isGrounded)
            {
                velocity.y += gravity * Time.deltaTime;
                controller.Move(velocity * Time.deltaTime);
            }
        }

        // Check if the Tab key is pressed
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            // Set Y position of Content Object to ZERO using a method
            SetPosition(ContentObject);

            // Toggle the canvas visibility
            InventoryToToggle.gameObject.SetActive(!InventoryToToggle.gameObject.activeSelf);

            // Update the inventory open state
            isInventoryOpen = InventoryToToggle.gameObject.activeSelf;

            // Enable or disable camera panning and player movement
            if (isInventoryOpen)
            {
                Cursor.lockState = CursorLockMode.None; // Unlock cursor
                Cursor.visible = true; // Make cursor visible
                cameraPan.m_YAxis.m_MaxSpeed = 0f;
                cameraPan.m_XAxis.m_MaxSpeed = 0f;

                InventoryManager.Instance.ListItems();

            }
            else
            {
                Cursor.lockState = CursorLockMode.Locked; // Lock cursor
                Cursor.visible = false; // Hide cursor
                cameraPan.m_YAxis.m_MaxSpeed = 8f;
                cameraPan.m_XAxis.m_MaxSpeed = 400f;


            }
        }
    }

    private void SetPosition(RectTransform go)
    {
        go.localPosition = new Vector3(go.GetComponent<RectTransform>().localPosition.x, 0, go.GetComponent<RectTransform>().localPosition.y);
    }
}
