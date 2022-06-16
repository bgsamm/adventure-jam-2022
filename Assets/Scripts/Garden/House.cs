using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class House : Interactable
{
    private Clock clock => ResourceLocator.instance.Clock;

    private void Update() {
        //if you've completed all three tasks, you can go to sleep
        InteractMessage = clock.TasksComplete ? "Press E to sleep" : "You aren't tired yet.";
    }

    public override void Interact() {
        clock.StartNextDay();
    }

    public override void StartCanInteract() {
        interactableFrame.SetActive(true);
    }

    public override void StopCanInteract() {
        interactableFrame.SetActive(false);
    }
}
