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
        if (clock.ActNum != 4)
            //if you've completed all three tasks, you can go to sleep
            InteractMessage = gardenManager.TasksComplete ? "Press E to sleep" : "You aren't tired yet.";
        else
            InteractMessage = "Press E to sleep";
    }

    public override void Interact() {
        //you don't need to complete tasks if it's act 4
        if (gardenManager.TasksComplete || clock.ActNum == 4)
            clock.StartNextDay();
    }

    public override void StartCanInteract() {
        interactableFrame.SetActive(true);
    }

    public override void StopCanInteract() {
        interactableFrame.SetActive(false);
    }
}
