using UnityEngine;

public class LeverInteraction : MonoBehaviour
{
    public Lever lever; 

    private bool playerInRange = false;

    void Update()
    {
        if (playerInRange && Input.GetKeyDown(KeyCode.E))
        {
            Debug.Log("Pressed E on lever " + lever.leverID);
            lever.PullLever();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerInRange = true;
            Debug.Log("Player in lever zone " + lever.leverID);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerInRange = false;
            Debug.Log("Player left lever zone " + lever.leverID);
        }
    }
}
