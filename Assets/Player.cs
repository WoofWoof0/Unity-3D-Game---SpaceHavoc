using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Player : MonoBehaviour
{
    public int lifePoints = 3;
    public TMP_Text loseText;  // Use TMP_Text here instead of Text

    void Start()
    {
        loseText.enabled = false; // Disable the text initially
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("EnemyBullet")) // Assuming projectiles have the right tag
        {
            lifePoints--;
            if (lifePoints <= 0)
            {
                loseText.text = "You Lose!";
                loseText.enabled = true; // Show the lose message
                gameObject.SetActive(false); // Disable the player GameObject
            }
        }
    }
}
