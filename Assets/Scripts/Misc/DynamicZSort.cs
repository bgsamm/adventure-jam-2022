using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class DynamicZSort : MonoBehaviour
{
    private SpriteRenderer spriteRenderer;

    private void Awake() {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void Update() {
        spriteRenderer.sortingOrder = (int)(-10 * transform.position.y);
    }
}
