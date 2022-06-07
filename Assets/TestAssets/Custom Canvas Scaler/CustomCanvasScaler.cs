using UnityEngine;
using UnityEngine.U2D;
using UnityEngine.UI;

public class CustomCanvasScaler : CanvasScaler
{
    private Canvas canvas;
    private new PixelPerfectCamera camera;

    protected override void Awake() {
        canvas = GetComponent<Canvas>();
    }

    protected override void Start() {
        camera = canvas.worldCamera.GetComponent<PixelPerfectCamera>();
        scaleFactor = camera.pixelRatio;
    }

    private void LateUpdate() {
        scaleFactor = camera.pixelRatio;
    }
}
