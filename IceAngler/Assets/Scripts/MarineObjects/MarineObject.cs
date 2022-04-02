using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MarineObject : MonoBehaviour, IMarineObject
{
    public float MoveSpeed { get { return moveSpeed; } set { moveSpeed = value; } }
    protected float moveSpeed;

    public delegate void OnDeactivateHandler();

    public event OnDeactivateHandler OnDeactivate;

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
    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.name == "Hook" || col.name == "RightBound")
        {
            Deactivate();
        }
    }
    public void Deactivate()
    {
        gameObject.SetActive(false);

        if (OnDeactivate != null)
        {
            OnDeactivate();
        }
    }
}
