using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class InteractionHotspot : MonoBehaviour
{
    public Interactable CurrentInteractable
    {
        get => _interactable;
        set {
            if (_interactable != null)
                _interactable.StopCanInteract();
            _interactable = value;
        }
    }
    private Interactable _interactable;

    private void OnTriggerEnter2D(Collider2D collision) {
        UpdateInteractable(collision);
    }

    private void OnTriggerStay2D(Collider2D collision) {
        UpdateInteractable(collision);
    }

    private void OnTriggerExit2D(Collider2D collision) {
        if (CurrentInteractable != null && collision.gameObject == CurrentInteractable.gameObject) {
            CurrentInteractable = null;
        }
    }

    private void UpdateInteractable(Collider2D collision) {
        var interactable = collision.GetComponent<Interactable>();
        if (interactable == null)
            return;
        // Prioritize the closest of multiple interactables
        if (CurrentInteractable == null || CompareDist(interactable.gameObject, CurrentInteractable.gameObject) < 0) {
            CurrentInteractable = interactable;
            CurrentInteractable.StartCanInteract();
        }
    }

    private float CompareDist(GameObject a, GameObject b) {
        return Vector3.Distance(a.transform.position, transform.position) - Vector3.Distance(b.transform.position, transform.position);
    }
}
