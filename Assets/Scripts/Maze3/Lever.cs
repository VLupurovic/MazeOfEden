using UnityEngine;

public class Lever : MonoBehaviour
{
    public int leverID; // 1, 2, 3
    public LeverManager leverManager;

    public void PullLever()
    {
        Debug.Log($"Lever {leverID} pressed.");
        leverManager.RegisterLeverPull(leverID);
    }
}
