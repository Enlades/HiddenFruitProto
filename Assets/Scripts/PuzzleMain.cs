using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PuzzleMain : MonoBehaviour
{
    public GameObject PuzzleBorder;

    public Action PuzzleComplete;

    private PuzzlePiece[] _puzzlePieces;

    private int _totalPieces;
    private int _currentPieces;

    private void Awake(){
        _puzzlePieces = GetComponentsInChildren<PuzzlePiece>();

        foreach(PuzzlePiece p in _puzzlePieces){
            p.PieceComplete += PieceComplete;
        }

        _totalPieces = _puzzlePieces.Length;
        _currentPieces = 0;
    }

    private void Start(){
        StartCoroutine(PuzzleBorderInit());
    }

    public void PieceComplete(){
        _currentPieces++;

        if(_currentPieces == _totalPieces){
            PuzzleComplete?.Invoke();
        }
    }

    private IEnumerator PuzzleBorderInit(){
        yield return new WaitForSeconds(1f);

        PuzzleBorder.GetComponent<Rigidbody>().isKinematic = true;
        PuzzleBorder.GetComponent<Collider>().isTrigger = true;

        PuzzleBorder.transform.rotation = Quaternion.LookRotation(Vector3.forward, Vector3.up);
        PuzzleBorder.transform.localPosition = Vector3.zero;
    }
}
