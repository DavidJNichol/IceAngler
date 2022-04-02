using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyInput : MonoBehaviour
{ 
    Timer timer;

    int timesPressedSpace;

    void Start()
    {
        timer = new Timer();
    }

    void Update()
    {
        if(timer.timeRemaining == 0)
        {
            if (timesPressedSpace < 10)
                Debug.Log("You Lose!");
            else
                Debug.Log("You Win!");
        }

        timer.RunTimer();

        if (Input.GetKeyDown("space"))
        {
            Debug.Log("Space Pressed!");
            timesPressedSpace++;
        }
    }
}
