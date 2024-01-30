using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class Enemy : MonoBehaviour
{
    public float health = 20f;
    float currentHealth;
    
    public GameObject healthBarPrefab;
    GameObject healthBar;

    public float worth = 4f;

    public Transform currentWaypoint;
    public float moveSpeed = 1f;

    private void Awake()
    {
        currentHealth = health;
        Instantiate(healthBarPrefab, transform.position + new Vector3(0,0,0.25f), Quaternion.identity, transform);
    }

    private void Update()
    {
        //Move enemy to next waypoint
        transform.position = Vector3.MoveTowards(transform.position, currentWaypoint.position, moveSpeed * Time.deltaTime);
    
        // Check if the enemy has reached the waypoint
        if (Vector3.Distance(transform.position, currentWaypoint.position) < 0.01f) // Adjust threshold as needed
        {
            // Check if there's a next waypoint
            if (currentWaypoint.GetComponent<Waypoint>().nextWaypoint != null)
            {
                // Set the next waypoint as the current waypoint
                currentWaypoint = currentWaypoint.GetComponent<Waypoint>().nextWaypoint;
            }
            else
            {
                // Handle the case when there's no next waypoint (end of path)
                Debug.Log("Reached end of path!");
                Destroy(gameObject);
            }
        }
    }


    public void Hurt(float damage)
    {
        currentHealth -= damage;
        
        //did enemy die?
        if (currentHealth <= 0)
        {
            Money.Amount += worth;
            Destroy(gameObject);
        }
        
        //update health bar
        Transform pivot = healthBar.transform.Find("HealthPivot");
        Vector3 scale = pivot.localScale;
        scale.x = Mathf.Clamp(currentHealth / health, 0, 1);
        pivot.localScale = scale;
        
    }
}