using UnityEngine;

public class BookPickupNew : MonoBehaviour
{
    private bool isPickedUp = false;

    // Adding: if player doesnt light up the book, he cannot pick her up
    // Before it was that player can pick book up even regardless of torch
    public BookVisibility bookVisibility;

    void OnTriggerEnter(Collider other)
    {
        if (isPickedUp)
            return;

        if (other.CompareTag("Player"))
        {
            if (bookVisibility != null && bookVisibility.IsVisible())
            {
                isPickedUp = true;
                gameObject.SetActive(false);
                PlayerBookCollector.Instance.CollectBook();  
            }
        }
        else
        {
            Debug.Log("Cannot pick up the book because it is not lit.");
        }
    }

}
