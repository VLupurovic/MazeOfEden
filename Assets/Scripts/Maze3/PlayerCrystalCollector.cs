using UnityEngine;

public class PlayerCrystalCollector : MonoBehaviour
{
    public static PlayerCrystalCollector Instance { get; private set; }

    private int crystalCount = 0;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject); // protection from duplicates
        }
        else
        {
            Instance = this;
        }
    }

    public void CollectCrystal()
    {
        crystalCount++;
        Debug.Log("Crystal collected! Total: " + crystalCount);
    }

    public int GetCrystalCount()
    {
        return crystalCount;
    }
}
