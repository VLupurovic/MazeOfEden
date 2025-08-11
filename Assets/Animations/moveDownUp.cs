using UnityEngine;
using System.Collections;

public class AnimationMoveDownUp : MonoBehaviour
{
    [Tooltip("How far down the object moves")]
    public float moveDistance = 0.5f;

    [Tooltip("How long the whole down-up animation takes (seconds)")]
    public float duration = 0.5f;

    private Vector3 startPos;

    private void Start()
    {
        startPos = transform.position;
    }

    // Call this method from your action to trigger the animation
    public void PlayMoveDownUp()
    {
        StopAllCoroutines();
        StartCoroutine(MoveDownAndUp());
    }

    private IEnumerator MoveDownAndUp()
    {
        Vector3 downPos = startPos + Vector3.down * moveDistance;
        float halfDuration = duration / 2f;
        float timer = 0f;

        // Move down
        while (timer < halfDuration)
        {
            transform.position = Vector3.Lerp(startPos, downPos, timer / halfDuration);
            timer += Time.deltaTime;
            yield return null;
        }
        transform.position = downPos;

        // Move up
        timer = 0f;
        while (timer < halfDuration)
        {
            transform.position = Vector3.Lerp(downPos, startPos, timer / halfDuration);
            timer += Time.deltaTime;
            yield return null;
        }
        transform.position = startPos;
    }
}
