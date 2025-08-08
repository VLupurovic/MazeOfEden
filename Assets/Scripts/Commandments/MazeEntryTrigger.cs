using UnityEngine;

public class MazeEntryTrigger : MonoBehaviour
{
    public CommandmentManager commandmentManager; 

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("Player entered new maze.");
            commandmentManager.OnEnterNewMaze();
            // deactivating trigger so it doesnt repeat
            gameObject.SetActive(false);
        }
    }
}
