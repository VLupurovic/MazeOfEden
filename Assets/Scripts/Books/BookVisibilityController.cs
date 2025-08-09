using UnityEngine;

public class BookVisibility : MonoBehaviour
{
    public TorchController torchController;

    private Renderer bookRenderer;

    void Start()
    {
        bookRenderer = GetComponent<Renderer>();
        if (bookRenderer == null)
            Debug.LogWarning("BookVisibility: Renderer not found on book!");
    }

    void Update()
    {
        if (torchController == null)
            return;

        // Book visible only when torch is on
        bookRenderer.enabled = torchController.IsTorchOn;
    }

    public bool IsVisible()
    {
        bool visible = bookRenderer != null & bookRenderer.enabled;
        Debug.Log($"BookVisibility.IsVisible() = {visible}");
        return visible;
    }
}
