using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CommandmentChecker : MonoBehaviour
{
    public CommandmentManager commandmentManager;
    private bool hasBrokenCommandment = false;


    void Update()
    {
        if (hasBrokenCommandment) return;

        foreach (var c in commandmentManager.GetActiveCommandments())
        {
            switch (c.id)
            {
                case 1:
                    if (Input.GetKeyDown(KeyCode.A))
                    {
                        hasBrokenCommandment = true;
                        Debug.Log("Zapovest prekrsena: Ne idi ulevo!");
                    }
                    break;

                case 2:
                    if (Input.GetKeyDown(KeyCode.Space))
                    {
                        hasBrokenCommandment = true;
                        Debug.Log("Zapovest prekrsena: Ne skaci!");
                    }
                    break;

                case 3:
                    if (Input.GetKeyDown(KeyCode.LeftShift))
                    {
                        hasBrokenCommandment = true;
                        Debug.Log("Zapovest prekrsena: Ne trci!");
                    }
                    break;

                case 6:
                    if (Input.GetKeyDown(KeyCode.T))
                    {
                        hasBrokenCommandment = true;
                        Debug.Log("Zapovest prekrsena: Ne koristi svetlost!");
                    }
                    break;

                case 10:
                    if (Input.GetKeyDown(KeyCode.S))
                    {
                        hasBrokenCommandment = true;
                        Debug.Log("Zapovest prekrsena: Ne idi unazad!");
                    }
                    break;

            }
        }

    }
}
