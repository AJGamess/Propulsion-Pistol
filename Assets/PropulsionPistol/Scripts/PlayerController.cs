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
        // 1. Check if we are touching the ground
        isGrounded = Physics.Raycast(transform.position, Vector3.down, playerHeight * 0.5f + 0.2f, whatIsGround);

        // 2. Get keyboard inputs (WASD / Arrow Keys)
        horizontalInput = Input.GetAxisRaw("Horizontal");
        verticalInput = Input.GetAxisRaw("Vertical");

        // 3. Control drag (so the player doesn't feel like they are sliding on ice)
        if (isGrounded)
            rb.linearDamping = groundDrag; // Note: In older Unity versions, use rb.drag
        else
            rb.linearDamping = 0; // No drag in the air so recoil pushes you perfectly
    }

    void FixedUpdate()
    {
        // 4. Calculate movement relative to the direction the camera/orientation is facing
        moveDirection = orientation.forward * verticalInput + orientation.right * horizontalInput;

        // 5. Apply the force to move the Rigidbody
        rb.AddForce(moveDirection.normalized * moveSpeed * 10f, ForceMode.Force);
    }
}
