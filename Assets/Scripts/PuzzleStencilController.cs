using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PuzzleStencilController : Controller, IInputReceiver
{
    public IInputLayer InputLayer { get; set; }

    public GameObject PuzzleStencil;
    public Sprite[] StencilSprites;

    public PuzzleMain Puzzle;
    
    public GameObject[] InvisibleStencils;

    public Sprite[] PaintSprites;
    public GameObject PaintInstancePrefab;

    private int _paintLayer;
    private int _stencilIndex;

    private Color _selectedColor;
    
    private Vector3 _lastPaintScreenPosition;
    private Vector3 _puzzleStencilOriginalPosition;

    private void Awake(){
        _paintLayer = 0;
        _stencilIndex = 0;

        _lastPaintScreenPosition = Vector3.zero;
        _puzzleStencilOriginalPosition = PuzzleStencil.transform.position;
    }

    private void Update()
    {
        if (this.GameState != GameState.Stencil || InputLayer.InputPosition.y < 800f)
        {
            return;
        }

        if (InputLayer.Input)
        {
            Ray ray = Camera.main.ScreenPointToRay(InputLayer.InputPosition);
            RaycastHit hit;

            if(Vector3.Distance(_lastPaintScreenPosition, InputLayer.InputPosition) > 40f){
                if (Physics.Raycast(ray, out hit, float.MaxValue, LayerMask.GetMask("PuzzleStencil")))
                {
                    SpriteRenderer newPaintInstance = CreatePaintInstance();
                    newPaintInstance.transform.position = hit.point + Vector3.up * 0.1f;
                    newPaintInstance.transform.SetParent(PuzzleStencil.transform);
                    newPaintInstance.maskInteraction = SpriteMaskInteraction.VisibleInsideMask;
                    newPaintInstance.sortingLayerName = "PuzzleStencil";

                    newPaintInstance = CreatePaintInstance();
                    newPaintInstance.transform.position = hit.point + Vector3.up * 0.1f;
                    newPaintInstance.transform.SetParent(InvisibleStencils[_stencilIndex].transform);
                    newPaintInstance.maskInteraction = SpriteMaskInteraction.VisibleOutsideMask;

                    _lastPaintScreenPosition = InputLayer.InputPosition;
                }
            }
        }
    }

    public override void OnGameStateChange(GameState p_gameState)
    {
        base.OnGameStateChange(p_gameState);

        if(this.GameState == GameState.WaitingStencil){
            StartCoroutine(SmoothStencilMove(Puzzle.transform.position + Vector3.up * 0.32f));

            InvisibleStencils[_stencilIndex].transform.position = Puzzle.transform.position + Vector3.up * 0.32f;
        }
    }

    public override void OnSprayColorChange(Color p_color)
    {
        _selectedColor = p_color;
        _paintLayer++;
    }
    
    public override void OnStencilChange(){
        _stencilIndex++;

        StartCoroutine(SmoothStencilMove(PuzzleStencil.transform.position + Vector3.right * 10f, 0.2f
        , () =>{ChangeStencil(); }));
    }

    public override void SetInputLayer(IInputLayer p_inputLayer)
    {
        this.InputLayer = p_inputLayer;
    }

    private SpriteRenderer CreatePaintInstance(){
        GameObject newPaintInstance = Instantiate(PaintInstancePrefab);

        SpriteRenderer newPaintSr = newPaintInstance.GetComponent<SpriteRenderer>();

        newPaintSr.sortingOrder = _paintLayer;

        newPaintSr.sprite
        = PaintSprites[UnityEngine.Random.Range(0, PaintSprites.Length)];

        newPaintSr.color = _selectedColor;

        return newPaintSr;
    }

    private void ChangeStencil(){
        PuzzleStencil.transform.position = _puzzleStencilOriginalPosition;

        for(int i = 0; i < PuzzleStencil.transform.childCount; i++){
            Destroy(PuzzleStencil.transform.GetChild(i).gameObject);
        }

        PuzzleStencil.GetComponent<SpriteRenderer>().sprite = StencilSprites[_stencilIndex];

        StartCoroutine(SmoothStencilMove(Puzzle.transform.position + Vector3.up * 0.32f));
        
        PuzzleStencil.GetComponent<SpriteMask>().sprite = StencilSprites[_stencilIndex];

        InvisibleStencils[_stencilIndex].transform.position = Puzzle.transform.position + Vector3.up * 0.32f;
    }

    private IEnumerator SmoothStencilMove(Vector3 p_targetPosition)
    {
        yield return SmoothStencilMove(p_targetPosition, 0.5f, null);
    }

    private IEnumerator SmoothStencilMove(Vector3 p_targetPosition, float p_maxDuration)
    {
        yield return SmoothStencilMove(p_targetPosition, p_maxDuration, null);
    }

    private IEnumerator SmoothStencilMove(Vector3 p_targetPosition, float p_maxDuration, Action p_action)
    {
        float maxDuration = p_maxDuration;
        float duration = p_maxDuration;

        Vector3 startPosition = PuzzleStencil.transform.position;


        while (duration > 0f)
        {
            PuzzleStencil.transform.position = Vector3.Lerp(startPosition, p_targetPosition
            , (maxDuration - duration) / maxDuration);

            duration -= Time.deltaTime;

            yield return null;
        }

        PuzzleStencil.transform.position = p_targetPosition;
        
        p_action?.Invoke();
    }
}
