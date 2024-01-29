using UnityEngine;
using UnityEngine.UI;

public class QuitMenu : MonoBehaviour
{
    public Button quitButton;

    void Start()
    {
        // Attach the Quit function to the button's click event
        quitButton.onClick.AddListener(QuitGame);
    }

    void QuitGame()
    {
        // Quit the game (works in standalone builds)
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
            Application.Quit();
#endif
    }
}
