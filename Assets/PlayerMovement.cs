using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class PlayerMovement : MonoBehaviour
{
    // Brzina kretanja, sila soka, dodatak brzini kada igrac sprintuje
    public float moveSpeed = 5f;
    public float jumpForce = 5f;
    public float sprint = 2f;

    private Rigidbody rb;
    public Transform head; // First-Person (kamera i rotacija)
    public Camera playerCamera; // Kamera igraca

    public Transform groundCheck;
    public float groundDistance = 0.4f;
    public LayerMask groundMask;

    private bool isGrounded;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.interpolation = RigidbodyInterpolation.Interpolate;
        rb.collisionDetectionMode = CollisionDetectionMode.Continuous;
        rb.constraints = RigidbodyConstraints.FreezeRotation; // da igrač ne rotira fizički

    }

    // Update is called once per frame
    void FixedUpdate()
    {
        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);

        Move();
        Jump();
    }

    //void Move()
    //{
    //    float x = Input.GetAxisRaw("Horizontal"); // Levo/Desno
    //    float z = Input.GetAxisRaw("Vertical"); // Napred/Nazad

    //    bool isSprinting = Input.GetKey(KeyCode.LeftShift); // proverava da li igrac drzi Shift

    //    float currentSpeed = isSprinting ? moveSpeed * 1.5f : moveSpeed;



    //    Vector3 move = transform.right * x + transform.forward * z;
    //    Vector3 newPosition = transform.position + move * currentSpeed * Time.deltaTime;
    //    rb.MovePosition(newPosition);
    //}

    // ukoliko igrac tokom sprinta se zakuca u zid, ostaje u tom mestu 
    // do sada je bilo, da ako se zakuca sile ga pomeraju levo/desno i prakticno moze doci do kraja mazea samo drzanjem W i shifta
    void Move()
    {
        float x = Input.GetAxisRaw("Horizontal"); // Levo/Desno  // raw za oštrije pomeranje 
        float z = Input.GetAxisRaw("Vertical");  // Napred/Nazad

        bool isSprinting = Input.GetKey(KeyCode.LeftShift);
        float speed = moveSpeed * (isSprinting ? sprint : 1f);

        Vector3 direction = (transform.right * x + transform.forward * z).normalized;

        if (direction.magnitude > 0)
        {
            // Provera da li ima zid ispred u pravcu kretanja
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
        if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
            Debug.Log("SKOK!");
            Vector3 velocity = rb.velocity;
            velocity.y = jumpForce;
            rb.velocity = velocity;
        }
    }


    //void OnCollisionStay(Collision collision)
    //{
    //    // Provera da li smo na zemlji (jednostavan ground check)
    //    foreach (ContactPoint contact in collision.contacts)
    //    {
    //        if (Vector3.Angle(contact.normal, Vector3.up) < 45)
    //        {
    //            isGrounded = true;
    //            return;
    //        }
    //    }
    //    isGrounded = false;
    //}

    //void OnCollisionExit(Collision collision)
    //{
    //    isGrounded = false;
    //}
}
