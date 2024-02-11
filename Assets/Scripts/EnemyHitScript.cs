using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHitScript : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        // Check if the collision is with an object tagged as "Object"
        if (other.CompareTag("Object"))
        {
            // Destroy the enemy GameObject
            Destroy(gameObject);
        }
    }
}