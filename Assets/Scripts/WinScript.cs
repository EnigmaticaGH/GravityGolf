using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;
using UnityEngine.UI;

public class WinScript : MonoBehaviour {

    public Canvas winMenu;
    public Button nextBtn;
    public Button menuBtn;
    public Text scoreText;
    public Text prevBest;
    public Text newBest;
    private int level;

    void OnDestroy()
    {
        EventHandler.win -= showWinScreen;
    }

    // Use this for initialization
    void Start ()
    {
        EventHandler.win += showWinScreen;
        winMenu.enabled = false;
        scoreText.enabled = false;
        prevBest.enabled = false;
        newBest.enabled = false;
        level = SceneManager.GetActiveScene().buildIndex - 1;
	}

    void Update()
    {
        if(scoreText.enabled)
        {
            float scoreEarned = ScoreManager.potentialEarned - ScoreManager.oldBest;
            if (scoreEarned < 0f) scoreEarned = 0f;
            scoreText.text = "Score Earned: " + scoreEarned.ToString("0") + "   Total Score: " + ScoreManager.score.ToString("0");
            if (ScoreManager.oldBest < ScoreManager.bestScore[level])
            {
                prevBest.text = "Prev. Best: " + ScoreManager.oldBest.ToString("0");
                newBest.text = "New Best: " + ScoreManager.bestScore[level].ToString("0");
            }
            else
            {
                prevBest.text = "Best: " + ScoreManager.oldBest.ToString("0");
                newBest.text = "Level Score: " + ScoreManager.potentialEarned.ToString("0");
            }
        }
    }

    public void nextPress()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void menuPress()
    {
        SceneManager.LoadScene(0);
    }

    void showWinScreen()
    {
        scoreText.enabled = true;
        winMenu.enabled = true;
        prevBest.enabled = true;
        newBest.enabled = true;
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        Time.timeScale = 0.0f;
    }
}
