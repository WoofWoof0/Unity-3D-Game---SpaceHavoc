using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControl : MonoBehaviour
{

    //Player's moving speed.
    public float speed = 10.0f;

    // Update is called once per frame
    void Update()
    {
        float horizon = Input.GetAxis("Horizontal") * speed * Time.deltaTime;
        // Invert the forward direction by multiplying by -1
        float forward = -Input.GetAxis("Vertical") * speed * Time.deltaTime;

        // Update the translation to move positively along the Z-axis
        transform.Translate(new Vector3(horizon, forward, 0));
    }
}
