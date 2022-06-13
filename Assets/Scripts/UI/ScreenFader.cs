using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ScreenFader : MonoBehaviour
{
    public CallbackEvent FadeInEvent;
    public CallbackEvent FadeOutEvent;

    private Animator animator;

    private void Awake() {
        animator = GetComponent<Animator>();
    }

    public void FadeIn() {
        animator.Play("Fade In");
    }

    public void FadeOut() {
        animator.Play("Fade Out");
    }

    public void OnFadeInFinished() {
        FadeInEvent?.Invoke();
    }

    public void OnFadeOutFinished() {
        FadeOutEvent?.Invoke();
    }
}
