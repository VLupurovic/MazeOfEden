using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class PlayerMovement : MonoBehaviour
{
    // Movement speed, jump fore, sprint speed
    public float moveSpeed = 5f;
    public float jumpForce = 5f;
    public float sprint = 2f;

    private Rigidbody rb;
    public Transform head; // First-Person (kamera and rotation)
    public Camera playerCamera; // Player camera

    public Transform groundCheck;
    public float groundDistance = 0.4f;
    public LayerMask groundMask;

    private bool isGrounded;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.interpolation = RigidbodyInterpolation.Interpolate;
        rb.collisionDetectionMode = CollisionDetectionMode.Continuous;
        rb.constraints = RigidbodyConstraints.FreezeRotation; // so that player doesnt rotate

    }

    void Update()
    {
        // check if player is grounded every frame
        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);
        //Debug.Log($"Is Grounded: {isGrounded}");

        // check for jump input in update, so that i dont miss quick presses
        if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
            Debug.Log("Jump activated");
            Jump();
        }
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        Move();
    }

    // if player is sprinting, and runs into the wall, he stops
    // had an issue where you could just hold W and sprint and finish the maze
    void Move()
    {
        float x = Input.GetAxisRaw("Horizontal"); // Left/Right // raw for sharper movement 
        float z = Input.GetAxisRaw("Vertical");  // Forwards/Backwards

        bool isSprinting = Input.GetKey(KeyCode.LeftShift);
        float speed = moveSpeed * (isSprinting ? sprint : 1f);

        Vector3 direction = (transform.right * x + transform.forward * z).normalized;

        if (direction.magnitude > 0)
        {
            // Check if there is wall in the direction of moving
            float distance = speed * Time.deltaTime + 0.1f;
            if (!Physics.Raycast(transform.position, direction, distance))
            {
                Vector3 moveVector = direction * speed * Time.deltaTime;
                rb.MovePosition(rb.position + moveVector);
            }
        }
    }

    void Jump()
    {
        Vector3 velocity = rb.velocity;
        velocity.y = jumpForce;
        rb.velocity = velocity;
    }
}
