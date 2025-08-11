using UnityEngine;
using TMPro;

public class LostWandererInteraction : MonoBehaviour
{
    public TextMeshProUGUI interactionText;  // message before game over text
    public TextMeshProUGUI gameOverText;     // game over ui text
    public GameObject player;
    private PlayerMovement playerMovement;   

    private bool playerInRange = false;
    private bool gameOverTriggered = false;

    void Start()
    {
        gameOverText.gameObject.SetActive(false);
        interactionText.gameObject.SetActive(false);
        playerMovement = player.GetComponent<PlayerMovement>();
    }

    void Update()
    {
        if (playerInRange && !gameOverTriggered && Input.GetKeyDown(KeyCode.E))
        {
            TriggerGameOver();
        }
    }

    private void TriggerGameOver()
    {
        gameOverTriggered = true;

        // Prikaži poruku ili Game Over UI
        interactionText.gameObject.SetActive(false);
        gameOverText.gameObject.SetActive(true);
        gameOverText.text = "Temptation killed the soul before the sin did.\r\n\r\nGAME OVER!";

        // Onemogući kretanje igrača
        if (playerMovement != null)
        {
            playerMovement.enabled = false;
        }

        Debug.Log("Game Over triggered by interacting with statue.");
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject == player && !gameOverTriggered)
        {
            playerInRange = true;
            interactionText.gameObject.SetActive(true);
            interactionText.text = "Press E to interact (but beware...)";
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject == player)
        {
            playerInRange = false;
            interactionText.gameObject.SetActive(false);
        }
    }
}
