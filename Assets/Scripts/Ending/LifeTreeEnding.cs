using UnityEngine;
using TMPro;

public class LifeTreeEnding : MonoBehaviour
{
    public GameObject messageUI;          
    public TextMeshProUGUI messageText;  

    private PlayerMovement playerMovement;

    private bool playerInRange = false;

    void Start()
    {
        if (messageUI != null)
            messageUI.SetActive(false);  

        if (playerMovement == null && GameObject.FindWithTag("Player") != null)
            playerMovement = GameObject.FindWithTag("Player").GetComponent<PlayerMovement>();
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

            if (playerMovement != null)
                playerMovement.enabled = false;
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
