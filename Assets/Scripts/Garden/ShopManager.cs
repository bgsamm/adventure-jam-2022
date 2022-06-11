using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopManager : Interactable
{
    [SerializeField] private int interactRange;
    private bool touching;
    private GameObject player;

    private void Awake()
    {
        touching = false;
        player = null;
    }

    private void Update()
    {
        if (player != null && !touching)
        {
            float dist = Vector3.Distance(gameObject.transform.position, transform.position) - Vector3.Distance(
                player.transform.position, transform.position);
            if(dist < interactRange)
            {
                player.GetComponent<PlayerController>().CurrentInteractable = null;
                player = null;
            }
        }
    }

    public override void Interact()
    {
        if (!Clock.instance.shopVisited)
        {
            Clock.instance.shopVisited = true;
            LoadingManager.instance.ShopScene();
        }
    }

    public override void StartCanInteract()
    {
        this.enabled = true;
    }

    public override void StopCanInteract()
    {
        this.enabled = false;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        touching = true;
        player = collision.gameObject;
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        touching = false;
    }
}
