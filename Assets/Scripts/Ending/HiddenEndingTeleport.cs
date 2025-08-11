using UnityEngine;
using TMPro;
using System.Collections;
using UnityEngine.SceneManagement;


public class HiddenEndingTeleport : MonoBehaviour
{
    public Transform spawnPoint;
    public GameObject player;

    public GameObject wallObject;        
    public GameObject canvasObject;      
    public TextMeshProUGUI messageText;  
    public string message = "Welcome to Eden!";

    private bool playerInRange = false;
    private bool messageShown = false;

    void Start()
    {
        
        if (wallObject != null)
            wallObject.SetActive(false);

        if (canvasObject != null)
            canvasObject.SetActive(false);
    }

    void Update()
    {
        if (playerInRange && Input.GetKeyDown(KeyCode.E))
        {
            Debug.Log("Teleporting player...");
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
        Debug.Log("Scene reset.");
        Debug.Log("Player teleported to spawn.");
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
            Debug.Log("Message shown.");

            StartCoroutine(HideMessageAfterDelay(5f));
        }
    }

    IEnumerator HideMessageAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);

        if (canvasObject != null)
            canvasObject.SetActive(false);

        messageShown = false;
        Debug.Log("Message hidden.");
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject == player)
        {
            playerInRange = true;
            Debug.Log("Player entered trigger zone.");
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject == player)
        {
            playerInRange = false;
            Debug.Log("Player left trigger zone.");
        }
    }
}
