using UnityEngine;

public class MazeStartTrigger : MonoBehaviour
{
    private bool triggered = false;

    private void OnTriggerEnter(Collider other)
    {
        if (!triggered && other.CompareTag("Player"))
        {
            triggered = true;
            FindObjectOfType<Maze4Manager>().StartTimer();
            Debug.Log("Maze timer started!");
        }
    }

    public void ResetTrigger()
    {
        triggered = false;
        Debug.Log("Start trigger reset!");
    }

}
