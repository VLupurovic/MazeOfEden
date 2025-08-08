using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerDoorController : MonoBehaviour
{
    public GameObject door; // Door that hide/show
    public GameObject trigger; // Trigger Taster
    public GameObject book; // Book that needs to be collected

    private bool playerOnTrigger = false; // Did player step on taster
    private bool bookCollected = false; // Is book collected

    private Coroutine doorCoroutine;

    void Start()
    {
        if (door == null)
            Debug.LogError("Door not set!");
        if (trigger == null)
            Debug.LogError("Trigger not set!");
        if (book == null)
            Debug.LogError("Book not set!");
    }

    void Update()
    {
        if (bookCollected && door.activeSelf)
        {
            door.SetActive(false); // If book is collected, door dissapears
        }
    }

    public void OnPlayerEnterTrigger()
    {
        if (bookCollected)
            return;

        if (doorCoroutine != null)
            StopCoroutine(doorCoroutine);

        doorCoroutine = StartCoroutine(HideDoorTemporarily());
    }

    public void OnPlayerExitTrigger()
    {
        if (bookCollected)
            return;

        if (doorCoroutine != null)
            StopCoroutine(doorCoroutine);

        doorCoroutine = StartCoroutine(ShowDoorTemporarily());
    }

    IEnumerator HideDoorTemporarily()
    {
        door.SetActive(false);
        yield return new WaitForSeconds(3f);
        if (!playerOnTrigger)
            door.SetActive(true);
    }

    IEnumerator ShowDoorTemporarily()
    {
        yield return new WaitForSeconds(3f);
        if (!playerOnTrigger && !bookCollected)
            door.SetActive(true);
    }

    public void OnBookCollected()
    {
        bookCollected = true;
        if (doorCoroutine != null)
            StopCoroutine(doorCoroutine);
        door.SetActive(false);
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerOnTrigger = true;
            OnPlayerEnterTrigger();
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerOnTrigger = false;
            OnPlayerExitTrigger();
        }
    }
}