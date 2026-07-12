using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// This script defines 'Enemy's' health and behavior. 
/// </summary>
public class Enemy : MonoBehaviour {

    #region FIELDS
    [Tooltip("Health points in integer")]
    public int health;

    [Tooltip("Shield points in integer")]
    public int shield;

    [Tooltip("Visual object for the shield")]
    public GameObject shieldVisual;

    [Tooltip("Enemy's projectile prefab")]
    public GameObject Projectile;

    [Tooltip("VFX prefab generating after destruction")]
    public GameObject destructionVFX;
    public GameObject hitEffect;
    
    [HideInInspector] public int shotChance; //probability of 'Enemy's' shooting during tha path
    [HideInInspector] public float shotTimeMin, shotTimeMax; //max and min time for shooting from the beginning of the path
    
    private float shotTimer;
    private bool hasShot = false;
    #endregion

    private void Start()
    {
        shotTimer = Random.Range(shotTimeMin, shotTimeMax);
    }

    private void Update()
    {
        if (!hasShot)
        {
            shotTimer -= Time.deltaTime;
            if (shotTimer <= 0)
            {
                ActivateShooting();
                hasShot = true;
            }
        }
    }

    //coroutine making a shot
    void ActivateShooting() 
    {
        if (Random.value < (float)shotChance / 100)                             //if random value less than shot probability, making a shot
        {                         
            GameObject proj = PoolingController.instance.GetPoolingObject(Projectile);
            proj.transform.position = gameObject.transform.position;
            proj.transform.rotation = Quaternion.identity;
            proj.SetActive(true);
        }
    }

    //method of getting damage for the 'Enemy'
    public void GetDamage(int damage) 
    {
        if (shield > 0)
        {
            shield -= damage;
            if (shield < 0)
            {
                damage = -shield;
                shield = 0;
            }
            else
            {
                damage = 0;
            }
            
            // Turn off visual if shield is destroyed
            if (shield <= 0 && shieldVisual != null)
            {
                shieldVisual.SetActive(false);
            }
        }

        if (damage > 0)
        {
            health -= damage;           //reducing health for damage value, if health is less than 0, starting destruction procedure
        }

        if (health <= 0)
            Destruction();
        else
        {
            if (hitEffect != null && PoolingController.instance != null)
            {
                GameObject hit = PoolingController.instance.GetPoolingObject(hitEffect);
                hit.transform.position = transform.position;
                hit.transform.rotation = Quaternion.identity;
                hit.SetActive(true);
            }
        }
    }    

    //if 'Enemy' collides 'Player', 'Player' gets the damage equal to projectile's damage value
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            if (Projectile.GetComponent<Projectile>() != null)
                Player.instance.GetDamage(Projectile.GetComponent<Projectile>().damage);
            else
                Player.instance.GetDamage(1);
        }
    }

    //method of destroying the 'Enemy'
    void Destruction()                           
    {        
        if (destructionVFX != null && PoolingController.instance != null)
        {
            GameObject vfx = PoolingController.instance.GetPoolingObject(destructionVFX);
            vfx.transform.position = transform.position;
            vfx.transform.rotation = Quaternion.identity;
            vfx.SetActive(true);
        }
        gameObject.SetActive(false);
    }
}
