using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Controller : MonoBehaviour
{
    public Action<GameState> GameStateChangeEvent;

    public virtual void SetGameState(GameState p_gameState){
        this.GameState = p_gameState;
    }

    protected GameState GameState{get; set;}
}
