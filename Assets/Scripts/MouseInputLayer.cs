using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseInputLayer : IInputLayer
{
    public bool GettingInput{
        get{
            return Input.GetMouseButton(0);
        }
    }

    public bool FirstInput{
        get{
            return Input.GetMouseButtonDown(0);
        }
    }

    public Vector3 InputPosition(){
        return Input.mousePosition;
    }
}
