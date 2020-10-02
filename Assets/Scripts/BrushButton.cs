using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BrushButton : MonoBehaviour
{
    private GameObject _buttonBG;

    private void OnEnable(){
        _buttonBG = transform.GetChild(0).gameObject;
        _buttonBG.SetActive(false);
    }

    public void EnableBG(){
        _buttonBG.SetActive(true);
    }

    public void DisableBG(){
        _buttonBG.SetActive(false);
    }
}
