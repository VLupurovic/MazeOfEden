using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExitDoorController : MonoBehaviour
{
    public GameObject doorObject;
    public int requiredItemsCount = 3;

    private CommandmentManager commandmentManager;

    void Start()
    {
        if (doorObject == null)
            doorObject = gameObject;

        commandmentManager = FindObjectOfType<CommandmentManager>();
        if (commandmentManager == null)
            Debug.LogError("CommandmentManager not found in scene!");
    }

    private int GetCollectedItemsForCurrentMaze()
    {
        int mazeIndex = commandmentManager.GetCurrentMazeIndex();

        switch (mazeIndex)
        {
            case 1: // Maze 2
                return PlayerBookCollector.Instance != null ? PlayerBookCollector.Instance.GetBookCount() : 0;
            case 2: // Maze 3
                return PlayerCrystalCollector.Instance != null ? PlayerCrystalCollector.Instance.GetCrystalCount() : 0;
            case 3: // Maze 4
                return Maze4Manager.Instance != null ? Maze4Manager.Instance.GetKeysCount() : 0;
            default:
                Debug.LogWarning("Unknown maze index: " + mazeIndex);
                return 0;
        }
    }

    private void TryOpenDoor()
    {
        int collected = GetCollectedItemsForCurrentMaze();

        if (collected == requiredItemsCount)
        {
            doorObject.SetActive(false);
            Debug.Log("Door unlocked!");
        }
        else
        {
            Debug.Log($"Door locked. Need {requiredItemsCount}, have {collected}.");
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            TryOpenDoor();
        }
    }

}
