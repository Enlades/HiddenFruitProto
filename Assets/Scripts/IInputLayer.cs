using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IInputLayer
{
    bool Input{get;}
    bool InputDown{get;}
    bool InputUp{get;}
    Vector3 InputPosition{get; }
}
