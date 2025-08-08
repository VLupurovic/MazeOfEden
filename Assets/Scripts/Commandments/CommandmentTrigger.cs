using UnityEngine;

public class CommandmentTrigger : MonoBehaviour
{
    public CommandmentManager commandmentManager;

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("Entering trigger detected.");

        if (commandmentManager == null)
        {
            Debug.LogError("commandmentManager is NULL!");
        }
        else
        {
            commandmentManager.AssignNewCommandment();
            Debug.Log("Called AssignNewCommandment()");
            Destroy(gameObject); // Avoiding repetition

        }
    }
}
