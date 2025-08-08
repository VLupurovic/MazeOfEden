using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallController : MonoBehaviour
{
    public int ballID;  // unique ID of this ball in the sequence
    private bool playerInRange = false; // is player in range to interact
    public SequenceManager sequenceManager; // reference to the sequence manager

    private bool isLit = false; // is this ball currently lit

    public Light pointLight; // reference to Point Light component (optional)

    void Update()
    {
        if (playerInRange && Input.GetKeyDown(KeyCode.E) && !isLit)
        {
            sequenceManager.RegisterBallPress(ballID);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
            playerInRange = true;
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
            playerInRange = false;
    }

    // light up the ball for a duration
    public void LightUp(float duration)
    {
        if (!isLit)
            StartCoroutine(LightUpRoutine(duration));
    }

    private System.Collections.IEnumerator LightUpRoutine(float duration)
    {
        Debug.Log($"Lighting up ball {ballID}");
        isLit = true;

        Renderer rend = GetComponent<Renderer>();
        Color original = rend.material.color;
        rend.material.color = Color.yellow;

        if (pointLight != null)
        {
            Debug.Log("Enabling point light");
            pointLight.enabled = true;
        }

        yield return new WaitForSeconds(duration);

        rend.material.color = original;

        if (pointLight != null)
        {
            Debug.Log("Disabling point light");
            pointLight.enabled = false;
        }

        isLit = false;
    }

    public void LightUpPermanent()
    {
        StopAllCoroutines();
        Renderer rend = GetComponent<Renderer>();
        rend.material.color = Color.yellow;
        isLit = true;
    }

}
