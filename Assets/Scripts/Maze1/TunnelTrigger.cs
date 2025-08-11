using UnityEngine;

public class TunnelTrigger : MonoBehaviour
{
    public int tunnelIndex; 
    private bool triggered = false;

    private void OnTriggerEnter(Collider other)
    {
        if (triggered) return;

        if (other.CompareTag("Player"))
        {
            triggered = true; 
            GlobalMazeManager.Instance.OnPlayerEnterTunnel(tunnelIndex);
            Debug.Log($"Tunnel {tunnelIndex} triggered.");
        }
    }

    // resetting trigger (when resseting the game)
    public void ResetTrigger()
    {
        triggered = false;
    }
}
