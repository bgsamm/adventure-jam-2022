using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Cart : Interactable
{
    private void Start() { }

    public override void Interact() {
        Debug.Log("Interacted with Cart");
    }

    public override void StartCanInteract() {

    }

    public override void StopCanInteract() {

    }
}
