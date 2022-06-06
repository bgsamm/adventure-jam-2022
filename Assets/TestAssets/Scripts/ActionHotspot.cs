using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ActionHotspot : MonoBehaviour
{
    public bool Actionable { get; private set; }

    private void OnTriggerEnter2D(Collider2D collision) {
        if (collision.gameObject.CompareTag("GardenSpace"))
            Debug.Log($"{collision.name}");
    }
}
