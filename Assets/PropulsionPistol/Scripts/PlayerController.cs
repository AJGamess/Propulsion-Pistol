using System;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [Header("Movement")]
    [SerializeField] float moveSpeed = 7f;
    [SerializeField] float groundDrag = 5f;

    [Header("Ground Check")]
    [SerializeField] float playerHeight = 2f;
    [SerializeField] LayerMask whatIsGround;
    bool isGrounded;

    [Header("References")]
    public Transform orientation;
    [SerializeField] private Transform playerCamera;

    float horizontalInput;
    float verticalInput;

    Vector3 moveDirection;
    Rigidbody rb;

    void Start()
    {
        rb = GetComponent<Rigidbody>();

        // This stops the capsule from falling over when moving
        rb.freezeRotation = true;
    }

    void Update()
    {
        // Check if we are touching the ground
        isGrounded = Physics.Raycast(transform.position, Vector3.down, playerHeight * 0.5f + 0.2f, whatIsGround);

        // Get keyboard inputs (WASD / Arrow Keys)
        horizontalInput = Input.GetAxisRaw("Horizontal");
        verticalInput = Input.GetAxisRaw("Vertical");

        // Control drag (so the player doesn't feel like they are sliding on ice)
        if (isGrounded)
            rb.linearDamping = groundDrag; // Note: In older Unity versions, use rb.drag
        else
            rb.linearDamping = 0; // No drag in the air so recoil pushes you perfectly

        // Apply recoil when the player presses the left mouse button
        if (Input.GetMouseButtonDown(0))
        {
            ApplyRecoil();
        }

        // Handle jumping
        if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
            Jump();
        }
    }

    void FixedUpdate()
    {
        // Calculate movement relative to the direction the orientation is facing
        moveDirection = orientation.forward * verticalInput + orientation.right * horizontalInput;

        // Get your flat horizontal speed (ignoring up/down falling/jumping)
        Vector3 flatVel = new Vector3(rb.linearVelocity.x, 0f, rb.linearVelocity.z);

        // Check if the player is already moving at max speed
        if (flatVel.magnitude > moveSpeed)
        {
            // If so, limit the velocity to the max speed while maintaining the current direction
            Vector3 limitedVel = flatVel.normalized * moveSpeed;
            rb.linearVelocity = new Vector3(limitedVel.x, rb.linearVelocity.y, limitedVel.z);
        }
        else
        {
            // If we are under the speed limit, apply force logic
            if (isGrounded)
            {
                rb.AddForce(moveDirection.normalized * moveSpeed * 10f, ForceMode.Force);
            }
            else
            {
                // Reduce the force applied in the air to make it feel more floaty and less controllable
                rb.AddForce(moveDirection.normalized * moveSpeed * 10f * 0.5f, ForceMode.Force);
            }
        }
    }
    public void ApplyRecoil()
    {
        // Apply a force in the opposite direction of the camera vector to allow the player to move opposite to the direction they are facing when shooting
        // Calculate the recoil direction based on the orientation's vector to allow vertical and horizontal recoil
        Vector3 cameraForward = playerCamera.forward;
        Vector3 recoilDirection = -cameraForward;
        // prevent downward gravity from being applied to the recoil direction if recoiling upwards
        if (recoilDirection.y > 0)
        {
            rb.linearVelocity = new Vector3(rb.linearVelocity.x, 0, rb.linearVelocity.z);
        }
        // Apply the recoil force to the Rigidbody
        rb.AddForce(recoilDirection * 5f, ForceMode.Impulse);


    }
    private void Jump()
    {
        // Reset the vertical velocity to 0 before applying the jump force
        rb.linearVelocity = Vector3.zero;
        // Apply an upward force to the Rigidbody to make the player jump
        rb.AddForce(Vector3.up * 5f, ForceMode.Impulse);
    }
}
