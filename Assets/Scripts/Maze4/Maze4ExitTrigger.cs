using UnityEngine;

public class Maze4ExitTrigger : MonoBehaviour
{
    public Maze4Manager mazeManager;

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            mazeManager.TryExitMaze();
        }
    }
}
