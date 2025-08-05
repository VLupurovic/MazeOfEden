using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseLook : MonoBehaviour
{

    public float mouseSensitivity = 10f;
    public Transform playerBody;

    float xRotation = 0f;

    // Start is called before the first frame update
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked; // Zakljucava i sakriva kursor misa
    }

    // Update is called once per frame
    void Update()
    {
        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;

        xRotation -= mouseY; // "gledanje gore" podize kameru

        xRotation = Mathf.Clamp(xRotation, -90f, 90f); // ne moze da okrece glavu unazad

        transform.localRotation = Quaternion.Euler(xRotation, 0f, 0f); // rotira kameru po x osi

        playerBody.Rotate(Vector3.up * mouseX); // rotira telo igraca po y osi
    }


}
