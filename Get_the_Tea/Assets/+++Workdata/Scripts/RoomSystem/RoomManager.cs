using System.Collections.Generic;
using UnityEngine;

public class RoomManager : MonoBehaviour
{
    public static RoomManager Instance; //to ensure that others find this 

    [Header("Room Settings")]
    public GameObject[] roomPrefabs;
    public Transform roomParent;
    private Room currentRoom;

    [Header("Player Settings")]
    public GameObject player;

    [Header("Enemies Settings")]
    public GameObject enemyPrefab;
    public float enemySpawnrate = 0.6f; // 60% chance to spawn at each point
    private List<GameObject> spawnedEnemies = new();

    [Header("Collectable Settings")]
    public GameObject collectablesPrefab;
    public float collectablesSpawnrate = 0.6f;
    public int totalCollectibles;
    private int collected;
    public int points;

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
    }

    void Start()
    {
        GetNextRoom(); // First room
    }

    // Resets Counters, loads the next room, places player at spawn, spawns enemies, spawns collectables
    public void GetNextRoom()
    {
        CleanupEnemies();
        collected = 0;
        totalCollectibles = 0;
        
        SpawnRandomRoom();
        SpawnPlayer();
        SpawnEnemies();
        SpawnCollectables();
    }

    #region Spawnfunctions

    // loads the next room
    void SpawnRandomRoom()
    {
        if (currentRoom != null)
            Destroy(currentRoom.gameObject);

        //select random room
        GameObject roomPrefab = roomPrefabs[Random.Range(0, roomPrefabs.Length)];
        //Instantialte room
        GameObject roomObject = Instantiate(roomPrefab, Vector3.zero, Quaternion.identity, roomParent);
        //get room script
        currentRoom = roomObject.GetComponent<Room>();
    }

    // places player at spawn
    void SpawnPlayer()
    {
        if (player == null) return;
        // Move player to the spawn point in new room
        Transform spawnPoint = currentRoom.spawnpoint_Player;
        if (spawnPoint != null)
        {
            player.transform.position = spawnPoint.position;
        }
    }

    // spawns enemies
    void SpawnEnemies()
    {
        Transform[] spawnPoints = currentRoom.spawnPoints_Enemies;
        if (spawnPoints == null) return;

        foreach (Transform spawn in spawnPoints)
        {
            if (Random.value <= enemySpawnrate) 
            {
                GameObject enemy = Instantiate(enemyPrefab, spawn.position, Quaternion.identity);
                spawnedEnemies.Add(enemy);
            }
        }
    }
    
    // spawns collectables
    void SpawnCollectables()
    {
        Transform[] spawnPoints = currentRoom.spawnPoints_Collectables;
        if (spawnPoints == null) return;

        foreach(Transform spawn in spawnPoints)
        {
            if(Random.value <= collectablesSpawnrate)
            {
                Instantiate(collectablesPrefab, spawn.position, Quaternion.identity);
            }
        }
    }

    #endregion



    // counts how many collectables are in a room
    public void RegisterCollectible()
    {
        totalCollectibles++;
        UIManager.Instance.UpdateCollectibles(collected, totalCollectibles);
        UIManager.Instance.UpdateScore(points);
    }

    // collects collectable
    public void CollectItem()
    {
        collected++;
        points += 10;
        Debug.Log($"Plus 10 Points! Total points now: {points}");
        UIManager.Instance.UpdateCollectibles(collected, totalCollectibles);
        UIManager.Instance.UpdateScore(points);


        if (collected >= totalCollectibles)
        {
            Debug.Log("Room complete. Opening doors...");

            foreach (Door door in currentRoom.doors)
            {
                door.OpenDoor();
            }
        }
    }


    // cleanup enemies
    void CleanupEnemies()
    {
        foreach (var enemy in spawnedEnemies)
        {
            if (enemy != null)
                Destroy(enemy);
        }
        spawnedEnemies.Clear();
    }

}
