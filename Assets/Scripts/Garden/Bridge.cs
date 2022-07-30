using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bridge : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision) {
        PlayerController.currentSurface = Surface.WOOD;
    }

    private void OnTriggerExit2D(Collider2D collision) {
        PlayerController.currentSurface = Surface.GRASS;
    }
}
