using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public abstract class Interactable : MonoBehaviour
{
    public abstract void StartCanInteract();
    public abstract void StopCanInteract();
    public abstract void Interact();
}
