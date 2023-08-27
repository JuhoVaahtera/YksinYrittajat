using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAI : MonoBehaviour
{
    public Transform player; // Reference to the player's transform
    public float moveSpeed = 3.0f; // Speed at which the enemy moves towards the player
    public float stoppingDistance = 2.0f; // Distance at which the enemy stops

    private void Update()
    {
        // Check if the player reference is set
        if (player != null)
        {
            // Calculate the direction from the enemy to the player
            Vector3 direction = player.position - transform.position;
            float distanceToPlayer = direction.magnitude;

            // Check if the enemy is farther from the player than the stopping distance
            if (distanceToPlayer > stoppingDistance)
            {
                direction.Normalize();

                // Move the enemy towards the player
                transform.Translate(direction * moveSpeed * Time.deltaTime);
            }
            else
            {
                // Enemy is close to the player, so stop moving
                // You can also add attacking logic here
            }
        }
    }
}
