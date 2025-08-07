using UnityEngine;

public class MazeEntryTrigger : MonoBehaviour
{
    public CommandmentManager commandmentManager; 

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("Igrac je ušao u novi maze.");
            commandmentManager.OnEnterNewMaze();
            // deaktiviram trigger da se ne ponavlja
            gameObject.SetActive(false);
        }
    }
}
