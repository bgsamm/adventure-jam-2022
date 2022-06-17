using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class WateringCan : ClickableObject
{
    private Animator animator;
    private Action animCallback;

    private void Awake() {
        animator = GetComponent<Animator>();
    }

    public void PlayWateringAnim(Action callback) {
        animCallback = callback;
        animator.SetTrigger("Watering");
    }

    public void OnAnimFinished() {
        animCallback.Invoke();
    }
}
