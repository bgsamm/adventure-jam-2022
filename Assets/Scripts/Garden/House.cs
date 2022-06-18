using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class House : Interactable
{
    private Clock clock => ResourceLocator.instance.Clock;
    private GardenManager gardenManager => ResourceLocator.instance.GardenManager;

    private void Update() {
        InteractMessage = gardenManager.TasksComplete ? "Press E to sleep" : "You still have chores to do.";
    }

    public override void Interact() {
        if (gardenManager.TasksComplete)
            clock.StartNextDay();
    }

    public override void StartCanInteract() {
        interactableFrame.SetActive(true);
    }

    public override void StopCanInteract() {
        interactableFrame.SetActive(false);
    }
}
