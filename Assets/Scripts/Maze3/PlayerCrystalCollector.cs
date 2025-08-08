using UnityEngine;

public class PlayerCrystalCollector : MonoBehaviour
{
    public static PlayerCrystalCollector Instance { get; private set; }

    private int crystalCount = 0;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject); // za�tita od duplikata
        }
        else
        {
            Instance = this;
        }
    }

    public void CollectCrystal()
    {
        crystalCount++;
        Debug.Log("Pokupio kristal! Ukupno: " + crystalCount);

        //// Ako treba da proveri� da li su sva 3 kristala pokupljena:
        //if (crystalCount >= 3)
        //{
        //    Debug.Log("Svi kristali su pokupljeni!");
        //    // Ovde mo�e� obavestiti vrata, commandmentManager, itd.
        //}
    }

    public int GetCrystalCount()
    {
        return crystalCount;
    }
}
