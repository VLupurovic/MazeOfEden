using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CommandmentChecker : MonoBehaviour
{
    public CommandmentManager commandmentManager;
    private bool hasBrokenCommandment = false;

    private float timeSinceLastStop = 0f;
    private float mustStopInterval = 10f;

    private float stillTime = 0f;
    private float maxStillTime = 5f;

    private KeyCode? lastPressedKey = null;
    private KeyCode[] monitoredKeys = new KeyCode[]
        {
            KeyCode.W, KeyCode.S, KeyCode.A, KeyCode.D,
            KeyCode.E, KeyCode.T, KeyCode.Space, KeyCode.LeftShift
        };

    //public WallCollisionChecker wallCollisionChecker;
    
    public Transform playerCamera;
    private float initialYRotation;
    private float maxAllowedAngle = 90f;

    void Start()
    {
        if (playerCamera == null)
            playerCamera = Camera.main.transform;

        initialYRotation = playerCamera.eulerAngles.y;
    }

    void Update()
    {
        if (hasBrokenCommandment) return;

        bool isMoving = Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.D);
        bool isAnyKeyDown = Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.D);


        foreach (var c in commandmentManager.GetActiveCommandments())
        {
            switch (c.id)
            {
                case 1:
                    if (Input.GetKeyDown(KeyCode.W))
                    {
                        hasBrokenCommandment = true;
                        Debug.Log("Commandment failed: Don't go forwards!");
                        RestartAfterDelay();
                    }
                    break;

                case 2:
                    if (Input.GetKeyDown(KeyCode.Space))
                    {
                        hasBrokenCommandment = true;
                        Debug.Log("Commandment failed: Don't jump!");
                        RestartAfterDelay();
                    }
                    break;

                case 3:
                    if (Input.GetKeyDown(KeyCode.LeftShift))
                    {
                        hasBrokenCommandment = true;
                        Debug.Log("Commandment failed: Don't run!");
                        RestartAfterDelay();
                    }
                    break;
                
                case 4:
                
                    if (!isMoving)
                    {
                        timeSinceLastStop = 0f;
                    }
                    else
                    {
                        timeSinceLastStop += Time.deltaTime;
                        if (timeSinceLastStop > mustStopInterval)
                        {
                            hasBrokenCommandment = true;
                            Debug.Log("Commandment failed: Must stop every 10 seconds!");
                            RestartAfterDelay();
                        }
                    }
                    
                    break;

                case 5:
                    //if (wallCollisionChecker != null && wallCollisionChecker.isTouchingWall)
                    //{

                    //    hasBrokenCommandment = true;
                    //    Debug.Log("Commandment failed: Don't touch walls!");
                    //    RestartAfterDelay();
                    //}

                    //break;

                    float currentYRotation = playerCamera.eulerAngles.y;
                    float angleDifference = Mathf.DeltaAngle(initialYRotation, currentYRotation);

                    if (Mathf.Abs(angleDifference) > maxAllowedAngle)
                    {
                        hasBrokenCommandment = true;
                        Debug.Log("Commandment failed: Don't look back!");
                        RestartAfterDelay();
                    }
                    break;

                case 6:
                    if (Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.S))
                    {
                        hasBrokenCommandment = true;
                        Debug.Log("Commandment failed: Only strafe!");
                        RestartAfterDelay();
                    }
                    
                    break;

                case 7:
                    if (!isMoving)
                    {
                        stillTime += Time.deltaTime;
                        if (stillTime > maxStillTime)
                        {
                            hasBrokenCommandment = true;
                            Debug.Log("Commandment failed: Don't stay still for too long!");
                            RestartAfterDelay();
                        }
                    }
                    else
                    {
                        stillTime = 0f;
                    }
                    
                    break;
                
                case 8:
                    if (!isAnyKeyDown)
                    {
                        hasBrokenCommandment = true;
                        Debug.Log("Commandment failed: Must keep moving!");
                        RestartAfterDelay();
                    }

                    break;

                case 9: // doesn't work yet
                    foreach (var key in monitoredKeys)
                    {
                        if (Input.GetKeyDown(key))
                        {
                            if (lastPressedKey.HasValue && lastPressedKey.Value == key)
                            {
                                hasBrokenCommandment = true;
                                Debug.Log("Commandment failed: Don't use same command twice!");
                                RestartAfterDelay();
                                return;
                            }
 
                            lastPressedKey = key;
                        }

                    }

                    break;
                    
                case 10:
                    if (Input.GetKeyDown(KeyCode.S))
                    {
                        hasBrokenCommandment = true;
                        Debug.Log("Commandment failed: Dont go backwards!");
                        RestartAfterDelay();
                    }
                    break;

            }
        }

    }

    private void RestartAfterDelay()
    {
        StartCoroutine(RestartCoroutine());
    }

    private IEnumerator RestartCoroutine()
    {
        Debug.Log("Restart incoming in 3 seconds...");
        yield return new WaitForSeconds(3);

        if (GlobalMazeManager.Instance != null)
        {
            GlobalMazeManager.Instance.FailCurrentMaze();
        }
        else
        {
            Debug.LogError("GlobalMazeManager instance is null!");
        }
    }

    public void ResetBrokenFlag()
    {
        hasBrokenCommandment = false;
    }

}
