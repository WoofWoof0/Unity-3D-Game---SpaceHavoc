using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    // public Transform target;
    // public float smoothing = 5f;
    // public Vector3 offset = new Vector3(0, 200, -150);

    // // Update is called once per frame
    // void Update()
    // {
    //     Vector3 targetCamPos = target.position + offset;
    //     // Interpolates the camera's position towards the target position
    //     transform.position = Vector3.Lerp(transform.position, targetCamPos, smoothing* Time.deltaTime);
    //     // Maintain a 45-degree rotation
    //     transform.rotation = Quaternion.Euler(45,0,0);
    // }
    public Transform player;       // Player's transform
    public float cameraDistance = -3.0f;  // Distance behind the player
    public float heightOffset = 3.0f;

    void LateUpdate()
    {
        // Set the camera position based on the player's position, fixed Y and Z with offset
        transform.position = new Vector3(player.position.x + 7, player.position.y + heightOffset, player.position.z - cameraDistance);
    }
}
