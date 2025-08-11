using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyPickup : MonoBehaviour
{
    public Maze4Manager mazeManager;

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
        
            mazeManager.CollectKey();
        }
    }

    private System.Collections.IEnumerator DestroyAfterSound()
    {
        yield return new WaitForSeconds(audioSource.clip.length);
        gameObject.SetActive(false);
    }

    public void ResetPickup()
    {
        isPickedUp = false;
        gameObject.SetActive(true);
    }
}
