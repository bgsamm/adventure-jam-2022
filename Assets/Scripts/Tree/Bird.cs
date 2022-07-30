using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Bird : ClickableObject
{
    private Animator animator;

    private Clock clock => ResourceLocator.instance.Clock;
    private GardenManager gardenManager => ResourceLocator.instance.GardenManager;

    private void Awake() {
        animator = GetComponent<Animator>();
    }

    protected override void Start() {
        base.Start();
        GetComponent<RectTransform>().anchoredPosition = clock.CurrentAct.birdLocation;
        if (clock.CurrentDay.birdPresent) {
            var hasLetter = clock.CurrentDay.letter != null && !gardenManager.LetterRead;
            animator.SetBool("HasLetter", hasLetter);
        }
    }
}
