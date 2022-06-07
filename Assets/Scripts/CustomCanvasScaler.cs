using UnityEngine;
using UnityEngine.U2D;
using UnityEngine.UI;

public class CustomCanvasScaler : CanvasScaler
{
    private PixelPerfectCamera pixelPerfectCamera;

    protected override void Awake() {
        var canvas = GetComponent<Canvas>();
        pixelPerfectCamera = canvas.worldCamera.GetComponent<PixelPerfectCamera>();
    }

    protected override void Start() {
        UpdateScaleFactor();
    }

    private void LateUpdate() {
        UpdateScaleFactor();
    }

    private void UpdateScaleFactor() {
        if (pixelPerfectCamera != null)
            scaleFactor = pixelPerfectCamera.pixelRatio;
    }
}
