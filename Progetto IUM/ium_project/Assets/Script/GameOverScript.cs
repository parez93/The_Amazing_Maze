using UnityEngine;
using UnityEngine.UI;

public class GameOverScript : MonoBehaviour {

    private Button[] buttons;

    void Awake()
    {
        // Get the buttons
        buttons = GetComponentsInChildren<Button>();

        // Disable them
    }



    public void ExitToMenu()
    {
        // Reload the level
        Application.LoadLevel("menu");
    }


}
