using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Controller : MonoBehaviour
{
    public Action<GameState> GameStateChange;
    public Action<Color> SprayColorChange;
    public Action StencilChange;

    public virtual void OnGameStateChange(GameState p_gameState){
        this.GameState = p_gameState;
    }

    public virtual void OnSprayColorChange(Color p_color){

    }

    public virtual void OnStencilChange(){
        
    }

    public virtual void SetInputLayer(IInputLayer p_inputLayer){

    }

    protected GameState GameState{get; set;}
}
