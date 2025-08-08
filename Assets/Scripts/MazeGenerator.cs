using UnityEngine;

public class MazeFromImage : MonoBehaviour
{
    public Texture2D mazeTexture; // slika lavirinta
    public GameObject wallPrefab; // prefab zida
    public GameObject floorPrefab; // prefab poda

    private float cellSize = 0.1f; // kolika je celija lavirinta

    void Start()
    {
        // pozicija poda
        Vector3 floorPos = new Vector3(
            (mazeTexture.width * cellSize) / 2f - cellSize / 2f,
            0,
            (mazeTexture.height * cellSize) / 2f - cellSize / 2f);

        GameObject floor = Instantiate(floorPrefab, floorPos, Quaternion.identity, transform);

        float originalFloorSizeX = 10f;
        float originalFloorSizeZ = 10f;

        float floorScaleX = (cellSize * mazeTexture.width) / originalFloorSizeX;
        float floorScaleZ = (cellSize * mazeTexture.height) / originalFloorSizeZ;
        floor.transform.localScale = new Vector3(floorScaleX, 1f, floorScaleZ);

        GenerateMaze();

        CombineWalls();
    }

    void GenerateMaze()
    {
        float wallHeight = 2.5f;  // visina zida
        float wallHeightOffset = wallHeight / 2f;  // polovina visine

        // prolazak kroz piksele slike
        // ako je piksel crn, dodajemo zid
        for (int x = 0; x < mazeTexture.width; x++)
        {
            for (int y = 0; y < mazeTexture.height; y++)
            {
                Color pixelColor = mazeTexture.GetPixel(x, y);

                if (pixelColor == Color.black)
                {
                    Vector3 position = new Vector3(
                    x * cellSize,
                    0 + wallHeightOffset, 
                    y * cellSize);

                    GameObject wall = Instantiate(wallPrefab, position, Quaternion.identity, transform);
                    wall.transform.localScale = new Vector3(cellSize, 3f, cellSize);
                }
            }
        }
    }

    // imao problema sa fpsom i rotacijom misa pre dodavanja ove funkcije
    // dodao mesh collider, da igrac ne bi prolazio kroz zidove
    void CombineWalls()
    {
        MeshFilter[] meshFilters = GetComponentsInChildren<MeshFilter>();
        CombineInstance[] combine = new CombineInstance[meshFilters.Length];
        
        for (int i = 0; i < meshFilters.Length; i++)
        {
            combine[i].mesh = meshFilters[i].sharedMesh;
            combine[i].transform = meshFilters[i].transform.localToWorldMatrix;
        }

        MeshFilter mf = GetComponent<MeshFilter>();
        if (mf == null) mf = gameObject.AddComponent<MeshFilter>();

        mf.mesh = new Mesh();
        mf.mesh.indexFormat = UnityEngine.Rendering.IndexFormat.UInt32;  
        mf.mesh.CombineMeshes(combine);


        MeshCollider mc = GetComponent<MeshCollider>();
        if (mc == null) mc = gameObject.AddComponent<MeshCollider>();
        mc.sharedMesh = mf.mesh;

        MeshRenderer mr = GetComponent<MeshRenderer>();
        if (mr == null) mr = gameObject.AddComponent<MeshRenderer>();

        mr.material = wallPrefab.GetComponent<MeshRenderer>().sharedMaterial;

        // unistavam sve child objekte koji su sada spojeni u jedan mesh
        foreach (Transform child in transform)
        {
            Destroy(child.gameObject);
        }
    }
}
