using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class OverworldNPC : MonoBehaviour
{
    private SpriteRenderer spriteRenderer;

    private void Awake() {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void Start() {
        var npc = ResourceLocator.instance.Clock.CurrentDay.NPC;
        spriteRenderer.sprite = npc.overworldSprite;
    }
}
