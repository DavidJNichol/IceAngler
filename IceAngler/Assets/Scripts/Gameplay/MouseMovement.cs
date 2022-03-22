using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseMovement : MonoBehaviour
{
    float mouseY;

    public float speed;

    public GameObject hook;

    private float hookY;

    public Bound bottomBound;

    private float bottomY;

    float lastMouseY;
    
    // Start is called before the first frame update
    void Start()
    {
        //Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Confined;
        bottomY = -1.6f;
        speed /= 100;
        bottomY = bottomBound.transform.position.y;
    }

    // Update is called once per frame
    void Update()
    {
        hookY = hook.transform.position.y;

        mouseY = Camera.main.ScreenToWorldPoint(Input.mousePosition).y / 40;

        Vector3 bottomBound = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, 0, 0));

        this.transform.localPosition = new Vector3(transform.localPosition.x, mouseY, transform.localPosition.z);

        lastMouseY = mouseY;

        //Debug.Log("Hook " + hookY);
        //Debug.Log(bottomBound.y);
    }
}
