using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAI : MonoBehaviour
{
    public Transform player; // Reference to the player's transform
    public float moveSpeed = 6.0f; // Speed at which the enemy moves towards the player
    public float stoppingDistance = 2.0f; // Distance at which the enemy stops
    public float detectionDistance = 10.0f; // Extended detection range
    public float searchDuration = 3.0f; // Duration of the search when player is lost
    public LayerMask obstacleLayer; // Layer mask to detect obstacles

    private bool isSearching = false; // Flag to indicate whether the enemy is currently searching
    private float searchTimer = 0.0f; // Timer for how long the enemy should search
    private Vector3 searchDirection; // Direction to move while searching
    private Vector3 lastKnownPlayerPosition; // Store the last known player position

    private void Start()
    {
        lastKnownPlayerPosition = Vector3.zero; // Initialize to an arbitrary value
    }

    private void Update()
    {
        // Check if the player reference is set
        if (player != null)
        {
            // Calculate the direction from the enemy to the player
            Vector3 direction = player.position - transform.position;
            direction.y = 0; // Restrict movement along the Y-axis
            float distanceToPlayer = direction.magnitude;

            // If not searching, try to move towards the player
            if (!isSearching && distanceToPlayer <= detectionDistance) // Check within detection range
            {
                // Cast a ray to check for obstacles in the path of the enemy
                if (!Physics.Raycast(transform.position + Vector3.up * 0.5f, direction, distanceToPlayer, obstacleLayer))
                {
                    // No obstacle detected, move the enemy towards the player
                    direction.Normalize();
                    transform.Translate(direction * moveSpeed * Time.deltaTime);

                    // Check if the distance is less than or equal to stoppingDistance
                    if (distanceToPlayer <= stoppingDistance)
                    {
                        // Stop moving when the enemy is close to the player
                        transform.position = player.position - direction * stoppingDistance;
                    }

                    // Update the last known player position
                    lastKnownPlayerPosition = player.position;
                }
                else
                {
                    // Obstacle detected, start searching
                    isSearching = true;
                    searchTimer = searchDuration;
                    // Choose a random search direction
                    searchDirection = new Vector3(Random.Range(-1f, 1f), 0f, Random.Range(-1f, 1f)).normalized;
                }
            }
            else
            {
                // Enemy is searching
                searchTimer -= Time.deltaTime;

                // If search time is over, stop searching and return to seeking the player
                if (searchTimer <= 0)
                {
                    isSearching = false;

                    // Move towards the last known player position to resume seeking
                    direction = lastKnownPlayerPosition - transform.position;
                    direction.y = 0; // Restrict movement along the Y-axis
                    direction.Normalize();
                    transform.Translate(direction * moveSpeed * Time.deltaTime);
                }
                else
                {
                    // Continue searching by moving in the chosen direction
                    // Cast a ray to check for obstacles while searching
                    if (!Physics.Raycast(transform.position + Vector3.up * 0.5f, searchDirection, moveSpeed * Time.deltaTime, obstacleLayer))
                    {
                        transform.Translate(searchDirection * moveSpeed * Time.deltaTime);
                    }
                    else
                    {
                        // Obstacle detected while searching, change search direction
                        searchDirection = new Vector3(Random.Range(-1f, 1f), 0f, Random.Range(-1f, 1f)).normalized;
                    }
                }
            }
        }
        else
        {
            // Player reference is not set, move towards the last known player position
            if (lastKnownPlayerPosition != Vector3.zero)
            {
                Vector3 direction = lastKnownPlayerPosition - transform.position;
                direction.y = 0; // Restrict movement along the Y-axis
                direction.Normalize();
                transform.Translate(direction * moveSpeed * Time.deltaTime);
            }
        }
    }
}
