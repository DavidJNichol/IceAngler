using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Eel : Fish
{
    // Start is called before the first frame update
    void Start()
    {
        moveSpeed = 1;

        Debug.Log(IsActivated);
    }
}
