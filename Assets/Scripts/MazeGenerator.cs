using UnityEngine;

public class MazeFromImage : MonoBehaviour
{
    public Texture2D mazeTexture; // maze picture
    public GameObject wallPrefab; // wall prefab
    public GameObject floorPrefab; // floor prefab

    private float cellSize = 0.1f; // size of maze cell

    public Material wallMaterial;


    void Start()
    {
       
        // floor position
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
        float wallHeight = 2.0f;  // wall Height
        float wallHeightOffset = wallHeight / 2f;  // half of the height

        // we go through image pixels
        // if the pixel == black we add wall there
        for (int x = 0; x < mazeTexture.width; x++)
        {
            for (int y = 0; y < mazeTexture.height; y++)
            {
                Color pixelColor = mazeTexture.GetPixel(x, y);

                if (pixelColor == Color.black)
                {
                    Vector3 position = new Vector3(
                    x * cellSize,
                    wallHeightOffset, 
                    y * cellSize);

                    GameObject wall = Instantiate(wallPrefab, position, Quaternion.Euler(0, 90f, 0), transform);
                    wall.transform.localScale = new Vector3(cellSize, 3f, cellSize);
                    //wall.tag = "Wall"; // adding this so that i can implement commandment

                    if (wallMaterial != null)
                    {
                        MeshRenderer mr = wall.GetComponent<MeshRenderer>();
                        if (mr != null)
                            mr.material = wallMaterial;
                    }

                }
            }
        }
    }

    // had problems with fps and mouse rotation before adding this function
    // added mesh collider, so that the player doesnt go through walls
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

        // destroying all child objecst that are now in one mash
        foreach (Transform child in transform)
        {
            Destroy(child.gameObject);
        }
    }
}
