using UnityEngine;
using TMPro;

public class InteractionPromptUICodeDoor : MonoBehaviour
{
    public static InteractionPromptUICodeDoor Instance;

    [SerializeField] private GameObject promptPanel;
    [SerializeField] private TextMeshProUGUI promptText;

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);
    }

    public void Show(string message)
    {
        promptPanel.SetActive(true);
        promptText.text = message;
    }

    public void Hide()
    {
        promptPanel.SetActive(false);
    }
}
