using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyShooting : MonoBehaviour
{
    public Transform player;
    public GameObject projectilePrefab;
    public Transform bulletSpawnPoint;  // Reference to the bullet spawn point
    public float shootingRange = 15f;
    public float shootingInterval = 2f;
    private float lastShootTime;

    void Update()
    {
        if (player && Vector3.Distance(transform.position, player.position) <= shootingRange)
        {
            if (Time.time - lastShootTime >= shootingInterval)
            {
                ShootPlayer();
                lastShootTime = Time.time;
            }
        }
    }

    void ShootPlayer()
    {
        if (projectilePrefab && bulletSpawnPoint)
        {
            GameObject bullet = Instantiate(projectilePrefab, bulletSpawnPoint.position, bulletSpawnPoint.rotation);
            bullet.GetComponent<Rigidbody>().velocity = (player.position - bulletSpawnPoint.position).normalized * 10f; // Adjust speed as necessary
        }
    }
}
