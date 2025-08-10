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

    private List<Commandment> originalCommandments = new List<Commandment>();


    void Start()
    {
        originalCommandments = new List<Commandment>(allCommandments);
    }

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


    public void ResetCommandments()
    {
        allCommandments = new List<Commandment>(originalCommandments);
        activeCommandments.Clear();
        currentMazeIndex = 0;
    }

   private void ShowCommandment(Commandment c)
{
    Debug.Log($"Showing commandment on index {currentMazeIndex}: {c.text}");

    if (currentMazeIndex < commandmentTextUIs.Length && commandmentTextUIs[currentMazeIndex] != null)
    {
        var uiText = commandmentTextUIs[currentMazeIndex];
        uiText.gameObject.SetActive(true);  // Aktiviraj da sigurno bude vidljiv
        uiText.text = "New commandment: " + c.text;
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

        if (activeCommandments.Count > 0)
        {
            Commandment lastCmd = activeCommandments[activeCommandments.Count - 1];
            ShowCommandment(lastCmd);
        }
    }

    public void ResetActiveCommandments()
    {
        activeCommandments.Clear();
    }

    public void ActivateCommandmentById(int id)
    {
        Commandment cmd = allCommandments.Find(c => c.id == id); 
        if (cmd != null)
        {
            activeCommandments.Add(cmd);
            allCommandments.Remove(cmd);
            ShowCommandment(cmd);
            Debug.Log($"Restored commandment: ID = {cmd.id}, Text = '{cmd.text}'");
        }
        else
        {
            Debug.LogWarning($"Commandment with ID = {id} not found in allCommandments");
        }
    }

    public int GetCurrentMazeIndex()
    {
        return currentMazeIndex;
    }


}
