using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class SequenceManager : MonoBehaviour
{
    [System.Serializable]
    public class Level
    {
        public List<BallController> balls;
        public List<int> sequence;
    }

    public GameObject wall;
    
    public List<Level> levels;
  
    //public GameObject wall;
    public float lightDuration = 0.5f;
    public float delayBetween = 1f;

    //private List<int> sequence = new List<int> { 1, 3, 4, 2 };
    private int currentLevelIndex = 0;
    private int currentStep = 0;

    private bool sequenceCompleted = false;


    // sequence feedback
    public TextMeshProUGUI feedbackText;
    public AudioSource audioSource;
    public float feedbackDuration = 2f;

    // making sure that player can't press balls before sequences finish
    private bool canPressBalls = false; 

    void Start()
    {
        if (levels == null || levels.Count == 0)
        {
            Debug.LogError("No levels!");
            return;
        }

        if (wall != null)
            wall.SetActive(true);

        //SetupLevel(currentLevelIndex);
    }

    public void StartGame()
    {
        canPressBalls = false;
        currentLevelIndex = 0;
        SetupLevel(currentLevelIndex);
    }

    void SetupLevel(int levelIndex)
    {
        canPressBalls = false;
        sequenceCompleted = false;
        currentStep = 0;

        Level level = levels[levelIndex];
    
        foreach (var ball in level.balls)
        {
            ball.sequenceManager = this;
            ball.ResetColor();
        }

        StartSequence();
    }


    public void StartSequence()
    {
        canPressBalls = false;
        StopAllCoroutines();
        StartCoroutine(PlaySequenceWithDelay(3f));
    }

    private IEnumerator PlaySequenceWithDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        StartCoroutine(PlaySequence());
    }


    private System.Collections.IEnumerator PlaySequence()
    {
        if (sequenceCompleted)
            yield break;

        Debug.Log($"Level {currentLevelIndex + 1} - Sequence started");
        
        Debug.Log("Sequence started");
        yield return new WaitForSeconds(2f); // small pause before start

        Level level = levels[currentLevelIndex];    

        foreach (int id in level.sequence)
        {
            Debug.Log($"Lighting ball with ID: {id}");
            BallController ball = level.balls.Find(b => b.ballID == id);
            if (ball != null)
            {
                ball.LightUp(lightDuration);
                yield return new WaitForSeconds(lightDuration + delayBetween);
            }
            else
            {
                Debug.LogWarning($"Ball with ID {id} not found in level {currentLevelIndex + 1}!");
            }
        }

        canPressBalls = true;
        currentStep = 0;
    }

    public void RegisterBallPress(int ballID)
    {
        if (sequenceCompleted || !canPressBalls)
            return;

        if (sequenceCompleted)
            return;

        Level level = levels[currentLevelIndex];

        //ShowFeedback($"Pressed ball : {ballID}");
        Debug.Log($"Player pressed ball {ballID}, waiting for {level.sequence[currentStep]}");

        if (audioSource != null)
            audioSource.Play();

        if (ballID == level.sequence[currentStep])
        {
            currentStep++;
            if (currentStep >= level.sequence.Count)
            {
                Debug.Log($"Level {currentLevelIndex + 1} complete!");
                ShowFeedback($"Level {currentLevelIndex + 1} completed!");

                sequenceCompleted = true;

                //currentLevelIndex++;

                StartCoroutine(WaitAndStartNextLevel(2f));

                

            }
        }
        else
        {
            Debug.Log("Wrong! Reseting sequence!");
            ShowFeedback("Wrong! Reseting sequence!");
            StartCoroutine(PlaySequence());
            currentStep = 0;
        }
    }

    private IEnumerator WaitAndStartNextLevel(float waitTime)
    {
        yield return new WaitForSeconds(waitTime);

        currentLevelIndex++;

        if (currentLevelIndex < levels.Count)
        {
            Debug.Log($"Starting level {currentLevelIndex + 1}");
            ShowFeedback($"Starting level {currentLevelIndex + 1}");
            SetupLevel(currentLevelIndex);
        }
        else
        {
            Debug.Log("All levels finished! Lowering the wall!");
            ShowFeedback("All levels finished! Lowering the wall!");
            if (wall != null)
                wall.SetActive(false); // lowering the wall

            foreach (var lvl in levels)
            {
                foreach (var ball in lvl.balls)
                {
                    ball.LightUpPermanent();
                }
            }
        }
    }

    public void ShowFeedback(string message)
    {
        if (feedbackText != null)
        {
            feedbackText.text = message;
            feedbackText.gameObject.SetActive(true);
            CancelInvoke(nameof(HideFeedback));
            Invoke(nameof(HideFeedback), feedbackDuration);
        }
    }

    public void HideFeedback()
    {
        if (feedbackText != null)
        {
            feedbackText.gameObject.SetActive(false);
        }
    }

    public bool CanPressBalls()
    {
        return canPressBalls;
    }
}
