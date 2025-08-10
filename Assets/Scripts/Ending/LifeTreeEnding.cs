using UnityEngine;
using TMPro;

public class LifeTreeEnding : MonoBehaviour
{
    public GameObject messageUI;          // UI Canvas za poruke
    public TextMeshProUGUI messageText;  // TextMeshPro komponenta za tekst

    private bool playerInRange = false;

    void Start()
    {
        if (messageUI != null)
            messageUI.SetActive(false);  // sakrij poruku na početku
    }

    void Update()
    {
        if (playerInRange && Input.GetKeyDown(KeyCode.E))
        {
            ShowMessage();
        }
    }

    void ShowMessage()
    {
        if (messageUI != null && messageText != null)
        {
            messageText.text = "You have been welcomed to Eden.";
            messageUI.SetActive(true);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
            playerInRange = true;
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
            playerInRange = false;
    }
}
