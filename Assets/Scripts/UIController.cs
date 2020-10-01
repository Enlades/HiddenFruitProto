using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIController : Controller, IInputReceiver
{
    public IInputLayer InputLayer { get; set; }

    public GameObject StencilPanel;
    public GameObject EndLevelPanel;
    public GameObject HoldAndRelease;

    private void Update(){
        if(this.GameState == GameState.Ready && InputLayer.GettingInput){
            GameStateChangeEvent.Invoke(GameState.Puzzle);
        }
    }

    public override void SetGameState(GameState p_gameState)
    {
        this.GameState = p_gameState;

        if(this.GameState == GameState.Puzzle){
            HoldAndRelease.SetActive(false);
        }
    }
}
