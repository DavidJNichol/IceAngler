using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MarineObject : MonoBehaviour, IMarineObject
{
    public float MoveSpeed { get { return moveSpeed; } set { moveSpeed = value; } }
    public bool IsActivated { get { return isActivated; } set { isActivated = value; } }
    public SpriteRenderer SpriteRenderer { get { return spriteRenderer; } set { spriteRenderer = value; } }
    public Collider2D Collider { get { return collider; } set { collider = value; } }

    protected float moveSpeed;
    protected bool isActivated; // if this object is allowed behaviors {move}
    protected SpriteRenderer spriteRenderer;
    protected Collider2D collider;


    void Start()
    {
        moveSpeed = 0;

        if(GetComponent<SpriteRenderer>())
            spriteRenderer = GetComponent<SpriteRenderer>();

        if (GetComponent<Collider2D>())
            collider = GetComponent<Collider2D>();
    }

    protected void Update()
    {
        Move();
    }
    public void Move()
    {
        if(isActivated)
            this.transform.position = new Vector3(transform.position.x + moveSpeed, transform.position.y, transform.position.z);
    }
}
