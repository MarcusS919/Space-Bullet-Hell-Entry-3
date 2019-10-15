using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBasic : MonoBehaviour
{

    public Transform[] waypoints;                // Array of waypoint for the enemy to follow
    public float moveSpeed;                      // To set the enemy move speed
    public int waypointIndex;                    // To hold the waypoint number that the enemy is moving to
    public float health;                         // How much health the enemy have

   
    void Start()
    {
        // To set the movespeed of the enemy to 4 if it wasn't set
        if (moveSpeed == 0)
        {
            moveSpeed = 4;
        }
        
        // To set the health of the enemy to 3 if it wasn't set
        if (health == 0)
        {
            health = 3; 
        }

        // To set the enemy position to the position of the first waypoint
        transform.position = waypoints[waypointIndex].transform.position;
    }


    void Update()
    {

        // Move the enemy
        Move();
    }

    // Function for moving the enemy using the waypoints
    void Move()
    {
        
        // To make sure the enemy can't move if it has reach the last waypoint
        if (waypointIndex <= waypoints.Length - 1)
        {

            // Move enemy to the next waypoint
            transform.position = Vector2.MoveTowards(transform.position,
               waypoints[waypointIndex].transform.position,
               moveSpeed * Time.deltaTime);

            // Increases the waypointIndex by 1 when the enemy reaches an index to move the enemy to the next waypoint
            if (transform.position == waypoints[waypointIndex].transform.position)
            {
                waypointIndex += 1;
            }
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        // When collides with the player's projectiles, the enemy loses health and when it's health reaches 0 it dies
        if (collision.gameObject.tag == "Player_Projectile")
        {
            health -= 1;
            if (health == 0)
            {
                Destroy(gameObject);
            }
        }
    }
}
