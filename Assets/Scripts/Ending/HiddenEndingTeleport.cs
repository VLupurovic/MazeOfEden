using UnityEngine;
using TMPro;
using System.Collections;
using UnityEngine.SceneManagement;


public class HiddenEndingTeleport : MonoBehaviour
{
    public Transform spawnPoint;
    public GameObject player;

    public GameObject wallObject;         // Ceo zid (root GameObject)
    public GameObject canvasObject;       // Canvas koji je child zida
    public TextMeshProUGUI messageText;   // TMP tekst komponenta u canvasu
    public string message = "Dobrodošao u Eden!";

    private bool playerInRange = false;
    private bool messageShown = false;

    void Start()
    {
        // Sakrij zid i UI na početku
        if (wallObject != null)
            wallObject.SetActive(false);

        if (canvasObject != null)
            canvasObject.SetActive(false);
    }

    void Update()
    {
        if (playerInRange && Input.GetKeyDown(KeyCode.E))
        {
            Debug.Log("Teleportujem igrača...");
            TeleportPlayer();
            ShowWallAndMessage();
        }
    }

    void TeleportPlayer()
    {
        player.transform.position = spawnPoint.position;
        player.transform.rotation = spawnPoint.rotation;
        Scene currentScene = SceneManager.GetActiveScene();
        SceneManager.LoadScene(currentScene.name);
        Debug.Log("Scena je resetovana.");
        Debug.Log("Igrač teleportovan na spawn.");
    }

    void ShowWallAndMessage()
    {
        if (wallObject != null)
            wallObject.SetActive(true);

        if (canvasObject != null && messageText != null && !messageShown)
        {
            messageText.text = message;
            canvasObject.SetActive(true);
            messageShown = true;
            Debug.Log("Poruka prikazana.");

            StartCoroutine(HideMessageAfterDelay(5f));
        }
    }

    IEnumerator HideMessageAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);

        if (canvasObject != null)
            canvasObject.SetActive(false);

        messageShown = false;
        Debug.Log("Poruka sakrivena.");
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject == player)
        {
            playerInRange = true;
            Debug.Log("Igrač ušao u trigger zonu.");
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject == player)
        {
            playerInRange = false;
            Debug.Log("Igrač izašao iz trigger zone.");
        }
    }
}
