using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ScreenFader : MonoBehaviour
{
    public Action FadeInEvent;
    public Action FadeOutEvent;

    private Animator animator;

    private void Awake() {
        animator = GetComponent<Animator>();
    }

    public void FadeIn(bool longFade) {
        if (longFade)
            animator.Play("Fade In (Long)");
        else
            animator.Play("Fade In");
    }

    public void FadeOut(bool longFade) {
        if (longFade)
            animator.Play("Fade Out (Long)");
        else
            animator.Play("Fade Out");
    }

    public void OnFadeInFinished() {
        FadeInEvent?.Invoke();
    }

    public void OnFadeOutFinished() {
        FadeOutEvent?.Invoke();
    }
}
