using UnityEngine;

public class CommandmentTrigger : MonoBehaviour
{
    public CommandmentManager commandmentManager;

    private bool triggered = false;

    private void OnTriggerEnter(Collider other)
    {
        if (triggered) return; // to avoid repetition

        if (other.CompareTag("Player"))
        {
            triggered = true;

            Debug.Log("Entering trigger detected");

            if (GlobalMazeManager.Instance != null)
            {
                GlobalMazeManager.Instance.CompleteMaze();
                Debug.Log("Called CompleteMaze()");
            }
            else
            {
                Debug.LogError("GlobalMazeManager instance is NULL!");
            }

            Destroy(gameObject);

        }
    }
}
