using UnityEngine;

public class BookPickupNew : MonoBehaviour
{
    private bool isPickedUp = false;

    void OnTriggerEnter(Collider other)
    {
        if (isPickedUp)
            return;

        if (other.CompareTag("Player"))
        {
            isPickedUp = true;
            gameObject.SetActive(false);
            PlayerBookCollector.Instance.CollectBook();  
        }
    }

}
