using UnityEngine;

public class BookPickup : MonoBehaviour
{
    public TriggerDoorController doorController;

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            doorController.OnBookCollected();
            PlayerBookCollector.Instance.CollectBook();

            Destroy(gameObject); // skini knjigu iz scene
        }
    }
}
