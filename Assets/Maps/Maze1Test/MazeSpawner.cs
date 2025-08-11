using UnityEngine;

public class MazeSpawner : MonoBehaviour
{
    // Ovde ubaci prefab tvoje mape (Maze1.prefab) u Inspectoru
    public GameObject mazePrefab;

    private GameObject spawnedMaze;

    void Start()
    {
        SpawnMaze();
    }

    public void SpawnMaze()
    {
        if (spawnedMaze != null)
        {
            Destroy(spawnedMaze);
        }

        spawnedMaze = Instantiate(mazePrefab, Vector3.zero, Quaternion.identity);
    }
}
