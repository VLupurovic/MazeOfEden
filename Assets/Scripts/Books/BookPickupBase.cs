using UnityEngine;

public class BookPickupBase : MonoBehaviour
{
    private bool isPickedUp = false;

    protected virtual bool canBePicked()
    {
        if (isPickedUp)
            return false;

        return true;
    }

    protected virtual void pickUp()
    {
        Debug.Log("Book picked up!");
        isPickedUp = true;
        PlayerBookCollector.Instance.CollectBook();
        Destroy(gameObject);
    }

    void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("Player"))
            return;

        if (canBePicked())
        {
            pickUp();
        }
    }
}
