using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.U2D;

public class CanvasScaleAdjuster : MonoBehaviour
{
    //script that allows the canvas to scale when the game resolution changes

    public Camera MainCamera;
 
    void Start()
    {
        AdjustScalingFactor();
    }
 
    void LateUpdate()
    {
        AdjustScalingFactor();
    }
 
    void AdjustScalingFactor()
    {
        GetComponent<CanvasScaler>().scaleFactor = MainCamera.GetComponent<PixelPerfectCamera>().pixelRatio;
    }
 
}

