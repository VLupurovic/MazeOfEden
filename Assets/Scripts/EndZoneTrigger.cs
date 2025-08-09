using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndZoneTrigger : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        // Checking if object is with Player tag
        if (other.CompareTag("Player"))
        {
            Debug.Log("You found the end!");
        }
    }
}
