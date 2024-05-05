using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControl : MonoBehaviour
{

    // //Player's moving speed.
    // public float speed = 10.0f;

    // // Update is called once per frame
    // void Update()
    // {
    //     float horizon = Input.GetAxis("Horizontal") * speed * Time.deltaTime;
    //     // Invert the forward direction by multiplying by -1
    //     float forward = -Input.GetAxis("Vertical") * speed * Time.deltaTime;

    //     // Update the translation to move positively along the Z-axis
    //     transform.Translate(new Vector3(horizon, forward, 0));
    // }
    public float speed = 5.0f;
    public float jumpForce = 5.0f;
    private Rigidbody rb;
    private bool isGrounded;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        float move = Input.GetAxis("Horizontal") * speed;
        transform.Translate(move * Time.deltaTime, 0, 0);

        // Check if the spacebar is pressed for jumping
        if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
            rb.AddForce(new Vector3(0, jumpForce, 0), ForceMode.Impulse);
        }
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = true;
        }
    }

    void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = false;
        }
    }
}
