using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PuzzleController : Controller, IInputReceiver
{
    public IInputLayer InputLayer { get; set; }

    public GameObject PuzzleMain;

    public override void SetGameState(GameState p_gameState)
    {
        this.GameState = p_gameState;
    }

    private void Update(){
        if(this.GameState != GameState.Puzzle){
            return;
        }

        PuzzleMainLoop();
    }

    private void PuzzleMainLoop(){
        if(!InputLayer.GettingInput){
            return;
        }

        if(InputLayer.FirstInput){
            Ray ray = Camera.main.ScreenPointToRay(InputLayer.InputPosition());
            RaycastHit hit;

            if(Physics.Raycast(ray, out hit, float.MaxValue, LayerMask.GetMask("PuzzlePiece"))){
                hit.collider.GetComponent<PuzzlePiece>().Select();
                hit.collider.GetComponent<PuzzlePiece>().Place();
            }
        }
    }
}
