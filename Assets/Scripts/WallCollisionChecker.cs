using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallCollisionChecker : MonoBehaviour
{
    public bool isTouchingWall { get; private set; } = false;

    private void OnColllisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Wall"))
        {
            isTouchingWall = true;
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.CompareTag("Wall"))
        {
            isTouchingWall = false;
        }
    }
}
