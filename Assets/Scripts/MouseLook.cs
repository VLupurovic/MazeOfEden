using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseLook : MonoBehaviour
{

    public float mouseSensitivity = 0.15f;
    public Transform playerBody;
    public float sens = 1f;
    float yaw, pitch;
    float xRotation = 0f;

    // Start is called before the first frame update
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        QualitySettings.vSyncCount = 0;
        Application.targetFrameRate = Screen.currentResolution.refreshRate;
        xRotation = transform.localEulerAngles.x;
    }

    // Update is called once per frame
    void Update()
    {
        float dx = Input.GetAxisRaw("Mouse X");   // not GetAxis
        float dy = Input.GetAxisRaw("Mouse Y");

        yaw += dx * sens;          // no * Time.deltaTime here
        pitch -= dy * sens;
        pitch = Mathf.Clamp(pitch, -89f, 89f);

        transform.localRotation = Quaternion.Euler(pitch, 0f, 0f);
        playerBody.rotation = Quaternion.Euler(0f, yaw, 0f);
    }

}
