using System.Collections;
using System.Collections.Generic;
using UnityEngine;

#region Serializable classes
[System.Serializable]
public class EnemyWaves 
{
    [Tooltip("time for wave generation from the moment the game started")]
    public float timeToStart;

    [Tooltip("Enemy wave's prefab")]
    public GameObject wave;
}

#endregion

public class LevelController : MonoBehaviour {

    //Serializable classes implements
    public EnemyWaves[] enemyWaves; 

    [Header("Level 2 Expansion")]
    [Tooltip("Enable or disable the second level")]
    public bool enableLevel2 = true;
    
    [Tooltip("Delay in seconds after Level 1 finishes before Level 2 starts")]
    public float delayBeforeLevel2 = 5f;
    
    [Tooltip("Wave configuration for Level 2 (harder enemies, shields, boss)")]
    public EnemyWaves[] enemyWavesLevel2;

    public GameObject powerUp;
    public float timeForNewPowerup;
    public GameObject[] planets;
    public float timeBetweenPlanets;
    public float planetsSpeed;
    List<GameObject> planetsList = new List<GameObject>();

    Camera mainCamera;   

    private void Start()
    {
        mainCamera = Camera.main;
        StartCoroutine(LevelOrchestrator());
        StartCoroutine(PowerupBonusCreation());
        StartCoroutine(PlanetsCreation());
    }

    IEnumerator LevelOrchestrator()
    {
        // --- LEVEL 1 ---
        float maxLevel1Time = 0f;
        if (enemyWaves != null)
        {
            for (int i = 0; i < enemyWaves.Length; i++) 
            {
                StartCoroutine(CreateEnemyWave(enemyWaves[i].timeToStart, enemyWaves[i].wave));
                if (enemyWaves[i].timeToStart > maxLevel1Time)
                    maxLevel1Time = enemyWaves[i].timeToStart;
            }
        }

        // --- LEVEL 2 ---
        if (enableLevel2)
        {
            // Wait for Level 1 waves to spawn + an extra delay to allow the player to clear them
            yield return new WaitForSeconds(maxLevel1Time + delayBeforeLevel2 + 10f); // 10s extra clearance time
            
            if (enemyWavesLevel2 != null)
            {
                for (int i = 0; i < enemyWavesLevel2.Length; i++) 
                {
                    StartCoroutine(CreateEnemyWave(enemyWavesLevel2[i].timeToStart, enemyWavesLevel2[i].wave));
                }
            }
        }
    }
    
    //Create a new wave after a delay
    IEnumerator CreateEnemyWave(float delay, GameObject Wave) 
    {
        if (delay != 0)
            yield return new WaitForSeconds(delay);
        if (Player.instance != null)
            Instantiate(Wave);
    }

    //endless coroutine generating 'levelUp' bonuses. 
    IEnumerator PowerupBonusCreation() 
    {
        while (true) 
        {
            yield return new WaitForSeconds(timeForNewPowerup);
            Instantiate(
                powerUp,
                //Set the position for the new bonus: for X-axis - random position between the borders of 'Player's' movement; for Y-axis - right above the upper screen border 
                new Vector2(
                    Random.Range(PlayerMoving.instance.borders.minX, PlayerMoving.instance.borders.maxX), 
                    mainCamera.ViewportToWorldPoint(Vector2.up).y + powerUp.GetComponent<Renderer>().bounds.size.y / 2), 
                Quaternion.identity
                );
        }
    }

    IEnumerator PlanetsCreation()
    {
        //Create a new list copying the arrey
        for (int i = 0; i < planets.Length; i++)
        {
            planetsList.Add(planets[i]);
        }
        yield return new WaitForSeconds(10);
        while (true)
        {
            ////choose random object from the list, generate and delete it
            int randomIndex = Random.Range(0, planetsList.Count);
            GameObject newPlanet = Instantiate(planetsList[randomIndex]);
            planetsList.RemoveAt(randomIndex);
            //if the list decreased to zero, reinstall it
            if (planetsList.Count == 0)
            {
                for (int i = 0; i < planets.Length; i++)
                {
                    planetsList.Add(planets[i]);
                }
            }
            newPlanet.GetComponent<DirectMoving>().speed = planetsSpeed;

            yield return new WaitForSeconds(timeBetweenPlanets);
        }
    }
}
