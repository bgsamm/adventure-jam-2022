using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class DynamicZSort : MonoBehaviour
{
    private GameObject player;
    private SpriteRenderer spriteRenderer;
    private int playerSortOrder;

    private void Awake() {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void Start() {
        //player = GameObject.FindGameObjectWithTag("Player");
        //// this assumes the player's sort order never changes
        //playerSortOrder = player.GetComponent<SpriteRenderer>().sortingOrder;
    }

    private void Update() {
        //if (player.transform.position.y > spriteRenderer.transform.position.y)
        //    spriteRenderer.sortingOrder = playerSortOrder + 1;
        //else
        //    spriteRenderer.sortingOrder = playerSortOrder - 1;
        spriteRenderer.sortingOrder = (int)(-10 * transform.position.y);
    }
}
