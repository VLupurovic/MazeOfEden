using UnityEngine;

public class BookVisibility : MonoBehaviour
{
    public TorchController torchController;

    private Renderer bookRenderer;

    void Start()
    {
        bookRenderer = GetComponent<Renderer>();
        if (bookRenderer == null)
            Debug.LogWarning("BookVisibility: Renderer nije pronađen na knjizi!");
    }

    void Update()
    {
        if (torchController == null)
            return;

        // Knjiga je vidljiva samo ako je torch upaljen
        bookRenderer.enabled = torchController.IsTorchOn;
    }
}
