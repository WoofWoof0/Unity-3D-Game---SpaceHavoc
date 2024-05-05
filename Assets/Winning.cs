using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Winning : MonoBehaviour
{
    public TMP_Text winText;  // Make sure this is TMP_Text for TextMeshPro

    void Start() {
        winText.enabled = false; // Example use, assuming you have a public TMP_Text field assigned in the Inspector
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))  // Make sure your player GameObject has the tag "Player"
        {
            winText.text = "You Won!";
            winText.enabled = true;  // Show the win message when the player touches the platform
        }
    }
}
