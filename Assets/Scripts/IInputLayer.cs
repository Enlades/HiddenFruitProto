using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IInputLayer
{
    bool GettingInput{get;}
    bool FirstInput{get;}
    Vector3 InputPosition();
}
