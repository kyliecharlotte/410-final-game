using UnityEngine;
using System.Collections.Generic;

public class HealthPickupSpawner : MonoBehaviour
{
    public List<GameObject> healthPickupPrefabs; // Prefabs of candies
    public List<Transform> spawnPoints;          // Pre-defined spawn locations
    public GameObject player;                    // Reference to the player

    public int maxHealth = 5;
    public float spawnCooldown = 10f; // seconds between spawn attempts
    private float spawnTimer = 0f;

    void Awake()
    {
        // Automatically find all spawn points with the tag "SpawnPoint"
        GameObject[] spawnPointObjects = GameObject.FindGameObjectsWithTag("SpawnPoint");
        spawnPoints = new List<Transform>();

        foreach (GameObject obj in spawnPointObjects)
        {
            spawnPoints.Add(obj.transform);
        }

        if (spawnPoints.Count == 0)
        {
            Debug.LogWarning("No spawn points found! Did you forget to tag them?");
        }
    }


    void Update()
    {
        // Player_Stats playerStats = player.GetComponent<Player_Stats>();
        // if (playerStats == null) return;

        // int maxHealth = (int)playerStats.healthStat.MaxVal;
        // int currentHealth = (int)(playerStats.healthBarUI.targetFill * maxHealth);
        // int pickupsNeeded = maxHealth - currentHealth;

        // spawnTimer += Time.deltaTime;

        // if (spawnTimer >= spawnCooldown && pickupsNeeded > 0)
        // {
        //     SpawnBasedOnHealth(pickupsNeeded);
        //     spawnTimer = 0f;
        // }
    }

    public void SpawnBasedOnHealth()
    {

        Player_Stats playerStats = player.GetComponent<Player_Stats>();
        if (playerStats == null) return;

        // int currentHealth = playerStats.health;
        int maxHealth = (int)playerStats.healthStat.MaxVal;
        int currentHealth = (int)(playerStats.healthBarUI.targetFill * maxHealth);
        // int currentHealth = playerStats.healthStat.currentVal;

        // int pickupsToSpawn = Mathf.Clamp(maxHealth - currentHealth, 0, spawnPoints.Count);
        int pickupsToSpawn = maxHealth - currentHealth;

        Debug.Log("Current Health: " + currentHealth);
        Debug.Log("Max Health:" + maxHealth);
        Debug.Log("Pickups to spawn: " + pickupsToSpawn);


        // Shuffle spawn points
        // int spawned = pickupsToSpawn;
        for (int i = 0; i < spawnPoints.Count; i++)
        {
            int rand = Random.Range(i, spawnPoints.Count);
            Transform temp = spawnPoints[i];
            spawnPoints[i] = spawnPoints[rand];
            spawnPoints[rand] = temp;
        }

        // Spawn pickups
        // for (int i = 0; i < pickupsToSpawn; i++)
        // {
        //     GameObject randomPrefab = healthPickupPrefabs[Random.Range(0, healthPickupPrefabs.Count)];
        //     // Instantiate(randomPrefab, spawnPoints[i].position, Quaternion.identity);
        //     GameObject newObject = Instantiate(randomPrefab, spawnPoints[i].position, Quaternion.identity);
        //     // newObject.transform.SetParent(this.transform);

        // }
        


        int spawned = 0;
        float checkRadius = 0.5f; // Adjust to match pickup size

        for (int i = 0; i < spawnPoints.Count && spawned < pickupsToSpawn; i++)
        {
            Transform point = spawnPoints[i];

            // Check for existing pickups using overlap sphere
            Collider[] colliders = Physics.OverlapSphere(point.position, checkRadius);
            bool occupied = false;

            foreach (var col in colliders)
            {
                if (col.CompareTag("Candy")) // Your pickup prefabs must have this tag
                {
                    occupied = true;
                    break;
                }
            }

            if (!occupied)
            {
                GameObject prefab = healthPickupPrefabs[Random.Range(0, healthPickupPrefabs.Count)];
                GameObject newObj = Instantiate(prefab, point.position, Quaternion.identity);
                newObj.tag = "Candy"; // Make sure it's tagged so you can detect it next time
                spawned++;
            }
        }
    }

    // void Update()
    // {
    //     Player_Stats playerStats = player.GetComponent<Player_Stats>();
    //     if (playerStats == null) return;
    //     // Example trigger: press H to force a spawn check
    //     int maxHealth = (int)playerStats.healthStat.MaxVal;
    //     int currentHealth = (int)(playerStats.healthBarUI.targetFill * maxHealth);

    //     int timeToSpawn = maxHealth - currentHealth;

    //     for (int i = 0; i < timeToSpawn; i++)
    //     {
    //         SpawnBasedOnHealth();
    //     }


    //         // if (Input.GetKeyDown(KeyCode.H))
    //         // {

    //         //     SpawnBasedOnHealth();
    //         // }

    //     // TODO: make it so that it is dependent on the players current health!!
    // }
}
