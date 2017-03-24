using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;
using System.IO;
using System.Text;

public class MenuScript : MonoBehaviour {

    public Canvas quitMenu;
    public Canvas levelMenu;
    public Button playBtn;
    public Button exitBtn;
    public GameObject levelPrefab;
    public Transform parent;

	// Use this for initialization
	void Start ()
    {
        quitMenu.enabled = false;
        levelMenu.enabled = false;
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;

        Vector2 canvasSize = new Vector2(1920f, 1080f);
        Rect box = levelPrefab.GetComponent<RectTransform>().rect;
        Vector2 boxSize = new Vector2(280, box.height);
        for(int i = 0; i < SceneManager.sceneCountInBuildSettings - 1; i++)
        {
            int index = i + 1;
            GameObject levelSelector = Instantiate(levelPrefab, Vector3.zero, Quaternion.identity, parent);
            Vector3 position = new Vector3(50 + (boxSize.x / 2) + (-canvasSize.x / 2) + ((canvasSize.x / 5) * (i % 5)), (canvasSize.y / 2) - 30 - (boxSize.y / 2), 0);
            levelSelector.GetComponent<RectTransform>().localPosition = position;
            levelSelector.GetComponent<Button>().onClick.AddListener(() => LoadScene(index));
            Text[] texts = levelSelector.GetComponentsInChildren<Text>();
            foreach(Text t in texts)
            {
                if (t.tag == "Level Name")
                {
                    t.text = "Level " + index;
                } else if (t.tag == "Best Score")
                {
                    t.text = "Best Score: 0";
                }
            }
        }
    }

    void LoadScene(int index)
    {
        SceneManager.LoadScene(index);
    }

    // Update is called once per frame
    void Update ()
    {
	
	}

    public void exitPress()
    {
        quitMenu.enabled = true;
        playBtn.enabled = false;
        exitBtn.enabled = false;
    }

    public void playPress()
    {
        levelMenu.enabled = true;
    }
    public void exitGame()
    {
        Application.Quit();
    }

    public void noPress()
    {
        quitMenu.enabled = false;
        playBtn.enabled = true;
        exitBtn.enabled = true;
    }
}
