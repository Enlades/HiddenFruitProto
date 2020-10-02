using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PuzzleController : Controller, IInputReceiver
{
    public IInputLayer InputLayer { get; set; }

    public PuzzleMain Puzzle;

    private PuzzlePiece _selectedPiece;

    private Plane _pieceCarryPlane;

    private void Awake(){
        _pieceCarryPlane = new Plane(Vector3.up, Vector3.up * 5f);

        Puzzle.PuzzleComplete += PuzzleComplete;
    }

    private void Update(){
        if(this.GameState != GameState.Puzzle){
            return;
        }

        PuzzleMainLoop();
    }

    public override void OnGameStateChange(GameState p_gameState)
    {
        base.OnGameStateChange(p_gameState);

        PuzzleMainLoop();
    }

    public override void SetInputLayer(IInputLayer p_inputLayer)
    {
        this.InputLayer = p_inputLayer;
    }

    private void PuzzleComplete(){
        GameStateChange?.Invoke(GameState.PuzzleComplete);
    }

    private void PuzzleMainLoop(){

        if(InputLayer.InputDown){
            Ray ray = Camera.main.ScreenPointToRay(InputLayer.InputPosition);
            RaycastHit hit;

            if(Physics.Raycast(ray, out hit, float.MaxValue, LayerMask.GetMask("PuzzlePiece"))){
                _selectedPiece = hit.collider.GetComponent<PuzzlePiece>();
                if(_selectedPiece.CanSelect){
                    _selectedPiece.Select();
                }else{
                    _selectedPiece = null;
                }
            }
        }

        if(_selectedPiece != null){
            Ray ray = Camera.main.ScreenPointToRay(InputLayer.InputPosition);
            float enter = 0f;

            _pieceCarryPlane.Raycast(ray, out enter);

            Vector3 intersectPoint = ray.origin + ray.direction * enter;

            _selectedPiece.transform.position = intersectPoint - _selectedPiece.CenterOffset;

            if(InputLayer.InputUp){
                if(Vector3.Distance(_selectedPiece.transform.position
                , _selectedPiece.transform.parent.position) < 4f){
                    _selectedPiece.Place();
                }else{
                    _selectedPiece.DeSelect();
                }

                _selectedPiece = null;
            }
        }
    }
}
