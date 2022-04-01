using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IMarineObject
{
    float MoveSpeed { get; set; }

    void Move();

    void Deactivate();
}
