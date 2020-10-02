using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIController : Controller, IInputReceiver
{
    public IInputLayer InputLayer { get; set; }

    public GameObject StencilPanel;
    public GameObject EndLevelPanel;
    public GameObject HoldAndRelease;
    public GameObject BrushColorsPanel;
    public GameObject NextButton;
    public GameObject DoneButton;

    public Button[] BrushColorButtons;

    private void Awake(){
        for (int i = 0; i < BrushColorButtons.Length; i++)
        {
            int indexer = i;
            BrushColorButtons[i].onClick.AddListener(() =>
            {
                BrushSelected(indexer);
            });
        }
    }

    private void Update(){
        if(this.GameState == GameState.Ready && InputLayer.Input){
            GameStateChangeEvent?.Invoke(GameState.Puzzle);
        }
    }

    public override void SetGameState(GameState p_gameState)
    {
        base.SetGameState(p_gameState);

        if(this.GameState == GameState.Puzzle){
            HoldAndRelease.SetActive(false);
        }

        if (this.GameState == GameState.WaitingStencil)
        {
            BrushColorsPanel.SetActive(true);
            StencilPanel.SetActive(true);
            NextButton.SetActive(true);
        }
    }

    private void BrushSelected(int p_index){

    }
}
