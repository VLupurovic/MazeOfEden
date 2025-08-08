using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SequenceManager : MonoBehaviour
{
    public List<BallController> balls;
    public GameObject wall;
    public float lightDuration = 0.5f;
    public float delayBetween = 1f;

    private List<int> sequence = new List<int> { 1, 3, 4, 2 };
    private int currentStep = 0;

    private bool sequenceCompleted = false;

    void Start()
    {
        foreach (var ball in balls)
        {
            ball.sequenceManager = this;
        }
    }

    public void StartSequence()
    {
        StopAllCoroutines();
        StartCoroutine(PlaySequence());
    }


    private System.Collections.IEnumerator PlaySequence()
    {
        if (sequenceCompleted)
            yield break;

        Debug.Log("Sequence started");
        yield return new WaitForSeconds(2f); // small pause before start
        foreach (int id in sequence)
        {
            Debug.Log($"Lighting ball with ID: {id}");
            BallController ball = balls.Find(b => b.ballID == id);
            if (ball != null)
            {
                ball.LightUp(lightDuration);
                yield return new WaitForSeconds(lightDuration + delayBetween);
            }
            else
            {
                Debug.LogWarning($"Ball with ID {id} not found!");
            }
        }
        currentStep = 0;
    }

    public void RegisterBallPress(int ballID)
    {
        if (sequenceCompleted)
            return;

        Debug.Log($"Player pressed ball {ballID}, waiting {sequence[currentStep]}");    

        if (ballID == sequence[currentStep])
        {
            currentStep++;
            if (currentStep >= sequence.Count)
            {
                Debug.Log("Sequence correct! Lowering the wall!");
                wall.SetActive(false);
                sequenceCompleted = true;

                foreach (var ball in balls)
                {
                    ball.LightUpPermanent();
                }
            }
        }
        else
        {
            Debug.Log("Wrong! Reseting sequence!");
            StartCoroutine(PlaySequence());
        }
    }
}
