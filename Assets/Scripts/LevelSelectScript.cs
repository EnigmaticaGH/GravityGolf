using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Collections;

public class LevelSelectScript : MonoBehaviour {
    public void levelPress(int level)
    {
        SceneManager.LoadScene(level);
    }
}
