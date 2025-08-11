using UnityEngine;

public class WallInteraction : MonoBehaviour
{
    public SequenceManager sequenceManager;
    private bool playerInRange = false;

    void Update()
    {
        if (playerInRange && Input.GetKeyDown(KeyCode.E))
        {
            Debug.Log("Player pressed E on the wall, starting sequence.");
            GetComponent<MoveDownUp>().PlayMoveDownUp();
            StartCoroutine(StartSequenceWithDelay(1f));
        }
    }

    private System.Collections.IEnumerator StartSequenceWithDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        sequenceManager.StartSequence();
        Debug.Log("Sequence started.");
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerInRange = true;
            Debug.Log("Player in wall zone.");
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerInRange = false;
            Debug.Log("Player left wall zone.");
        }
    }
}
