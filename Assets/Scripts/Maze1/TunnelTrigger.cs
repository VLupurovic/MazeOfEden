using UnityEngine;

public class TunnelTrigger : MonoBehaviour
{
    public int tunnelIndex; // postavi u inspector
    private bool triggered = false;

    private void OnTriggerEnter(Collider other)
    {
        if (triggered) return; // već aktivirano, preskoči

        if (other.CompareTag("Player"))
        {
            triggered = true; // označi da je aktivirano
            GlobalMazeManager.Instance.OnPlayerEnterTunnel(tunnelIndex);
            Debug.Log($"Tunnel {tunnelIndex} triggered.");
        }
    }

    // Ako želiš da resetuješ trigger (npr. prilikom restartovanja igre)
    public void ResetTrigger()
    {
        triggered = false;
    }
}
