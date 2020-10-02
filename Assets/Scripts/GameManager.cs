using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public Controller[] Controllers;

    public GameState GameState{ get; private set;}

    private void Awake(){
        IInputLayer inputLayerInstance = new MouseInputLayer();

        foreach (Controller c in Controllers)
        {
            c.GameStateChange += GameStateChangeRequest;
            c.SprayColorChange += SprayColorChanged;
            c.StencilChange += StencilChange;
            c.SetInputLayer(inputLayerInstance);
        }
    }

    private void Start(){
        UpdateControllersGameState(GameState.Ready);
    }

    private void GameStateChangeRequest(GameState p_newGameState){
        GameState = p_newGameState;

        UpdateControllersGameState(p_newGameState);

        if(p_newGameState == GameState.PuzzleComplete){
            GameState = GameState.WaitingStencil;

            UpdateControllersGameState(GameState);
        }
    }

    private void UpdateControllersGameState(GameState p_newGameState){
        foreach(Controller c in Controllers){
            c.OnGameStateChange(p_newGameState);
        }
    }

    private void SprayColorChanged(Color p_sprayColor){
        foreach (Controller c in Controllers)
        {
            c.OnSprayColorChange(p_sprayColor);
        }
    }

    private void StencilChange()
    {
        foreach (Controller c in Controllers)
        {
            c.OnStencilChange();
        }
    }
}