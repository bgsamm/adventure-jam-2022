using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class House : Interactable
{
    private void Start() { }

    public override void Interact() {
        Debug.Log("Interacted with House");
    }

    public override void StartCanInteract() {

    }

    public override void StopCanInteract() {

    }
}
