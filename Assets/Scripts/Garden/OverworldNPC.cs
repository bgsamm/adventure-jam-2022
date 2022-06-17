using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class OverworldNPC : MonoBehaviour
{
    private SpriteRenderer spriteRenderer;

    private Clock clock => ResourceLocator.instance.Clock;
    private GardenManager gardenManager => ResourceLocator.instance.GardenManager;

    private void Awake() {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void Start() {
        if (!gardenManager.ShopVisited && clock.CurrentDay.NPC != null) {
            spriteRenderer.sprite = clock.CurrentDay.NPC.overworldSprite;
        }
        else {
            spriteRenderer.sprite = null;
        }
    }
}
