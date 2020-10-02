using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : Controller
{
    private Transform _cameraTransform;

    private void Awake(){
        _cameraTransform = Camera.main.transform;
    }

    public override void OnGameStateChange(GameState p_gameState)
    {
        base.OnGameStateChange(p_gameState);

        if (this.GameState == GameState.PuzzleComplete)
        {
            StartCoroutine(SmoothTranslate(_cameraTransform.position + Vector3.forward * 2f));
        }
    }

    private IEnumerator SmoothTranslate(Vector3 p_targetPosition){
        float maxDuration = 1f;
        float duration = maxDuration;

        Vector3 startPosition = _cameraTransform.position;

        while (duration > 0f)
        {
            _cameraTransform.position = Vector3.Lerp(startPosition, p_targetPosition
            , (maxDuration - duration) / maxDuration);

            duration -= Time.deltaTime;

            yield return null;
        }

        _cameraTransform.position = p_targetPosition;

        yield return null;
    }
}
