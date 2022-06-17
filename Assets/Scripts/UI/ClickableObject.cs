using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ClickableObject : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField] private Image frame;

    protected virtual void Start() {
        frame.enabled = false;
    }

    public void OnPointerEnter(PointerEventData eventData) {
        frame.enabled = true;
    }

    public void OnPointerExit(PointerEventData eventData) {
        frame.enabled = false;
    }
}
