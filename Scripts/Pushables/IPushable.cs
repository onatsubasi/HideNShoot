using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IPushable 
{
    void Push(Vector3 handVelocity, Vector3 hitPosition);
}
