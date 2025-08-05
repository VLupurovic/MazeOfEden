using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    // Brzina kretanja, sila soka, dodatak brzini kada igrac sprintuje
    public float moveSpeed = 5f;
    public float jumpForce = 5f;
    public float sprint = 2f;

    private Rigidbody rb;
    public Transform head; // First-Person (kamera i rotacija)
    public Camera camera; // Kamera igraca
    
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        Move();
        Jump();
    }

    void Move()
    {
        float x = Input.GetAxis("Horizontal"); // Levo/Desno
        float z = Input.GetAxis("Vertical"); // Napred/Nazad

        bool isSprinting = Input.GetKey(KeyCode.LeftShift); // proverava da li igrac drzi Shift

        float currentSpeed = moveSpeed;
        if (isSprinting)
            currentSpeed += sprint;


        Vector3 move = transform.right * x + transform.forward * z;
        Vector3 newPosition = transform.position + move * currentSpeed * Time.deltaTime;
        rb.MovePosition(newPosition);
    }

    void Jump()
    {
        if (Input.GetKeyDown(KeyCode.Space) && Mathf.Abs(rb.velocity.y) < 0.01f) // proverava da li je igrac stisnuo Space
        {
            rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
        }
    }
}
