using UnityEngine;

public class PickupTorch : MonoBehaviour
{
    private bool playerNearby = false;

    void Update()
    {
        if (playerNearby && Input.GetKeyDown(KeyCode.E))
        {
            Debug.Log("Pressed E near torch.");
            TorchController torchController = FindObjectOfType<TorchController>();
            if (torchController != null)
            {
                Debug.Log("TorchController found.");
                torchController.PickupTorch();
                Destroy(gameObject);
            }
            else
            {
                Debug.LogWarning("TorchController not found!");
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerNearby = true;
            Debug.Log("Player entered torch area.");
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerNearby = false;
            Debug.Log("Player has left torch area.");
        }
    }

}
