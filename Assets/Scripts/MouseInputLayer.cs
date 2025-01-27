﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class MouseInputLayer : IInputLayer
{
    public bool Input{
        get{
            return UnityEngine.Input.GetMouseButton(0);
        }
    }

    public bool InputDown{
        get{
            return UnityEngine.Input.GetMouseButtonDown(0);
        }
    }

    public bool InputUp
    {
        get
        {
            return UnityEngine.Input.GetMouseButtonUp(0);
        }
    }

    public bool IsOverUI{
        get{
            return EventSystem.current.IsPointerOverGameObject();
        }
    }

    public Vector3 InputPosition{
        get{
            return UnityEngine.Input.mousePosition;
        }
    }
}
