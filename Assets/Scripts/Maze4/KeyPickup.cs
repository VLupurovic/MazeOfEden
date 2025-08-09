using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyPickup : MonoBehaviour
{
    public Maze4Manager mazeManager;

    private bool isPickedUp = false;

    void OnTriggerEnter(Collider other)
    {
        if (isPickedUp)
            return;

        if (other.CompareTag("Player"))
        {
            isPickedUp = true;
            gameObject.SetActive(false);
            mazeManager.CollectKey();
        }
    }

    public void ResetPickup()
    {
        isPickedUp = false;
        gameObject.SetActive(true);
    }
}
