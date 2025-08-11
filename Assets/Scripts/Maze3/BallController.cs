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

    private Color originalColor;
    public Color highlightColor = Color.yellow;
    public float highlightDuration = 0.7f;
    private Coroutine highlightCoroutine;

    void Start()
    {
        Renderer rend = GetComponent<Renderer>();
        originalColor = rend.material.color;
    }

    void Update()
    {
        if (playerInRange && Input.GetKeyDown(KeyCode.E) && !isLit)
        {
            if (sequenceManager != null && sequenceManager.CanPressBalls())
            {
                Highlight(); 
                sequenceManager.RegisterBallPress(ballID);
            }
            else
            {
                Debug.Log("Can't press balls right now!");
            }

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

    public void Highlight()
    {
        if (highlightCoroutine != null)
            StopCoroutine(highlightCoroutine);

        highlightCoroutine = StartCoroutine(HighlightCoroutine());
    }

    private IEnumerator HighlightCoroutine()
    {
        Renderer rend = GetComponent<Renderer>();

        rend.material.color = highlightColor;
        yield return new WaitForSeconds(highlightDuration);
        rend.material.color = originalColor;


        highlightCoroutine = null;
    }

    public void ResetColor()
    {
        Renderer rend = GetComponent<Renderer>();
        rend.material.color = originalColor;
        isLit = false;
    }

}
