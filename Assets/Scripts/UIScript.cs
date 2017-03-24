using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class UIScript : MonoBehaviour {

    public Text timeText;
    public Text objectiveText;
    public static float totalTime;
    float seconds;
    int minutes;
    bool isTimeFrozen;
    bool launched;
    EventHandler EventInitializer;

    void OnDestroy()
    {
        EventHandler.launch -= launch;
        EventHandler.reset -= resetTime;
        EventHandler.reset -= noLaunch;
        EventHandler.win -= freezeTime;
        EventHandler.pause -= togglePause;
    }

    // Use this for initialization
    void Start () {
        EventHandler.launch += launch;
        EventHandler.reset += resetTime;
        EventHandler.reset += noLaunch;
        EventHandler.win += freezeTime;
        EventHandler.pause += togglePause;
        timeText.enabled = true;
        seconds = 0f;
        minutes = 0;
        isTimeFrozen = false;
        launched = false;
        EventInitializer = GameObject.Find("EventInitializer").GetComponent<EventHandler>();

        if(EventInitializer.getLevelType() == EventHandler.levelTypes.hole)
        {
            objectiveText.text = "Objective: Get your ball into the hole as quick as possible.";
        }
        if(EventInitializer.getLevelType() == EventHandler.levelTypes.endurance)
        {
            objectiveText.text = "Objective: Survive as long as possible";
        }
	}
	
	// Update is called once per frame
	void Update ()
    {
        if (launched && !isTimeFrozen)
        {
            seconds += Time.deltaTime;
        }
        if(seconds >= 60.00f)
        {
            seconds = 0f;
            minutes++;
        }
        timeText.text = "Time: " + minutes .ToString("00") + ":" + seconds.ToString("00.00");
        totalTime = seconds + (minutes * 60f);
	}

    void resetTime()
    {
        seconds = 0f;
        minutes = 0;
    }
    void freezeTime()
    {
        isTimeFrozen = true;
    }
    void togglePause()
    {
        isTimeFrozen = !isTimeFrozen;
        timeText.enabled = !timeText.enabled;
    }
    void launch()
    {
        launched = true;
    }
    void noLaunch()
    {
        launched = false;
    }

    public static float getTime()
    {
        return totalTime;
    }
}
