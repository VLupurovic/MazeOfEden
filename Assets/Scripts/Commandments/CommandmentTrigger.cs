using UnityEngine;

public class CommandmentTrigger : MonoBehaviour
{
    public CommandmentManager commandmentManager;

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("Ulazak u trigger detektovan.");

        if (commandmentManager == null)
        {
            Debug.LogError("commandmentManager je NULL!");
        }
        else
        {
            commandmentManager.AssignNewCommandment();
            Debug.Log("Pozvana AssignNewCommandment()");
            Destroy(gameObject); // da se ne ponovi

        }
    }
}
