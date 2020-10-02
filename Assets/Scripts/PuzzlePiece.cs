using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PuzzlePiece : MonoBehaviour
{
    public Action PieceComplete;

    public bool CanSelect{get; private set;}

    public Vector3 CenterOffset{ 
        get{
            return _rb.worldCenterOfMass - transform.position;
        }
    }

    private Rigidbody _rb;
    private Collider _collider;

    private Vector3 _startingPosition;

    private void Awake(){
        _rb = GetComponent<Rigidbody>();
        _collider = GetComponent<Collider>();

        CanSelect = true;
    }

    public void Select(){
        _rb.isKinematic = true;
        _collider.isTrigger = true;

        // A bit wierd
        _startingPosition = transform.position;

        transform.rotation = Quaternion.LookRotation(Vector3.forward, Vector3.up);
    }

    public void DeSelect(){
        StartCoroutine(SmoothMove(_startingPosition));
    }

    public void Place(){
        StartCoroutine(SmoothMove(transform.parent.position
        , ()=>{CanSelect = false; PieceComplete?.Invoke();}));
    }

    private IEnumerator SmoothMove(Vector3 p_targetPosition){
        yield return SmoothMove(p_targetPosition, null);
    }

    private IEnumerator SmoothMove(Vector3 p_targetPosition, Action p_Action)
    {
        float maxDuration = 0.5f;
        float duration = maxDuration;

        Vector3 startPosition = transform.position;

        CanSelect = false;

        while (duration > 0f)
        {
            transform.position = Vector3.Lerp(startPosition, p_targetPosition
            , (maxDuration - duration) / maxDuration);

            duration -= Time.deltaTime;

            yield return null;
        }

        transform.position = p_targetPosition;

        CanSelect = true;

        p_Action?.Invoke();
    }
}
