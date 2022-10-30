using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SongTime : MonoBehaviour
{
    public static float progress = 0;

    public static void setProgress(float newProgress)
    {
        progress = newProgress;
    }

    public static float getProgress()
    {
        return progress;
    }

    public static void updateProgress()
    {
        AudioManager go = FindObjectOfType<AudioManager>();
        setProgress(go.source.time);
    }

}
