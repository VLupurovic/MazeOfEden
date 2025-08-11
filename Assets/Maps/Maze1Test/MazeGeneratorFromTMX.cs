using System.Xml;
using System.IO;
using UnityEngine;

public class MazeGeneratorFromTMX : MonoBehaviour
{
    public GameObject wallPrefab;
    public string tmxFileName = "Maze1";  // ime fajla sa ekstenzijom

    public float tileSize = 1f; // veličina jedne ćelije u svetu

    void Start()
    {
        string path = Path.Combine(Application.streamingAssetsPath, tmxFileName + ".tmx");

        if (!File.Exists(path))
        {
            Debug.LogError("TMX fajl nije pronađen na putanji: " + path);
            return;
        }

        string tmxText = File.ReadAllText(path);

        XmlDocument xmlDoc = new XmlDocument();
        xmlDoc.LoadXml(tmxText);

        // Pronađi layer koji sadrži mapu (obično prvi layer)
        XmlNode layerNode = xmlDoc.SelectSingleNode("//layer/data");

        if (layerNode == null)
        {
            Debug.LogError("Layer data nije pronađen u TMX fajlu!");
            return;
        }

        // Podaci su obično u CSV formatu, jedan broj za svaki tile
        string csvData = layerNode.InnerText.Trim();

        string[] tileIds = csvData.Split(',');

        // Dimenzije mape
        XmlNode layer = xmlDoc.SelectSingleNode("//layer");
        int width = int.Parse(layer.Attributes["width"].Value);
        int height = int.Parse(layer.Attributes["height"].Value);

        // Iteriraj kroz sve tile-ove
        for (int y = 0; y < height; y++)
        {
            for (int x = 0; x < width; x++)
            {
                int index = y * width + x;
                int tileId = int.Parse(tileIds[index].Trim());

                // Pretpostavimo da zid ima tileId > 0, a prazno je 0
                if (tileId > 0)
                {
                    // Instantiate wallPrefab na poziciji
                    Vector3 pos = new Vector3(x * tileSize, 0, -y * tileSize);
                    Instantiate(wallPrefab, pos, Quaternion.identity, this.transform);
                }
            }
        }
    }
}
