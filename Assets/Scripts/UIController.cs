using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class UIController : Controller, IInputReceiver
{
    public IInputLayer InputLayer { get; set; }

    public GameObject StencilPanel;
    public GameObject EndLevelPanel;
    public GameObject BrushColorsPanel;
    public GameObject HoldAndRelease;
    public GameObject NextButton;
    public GameObject DoneButton;
    public GameObject[] StencilBGs;

    public Button[] BrushColorButtons;

    private int _stencilIndex;

    private void Awake(){
        for (int i = 0; i < BrushColorButtons.Length; i++)
        {
            int indexer = i;
            BrushColorButtons[i].onClick.AddListener(() =>
            {
                BrushSelected(indexer);
            });
        }

        _stencilIndex = 0;
    }

    private void Update(){
        if(this.GameState == GameState.Ready && InputLayer.Input){
            GameStateChange?.Invoke(GameState.Puzzle);
        }
    }

    public override void OnGameStateChange(GameState p_gameState)
    {
        base.OnGameStateChange(p_gameState);

        if(this.GameState == GameState.Puzzle){
            HoldAndRelease.SetActive(false);
        }

        if (this.GameState == GameState.WaitingStencil)
        {
            BrushColorsPanel.SetActive(true);
            StencilPanel.SetActive(true);
            NextButton.SetActive(true);

            StencilBGs[_stencilIndex].SetActive(true);
        }
    }

    public override void SetInputLayer(IInputLayer p_inputLayer){
        this.InputLayer = p_inputLayer;
    }

    public void BrushSelected(int p_index){
        Color selectedColor = BrushColorButtons[p_index].image.color;

        foreach(Button b in BrushColorButtons){
            b.GetComponent<BrushButton>().DisableBG();
        }

        BrushColorButtons[p_index].GetComponent<BrushButton>().EnableBG();

        SprayColorChange?.Invoke(selectedColor);

        if(this.GameState == GameState.WaitingStencil){
            GameStateChange?.Invoke(GameState.Stencil);
        }
    }

    public void OnNextButton(){
        if(_stencilIndex == 2){
            return;
        }

        StencilChange?.Invoke();
        
        _stencilIndex++;

        StencilBGs[_stencilIndex].SetActive(true);

        if(_stencilIndex == 2){
            NextButton.SetActive(false);
            DoneButton.SetActive(true);
        }
    }

    public void OnDoneButton(){
        StencilPanel.SetActive(false);
        BrushColorsPanel.SetActive(false);
        EndLevelPanel.SetActive(true);
        DoneButton.SetActive(false);

        StencilChange?.Invoke();
    }

    public void OnOkButton(){
        SceneManager.LoadScene(0);
    }
}
