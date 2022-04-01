using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MarineObject : MonoBehaviour, IMarineObject
{
    public float MoveSpeed { get { return moveSpeed; } set { moveSpeed = value; } }
    protected float moveSpeed;

    void Start()
    {
        moveSpeed = 0;
    }

    protected void Update()
    {
        Move();
    }
    public void Move()
    {
        this.transform.position = new Vector3(transform.position.x + moveSpeed, transform.position.y, transform.position.z);
    }
    private void OnBecameInvisible()
    {
        Deactivate();
    }
    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.name == "Hook")
        {
            Deactivate();
        }
    }
    public void Deactivate()
    {
        gameObject.SetActive(false);
    }
}
