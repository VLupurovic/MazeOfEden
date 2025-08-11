using UnityEngine;

public class HideTimerOnTrigger : MonoBehaviour
{
    public GameObject timerUI;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (timerUI != null)
                timerUI.SetActive(false);
        }
    }
}
