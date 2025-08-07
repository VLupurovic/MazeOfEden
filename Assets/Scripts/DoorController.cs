using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorController : MonoBehaviour
{
    public float openDistance = 4f; // udaljenost za aktivaciju
    public float slideAmount = 3f; // koliko da se spuste vrata
    public float slideSpeed = 0.4f; // brzina spustanja vrata

    private Vector3 closedPosition;
    private Vector3 openPosition;
    private bool playerNearby = false;
    private bool isOpen = false;

    void Start()
    {
        closedPosition = transform.position;
        openPosition = closedPosition + Vector3.down * slideAmount;
    }

    void Update()
    {
        if (playerNearby && Input.GetKeyDown(KeyCode.E))
        {
            isOpen = !isOpen;
        }

        if (isOpen)
        {
            transform.position = Vector3.MoveTowards(transform.position, openPosition, slideSpeed);
        }
        else
        {
            transform.position = Vector3.MoveTowards(transform.position, closedPosition, slideSpeed);
        }

    }

    //void OnTriggerEnter(Collider other)
    //{
    //    if (other.CompareTag("Player"))
    //    {
    //        playerNearby = true;

    //        InteractionPromptUI.Instance.Show("Pritisni E da otvoriš vrata");
    //    }
    //}

    //void OnTriggerExit(Collider other)
    //{
    //    { if (other.CompareTag("Player"))
    //        {
    //            playerNearby = false;

    //            InteractionPromptUI.Instance.Hide();
    //        }
    //    }
    //}
}
