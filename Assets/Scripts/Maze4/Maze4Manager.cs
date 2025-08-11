using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Maze4Manager : MonoBehaviour
{
    public static Maze4Manager Instance { get; private set; }


    public float timerDuration = 180f;
    private float timer;
    private bool timerRunning = false;

    private int keysCollected = 0;
    public int totalKeysRequired = 3;

    public Transform teleportDestination;
    public GameObject player;

    private bool exitReached = false;

    public TextMeshProUGUI timerText;

    // List of items that need to be reseted
    private List<Vector3> itemStartPosition = new List<Vector3>();
    private List<GameObject> mazeItems = new List<GameObject>();

    public GameObject[] itemPrefabs;

    // Maze start trigger, because when i restart the maze, trigger doesnt resets
    public MazeStartTrigger mazeStartTrigger;

    void Start()
    {
        CacheItems();

        timer = timerDuration;

        if (player == null)
            player = GameObject.FindGameObjectWithTag("Player");

        timerText.gameObject.SetActive(false);
        UpdateTimerUI();

    }

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject); // protection from duplicates
        }
        else
        {
            Instance = this;
        }
    }

    void Update()
    {
        if (!timerRunning)
            return;

        timer -= Time.deltaTime;
        UpdateTimerUI();

        if (timer <= 0f)
        {
            timer = 0f;
            timerRunning = false;
            Debug.Log("Time expired!");
            FailMaze();
        }
    }

    public void CollectKey()
    {
        keysCollected++;
        Debug.Log("Key collected. Currently: " + keysCollected);
    }

    public int GetKeysCount()
    {
        return keysCollected;
    }

    private void CacheItems()
    {
        mazeItems.Clear();
        itemStartPosition.Clear();

        foreach (GameObject item in itemPrefabs)
        {
            mazeItems.Add(item);
            itemStartPosition.Add(item.transform.position);
        }

        Debug.Log($"Items cached. Found {mazeItems.Count} items.");
    }

    public void TryExitMaze()
    {
        if (keysCollected == totalKeysRequired)
        {
            Debug.Log("You have successfully finished the maze!");
            timerRunning = false;
        }
        else
        {
            Debug.Log("Don't have enough keys!");
            
        }
    }
    // changed fail maze teleport code, because previous one didn't always teleport me after fail
    private void FailMaze()
    {
        if (player != null && teleportDestination != null)
        {
            Rigidbody rb = player.GetComponent<Rigidbody>();
            if (rb != null)
            {
                rb.MovePosition(teleportDestination.position);
                rb.velocity = Vector3.zero;
                rb.angularVelocity = Vector3.zero;
            }
            else
            {
                player.transform.position = teleportDestination.position;
            }

            ResetMaze();

            Debug.Log($"Player position after ResetMaze(): {player.transform.position}");
            Debug.Log("You have been teleported back.");
        }
        else
        {
            Debug.LogError("Player or teleportDestination are not set!");
        }
    }


    public void ResetMaze()
    {
        // Reset timer
        timer = timerDuration;
        timerRunning = false;
        exitReached = false;

        // Reset keys
        keysCollected = 0;
        Debug.Log("Keys collected reset to 0");

        Debug.Log($"📋 mazeItems.Count = {mazeItems.Count}");

        //CacheItems();

        // Reset items
        for (int i = 0; i < mazeItems.Count; i++)
        {
            mazeItems[i].transform.position = itemStartPosition[i];
            mazeItems[i].SetActive(true);
            Debug.Log($"📦 Item {i} reset to start position {itemStartPosition[i]}");

        }

        // Reset maze start trigger
        if (mazeStartTrigger != null)
        {
            mazeStartTrigger.ResetTrigger();
            Debug.Log("Trigger reset!");
        }

        // Reset keys pickup
        for (int i = 0; i < mazeItems.Count; i++)
        {
            mazeItems[i].transform.position = itemStartPosition[i];

            KeyPickup keyPickup = mazeItems[i].GetComponent<KeyPickup>();
            if (keyPickup != null)
            {
                keyPickup.ResetPickup();
            }
            else
            {
                mazeItems[i].SetActive(true); 
            }

            Debug.Log($"Item {i} reset to start position {itemStartPosition[i]}");
        }

        UpdateTimerUI();

        Debug.Log("Maze reset!");
    }

    public void StartTimer()
    {
        timer = timerDuration;
        timerRunning = true;
        timerText.gameObject.SetActive(true);
        UpdateTimerUI();
    }


    void UpdateTimerUI()
    {
        int minutes = Mathf.FloorToInt(timer / 60f);
        int seconds = Mathf.FloorToInt(timer % 60f);
        timerText.text = string.Format("{0:00}:{1:00}", minutes, seconds);
    }


}
