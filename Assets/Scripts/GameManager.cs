using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public Controller[] Controllers;

    public GameState GameState{ get; private set;}

    private void Awake(){
        IInputLayer inputLayerInstance = new MouseInputLayer();

        foreach (IInputReceiver ir in Controllers){
            ir.InputLayer = inputLayerInstance;
        }

        foreach (Controller c in Controllers)
        {
            c.GameStateChangeEvent += GameStateChangeRequest;
        }
    }

    private void Start(){
        UpdateControllersGameState(GameState.Ready);
    }

    private void GameStateChangeRequest(GameState p_newGameState){
        GameState = p_newGameState;

        UpdateControllersGameState(p_newGameState);
    }

    private void UpdateControllersGameState(GameState p_newGameState){
        foreach(Controller c in Controllers){
            c.SetGameState(p_newGameState);
        }
    }
}