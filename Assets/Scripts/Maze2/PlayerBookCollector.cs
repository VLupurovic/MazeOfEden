using UnityEngine;

public class PlayerBookCollector : MonoBehaviour
{
    public static PlayerBookCollector Instance;

    private int collectedBooks = 0;

    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
        }
        else
        {
            Instance = this;
        }
    }

    public void CollectBook()
    {
        collectedBooks++;
        Debug.Log("Number of collected books: " + collectedBooks);
    }

    public int GetBookCount()
    {
        return collectedBooks;
    }
}
