using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CommandmentChecker : MonoBehaviour
{
    public CommandmentManager commandmentManager;
    private bool hasBrokenCommandment = false;


    void Update()
    {
        if (hasBrokenCommandment) return;

        foreach (var c in commandmentManager.GetActiveCommandments())
        {
            switch (c.id)
            {
                case 1:
                    if (Input.GetKeyDown(KeyCode.W))
                    {
                        hasBrokenCommandment = true;
                        Debug.Log("Commandment failed:Dont go forward!");
                        RestartAfterDelay();
                    }
                    break;

                case 2:
                    if (Input.GetKeyDown(KeyCode.Space))
                    {
                        hasBrokenCommandment = true;
                        Debug.Log("Commandment failed: Dont jump!");
                        RestartAfterDelay();
                    }
                    break;

                case 3:
                    if (Input.GetKeyDown(KeyCode.LeftShift))
                    {
                        hasBrokenCommandment = true;
                        Debug.Log("Commandment failed: Dont run!");
                        RestartAfterDelay();
                    }
                    break;

                case 6:
                    if (Input.GetKeyDown(KeyCode.T))
                    {
                        hasBrokenCommandment = true;
                        Debug.Log("Commandment failed: Dont use torch!");
                        RestartAfterDelay();
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
