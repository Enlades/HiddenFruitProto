using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SprayCanController : Controller, IInputReceiver
{
    public IInputLayer InputLayer { get; set; }

    public GameObject SprayCan;
    public ParticleSystem SprayParticle;
    
    public Material SprayMaterial;

    private ParticleSystem.MainModule _particleMain;
    
    private Color _selectedColor;

    private Plane _sprayPlane;

    private void Awake(){
        _particleMain = SprayParticle.main;

        _sprayPlane = new Plane(Vector3.up, Vector3.up * 4.52f);

        SprayMaterial.color = Color.gray;
    }

    private void Update(){
        if(this.GameState != GameState.Stencil || InputLayer.InputPosition.y < 800f){
            return;
        }

        if(InputLayer.InputDown){
            SprayParticle.Play();
        }else if(InputLayer.Input){
            Ray ray = Camera.main.ScreenPointToRay(InputLayer.InputPosition);
            float enter = 0f;

            _sprayPlane.Raycast(ray, out enter);

            Vector3 intersectPoint = ray.origin + ray.direction * enter;
            Vector3 offSet = Vector3.back * 2f + Vector3.right * 1.5f;

            SprayCan.transform.position = intersectPoint + offSet;
        }else if(InputLayer.InputUp){
            SprayParticle.Stop();
        }
    }

    public override void OnGameStateChange(GameState p_gameState)
    {
        base.OnGameStateChange(p_gameState);

        if(p_gameState == GameState.WaitingStencil){
            SprayCan.SetActive(true);
        }
    }

    public override void SetInputLayer(IInputLayer p_inputLayer)
    {
        this.InputLayer = p_inputLayer;
    }

    public override void OnSprayColorChange(Color p_color){
        SprayMaterial.color = p_color;
        _particleMain.startColor = p_color;

        _selectedColor = p_color;
    }
}
