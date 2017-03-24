using System.IO;
using System.Text;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ScoreManager : MonoBehaviour {

    public static float score = 0f;
    //Keep track of personal best score for each level (not including main menu)
    public static float[] bestScore;
    public static float oldBest;
    public static float potentialEarned;
    public float parTime;
    public float parScore;
    float earnedScore;
    int level;
    EventHandler EventInitializer;

    void OnDestroy()
    {
        EventHandler.win -= calcScore;
    }

	// Use this for initialization
	void Start ()
    {
        bestScore = new float[SceneManager.sceneCountInBuildSettings - 1];
        earnedScore = 0f;
        potentialEarned = 0f;

        for (int i = 0; i < bestScore.Length; i++)
        {
            bestScore[i] = 0f;
        }

        level = SceneManager.GetActiveScene().buildIndex - 1;

        if(SceneManager.GetActiveScene().buildIndex > 0)
        {
            bestScore[level] = 0f;
            EventInitializer = GameObject.Find("EventInitializer").GetComponent<EventHandler>();
        }
        else
        {
            if (readScoreFile())
            {
                Debug.Log("Successfully loaded scores");
            }
        }
        EventHandler.win += calcScore;
	}
	
	// Update is called once per frame
	void Update ()
    {
	}

    public static void resetScore()
    {
        score = 0f;
    }

    public float getEarnedScore()
    {
        return earnedScore;
    }

    void calcScore()
    {
        if (readScoreFile())
        {
            Debug.Log("Successfully loaded scores");
            Debug.Log("The current best score for level " + level + " is " + bestScore[level]);
            oldBest = bestScore[level];
        }

        if (EventInitializer.getLevelType() == EventHandler.levelTypes.hole)
        {
            //hole-related score work here
            earnedScore = (parScore * (parTime/UIScript.totalTime));
            potentialEarned = earnedScore;
        }
        if (EventInitializer.getLevelType() == EventHandler.levelTypes.endurance)
        {
            //endurance-related score work here
            earnedScore = (parScore * (UIScript.totalTime / parTime));
            potentialEarned = earnedScore;
        }
        if (earnedScore > bestScore[level])
        {
            float temp = earnedScore;
            earnedScore -= bestScore[level];
            bestScore[level] = temp;
        }
        else
        {
            earnedScore = 0f;
        }
        score += earnedScore;
        writeScoreFile();
    }

    bool readScoreFile()
    {
        /*try
        {
            string jsonScores = File.ReadAllText(scoreSavePath + "/" + scoreSaveFile);
            LocalSaveData local = JsonUtility.FromJson<LocalSaveData>(jsonScores);
            bestScore = savedScores;
            Debug.Log(bestScore);
            return true;
        }
        catch (System.Exception e)
        {
            Debug.Log(e.Message);
            writeScoreFile();
            return false;
        }*/
        return true;
    }
    void writeScoreFile()
    {
        /*if (!Directory.Exists(scoreSavePath))
            Directory.CreateDirectory(scoreSavePath);
        
        FileStream saveFile = File.Create(scoreSavePath + "/" + scoreSaveFile);
        savedScores = bestScore;
        string jsonScores = JsonUtility.ToJson(this);
        Debug.Log(jsonScores);

        StreamWriter scoreSaver = new StreamWriter(saveFile);

        using (scoreSaver)
            scoreSaver.Write(jsonScores);
        scoreSaver.Close();
        saveFile.Close();*/
    }
}