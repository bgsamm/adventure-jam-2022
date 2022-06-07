using UnityEngine;
using UnityEngine.U2D;
using UnityEngine.UI;

public class CustomCanvasScaler : CanvasScaler
{
    private PixelPerfectCamera pixelPerfectCamera;

    protected override void Awake() {
        var canvas = GetComponent<Canvas>();
        if (canvas.worldCamera != null)
            pixelPerfectCamera = canvas.worldCamera.GetComponent<PixelPerfectCamera>();
    }

    protected override void Start() {
        UpdateScaleFactor();
    }

    private void LateUpdate() {
        UpdateScaleFactor();
    }

    private void UpdateScaleFactor() {
        // Attempting to get the pixel ratio while the camera
        // is not running will throw a null reference exception
        try {
            scaleFactor = pixelPerfectCamera.pixelRatio;
        }
        catch { }
    }
}
