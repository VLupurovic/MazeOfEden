using UnityEngine;

public class ObjectMotion : MonoBehaviour
{
    public enum MotionType { Rotation, Float }
    [Tooltip("Choose how this object should move")]
    public MotionType motionType = MotionType.Rotation;

    [Header("Rotation Settings")]
    public Vector3 rotationSpeed = new Vector3(0f, 90f, 0f);
    public bool useLocalSpace = true;

    [Header("Floating Settings")]
    public float floatAmplitude = 0.5f; // how high to move
    public float floatFrequency = 1f;   // how fast to move

    [Header("General Settings")]
    public bool playOnStart = true;

    private bool isMoving;
    private Vector3 startPos;

    private void Start()
    {
        isMoving = playOnStart;
        startPos = transform.position;
    }

    private void Update()
    {
        if (!isMoving) return;

        switch (motionType)
        {
            case MotionType.Rotation:
                RotateObject();
                break;
            case MotionType.Float:
                FloatObject();
                break;
        }
    }

    private void RotateObject()
    {
        Vector3 delta = rotationSpeed * Time.deltaTime;
        transform.Rotate(delta, useLocalSpace ? Space.Self : Space.World);
    }

    private void FloatObject()
    {
        float newY = startPos.y + Mathf.Sin(Time.time * floatFrequency) * floatAmplitude;
        transform.position = new Vector3(startPos.x, newY, startPos.z);
    }

    // Public controls
    public void StartMotion() => isMoving = true;
    public void StopMotion() => isMoving = false;
    public void ToggleMotion() => isMoving = !isMoving;
}
