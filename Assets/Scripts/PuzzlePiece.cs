using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PuzzlePiece : MonoBehaviour
{
    public bool CanSelect{get; private set;}

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
        StartCoroutine(SmootMove(_startingPosition));
    }

    public void Place(){
        StartCoroutine(SmootMove(transform.parent.position));
    }

    private IEnumerator SmootMove(Vector3 p_targetPosition){
        float maxDuration = 1f;
        float duration = maxDuration;

        Vector3 startPosition = transform.position;

        CanSelect = false;

        while(duration > 0f){
            transform.position = Vector3.Lerp(startPosition, p_targetPosition
            , (maxDuration - duration) / maxDuration);

            duration -= Time.deltaTime;

            yield return null;
        }

        transform.position = p_targetPosition;

        CanSelect = true;
    }
}
