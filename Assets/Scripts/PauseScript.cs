using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Collections;

public class PauseScript : MonoBehaviour {

    public Canvas pauseScreen;
    public Text ctrlBox;
    public Button menuBtn;
    public Button ctrlBtn;
    public Button resetBtn;

    void OnDestroy()
    {
        EventHandler.pause -= togglePauseScreen;
    }

    // Use this for initialization
    void Start ()
    {
        EventHandler.pause += togglePauseScreen;
        pauseScreen.enabled = false;
        ctrlBox.enabled = false;
    }
	
	// Update is called once per frame
	void Update () {
	
	}

    void togglePauseScreen()
    {
        ctrlBox.enabled = false;
        if(pauseScreen.enabled)
        {
            pauseScreen.enabled = false;
            //Cursor.lockState = CursorLockMode.Locked;
            //Cursor.visible = false;
            Time.timeScale = 1.0f;
        }
        else
        {
            pauseScreen.enabled = true;
            //Cursor.lockState = CursorLockMode.None;
            //Cursor.visible = true;
            Time.timeScale = 0.0f;
        }
    }

    public void menuPress()
    {
        SceneManager.LoadScene(0);
    }

    public void resetPress()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void ctrlPress()
    {
        ctrlBox.enabled = !ctrlBox.enabled;
    }
}
