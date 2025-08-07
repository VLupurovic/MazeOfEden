using UnityEngine;

public class PickupTorch : MonoBehaviour
{
    private bool playerNearby = false;

    void Update()
    {
        if (playerNearby && Input.GetKeyDown(KeyCode.E))
        {
            Debug.Log("Pritisnut E kod torcha");
            TorchController torchController = FindObjectOfType<TorchController>();
            if (torchController != null)
            {
                Debug.Log("TorchController pronađen");
                torchController.PickupTorch();
                Destroy(gameObject);
            }
            else
            {
                Debug.LogWarning("TorchController nije pronađen!");
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerNearby = true;
            Debug.Log("Igrac u blizini torcha");
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerNearby = false;
            Debug.Log("Igrac je napustio oblast torcha");
        }
    }

}
