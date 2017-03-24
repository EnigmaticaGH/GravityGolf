using UnityEngine;
using System.Collections;

public class EventHandler : MonoBehaviour {

    public delegate void resetAction();
    public static event resetAction reset;
    public delegate void winAction();
    public static event winAction win;
    public delegate void pauseAction();
    public static event pauseAction pause;
    public delegate void launchAction();
    public static event launchAction launch;

    public enum levelTypes : byte
    {
        hole,
        endurance
    }
    public levelTypes levelType;

    public static void Reset()
    {
        if(reset != null)
        {
            reset();
        }
    }
    public static void Pause()
    {
        if (pause != null)
        {
            pause();
        }
    }
    public static void Win()
    {
        if (win != null)
        {
            win();
        }
    }
    public static void Launch()
    {
        if (launch != null)
        {
            launch();
        }
    }
    public levelTypes getLevelType()
    {
        return levelType;
    }
}
