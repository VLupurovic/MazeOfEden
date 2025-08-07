using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class CommandmentManager : MonoBehaviour
{
    public List<Commandment> allCommandments; // sve zapovesti
    private List<Commandment> activeCommandments = new List<Commandment>();

    public AudioSource audioSource;
    public TextMeshProUGUI[] commandmentTextUIs;

    public int currentMazeIndex = 0; // broj mazea


    public void AssignNewCommandment()
    {


        if (currentMazeIndex == 0)
        {
            // Prvi maze je slobodan, nema zapovesti
            return;
        }

        if (allCommandments.Count == 0)
        {
            Debug.LogWarning("Nema više zapovesti za dodelu");
            return;
        }

        int randomIndex = Random.Range(0, allCommandments.Count);
        Commandment newCommandment = allCommandments[randomIndex];

        allCommandments.RemoveAt(randomIndex);
        activeCommandments.Add(newCommandment);

        ShowCommandment(newCommandment);
        Debug.Log($"Dodeljena zapovest: ID={newCommandment.id}, Tekst='{newCommandment.text}'");

        PrintActiveCommandments();

    }
    public void PrintActiveCommandments()
    {
        Debug.Log("Aktivne zapovesti:");

        if (activeCommandments.Count == 0)
        {
            Debug.Log("Nema aktivnih zapovesti.");
            return;
        }

        foreach (var c in activeCommandments)
        {
            Debug.Log($"ID: {c.id}, Tekst: {c.text}");
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
        Debug.Log($"Prikazujem zapovest na indexu {currentMazeIndex}: {c.text}");

        if (currentMazeIndex < commandmentTextUIs.Length && commandmentTextUIs[currentMazeIndex] != null)
        {
            commandmentTextUIs[currentMazeIndex].text = "Nova zapovest: " + c.text;
        }
        else
        {
            Debug.LogWarning($"Ne postoji UI tekst za currentMazeIndex={currentMazeIndex}");
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
        Debug.Log($"Ulazak u maze broj: {currentMazeIndex}");

    }


}
