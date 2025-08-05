using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndZoneTrigger : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        // Proverava da li je objekat sa tagom Player
        if (other.CompareTag("Player"))
        {
            Debug.Log("You found the end!");
        }
    }
}
