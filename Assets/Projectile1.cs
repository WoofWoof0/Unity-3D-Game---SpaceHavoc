using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile1 : MonoBehaviour
{
    public float speed = 20.0f;
    //public float lifetime = 5.0f;

    void Start()
    {
        //Destroy(gameObject, lifetime);
    }

    void Update()
    {
        // Move the projectile in the forward direction
        transform.Translate(Vector3.forward * speed * Time.deltaTime);
    }

    void OnTriggerEnter(Collider other)
    {
        // Add collision logic here if needed
        Destroy(gameObject);
        if (other.gameObject.CompareTag("Player"))  // Ensure your player GameObject has the tag "Player"
        {
            // Apply damage to player here
            Destroy(gameObject);  // Destroy the bullet
        }
        if (other.gameObject.CompareTag("Enemy"))  // Ensure your enemy GameObject has the tag "Enemy"
        {
            Destroy(other.gameObject);  // Destroy the enemy
            Destroy(gameObject);  // Destroy the projectile
        }
    }
    
}
