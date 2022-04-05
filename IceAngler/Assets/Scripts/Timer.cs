using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Timer
{
    public float timeRemaining = 5;

    public void SetTimerDuration(float duration)
    {
        timeRemaining = duration;
    }

    public void RunTimer()
    {
        if (timeRemaining > 0)
        {
            timeRemaining -= Time.deltaTime;
        }
        else
        {
            Debug.Log("Time has run out!");
            timeRemaining = 0;
        }
    }
}
