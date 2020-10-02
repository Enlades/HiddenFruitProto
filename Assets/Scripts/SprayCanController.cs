using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SprayCanController : Controller, IInputReceiver
{
    public IInputLayer InputLayer { get; set; }

    public GameObject SprayCan;
    public ParticleSystem SprayParticle;

    private void Update(){
        if(this.GameState != GameState.Stencil){
            return;
        }
    }

    public override void SetGameState(GameState p_gameState)
    {
        base.SetGameState(p_gameState);

        if(p_gameState == GameState.WaitingStencil){
            SprayCan.SetActive(true);
        }
    }
}
