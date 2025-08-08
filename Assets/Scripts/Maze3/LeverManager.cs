using System.Collections.Generic;
using UnityEngine;

public class LeverManager : MonoBehaviour
{
    public List<int> correctSequence = new List<int> { 1, 2, 2, 1, 3, 1 }; // lever wall combination
    private List<int> playerSequence = new List<int>();

    public GameObject wallToLower;
    public float lowerDistance = 5f;
    public float lowerSpeed = 2f;

    private bool isWallLowered = false;
    private Vector3 wallStartPos;
    private Vector3 wallTargetPos;

    void Start()
    {
        if (wallToLower != null)
        {
            wallStartPos = wallToLower.transform.position;
            wallTargetPos = wallStartPos + Vector3.down * lowerDistance;
        }
    }

    public void RegisterLeverPull(int leverID)
    {
        Debug.Log($"LeverManager: Lever {leverID} pressed.");

        if (isWallLowered)
            return;

        playerSequence.Add(leverID);

        if (!IsSequenceCorrectSoFar())
        {
            Debug.Log("Wrong order! Combination reset!.");
            playerSequence.Clear();
            return;
        }

        Debug.Log($"Order entered: {string.Join(",", playerSequence)}");

        if (playerSequence.Count == correctSequence.Count)
        {
            Debug.Log("Correct combination! Lowering the wall.");
            StartCoroutine(LowerWall());
            isWallLowered = true;
        }
    }

    private bool IsSequenceCorrectSoFar()
    {
        for (int i = 0; i < playerSequence.Count; i++)
        {
            if (playerSequence[i] != correctSequence[i])
                return false;
        }
        return true;
    }

    private System.Collections.IEnumerator LowerWall()
    {
        float progress = 0f;

        while (progress < 1f)
        {
            wallToLower.transform.position = Vector3.Lerp(wallStartPos, wallTargetPos, progress);
            progress += Time.deltaTime * lowerSpeed;
            yield return null;
        }

        wallToLower.transform.position = wallTargetPos;
        Debug.Log("Wall lowered.");
    }
}
