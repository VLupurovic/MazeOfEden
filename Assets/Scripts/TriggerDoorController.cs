using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerDoorController : MonoBehaviour
{
    public GameObject door; // vrata koja nestaju/pojavljuju se
    public GameObject trigger; // taster na koji se staje
    public GameObject book; // knjiga koja se kupi

    private bool playerOnTrigger = false; // da li je igrac na tasteru
    private bool bookCollected = false; // da li je knjiga pokupljena

    private Coroutine doorCoroutine;

    void Start()
    {
        if (door == null)
            Debug.LogError("Door nije postavljen!");
        if (trigger == null)
            Debug.LogError("Trigger nije postavljen!");
        if (book == null)
            Debug.LogError("Book nije postavljen!");
    }

    void Update()
    {
        if (bookCollected && door.activeSelf)
        {
            door.SetActive(false); // ako je knjiga pokupljena, vrata nestaju
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