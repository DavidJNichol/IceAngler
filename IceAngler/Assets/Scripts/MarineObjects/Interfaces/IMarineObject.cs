using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IMarineObject
{
    float MoveSpeed { get; set; }

    SpriteRenderer SpriteRenderer { get; set; }

    Collider2D Collider { get; set; }

    bool IsActivated { get; set; }

    void Move();
}
