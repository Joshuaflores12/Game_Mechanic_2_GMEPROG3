using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    private Transform player; // Reference to the player's transform
    public float speed;

    void Update()
    {
        // Check if the player reference is valid
        if (player != null)
        {
            MoveTowardsPlayer();
        }
        else
        {
            // If the player reference is null, try to find the player
            FindPlayer();
        }
    }

    void FindPlayer()
    {
        // You can find the player using a specific tag or other methods
        // For example, if the player has a "Player" tag:
        GameObject playerObj = GameObject.FindWithTag("Player");

        if (playerObj != null)
        {
            player = playerObj.transform;
        }
    }

    void MoveTowardsPlayer()
    {
        // Calculate the direction from the enemy to the player
        Vector3 directionToPlayer = (player.position - transform.position).normalized;

        // Move the enemy in the direction of the player
        transform.Translate(directionToPlayer * speed * Time.deltaTime);
    }
}