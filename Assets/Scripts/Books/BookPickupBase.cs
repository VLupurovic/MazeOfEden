using UnityEngine;

public class BookPickupBase : MonoBehaviour
{
    private bool isPickedUp = false;
    public AudioSource audioSource;

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

        if (audioSource != null)
        {
            audioSource.Play();
            StartCoroutine(DestroyAfterSound());
        }
        else
        {
            Destroy(gameObject);
        }

        PlayerBookCollector.Instance.CollectBook();
    }

    private System.Collections.IEnumerator DestroyAfterSound()
    {
        yield return new WaitForSeconds(audioSource.clip.length);
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
