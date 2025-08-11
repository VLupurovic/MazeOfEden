using UnityEngine;
using UnityEngine.Tilemaps;

public class Maze3DGenerator : MonoBehaviour
{
    public GameObject wallPrefab; // tvoj 3D zid prefab
    public GameObject floorPrefab; // opcionalno
    public GameObject mazePrefab; // prefab mape koju je SuperTiled2Unity importovao

    private Tilemap tilemap;

    void Start()
    {
        // Instanciraj mapu u scenu
        GameObject mazeInstance = Instantiate(mazePrefab, Vector3.zero, Quaternion.identity);

        // Uzmi Tilemap komponentu iz mape
        tilemap = mazeInstance.GetComponentInChildren<Tilemap>();

        // Generiši zidove
        GenerateWalls();
    }

    void GenerateWalls()
    {
        BoundsInt bounds = tilemap.cellBounds;

        for (int x = bounds.xMin; x < bounds.xMax; x++)
        {
            for (int y = bounds.yMin; y < bounds.yMax; y++)
            {
                Vector3Int cellPos = new Vector3Int(x, y, 0);
                TileBase tile = tilemap.GetTile(cellPos);

                if (tile != null)
                {
                    // Ovdje proveri da li je taj tile "zid"
                    // Na primer, proveri ime tile-a ili njegovu referencu
                    // Ovde je jednostavan primer da je svaki tile zid
                    Vector3 worldPos = tilemap.CellToWorld(cellPos);

                    // Pomeri zid po Y osi ako treba (zavisi od visine zida)
                    Vector3 spawnPos = new Vector3(worldPos.x, 1.25f, worldPos.z); // 1.25f je polovina visine zida

                    Instantiate(wallPrefab, spawnPos, Quaternion.identity, this.transform);
                }
            }
        }
    }
}
