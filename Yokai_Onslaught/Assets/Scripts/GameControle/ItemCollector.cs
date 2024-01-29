using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement; // Added this line for SceneManager

public class ItemCollector : MonoBehaviour
{
    private int SEEDS = 0;

    [SerializeField] private Text SEEDSText;
    [SerializeField] private AudioSource collectionSoundEffect;

    // Add the name of the next level here
    public string nextLevelName;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("SEED"))
        {
            collectionSoundEffect.Play();
            Destroy(collision.gameObject);
            SEEDS++;
            SEEDSText.text = "SEEDS: " + SEEDS;

            // Check if there is a next level set
            if (!string.IsNullOrEmpty(nextLevelName))
            {
                // Load the next level
                SceneManager.LoadScene(nextLevelName);
            }
        }
    }
}
