using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class CommandmentManager : MonoBehaviour
{
    public List<Commandment> allCommandments; // All commandments
    private List<Commandment> activeCommandments = new List<Commandment>();

    public AudioSource audioSource;
    public TextMeshProUGUI[] commandmentTextUIs;

    public int currentMazeIndex = 0; // Maze number


    public void AssignNewCommandment()
    {


        if (currentMazeIndex == 0)
        {
            // First maze is free, no commandments
            return;
        }

        if (allCommandments.Count == 0)
        {
            Debug.LogWarning("No more commandments to add");
            return;
        }

        int randomIndex = Random.Range(0, allCommandments.Count);
        Commandment newCommandment = allCommandments[randomIndex];

        allCommandments.RemoveAt(randomIndex);
        activeCommandments.Add(newCommandment);

        ShowCommandment(newCommandment);
        Debug.Log($"Commandment assigned: ID={newCommandment.id}, Text='{newCommandment.text}'");

        PrintActiveCommandments();

    }
    public void PrintActiveCommandments()
    {
        Debug.Log("Active commandments:");

        if (activeCommandments.Count == 0)
        {
            Debug.Log("No active commandments.");
            return;
        }

        foreach (var c in activeCommandments)
        {
            Debug.Log($"ID: {c.id}, Text: {c.text}");
        }
    }


    public void ResetCommandments(List<Commandment> originalList)
    {
        allCommandments = new List<Commandment>(originalList);
        activeCommandments.Clear();
        currentMazeIndex = 0;
    }

    private void ShowCommandment(Commandment c)
    {
        Debug.Log($"Showing commandment on index {currentMazeIndex}: {c.text}");

        if (currentMazeIndex < commandmentTextUIs.Length && commandmentTextUIs[currentMazeIndex] != null)
        {
            commandmentTextUIs[currentMazeIndex].text = "New commandment: " + c.text;
        }
        else
        {
            Debug.LogWarning($"There is no UI text for currentMazeIndex={currentMazeIndex}");
        }
    }


    public bool IsCommandmentActive(int id)
    {
        return activeCommandments.Exists(c => c.id == id);
    }

    public List<Commandment> GetActiveCommandments()
    {
        return activeCommandments;
    }

    public void OnEnterNewMaze()
    {
        currentMazeIndex++;
        Debug.Log($"Entering maze number: {currentMazeIndex}");

    }


}
