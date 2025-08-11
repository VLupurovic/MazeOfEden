using UnityEngine;
using TMPro;

public class LeverInteraction : MonoBehaviour
{
    public Lever lever; 

    private bool playerInRange = false;

    // adding lever feedback
    public TextMeshProUGUI feedbackText;
    public AudioSource clickSound;
    public float feedbackDuration = 2f;

    public LeverManager leverManager;

    void Start()
    {
        if (feedbackText != null)
            feedbackText.gameObject.SetActive(false);
    }

    void Update()
    {
        if (playerInRange && Input.GetKeyDown(KeyCode.E))
        {
            if (clickSound != null)
            {
                Debug.Log("Playing click sound for lever " + lever.leverID);
                clickSound.Play();
            }
            else
            {
                Debug.LogWarning("clickSound AudioSource is not assigned on lever " + lever.leverID);
            }

            if (leverManager != null)
            {
                leverManager.RegisterLeverPull(lever.leverID);
            }
            else
            {
                Debug.Log("Pressed E on lever " + lever.leverID);

                ShowFeedback("Pressed lever: " + lever.leverID);

                

                lever.PullLever();
            }   
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

    public void ShowFeedback(string message)
    {
        if (feedbackText != null)
        {
            feedbackText.text = message;
            feedbackText.gameObject.SetActive(true);

            CancelInvoke(nameof(HideFeedback));
            Invoke(nameof(HideFeedback), feedbackDuration);
        }
    }

    void HideFeedback()
    {
        if (feedbackText != null)
            feedbackText.gameObject.SetActive(false);
    }
}
