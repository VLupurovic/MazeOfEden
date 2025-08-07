using UnityEngine;

public class PlayerBookCollector : MonoBehaviour
{
    public static PlayerBookCollector Instance;

    private int collectedBooks = 0;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void CollectBook()
    {
        collectedBooks++;
        Debug.Log("Broj pokupjenih knjiga: " + collectedBooks);
    }
}
