using System.Collections;
using System.Collections.Generic;
using UnityEngine;

#region Serializable classes
[System.Serializable]
public class EnemyWaves 
{
    [Tooltip("time for wave generation from the moment the level started")]
    public float timeToStart;

    [Tooltip("Enemy wave's prefab")]
    public GameObject wave;
}

[System.Serializable]
public class LevelData
{
    [Tooltip("Delay before this level starts (after the previous level is cleared)")]
    public float delayBeforeLevel = 5f;
    [Tooltip("Waves for this level")]
    public EnemyWaves[] enemyWaves;
}
#endregion

public class LevelController : MonoBehaviour {

    public List<LevelData> levels = new List<LevelData>();
    
    public GameObject powerUp;
    public float timeForNewPowerup;
    public GameObject[] planets;
    public float timeBetweenPlanets;
    public float planetsSpeed;
    
    private List<GameObject> planetsList = new List<GameObject>();
    private Camera mainCamera;   

    // Timers & State for Levels
    private int currentLevelIndex = 0;
    private float levelTimer = 0f;
    private int currentWaveIndex = 0;
    private bool isWaitingForNextLevel = false;

    // Timers for Powerups and Planets
    private float powerupTimer = 0f;
    private float planetTimer = 0f;

    private void Start()
    {
        mainCamera = Camera.main;
        
        // Initialize planets list
        for (int i = 0; i < planets.Length; i++)
        {
            planetsList.Add(planets[i]);
        }
        
        // Set initial delays
        planetTimer = 10f;
        powerupTimer = timeForNewPowerup;

        if (levels.Count > 0)
        {
            isWaitingForNextLevel = true;
            levelTimer = levels[0].delayBeforeLevel;
        }
    }

    private void Update()
    {
        UpdateLevels();
        UpdatePowerups();
        UpdatePlanets();
    }

    private void UpdateLevels()
    {
        if (currentLevelIndex >= levels.Count) return;

        if (isWaitingForNextLevel)
        {
            levelTimer -= Time.deltaTime;
            if (levelTimer <= 0)
            {
                isWaitingForNextLevel = false;
                levelTimer = 0f;
                currentWaveIndex = 0;
            }
        }
        else
        {
            // We are actively spawning waves in the current level
            LevelData currentLevel = levels[currentLevelIndex];
            
            // Advance timer forwards for wave spawning logic
            levelTimer += Time.deltaTime;

            while (currentWaveIndex < currentLevel.enemyWaves.Length && 
                   levelTimer >= currentLevel.enemyWaves[currentWaveIndex].timeToStart)
            {
                if (Player.instance != null)
                {
                    GameObject waveObj = PoolingController.instance.GetPoolingObject(currentLevel.enemyWaves[currentWaveIndex].wave);
                    waveObj.transform.position = currentLevel.enemyWaves[currentWaveIndex].wave.transform.position;
                    waveObj.transform.rotation = currentLevel.enemyWaves[currentWaveIndex].wave.transform.rotation;
                    waveObj.SetActive(true);
                }
                currentWaveIndex++;
            }

            // Check if level is complete (all waves spawned)
            if (currentWaveIndex >= currentLevel.enemyWaves.Length)
            {
                currentLevelIndex++;
                if (currentLevelIndex < levels.Count)
                {
                    isWaitingForNextLevel = true;
                    // Add 10 seconds of extra clearance time for the player to defeat the remaining enemies
                    levelTimer = levels[currentLevelIndex].delayBeforeLevel + 10f; 
                }
            }
        }
    }

    private void UpdatePowerups()
    {
        powerupTimer -= Time.deltaTime;
        if (powerupTimer <= 0)
        {
            GameObject powerUpObj = PoolingController.instance.GetPoolingObject(powerUp);
            powerUpObj.transform.position = new Vector2(
                Random.Range(PlayerMoving.instance.borders.minX, PlayerMoving.instance.borders.maxX), 
                mainCamera.ViewportToWorldPoint(Vector2.up).y + powerUpObj.GetComponent<Renderer>().bounds.size.y / 2);
            powerUpObj.transform.rotation = Quaternion.identity;
            powerUpObj.SetActive(true);

            powerupTimer = timeForNewPowerup;
        }
    }

    private void UpdatePlanets()
    {
        planetTimer -= Time.deltaTime;
        if (planetTimer <= 0)
        {
            int randomIndex = Random.Range(0, planetsList.Count);
            
            GameObject newPlanet = PoolingController.instance.GetPoolingObject(planetsList[randomIndex]);
            newPlanet.transform.position = planetsList[randomIndex].transform.position;
            newPlanet.transform.rotation = planetsList[randomIndex].transform.rotation;

            planetsList.RemoveAt(randomIndex);
            
            if (planetsList.Count == 0)
            {
                for (int i = 0; i < planets.Length; i++)
                {
                    planetsList.Add(planets[i]);
                }
            }
            newPlanet.GetComponent<DirectMoving>().speed = planetsSpeed;
            newPlanet.SetActive(true);

            planetTimer = timeBetweenPlanets;
        }
    }
}
