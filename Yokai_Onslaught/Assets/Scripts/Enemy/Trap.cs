using UnityEngine;
using UnityEngine.SceneManagement;

public class Trap : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            // Replace this with your own logic for handling player death
            KillPlayer();

            // Reset the level after a delay (you can adjust the delay as needed)
            Invoke("ResetLevel", 2f);
        }
    }

    void KillPlayer()
    {
        // Replace this with your own logic for killing the player

        Debug.Log("Player killed!");
    }

    void ResetLevel()
    {
        // Reset the level (reload the current scene)
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
