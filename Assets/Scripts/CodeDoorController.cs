using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class CodeDoorController : MonoBehaviour
{
    public TMP_InputField codeInputField;    // poveži u inspectoru UI InputField
    public Button confirmButton;       // poveži u inspectoru UI Button
    public string correctCode = "1111";

    public float slideAmount = 3f;
    public float slideSpeed = 0.4f;

    public GameObject codePanel;

    private Vector3 closedPosition;
    private Vector3 openPosition;
    private bool playerNearby = false;
    private bool isOpen = false;

    void Start()
    {
        closedPosition = transform.position;
        openPosition = closedPosition + Vector3.down * slideAmount;

        if (confirmButton != null)
        {
            confirmButton.onClick.AddListener(OnConfirmButtonClicked);
        }
    }

    void Update()
    {
        if (isOpen)
        {
            transform.position = Vector3.MoveTowards(transform.position, openPosition, slideSpeed);
        }
        else
        {
            transform.position = Vector3.MoveTowards(transform.position, closedPosition, slideSpeed);
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerNearby = true;
            InteractionPromptUICodeDoor.Instance.Show("Unesi kod i potvrdi");

            if (codePanel != null)
                codePanel.SetActive(true);
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerNearby = false;
            InteractionPromptUICodeDoor.Instance.Hide();

            if (codePanel != null)
                codePanel.SetActive(false);

            if (!isOpen)
                codeInputField.text = "";
        }
    }

    public void OnConfirmButtonClicked()
    {
        if (codeInputField == null)
        {
            Debug.LogWarning("InputField nije povezan!");
            return;
        }

        if (codeInputField.text == correctCode)
        {
            Debug.Log("Kod tačan! Vrata se otvaraju.");
            isOpen = true;
        }
        else
        {
            Debug.Log("Pogrešan kod!");
        }
    }


}
