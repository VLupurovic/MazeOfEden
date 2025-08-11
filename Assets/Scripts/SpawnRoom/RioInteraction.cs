using UnityEngine;
using TMPro;

public class RioInteraction : MonoBehaviour
{
    public TextMeshProUGUI worldText;
    public GameObject bookUI;     

    private bool playerInRange = false;

    void Start()
    {
        if (worldText != null)
            worldText.gameObject.SetActive(false);

        if (bookUI != null)
            bookUI.SetActive(false);
    }

    void Update()
    {
        if (playerInRange)
        {
            if (worldText != null && !worldText.gameObject.activeSelf)
                worldText.gameObject.SetActive(true);

            if (Input.GetKeyDown(KeyCode.E))
            {
                if (bookUI != null)
                {
                    bookUI.SetActive(true);
                }

                if (worldText != null)
                    worldText.gameObject.SetActive(false);
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerInRange = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerInRange = false;

            if (worldText != null)
                worldText.gameObject.SetActive(false);

            if (bookUI != null)
                bookUI.SetActive(false);
        }
    }
}
