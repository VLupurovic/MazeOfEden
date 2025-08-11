using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GlobalMazeManager : MonoBehaviour
{
    public static GlobalMazeManager Instance;

    [Header("Player and Teleports")]
    public GameObject player;
    public Transform[] tunnelSpawnPoints; // if player fails, respawn them at previous tunnel

    [Header("Managers")]
    public CommandmentManager commandmentManager;
    public Maze4Manager maze4Manager;


    [Header("Progress Tracking")]
    // track how many commandments and tunnels
    // when you enter new tunnel you get new commandment, so that means tunnelNumber == commandmentNumber
    // and if you fail with 2 commandments, you respawn at tunnelNumber 2
    public int commandmentsNumber = 0;
    public int tunnelNumber = 0;

    private List<int> savedActiveDommandmentIDs = new List<int>();
    private CommandmentChecker commandmentChecker;

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);

        if (commandmentChecker == null)
            commandmentChecker = FindObjectOfType<CommandmentChecker>();
    }

    private void SaveActiveCommandments()
    {
        savedActiveDommandmentIDs.Clear();
        foreach (var cmd in commandmentManager.GetActiveCommandments())
        {
            savedActiveDommandmentIDs.Add(cmd.id);
        }
    }

    private void RestoreActiveCommandments()
    {
        commandmentManager.ResetActiveCommandments();

        foreach (int id in savedActiveDommandmentIDs)
        {
            commandmentManager.ActivateCommandmentById(id);
        }
    }

    // called when maze is completed
    public void CompleteMaze()
    {
        tunnelNumber++;
        commandmentsNumber++;

        commandmentManager.currentMazeIndex = tunnelNumber;
        //commandmentManager.AssignNewCommandment();

        Debug.Log($"Maze completed! Now tunnel = {tunnelNumber}, commandments = {commandmentsNumber}");
    }

    public void OnPlayerEnterTunnel(int tunnelIndex)
    {
        // Postavi tunnelNumber na tunnelIndex (ili +1 ako ti treba 1-based indeks)
        tunnelNumber = tunnelIndex;

        // Postavi currentMazeIndex u CommandmentManager na tunnelNumber
        commandmentManager.currentMazeIndex = tunnelNumber;

        // Dodeli novi commandment
        commandmentManager.AssignNewCommandment();

        Debug.Log($"Player entered tunnel {tunnelIndex}, assigned commandment for mazeIndex {tunnelNumber}");
    }


    // called when maze is failed/commandment is broken
    public void FailCurrentMaze()
    {
        Debug.Log($"Maze failed! Returning to tunnel: {tunnelNumber}");

        if (commandmentChecker != null)
        {
            commandmentChecker.ResetBrokenFlag();
        }

        if (commandmentsNumber > 0 && commandmentsNumber <= tunnelSpawnPoints.Length)
        {
            player.transform.position = tunnelSpawnPoints[commandmentsNumber - 1].position; // mozda tunnelNumber
        }
        else
        {
            Debug.LogError("Invalid tunnel index for teleport");

            if (tunnelSpawnPoints.Length > 0)
                player.transform.position = tunnelSpawnPoints[0].position;
        }

        SaveActiveCommandments();

        ResetCommandments();

        RestoreActiveCommandments();

        // if in Maze4, also reset Maze4Manager
        if (maze4Manager != null && maze4Manager.gameObject.activeInHierarchy)
        {
            maze4Manager.ResetMaze();
        }
    }

    private void ResetCommandments()
    {
        //var active = commandmentManager.GetActiveCommandments();
        //if (active.Count > 0)
        //{
        //    foreach (var c in active)
        //    {
        //        Debug.Log($"Resetting commandment ID = {c.id}, Text = {c.text}");
        //    }
        //}
        //else
        //{
        //    Debug.Log("No active commandments to reset.");
        //}

        //commandmentManager.ResetCommandments(new List<Commandment>());

        Debug.Log("Resseting commandments and progress.");

        commandmentManager.ResetCommandments();
        commandmentManager.currentMazeIndex = commandmentsNumber;
    }

}
