using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndZoneTrigger : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        // checking if object is with Player tag
        if (other.CompareTag("Player"))
        {
            Debug.Log("Successfully completed all mazes! No more commandments!");

            if (GlobalMazeManager.Instance != null)
            {
                GlobalMazeManager.Instance.commandmentManager.ResetCommandments();
                GlobalMazeManager.Instance.commandmentsNumber = 0;
                GlobalMazeManager.Instance.tunnelNumber = 0;
            }
        }
    }
}
