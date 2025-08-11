using UnityEngine;

public class CrystalPickup : MonoBehaviour
{
    private bool isPickedUp = false;

    public AudioSource audioSource;

    void OnTriggerEnter(Collider other)
    {
        if (isPickedUp)
            return;

        if (other.CompareTag("Player"))
        {
            isPickedUp = true;

            if (audioSource != null)
            {
                audioSource.Play();
                StartCoroutine(DestroyAfterSound());
            }
            else
            {
                gameObject.SetActive(false);
            }

            PlayerCrystalCollector.Instance.CollectCrystal();
        }
    }

    private System.Collections.IEnumerator DestroyAfterSound()
    {
        yield return new WaitForSeconds(audioSource.clip.length);
        gameObject.SetActive(false);
    }
}
